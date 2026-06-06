// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EdFi.Common.Utils.Extensions;
using EdFi.Ods.Api.IdentityValueMappers;
using EdFi.Ods.Common.Caching;
using EdFi.Ods.Common.Configuration;
using EdFi.Ods.Common.Context;
using log4net;

namespace EdFi.Ods.Api.Caching.Person;

/// <summary>
/// Provides a base class for common behavior needed for person identifier resolution (see <see cref="IPersonUniqueIdResolver"/>
/// and <see cref="IPersonUsiResolver"/>).
/// </summary>
/// <typeparam name="TLookup">The type of the identifier being used for the lookup.</typeparam>
/// <typeparam name="TResolved">The type of the identifier being resolved.</typeparam>
public abstract class PersonIdentifierResolverBase<TLookup, TResolved>
{
    private readonly IMapCache<(ulong odsInstanceHashId, string personType, PersonMapType mapType), TLookup, TResolved> _mapCache;
    private readonly IMapCache<(ulong odsInstanceHashId, string personType, PersonMapType mapType), TResolved, TLookup> _reverseMapCache;
    private readonly Dictionary<string, bool> _cacheSuppressionByPersonType;
    private readonly IPersonMapCacheInitializer _personMapCacheInitializer;
    private readonly IDistributedLockProvider _distributedLockProvider;
    private readonly IContextProvider<OdsInstanceConfiguration> _odsInstanceConfigurationContextProvider;
    private readonly TLookup[] _cacheInitializationMarkerKeyForLookup;
    private readonly TResolved[] _cacheInitializationMarkerKeyForResolved;
    private readonly ILog _logger;
    private readonly bool _performBackgroundInitialization;

    protected PersonIdentifierResolverBase(
        IPersonMapCacheInitializer personMapCacheInitializer,
        IDistributedLockProvider distributedLockProvider,
        IContextProvider<OdsInstanceConfiguration> odsInstanceConfigurationContextProvider,
        IMapCache<(ulong odsInstanceHashId, string personType, PersonMapType mapType), TLookup, TResolved> mapCache,
        IMapCache<(ulong odsInstanceHashId, string personType, PersonMapType mapType), TResolved, TLookup> reverseMapCache,
        ICacheInitializationMarkerKeyProvider<TLookup> cacheInitializationMarkerKeyForLookupProvider,
        ICacheInitializationMarkerKeyProvider<TResolved> cacheInitializationMarkerKeyForResolvedProvider,
        Dictionary<string, bool> cacheSuppressionByPersonType,
        bool useProgressiveLoading)
    {
        _logger = LogManager.GetLogger(GetType());
        _personMapCacheInitializer = personMapCacheInitializer;
        _distributedLockProvider = distributedLockProvider;
        _odsInstanceConfigurationContextProvider = odsInstanceConfigurationContextProvider;
        _mapCache = mapCache;
        _reverseMapCache = reverseMapCache;
        _cacheSuppressionByPersonType = cacheSuppressionByPersonType;
        _performBackgroundInitialization = !useProgressiveLoading;
        _cacheInitializationMarkerKeyForLookup = new[] { cacheInitializationMarkerKeyForLookupProvider.CacheKey };
        _cacheInitializationMarkerKeyForResolved = new[] { cacheInitializationMarkerKeyForResolvedProvider.CacheKey };
    }

    protected abstract PersonMapType MapType { get; }

    protected async Task ResolveIdentifiersAsync(string personType, IDictionary<TLookup, TResolved> lookups)
    {
        TLookup[] suppliedLookupIdentifiers = lookups.Keys.ToArray();

        ICollection<TLookup> identifiersToLoad = IsCacheSuppressed(personType)
            ? suppliedLookupIdentifiers
            : await ResolveIdentifiersFromCacheAsync();

        if (identifiersToLoad is null || identifiersToLoad.Count == 0)
        {
            return;
        }

        var loadedIdentifierMappings = (await LoadUnresolvedPersonIdentifiersAsync(personType, identifiersToLoad))
            .Select(ExtractKeyValueTuple)
            .ToArray();

        foreach (var loadedIdentifierMapping in loadedIdentifierMappings)
        {
            lookups[loadedIdentifierMapping.key] = loadedIdentifierMapping.value;
        }

        ulong odsInstanceHashId = _odsInstanceConfigurationContextProvider.Get().OdsInstanceHashId;

        await Task.WhenAll(
            _mapCache.SetMapEntriesAsync(
                (odsInstanceHashId, personType, MapType),
                loadedIdentifierMappings),
            _reverseMapCache.SetMapEntriesAsync(
                (odsInstanceHashId, personType, MapType.Inverse()),
                loadedIdentifierMappings.Select(x => (x.value, x.key)).ToArray()));

        async Task<ICollection<TLookup>> ResolveIdentifiersFromCacheAsync()
        {
            ulong odsInstanceHashId = _odsInstanceConfigurationContextProvider.Get().OdsInstanceHashId;
            TLookup[] cacheLookupIdentifiers = _performBackgroundInitialization
                ? suppliedLookupIdentifiers.Concat(_cacheInitializationMarkerKeyForLookup).ToArray()
                : suppliedLookupIdentifiers.ToArray();

            TResolved[] resolvedIdentifiers = await _mapCache.GetMapEntriesAsync((odsInstanceHashId, personType, MapType), cacheLookupIdentifiers);
            List<TLookup> unresolvedIdentifiers = null;
            int lookupsToProcess = _performBackgroundInitialization ? resolvedIdentifiers.Length - 1 : resolvedIdentifiers.Length;

            for (int i = 0; i < lookupsToProcess; i++)
            {
                if (IsUnresolved(resolvedIdentifiers[i]))
                {
                    unresolvedIdentifiers ??= new List<TLookup>();
                    unresolvedIdentifiers.Add(cacheLookupIdentifiers[i]);
                }
                else
                {
                    lookups[cacheLookupIdentifiers[i]] = resolvedIdentifiers[i];
                }
            }

            if (_performBackgroundInitialization && IsUnresolved(resolvedIdentifiers[^1]))
            {
                string lockKey = $"cache-init-lock:{odsInstanceHashId}:{personType}:{MapType}";

                if (await _distributedLockProvider.TryAcquireLockAsync(lockKey, TimeSpan.FromMinutes(5)))
                {
                    try
                    {
                        await Task.WhenAll(
                            _mapCache.SetMapEntriesAsync(
                                (odsInstanceHashId, personType, MapType),
                                new[] { (_cacheInitializationMarkerKeyForLookup[0], _cacheInitializationMarkerKeyForResolved[0]) }),
                            _reverseMapCache.SetMapEntriesAsync(
                                (odsInstanceHashId, personType, MapType.Inverse()),
                                new[] { (_cacheInitializationMarkerKeyForResolved[0], _cacheInitializationMarkerKeyForLookup[0]) }));

                        _ = _personMapCacheInitializer.InitializePersonMapAsync(
                            odsInstanceHashId,
                            personType,
                            lockKey,
                            CancellationToken.None);
                    }
                    catch (Exception ex)
                    {
                        _logger.Error("An error occurred while attempting to add the 'initialization' marker cache entry to the cache.", ex);

                        try
                        {
                            await _distributedLockProvider.ReleaseLockAsync(lockKey);
                        }
                        catch (Exception releaseEx)
                        {
                            _logger.Error($"An error occurred while releasing the Redis initialization lock '{lockKey}' after marker write failure.", releaseEx);
                        }

                        throw;
                    }
                }
            }

            return unresolvedIdentifiers;
        }

        static bool IsUnresolved(TResolved resolvedIdentifier)
            => resolvedIdentifier is null || EqualityComparer<TResolved>.Default.Equals(resolvedIdentifier, default);
    }

    private bool IsCacheSuppressed(string personType)
        => _cacheSuppressionByPersonType.TryGetValue(personType, out bool isSuppressed) && isSuppressed;

    /// <summary>
    /// Gets the key/value tuple from the <see cref="PersonIdentifiersValueMap" /> appropriately for the <see cref="MapType" />.
    /// </summary>
    /// <param name="personIdentifiers">The UniqueId/USI values for a person in the ODS to be converted to a key/value tuple.</param>
    protected abstract (TLookup key, TResolved value) ExtractKeyValueTuple(PersonIdentifiersValueMap personIdentifiers);

    protected abstract Task<IEnumerable<PersonIdentifiersValueMap>> LoadUnresolvedPersonIdentifiersAsync(
        string personType,
        ICollection<TLookup> identifiersToLoad);
}

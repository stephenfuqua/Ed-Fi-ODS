// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Globalization;
using System.Threading.Tasks;
using EdFi.Common.Security;
using EdFi.Ods.Common.Caching;
using EdFi.Ods.Common.Descriptors;
using EdFi.Ods.Common.Exceptions;
using log4net;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;

namespace EdFi.Ods.Features.ExternalCache;

/// <summary>
/// Implements asynchronous distributed cache access for descriptor cache entries.
/// </summary>
public class AsyncExternalCacheProvider : IAsyncCacheProvider<ulong>
{
    private const string DefaultExceptionMessage = "Unable to access distributed cache.";

    private readonly IDistributedCache _distributedCache;
    private readonly TimeSpan _absoluteExpiration;
    private readonly TimeSpan _slidingExpiration;
    private readonly ILog _logger = LogManager.GetLogger(typeof(AsyncExternalCacheProvider));

    public AsyncExternalCacheProvider(IDistributedCache distributedCache, TimeSpan slidingExpiration, TimeSpan absoluteExpiration)
    {
        _distributedCache = distributedCache;
        _slidingExpiration = slidingExpiration;
        _absoluteExpiration = absoluteExpiration;
    }

    public async Task<(bool found, object value)> TryGetCachedObjectAsync(ulong key)
    {
        try
        {
            var keyAsString = key.ToString(CultureInfo.InvariantCulture);
            var cachedValue = await _distributedCache.GetStringAsync(keyAsString).ConfigureAwait(false);

            if (string.IsNullOrEmpty(cachedValue))
            {
                return (false, null);
            }

            object value = keyAsString.StartsWith(ApiClientDetailsCacheKeyProvider.CacheKeyPrefix, StringComparison.Ordinal)
                ? JsonConvert.DeserializeObject<ApiClientDetails>(cachedValue)
                : ExternalCacheSerializationHelper.Deserialize(cachedValue, _logger);

            return (true, value);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            throw new DistributedCacheException(DefaultExceptionMessage, ex);
        }
    }

    public async Task SetCachedObjectAsync(ulong key, object obj)
    {
        try
        {
            await _distributedCache.SetStringAsync(
                    key.ToString(CultureInfo.InvariantCulture),
                    ExternalCacheSerializationHelper.Serialize(obj),
                    CreateDistributedCacheEntryOptions())
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            throw new DistributedCacheException(DefaultExceptionMessage, ex);
        }
    }

    public async Task InsertAsync(ulong key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        try
        {
            await _distributedCache.SetStringAsync(
                    key.ToString(CultureInfo.InvariantCulture),
                    ExternalCacheSerializationHelper.Serialize(value),
                    new DistributedCacheEntryOptions
                    {
                        AbsoluteExpiration = absoluteExpiration < DateTime.MaxValue ? absoluteExpiration : null,
                        SlidingExpiration = slidingExpiration > TimeSpan.Zero ? slidingExpiration : null
                    })
                .ConfigureAwait(false);
        }
        catch (Exception ex)
        {
            _logger.Error(ex);
            throw new DistributedCacheException(DefaultExceptionMessage, ex);
        }
    }

    private DistributedCacheEntryOptions CreateDistributedCacheEntryOptions()
    {
        return new DistributedCacheEntryOptions
        {
            AbsoluteExpirationRelativeToNow = _absoluteExpiration > TimeSpan.Zero ? _absoluteExpiration : null,
            SlidingExpiration = _slidingExpiration > TimeSpan.Zero ? _slidingExpiration : null
        };
    }

    private static class ExternalCacheSerializationHelper
    {
        private const string GuidPrefix = "(Guid)";
        private const string IntPrefix = "(int)";

        private static readonly JsonSerializerSettings DefaultSerializerSettings = new()
        {
            TypeNameHandling = TypeNameHandling.None,
            ReferenceLoopHandling = ReferenceLoopHandling.Ignore
        };

        public static string Serialize(object @object)
        {
            if (@object is Guid guid)
            {
                return $"{GuidPrefix}{guid.ToString("N", CultureInfo.InvariantCulture)}";
            }

            if (@object is int @int)
            {
                return $"{IntPrefix}{@int.ToString(CultureInfo.InvariantCulture)}";
            }

            return JsonConvert.SerializeObject(@object, DefaultSerializerSettings);
        }

        public static object Deserialize(string value, ILog logger)
        {
            if (value.StartsWith(GuidPrefix, StringComparison.InvariantCulture)
                && Guid.TryParse(value[GuidPrefix.Length..], out var guid))
            {
                return guid;
            }

            if (value.StartsWith(IntPrefix, StringComparison.InvariantCulture)
                && int.TryParse(value[IntPrefix.Length..], out var @int))
            {
                return @int;
            }

            try
            {
                return JsonConvert.DeserializeObject<DescriptorMaps>(value, DefaultSerializerSettings);
            }
            catch (JsonException e)
            {
                logger.Warn($"Exception during deserialization of the string \"{value}\". Message: \"{e.Message}\"");
                return null;
            }
        }
    }
}

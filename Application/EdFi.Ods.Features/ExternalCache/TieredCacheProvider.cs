// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Threading.Tasks;
using EdFi.Ods.Common.Caching;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

namespace EdFi.Ods.Features.ExternalCache;

/// <summary>
/// Provides a short-lived in-process cache in front of the distributed descriptor cache.
/// </summary>
public class TieredCacheProvider : ICacheProvider<ulong>, IAsyncCacheProvider<ulong>, IClearable
{
    private static readonly object NullValue = new();

    private readonly IMemoryCache _memoryCache;
    private readonly AsyncExternalCacheProvider _distributedCacheProvider;
    private readonly TimeSpan _l1CacheDuration;

    public TieredCacheProvider(
        IMemoryCache memoryCache,
        IDistributedCache distributedCache,
        TimeSpan l1CacheDuration,
        TimeSpan slidingExpiration,
        TimeSpan absoluteExpiration)
    {
        _memoryCache = memoryCache;
        _distributedCacheProvider = new AsyncExternalCacheProvider(distributedCache, slidingExpiration, absoluteExpiration);
        _l1CacheDuration = l1CacheDuration;
    }

    public bool TryGetCachedObject(ulong key, out object value)
    {
        if (_memoryCache.TryGetValue(key, out var cachedValue))
        {
            value = ReferenceEquals(cachedValue, NullValue)
                ? null
                : cachedValue;

            return true;
        }

        value = null;
        return false;
    }

    public void SetCachedObject(ulong key, object obj)
    {
        SetLocalCacheValue(key, obj);
    }

    public void Insert(ulong key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        SetLocalCacheValue(key, value);
    }

    public async Task<(bool found, object value)> TryGetCachedObjectAsync(ulong key)
    {
        if (TryGetCachedObject(key, out var value))
        {
            return (true, value);
        }

        var result = await _distributedCacheProvider.TryGetCachedObjectAsync(key).ConfigureAwait(false);

        if (result.found)
        {
            SetLocalCacheValue(key, result.value);
        }

        return result;
    }

    public async Task SetCachedObjectAsync(ulong key, object obj)
    {
        SetLocalCacheValue(key, obj);
        await _distributedCacheProvider.SetCachedObjectAsync(key, obj).ConfigureAwait(false);
    }

    public async Task InsertAsync(ulong key, object value, DateTime absoluteExpiration, TimeSpan slidingExpiration)
    {
        SetLocalCacheValue(key, value);
        await _distributedCacheProvider.InsertAsync(key, value, absoluteExpiration, slidingExpiration).ConfigureAwait(false);
    }

    public void Clear()
    {
        if (_memoryCache is MemoryCache memoryCache)
        {
            memoryCache.Compact(1.0);
            return;
        }

        throw new NotSupportedException(
            $"Unable to clear the {nameof(TieredCacheProvider)} because the registered {nameof(IMemoryCache)} does not support compaction.");
    }

    private void SetLocalCacheValue(ulong key, object value)
    {
        var entryOptions = new MemoryCacheEntryOptions();

        if (_l1CacheDuration > TimeSpan.Zero)
        {
            entryOptions.AbsoluteExpirationRelativeToNow = _l1CacheDuration;
        }
        else
        {
            entryOptions.AbsoluteExpiration = DateTimeOffset.MaxValue;
        }

        _memoryCache.Set(key, value ?? NullValue, entryOptions);
    }
}

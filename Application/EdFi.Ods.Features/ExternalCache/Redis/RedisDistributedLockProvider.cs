// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Threading.Tasks;
using EdFi.Ods.Api.Caching.Person;
using EdFi.Ods.Features.Services.Redis;
using StackExchange.Redis;

namespace EdFi.Ods.Features.ExternalCache.Redis;

/// <summary>
/// Provides a Redis-backed distributed lock implementation for cache initialization.
/// </summary>
public class RedisDistributedLockProvider : IDistributedLockProvider
{
    private readonly IRedisConnectionProvider _redisConnectionProvider;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisDistributedLockProvider"/> class.
    /// </summary>
    /// <param name="redisConnectionProvider">The Redis connection provider.</param>
    public RedisDistributedLockProvider(IRedisConnectionProvider redisConnectionProvider)
    {
        _redisConnectionProvider = redisConnectionProvider ?? throw new ArgumentNullException(nameof(redisConnectionProvider));
    }

    /// <inheritdoc />
    public Task<bool> TryAcquireLockAsync(string lockKey, TimeSpan expiration)
    {
        ArgumentNullException.ThrowIfNull(lockKey);

        IDatabase database = _redisConnectionProvider.Get();
        return database.StringSetAsync(lockKey, "1", expiration, when: When.NotExists);
    }

    /// <inheritdoc />
    public Task ReleaseLockAsync(string lockKey)
    {
        ArgumentNullException.ThrowIfNull(lockKey);

        IDatabase database = _redisConnectionProvider.Get();
        return database.KeyDeleteAsync(lockKey);
    }
}

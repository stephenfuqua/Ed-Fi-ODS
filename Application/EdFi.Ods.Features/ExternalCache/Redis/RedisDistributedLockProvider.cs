// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Collections.Generic;
using System.Linq;
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
    private readonly Dictionary<string, string> _lockTokens = new();
    private byte[] _lockReleaseScriptSha;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisDistributedLockProvider"/> class.
    /// </summary>
    /// <param name="redisConnectionProvider">The Redis connection provider.</param>
    public RedisDistributedLockProvider(IRedisConnectionProvider redisConnectionProvider)
    {
        _redisConnectionProvider =
            redisConnectionProvider
            ?? throw new ArgumentNullException(nameof(redisConnectionProvider));
        LoadScripts();
    }

    /// <inheritdoc />
    public async Task<bool> TryAcquireLockAsync(string lockKey, TimeSpan expiration)
    {
        ArgumentNullException.ThrowIfNull(lockKey);

        string token = Guid.NewGuid().ToString();
        IDatabase database = _redisConnectionProvider.Get();
        bool acquired = await database.StringSetAsync(
            lockKey,
            token,
            expiration,
            when: When.NotExists
        );

        if (acquired)
        {
            _lockTokens[lockKey] = token;
        }

        return acquired;
    }

    /// <inheritdoc />
    public Task ReleaseLockAsync(string lockKey)
    {
        ArgumentNullException.ThrowIfNull(lockKey);

        if (!_lockTokens.TryGetValue(lockKey, out var token))
        {
            return Task.CompletedTask;
        }

        IDatabase database = _redisConnectionProvider.Get();
        _lockTokens.Remove(lockKey);
        return database.ScriptEvaluateAsync(
            _lockReleaseScriptSha,
            new[] { (RedisKey)lockKey },
            new[] { (RedisValue)token }
        );
    }

    private void LoadScripts()
    {
        // Lua script for safe distributed lock release.
        // This script ensures only the lock owner can delete the lock by verifying the token matches.
        // Prevents other nodes from accidentally deleting locks they don't own if a lock expires
        // and gets reacquired by another node.
        const string luaReleaseScript =
            @"
            -- Check if the current lock value (KEYS[1]) matches our token (ARGV[1])
            if redis.call('GET', KEYS[1]) == ARGV[1] then
                -- Token matches: this node owns the lock, so delete it
                return redis.call('DEL', KEYS[1])
            else
                -- Token doesn't match: another node owns this lock, so don't delete
                return 0
            end
        ";

        try
        {
            var server = _redisConnectionProvider
                .Get()
                .Multiplexer.GetServer(
                    _redisConnectionProvider.Get().Multiplexer.GetEndPoints().FirstOrDefault()
                );
            _lockReleaseScriptSha = server.ScriptLoad(luaReleaseScript);
        }
        catch (Exception ex)
        {
            throw new InvalidOperationException(
                "Failed to load Lua script for distributed lock release. Ensure Redis is accessible.",
                ex
            );
        }
    }
}

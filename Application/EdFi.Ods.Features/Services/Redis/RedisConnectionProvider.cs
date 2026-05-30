// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Threading;
using EdFi.Ods.Common.Configuration;
using StackExchange.Redis;

namespace EdFi.Ods.Features.Services.Redis;

/// <summary>
/// Provides access to a Redis database connection.
/// </summary>
public class RedisConnectionProvider : IRedisConnectionProvider
{
    private readonly ConfigurationOptions _configurationOptions;
    private readonly SemaphoreSlim _connectionLock = new(initialCount: 1, maxCount: 1);

    private volatile IConnectionMultiplexer _connection;
    private IDatabase _cache;

    /// <summary>
    /// Initializes a new instance of the <see cref="RedisConnectionProvider"/> class.
    /// </summary>
    /// <param name="redisConfiguration">The Redis connection settings.</param>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="redisConfiguration"/> is null.</exception>
    public RedisConnectionProvider(RedisConfiguration redisConfiguration)
    {
        if (redisConfiguration is null)
        {
            throw new ArgumentNullException(nameof(redisConfiguration));
        }

        _configurationOptions = CreateConfigurationOptions(redisConfiguration);
    }

    public IDatabase Get()
    {
        EnsureConnected();

        return _cache;
    }

    internal static ConfigurationOptions CreateConfigurationOptions(RedisConfiguration redisConfiguration)
    {
        if (redisConfiguration is null)
        {
            throw new ArgumentNullException(nameof(redisConfiguration));
        }

        var configurationOptions = ConfigurationOptions.Parse(redisConfiguration.Configuration ?? "localhost");
        configurationOptions.SyncTimeout = redisConfiguration.SyncTimeoutMs;
        configurationOptions.AsyncTimeout = redisConfiguration.AsyncTimeoutMs;
        configurationOptions.ConnectTimeout = redisConfiguration.ConnectTimeoutMs;
        configurationOptions.ConnectRetry = redisConfiguration.ConnectRetry;
        configurationOptions.AbortOnConnectFail = redisConfiguration.AbortOnConnectFail;
        configurationOptions.KeepAlive = redisConfiguration.KeepAliveSeconds;
        configurationOptions.Ssl = redisConfiguration.Ssl;

        if (!string.IsNullOrWhiteSpace(redisConfiguration.Password))
        {
            configurationOptions.Password = redisConfiguration.Password;
        }

        return configurationOptions;
    }

    private void EnsureConnected()
    {
        if (_cache is not null)
        {
            return;
        }

        _connectionLock.Wait();

        try
        {
            if (_cache is null)
            {
                _connection = ConnectionMultiplexer.Connect(_configurationOptions);
                _cache = _connection.GetDatabase();
            }
        }
        finally
        {
            _connectionLock.Release();
        }
    }
}

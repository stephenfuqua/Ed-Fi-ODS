// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Threading;
using EdFi.Ods.Common.Configuration;
using log4net;
using StackExchange.Redis;

namespace EdFi.Ods.Features.Services.Redis;

/// <summary>
/// Provides access to a Redis database connection.
/// </summary>
public class RedisConnectionProvider : IRedisConnectionProvider
{
    private readonly ConfigurationOptions _configurationOptions;
    private readonly SemaphoreSlim _connectionLock = new(initialCount: 1, maxCount: 1);
    private readonly ILog _logger = LogManager.GetLogger(typeof(RedisConnectionProvider));

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

    /// <inheritdoc />
    public bool IsConnected { get; private set; }

    /// <inheritdoc />
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
                SubscribeToConnectionEvents(_connection);
                _cache = _connection.GetDatabase();
                IsConnected = _connection.IsConnected;
            }
        }
        finally
        {
            _connectionLock.Release();
        }
    }

    private void SubscribeToConnectionEvents(IConnectionMultiplexer connection)
    {
        connection.ConnectionFailed += OnConnectionFailed;
        connection.ConnectionRestored += OnConnectionRestored;
        connection.ErrorMessage += OnErrorMessage;
        connection.InternalError += OnInternalError;
    }

    private void OnConnectionFailed(object sender, ConnectionFailedEventArgs args)
    {
        IsConnected = false;

        _logger.Error(
            $"Redis connection failed. Endpoint: {args.EndPoint}, FailureType: {args.FailureType}, ConnectionType: {args.ConnectionType}.",
            args.Exception);
    }

    private void OnConnectionRestored(object sender, ConnectionFailedEventArgs args)
    {
        IsConnected = true;

        _logger.Info(
            $"Redis connection restored. Endpoint: {args.EndPoint}, FailureType: {args.FailureType}, ConnectionType: {args.ConnectionType}.");
    }

    private void OnErrorMessage(object sender, RedisErrorEventArgs args)
    {
        _logger.Warn($"Redis reported an error message: {args.Message}");
    }

    private void OnInternalError(object sender, InternalErrorEventArgs args)
    {
        _logger.Warn($"Redis reported an internal error from '{args.Origin}'.", args.Exception);
    }
}

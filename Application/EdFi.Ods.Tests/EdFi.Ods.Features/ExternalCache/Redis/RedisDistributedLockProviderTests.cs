// SPDX-License-Identifier: Apache-2.0
// Licensed to the Ed-Fi Alliance under one or more agreements.
// The Ed-Fi Alliance licenses this file to you under the Apache License, Version 2.0.
// See the LICENSE and NOTICES files in the project root for more information.

using System;
using System.Threading.Tasks;
using EdFi.Ods.Features.ExternalCache.Redis;
using EdFi.Ods.Features.Services.Redis;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;
using StackExchange.Redis;

namespace EdFi.Ods.Tests.EdFi.Ods.Features.ExternalCache.Redis;

[TestFixture]
public class RedisDistributedLockProviderTests
{
    private IRedisConnectionProvider _redisConnectionProvider;
    private IDatabase _database;
    private RedisDistributedLockProvider _provider;

    [SetUp]
    public void SetUp()
    {
        _redisConnectionProvider = A.Fake<IRedisConnectionProvider>();
        _database = A.Fake<IDatabase>();

        A.CallTo(() => _redisConnectionProvider.Get()).Returns(_database);

        _provider = new RedisDistributedLockProvider(_redisConnectionProvider);
    }

    [Test]
    public async Task TryAcquireLockAsync_ShouldUseSetNxWithProvidedExpiration()
    {
        const string lockKey = "cache-init-lock";
        var expiration = TimeSpan.FromSeconds(30);

        A.CallTo(() => _database.StringSetAsync(lockKey, "1", expiration, When.NotExists, CommandFlags.None))
            .Returns(true);

        var result = await _provider.TryAcquireLockAsync(lockKey, expiration);

        result.ShouldBeTrue();

        A.CallTo(() => _database.StringSetAsync(lockKey, "1", expiration, When.NotExists, CommandFlags.None))
            .MustHaveHappenedOnceExactly();
        A.CallTo(() => _redisConnectionProvider.Get()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task TryAcquireLockAsync_ShouldReturnFalse_WhenLockAlreadyExists()
    {
        const string lockKey = "cache-init-lock";
        var expiration = TimeSpan.FromSeconds(30);

        A.CallTo(() => _database.StringSetAsync(lockKey, "1", expiration, When.NotExists, CommandFlags.None))
            .Returns(false);

        var result = await _provider.TryAcquireLockAsync(lockKey, expiration);

        result.ShouldBeFalse();
    }

    [Test]
    public async Task ReleaseLockAsync_ShouldDeleteLockKey()
    {
        const string lockKey = "cache-init-lock";

        A.CallTo(() => _database.KeyDeleteAsync(lockKey, CommandFlags.None)).Returns(true);

        await _provider.ReleaseLockAsync(lockKey);

        A.CallTo(() => _database.KeyDeleteAsync(lockKey, CommandFlags.None)).MustHaveHappenedOnceExactly();
        A.CallTo(() => _redisConnectionProvider.Get()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void TryAcquireLockAsync_ShouldThrow_WhenLockKeyIsNull()
    {
        var exception = Should.Throw<ArgumentNullException>(() => _provider.TryAcquireLockAsync(null!, TimeSpan.FromSeconds(10)));

        exception.ParamName.ShouldBe("lockKey");
    }

    [Test]
    public void ReleaseLockAsync_ShouldThrow_WhenLockKeyIsNull()
    {
        var exception = Should.Throw<ArgumentNullException>(() => _provider.ReleaseLockAsync(null!));

        exception.ParamName.ShouldBe("lockKey");
    }
}

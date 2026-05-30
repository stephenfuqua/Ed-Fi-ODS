// SPDX-License-Identifier: Apache-2.0
// Copyright (c) Ed-Fi Alliance, LLC and Contributors.

using System;
using System.Threading.Tasks;
using Castle.DynamicProxy;
using EdFi.Ods.Common.Caching;
using EdFi.Ods.Common.Exceptions;
using FakeItEasy;
using NUnit.Framework;
using Shouldly;

namespace EdFi.Ods.Tests.EdFi.Ods.Common.Caching;

[TestFixture]
public class AsyncCachingInterceptorTests
{
    [Test]
    public void Intercept_SyncMethod_ShouldReturnFromL1Cache_WhenPresent()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.GetData));

        object cachedValue = "cached-l1";
        A.CallTo(() => localCacheProvider.TryGetCachedObject(A<object>._, out cachedValue)).Returns(true);

        interceptor.Intercept(invocation);

        invocation.ReturnValue.ShouldBe(cachedValue);
        A.CallTo(() => invocation.Proceed()).MustNotHaveHappened();
        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).MustNotHaveHappened();
    }

    [Test]
    public void Intercept_SyncMethod_ShouldFallbackToAsyncProvider_WhenL1Misses()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.GetData));

        object ignored = null!;
        A.CallTo(() => localCacheProvider.TryGetCachedObject(A<object>._, out ignored)).Returns(false);

        var cachedValue = "cached-l2";
        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).Returns((true, (object) cachedValue));

        interceptor.Intercept(invocation);

        invocation.ReturnValue.ShouldBe(cachedValue);
        A.CallTo(() => invocation.Proceed()).MustNotHaveHappened();
        A.CallTo(() => asyncCacheProvider.SetCachedObjectAsync(A<object>._, A<object>._)).MustNotHaveHappened();
    }

    [Test]
    public void Intercept_SyncMethod_ShouldProceedAndCache_WhenBothMiss()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.GetData));

        object ignored = null!;
        A.CallTo(() => localCacheProvider.TryGetCachedObject(A<object>._, out ignored)).Returns(false);
        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).Returns((false, null!));
        A.CallTo(() => invocation.Proceed()).Invokes(() => invocation.ReturnValue = "from-target");

        interceptor.Intercept(invocation);

        invocation.ReturnValue.ShouldBe("from-target");
        A.CallTo(() => invocation.Proceed()).MustHaveHappenedOnceExactly();
        A.CallTo(() => asyncCacheProvider.SetCachedObjectAsync(A<object>._, "from-target")).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task Intercept_AsyncTaskOfT_ShouldReturnCachedValue()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.GetDataAsync));

        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).Returns((true, (object) "cached-result"));

        interceptor.Intercept(invocation);
        var result = await (Task<string>) invocation.ReturnValue;

        result.ShouldBe("cached-result");
        A.CallTo(() => invocation.Proceed()).MustNotHaveHappened();
        A.CallTo(() => localCacheProvider.TryGetCachedObject(A<object>._, out _)).MustNotHaveHappened();
    }

    [Test]
    public async Task Intercept_AsyncTaskOfT_ShouldProceedAndCache_WhenMiss()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.GetDataAsync));

        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).Returns((false, null!));
        A.CallTo(() => invocation.Proceed()).Invokes(() => invocation.ReturnValue = Task.FromResult("from-target"));

        interceptor.Intercept(invocation);
        var result = await (Task<string>) invocation.ReturnValue;

        result.ShouldBe("from-target");
        A.CallTo(() => invocation.Proceed()).MustHaveHappenedOnceExactly();
        A.CallTo(() => asyncCacheProvider.SetCachedObjectAsync(A<object>._, "from-target")).MustHaveHappenedOnceExactly();
    }

    [Test]
    public async Task Intercept_AsyncTask_ShouldSkipExecution_WhenCached()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.ExecuteOperationAsync));

        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).Returns((true, (object) AsyncCachingInterceptor<object>.AsyncVoidTaskMarker));

        interceptor.Intercept(invocation);
        await (Task) invocation.ReturnValue;

        A.CallTo(() => invocation.Proceed()).MustNotHaveHappened();
        A.CallTo(() => asyncCacheProvider.SetCachedObjectAsync(A<object>._, A<object>._)).MustNotHaveHappened();
    }

    [Test]
    public async Task Intercept_AsyncTask_ShouldProceedAndCache_WhenMiss()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = CreateInvocation(nameof(ISampleService.ExecuteOperationAsync));

        A.CallTo(() => asyncCacheProvider.TryGetCachedObjectAsync(A<object>._)).Returns((false, null!));
        A.CallTo(() => invocation.Proceed()).Invokes(() => invocation.ReturnValue = Task.CompletedTask);

        interceptor.Intercept(invocation);
        await (Task) invocation.ReturnValue;

        A.CallTo(() => invocation.Proceed()).MustHaveHappenedOnceExactly();
        A.CallTo(() => asyncCacheProvider.SetCachedObjectAsync(A<object>._, AsyncCachingInterceptor<object>.AsyncVoidTaskMarker)).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void Intercept_ShouldThrowCacheKeyGenerationException_WhenNoDeclaringType()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);
        var invocation = A.Fake<IInvocation>();
        var method = A.Fake<System.Reflection.MethodInfo>();

        A.CallTo(() => invocation.Method).Returns(method);
        A.CallTo(() => invocation.Arguments).Returns(Array.Empty<object>());
        A.CallTo(() => method.ReturnType).Returns(typeof(string));
        A.CallTo(() => method.DeclaringType).Returns(null);
        A.CallTo(() => method.Name).Returns("TestMethod");

        var act = () => interceptor.Intercept(invocation);

        var exception = act.ShouldThrow<CachingInterceptorCacheKeyGenerationException>();
        exception.Message.ShouldContain("Cannot generated a cache key for invocation with method 'TestMethod' because it has no DeclaringType.");
    }

    [Test]
    public void Clear_ShouldClearBothProviders()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>(options => options.Implements(typeof(IClearable)));
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>(options => options.Implements(typeof(IClearable)));
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);

        interceptor.Clear();

        A.CallTo(() => ((IClearable) localCacheProvider).Clear()).MustHaveHappenedOnceExactly();
        A.CallTo(() => ((IClearable) asyncCacheProvider).Clear()).MustHaveHappenedOnceExactly();
    }

    [Test]
    public void Clear_ShouldThrow_WhenNeitherProviderIsClearable()
    {
        var localCacheProvider = A.Fake<ICacheProvider<object>>();
        var asyncCacheProvider = A.Fake<IAsyncCacheProvider<object>>();
        var interceptor = new AsyncCachingInterceptor<object>(localCacheProvider, asyncCacheProvider);

        var act = () => interceptor.Clear();

        var exception = act.ShouldThrow<NotSupportedException>();
        exception.Message.ShouldBe("Neither underlying cache provider supports cache clearing.");
    }

    private static IInvocation CreateInvocation(string methodName)
    {
        var invocation = A.Fake<IInvocation>();
        A.CallTo(() => invocation.Method).Returns(typeof(ISampleService).GetMethod(methodName)!);
        A.CallTo(() => invocation.Arguments).Returns(Array.Empty<object>());
        return invocation;
    }

    private interface ISampleService
    {
        string GetData();
        Task<string> GetDataAsync();
        Task ExecuteOperationAsync();
    }
}

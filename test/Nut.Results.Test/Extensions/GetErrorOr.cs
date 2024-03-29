using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class GetErrorOr
{
    //sync - sync
    [Fact]
    public void SyncSync_ifOkがnullの場合は例外が発生する()
    {
        Action act = () => Result.Ok().GetErrorOr((Func<Exception>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SyncSync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = Result.Error(expected).GetErrorOr(() => new Exception());
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public void SyncSync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var result = Result.Ok().GetErrorOr(() =>
        {
            actionExecuted = true;
            return actionResult;
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //async - sync
    [Fact]
    public async Task AsyncSync_ifOkがnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().GetErrorOr((Func<Exception>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncSync_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.GetErrorOr((Task<Result>)null, () => new Exception());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncSync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = await Result.Error(expected).AsTask().GetErrorOr(() => new Exception());
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task AsyncSync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var result = await Result.Ok().AsTask().GetErrorOr(() =>
        {
            actionExecuted = true;
            return actionResult;
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //sync - async
    [Fact]
    public async Task SyncAsync_ifOkがnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().GetErrorOr((Func<Task<Exception>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SyncAsync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = await Result.Error(expected).GetErrorOr(() => Task.FromResult(new Exception()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task SyncAsync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var result = await Result.Ok().GetErrorOr(() =>
        {
            actionExecuted = true;
            return Task.FromResult(actionResult);
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //async - async
    [Fact]
    public async Task AsyncAsync_ifOkがnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().GetErrorOr((Func<Task<Exception>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncAsync_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.GetErrorOr((Task<Result>)null, () => Task.FromResult(new Exception()));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = await Result.Error(expected).AsTask().GetErrorOr(() => Task.FromResult(new Exception()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var result = await Result.Ok().AsTask().GetErrorOr(() =>
        {
            actionExecuted = true;
            return Task.FromResult(actionResult);
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }
}

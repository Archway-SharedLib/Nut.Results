using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class GetErrorOr_T
{
    //sync - sync
    [Fact]
    public void SyncSync_ifOkがnullの場合は例外が発生する()
    {
        Action act = () => Result.Ok("Ok").GetErrorOr((Func<string, Exception>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void SyncSync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = Result.Error<string>(expected).GetErrorOr(_ => new Exception());
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public void SyncSync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var sourceValue = "ok";
        string paramValue = null;
        var result = Result.Ok(sourceValue).GetErrorOr(value =>
        {
            actionExecuted = true;
            paramValue = value;
            return actionResult;
        });

        actionExecuted.Should().BeTrue();
        paramValue.Should().Be(sourceValue);
        result.Should().BeSameAs(actionResult);
    }

    //async - sync
    [Fact]
    public async Task AsyncSync_ifOkがnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("Ok").AsTask().GetErrorOr((Func<string, Exception>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncSync_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.GetErrorOr((Task<Result<string>>)null, _ => new Exception());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncSync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = await Result.Error<string>(expected).AsTask().GetErrorOr(_ => new Exception());
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task AsyncSync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var sourceValue = "ok";
        string paramValue = null;
        var result = await Result.Ok(sourceValue).AsTask().GetErrorOr(value =>
        {
            actionExecuted = true;
            paramValue = value;
            return actionResult;
        });

        actionExecuted.Should().BeTrue();
        paramValue.Should().Be(sourceValue);
        result.Should().BeSameAs(actionResult);
    }

    //sync - async
    [Fact]
    public async Task SyncAsync_ifOkがnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("Ok").GetErrorOr((Func<string, Task<Exception>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task SyncAsync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = await Result.Error<string>(expected).GetErrorOr(_ => Task.FromResult(new Exception()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task SyncAsync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var sourceValue = "ok";
        string paramValue = null;
        var result = await Result.Ok(sourceValue).GetErrorOr(value =>
        {
            actionExecuted = true;
            paramValue = value;
            return Task.FromResult(actionResult);
        });

        actionExecuted.Should().BeTrue();
        paramValue.Should().Be(sourceValue);
        result.Should().BeSameAs(actionResult);
    }

    //async - sync
    [Fact]
    public async Task AsyncAsync_ifOkがnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("Ok").AsTask().GetErrorOr((Func<string, Task<Exception>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncAsync_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.GetErrorOr((Task<Result<string>>)null, _ => Task.FromResult(new Exception()));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合は失敗の値が返される()
    {
        var expected = new Exception();
        var result = await Result.Error<string>(expected).AsTask().GetErrorOr(_ => Task.FromResult(new Exception()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Exception();
        var actionExecuted = false;
        var sourceValue = "ok";
        string paramValue = null;
        var result = await Result.Ok(sourceValue).AsTask().GetErrorOr(value =>
        {
            actionExecuted = true;
            paramValue = value;
            return Task.FromResult(actionResult);
        });

        actionExecuted.Should().BeTrue();
        paramValue.Should().Be(sourceValue);
        result.Should().BeSameAs(actionResult);
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class GetErrorOr_VT
{
    //sync - sync
    [Fact]
    public async Task ValTaskRes_TError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().GetErrorOr((Func<IError>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskRes_TError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error(expected).AsValueTask().GetErrorOr(() => new Error());
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ValTaskRes_TError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok().AsValueTask().GetErrorOr(() =>
        {
            actionExecuted = true;
            return actionResult;
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task TaskRes_ValTaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok().AsTask().GetErrorOr((Func<ValueTask<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskRes_ValTaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error(expected).AsTask()
            .GetErrorOr(() => new ValueTask<Error>(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task TaskRes_ValTaskTError_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.GetErrorOr((Task<Result>)
            null,
            () => new ValueTask<Error>(new Error())
        ).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskRes_ValTaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok().AsTask().GetErrorOr(() =>
        {
            actionExecuted = true;
            return new ValueTask<Error>(actionResult);
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task ValTaskRes_TaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask()
            .GetErrorOr((Func<Task<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskRes_TaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error(expected).AsValueTask()
            .GetErrorOr(() => Task.FromResult(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ValTaskRes_TaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok().AsValueTask()
            .GetErrorOr(() =>
            {
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task Res_ValTaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok()
            .GetErrorOr((Func<ValueTask<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Res_ValTaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error(expected)
            .GetErrorOr(() => new ValueTask<Error>(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task Res_ValTaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok()
            .GetErrorOr(() =>
            {
                actionExecuted = true;
                return new ValueTask<Error>(actionResult);
            });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task ValTaskRes_ValTaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask()
            .GetErrorOr((Func<ValueTask<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskRes_ValTaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error(expected).AsValueTask()
            .GetErrorOr(() => new ValueTask<Error>(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ValTaskRes_ValTaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok().AsValueTask()
            .GetErrorOr(() =>
            {
                actionExecuted = true;
                return new ValueTask<Error>(actionResult);
            });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }
}

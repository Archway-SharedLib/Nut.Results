using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class GetErrorOr_T_VT
{
    [Fact]
    public async Task ValTaskResT_TError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsValueTask().GetErrorOr((Func<int, IError>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_TError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error<int>(expected).AsValueTask()
            .GetErrorOr(_ => new Error());
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ValTaskResT_TError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok(1).AsValueTask().GetErrorOr(_ =>
        {
            actionExecuted = true;
            return actionResult;
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task TaskResT_ValTaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsTask()
            .GetErrorOr((Func<int, ValueTask<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ValTaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error<int>(expected).AsTask()
            .GetErrorOr(_ => new ValueTask<Error>(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task TaskResT_ValTaskTError_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.GetErrorOr((Task<Result<int>>)
            null,
            _ => new ValueTask<Error>(new Error())
        ).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ValTaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok(1).AsTask().GetErrorOr(_ =>
        {
            actionExecuted = true;
            return new ValueTask<Error>(actionResult);
        });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task ValTaskResT_TaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsValueTask()
            .GetErrorOr((Func<int, Task<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_TaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error<int>(expected).AsValueTask()
            .GetErrorOr(_ => Task.FromResult(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ValTaskResT_TaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok(1).AsValueTask()
            .GetErrorOr(_ =>
            {
                actionExecuted = true;
                return Task.FromResult(actionResult);
            });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task ResT_ValTaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1)
            .GetErrorOr((Func<int, ValueTask<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResT_ValTaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error<int>(expected)
            .GetErrorOr(_ => new ValueTask<Error>(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ResT_ValTaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok(1)
            .GetErrorOr(_ =>
            {
                actionExecuted = true;
                return new ValueTask<Error>(actionResult);
            });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }

    //-------------

    [Fact]
    public async Task ValTaskResT_ValTaskTError_ifOkがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsValueTask()
            .GetErrorOr((Func<int, ValueTask<IError>>)null).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskRes_ValTaskTError_失敗の場合は失敗の値が返される()
    {
        var expected = new Error();
        var result = await Result.Error<int>(expected).AsValueTask()
            .GetErrorOr(_ => new ValueTask<Error>(new Error()));
        result.Should().BeSameAs(expected);
    }

    [Fact]
    public async Task ValTaskRes_ValTaskTError_成功の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = new Error();
        var actionExecuted = false;
        var result = await Result.Ok(1).AsValueTask()
            .GetErrorOr(_ =>
            {
                actionExecuted = true;
                return new ValueTask<Error>(actionResult);
            });

        actionExecuted.Should().BeTrue();
        result.Should().BeSameAs(actionResult);
    }
}

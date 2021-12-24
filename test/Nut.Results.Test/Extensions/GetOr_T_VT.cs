using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class GetOr_T_VT
{
    [Fact]
    public async Task ValTaskResT_T_ifErrorがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsValueTask()
            .GetOr((Func<IError, int>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_T_成功の場合は成功の値が返される()
    {
        var expected = 1;
        var result = await Result.Ok(expected).AsValueTask().GetOr(_ => 2);
        result.Should().Be(expected);
    }

    [Fact]
    public async Task ValTaskResT_T_失敗の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = 1;
        var actionExecuted = false;
        var sourceError = new Error();
        IError paramError = null;
        var result = await Result.Error<int>(sourceError).AsValueTask()
            .GetOr(e =>
            {
                paramError = e;
                actionExecuted = true;
                return actionResult;
            });

        actionExecuted.Should().BeTrue();
        paramError.Should().BeSameAs(sourceError);
        result.Should().Be(actionResult);
    }

    //------

    [Fact]
    public async Task TaskResT_ValTaskT_ifErrorがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsTask()
            .GetOr((Func<IError, ValueTask<int>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ValTaskT_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.GetOr(
            (Task<Result<int>>)null,
            _ => new ValueTask<int>(1)).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ValTaskT_成功の場合は成功の値が返される()
    {
        var expected = 1;
        var result = await Result.Ok(expected).AsTask().GetOr(_ => new ValueTask<int>(2));
        result.Should().Be(expected);
    }

    [Fact]
    public async Task TaskResT_ValTaskT_失敗の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = 1;
        var actionExecuted = false;
        var sourceError = new Error();
        IError paramError = null;
        var result = await Result.Error<int>(sourceError).AsTask().GetOr(e =>
        {
            paramError = e;
            actionExecuted = true;
            return new ValueTask<int>(actionResult);
        });

        actionExecuted.Should().BeTrue();
        paramError.Should().BeSameAs(sourceError);
        result.Should().Be(actionResult);
    }

    //------

    [Fact]
    public async Task ValTaskResT_TaskT_ifErrorがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsValueTask()
            .GetOr((Func<IError, Task<int>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_TaskT_成功の場合は成功の値が返される()
    {
        var expected = 1;
        var result = await Result.Ok(expected).AsValueTask()
            .GetOr(_ => Task.FromResult(2));
        result.Should().Be(expected);
    }

    [Fact]
    public async Task ValTaskResT_TaskT_失敗の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = 1;
        var actionExecuted = false;
        var sourceError = new Error();
        IError paramError = null;
        var result = await Result.Error<int>(sourceError).AsValueTask().GetOr(e =>
        {
            paramError = e;
            actionExecuted = true;
            return Task.FromResult(actionResult);
        });

        actionExecuted.Should().BeTrue();
        paramError.Should().BeSameAs(sourceError);
        result.Should().Be(actionResult);
    }

    //------

    [Fact]
    public async Task ResT_ValTaskT_ifErrorがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1)
            .GetOr((Func<IError, ValueTask<int>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResT_ValTaskT_成功の場合は成功の値が返される()
    {
        var expected = 1;
        var result = await Result.Ok(expected)
            .GetOr(_ => new ValueTask<int>(2));
        result.Should().Be(expected);
    }

    [Fact]
    public async Task ResT_ValTaskT_失敗の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = 1;
        var actionExecuted = false;
        var sourceError = new Error();
        IError paramError = null;
        var result = await Result.Error<int>(sourceError).GetOr(e =>
        {
            paramError = e;
            actionExecuted = true;
            return new ValueTask<int>(actionResult);
        });

        actionExecuted.Should().BeTrue();
        paramError.Should().BeSameAs(sourceError);
        result.Should().Be(actionResult);
    }

    //------

    [Fact]
    public async Task ValTaskResT_ValTaskT_ifErrorがnullの場合は例外が発生する()
    {
        var act = () => Result.Ok(1).AsValueTask()
            .GetOr((Func<IError, ValueTask<int>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ValTaskT_成功の場合は成功の値が返される()
    {
        var expected = 1;
        var result = await Result.Ok(expected).AsValueTask()
            .GetOr(_ => new ValueTask<int>(2));
        result.Should().Be(expected);
    }

    [Fact]
    public async Task ValTaskResT_ValTaskT_失敗の場合はアクションが実行されてその結果が返る()
    {
        var actionResult = 1;
        var actionExecuted = false;
        var sourceError = new Error();
        IError paramError = null;
        var result = await Result.Error<int>(sourceError).AsValueTask().GetOr(e =>
        {
            paramError = e;
            actionExecuted = true;
            return new ValueTask<int>(actionResult);
        });

        actionExecuted.Should().BeTrue();
        paramError.Should().BeSameAs(sourceError);
        result.Should().Be(actionResult);
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class FlatMapError_T_VT
{
    [Fact]
    public async Task ValTaskResT_ReturnResT_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(Result.Ok("A").AsValueTask(), (Func<IError, Result<string>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnResT_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A");
        var executed = false;
        var result = await errResult.AsValueTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("A");
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTasResT_ReturnResT_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error<string>(expected).AsValueTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("A");
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task ResT_ReturnValueTaskResT_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok("A"),
            (Func<IError, ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResT_ReturnValueTaskResT_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A");
        var executed = false;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("A").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ResT_ReturnValueTaskResT_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error<string>(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("A").AsValueTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task ValueTaskResT_ReturnValueTaskResT_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok("A").AsValueTask(),
            (Func<IError, ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValueTaskResT_ReturnValueTaskResT_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A");
        var executed = false;
        var result = await errResult.AsValueTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("A").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_ReturnValueTaskResT_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error<string>(expected).AsValueTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("A").AsValueTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task ValueTaskResT_ReturnTaskResT_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok("A").AsValueTask(),
            (Func<IError, Task<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValueTaskResT_ReturnTaskResT_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A");
        var executed = false;
        var result = await errResult.AsValueTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("A").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_ReturnTaskResT_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error<string>(expected).AsValueTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("A").AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task TaskResT_ReturnValueTaskResT_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            (Task<Result<string>>)null,
            e => Result.Ok("A").AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ReturnValueTaskResT_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok("A").AsTask(),
            (Func<IError, ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ReturnValueTaskResT_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A");
        var executed = false;
        var result = await errResult.AsTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("A").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task TaskResT_ReturnValueTaskResT_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error<string>(expected).AsTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("A").AsValueTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }
}

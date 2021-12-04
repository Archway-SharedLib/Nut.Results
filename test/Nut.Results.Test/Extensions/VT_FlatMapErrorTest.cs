using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;


namespace Nut.Results.Test;

public class VT_FlatMapErrorTest
{
    [Fact]
    public async Task ValTaskRes_ReturnRes_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(Result.Ok().AsValueTask(), (Func<IError, Result>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskRes_ReturnRes_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = await errResult.AsValueTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTasRes_ReturnRes_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error(expected).AsValueTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task Res_ReturnValueTaskRes_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(Result.Ok(), (Func<IError, ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Res_ReturnValueTaskRes_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task Res_ReturnValueTaskRes_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task ValueTaskRes_ReturnValueTaskRes_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok().AsValueTask(),
            (Func<IError, ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValueTaskRes_ReturnValueTaskRes_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = await errResult.AsValueTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskRes_ReturnValueTaskRes_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error(expected).AsValueTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task ValueTaskRes_ReturnTaskRes_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok().AsValueTask(),
            (Func<IError, Task<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValueTaskRes_ReturnTaskRes_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = await errResult.AsValueTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskRes_ReturnTaskRes_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error(expected).AsValueTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    // ----------

    [Fact]
    public async Task TaskRes_ReturnValueTaskRes_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            (Task<Result>)null,
            e => Result.Ok().AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskRes_ReturnValueTaskRes_errorがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMapError(
            Result.Ok().AsTask(),
            (Func<IError, ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskRes_ReturnValueTaskRes_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = await errResult.AsTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task TaskRes_ReturnValueTaskRes_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var result = await Result.Error(expected).AsTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class MapError_T_VT
{
    [Fact]
    public async Task ValueTaskResT_TError_errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError((Func<IError, IError>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ValueTaskResT_TError_成功の場合はActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A").AsValueTask();
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return new Error();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_TError_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).AsValueTask().MapError(e =>
        {
            executed = true;
            param = e;
            return resultError;
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task ValueTaskResT_TError_アクションの結果にnullが渡されると例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError(_ => (IError)null).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    // --------------

    [Fact]
    public async Task ValueTaskResT_TaskTError_errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError((Func<IError, Task<IError>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ValueTaskResT_TaskTError_成功の場合はActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A").AsValueTask();
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return Task.FromResult(new Error());
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_TaskTError_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).AsValueTask().MapError(e =>
        {
            executed = true;
            param = e;
            return Task.FromResult(resultError);
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task ValueTaskResT_TaskTError_アクションの結果にnullが渡されると例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError(_ => (Task<IError>)null).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    [Fact]
    public async Task ValueTaskResT_TaskTError_アクションの結果のTaskにnullが渡されると例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError(_ => Task.FromResult<IError>(null)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    // --------------

    [Fact]
    public async Task ResT_ValueTaskTError_errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).MapError((Func<IError, ValueTask<IError>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ResT_ValueTaskTError_成功の場合はActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A");
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return new ValueTask<Error>(new Error());
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ResT_ValueTaskTError_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).MapError(e =>
        {
            executed = true;
            param = e;
            return new ValueTask<Error>(resultError);
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task ResT_ValueTaskTError_アクションの結果のTaskにnullが渡されると例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).MapError(_ => new ValueTask<Error>((Error)null)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    // --------------

    [Fact]
    public async Task ValueTaskResT_ValueTaskTError_errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError((Func<IError, ValueTask<IError>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ValueTaskResT_ValueTaskTError_成功の場合はActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A").AsValueTask();
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return new ValueTask<Error>(new Error());
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_ValueTaskTError_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).AsValueTask().MapError(e =>
        {
            executed = true;
            param = e;
            return new ValueTask<Error>(resultError);
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task ValueTaskResT_ValueTaskTError_アクションの結果のTaskにnullが渡されると例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().MapError(_ => new ValueTask<Error>((Error)null)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    // --------------

    [Fact]
    public async Task TaskResT_ValueTaskTError_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.MapError((Task<Result<string>>)null, err => new ValueTask<IError>(new Error())).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ValueTaskTError_errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsTask().MapError((Func<IError, ValueTask<IError>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task TaskResT_ValueTaskTError_成功の場合はActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("A").AsTask();
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return new ValueTask<Error>(new Error());
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task TaskResT_ValueTaskTError_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).AsTask().MapError(e =>
        {
            executed = true;
            param = e;
            return new ValueTask<Error>(resultError);
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task TaskResT_ValueTaskTError_アクションの結果のTaskにnullが渡されると例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsTask().MapError(_ => new ValueTask<Error>((Error)null)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }
}

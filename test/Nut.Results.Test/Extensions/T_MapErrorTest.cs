using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test;

public class T_MapErrorTest
{
    //sync - sync
    [Fact]
    public void SyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Error<string>(new Error()).MapError((Func<IError, IError>)null);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
    }

    [Fact]
    public void SyncSync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("ok");
        var executed = false;
        var result = errResult.MapError(e =>
        {
            executed = true;
            return new Error();
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public void SyncSync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = Result.Error<string>(expected).MapError(e =>
        {
            executed = true;
            param = e;
            return resultError;
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => Object.ReferenceEquals(e, resultError));
    }

    [Fact]
    public void SyncSync_アクションの結果にnullが渡されると例外が発生する()
    {
        Action act = () => Result.Error<string>(new Error()).MapError(_ => (IError)null);
        act.Should().Throw<InvalidReturnValueException>();
    }

    //async - sync
    [Fact]
    public async Task AsyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).AsTask()
            .MapError((Func<IError, IError>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.MapError((Task<Result<string>>)null, _ => new Error());
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("ok").AsTask();
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return new Error();
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).AsTask().MapError(e =>
        {
            executed = true;
            param = e;
            return resultError;
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => Object.ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task AsyncSync_アクションの結果にnullが渡されると例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).AsTask().MapError(_ => (IError)null);
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //sync - async
    [Fact]
    public async Task SyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).MapError((Func<IError, Task<IError>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task SyncAsync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("ok");
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return Task.FromResult(new Error());
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).MapError(e =>
        {
            executed = true;
            param = e;
            return Task.FromResult(resultError);
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => Object.ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task SyncAsync_アクションの結果がnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).MapError(_ => (Task<IError>)null);
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    [Fact]
    public async Task SyncAsync_アクションの結果にnullが渡された場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).MapError(_ => Task.FromResult((IError)null));
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //async - async
    [Fact]
    public async Task AsyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).AsTask().MapError((Func<IError, Task<IError>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.MapError((Task<Result<string>>)null, _ => Task.FromResult(new Error()));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("ok").AsTask();
        var executed = false;
        var result = await errResult.MapError(e =>
        {
            executed = true;
            return Task.FromResult(new Error());
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error<string>(expected).AsTask().MapError(e =>
        {
            executed = true;
            param = e;
            return Task.FromResult(resultError);
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.Match(e => Object.ReferenceEquals(e, resultError));
    }

    [Fact]
    public async Task AsyncAsync_アクションの結果がnullの場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).AsTask().MapError(_ => (Task<IError>)null);
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    [Fact]
    public async Task AsyncAsync_アクションの結果にnullが渡された場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error<string>(new Error()).AsTask().MapError(_ => Task.FromResult((IError)null));
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }
}

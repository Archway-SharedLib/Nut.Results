using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;



namespace Nut.Results.Test;

public class MapErrorTest
{
    //sync - sync
    [Fact]
    public void SyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Error(new Error()).MapError((Func<IError, IError>)null);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
    }

    [Fact]
    public void SyncSync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = errResult.MapError(e =>
        {
            executed = true;
            return new Error();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public void SyncSync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = Result.Error(expected).MapError(e =>
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
        Action act = () => Result.Error(new Error()).MapError(_ => (IError)null);
        act.Should().Throw<InvalidReturnValueException>();
    }

    //async - sync
    [Fact]
    public async Task AsyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error(new Error()).AsTask()
            .MapError((Func<IError, IError>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.MapError((Task<Result>)null, _ => new Error());
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok().AsTask();
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
    public async Task AsyncSync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error(expected).AsTask().MapError(e =>
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
        Func<Task> act = () => Result.Error(new Error()).AsTask().MapError(_ => (IError)null);
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //sync - async
    [Fact]
    public async Task SyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error(new Error()).MapError((Func<IError, Task<IError>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task SyncAsync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
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
    public async Task SyncAsync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error(expected).MapError(e =>
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
        Func<Task> act = () => Result.Error(new Error()).MapError(_ => (Task<IError>)null);
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    [Fact]
    public async Task SyncAsync_アクションの結果にnullが渡された場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error(new Error()).MapError(_ => Task.FromResult((IError)null));
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //async - async
    [Fact]
    public async Task AsyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error(new Error()).AsTask().MapError((Func<IError, Task<IError>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.MapError((Task<Result>)null, _ => Task.FromResult(new Error()));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok().AsTask();
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
    public async Task AsyncAsync_失敗の場合はアクションが実行されて結果が返る()
    {
        var executed = false;
        var expected = new Error();
        IError param = null;
        var resultError = new Error();
        var result = await Result.Error(expected).AsTask().MapError(e =>
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
        Func<Task> act = () => Result.Error(new Error()).AsTask().MapError(_ => (Task<IError>)null);
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    [Fact]
    public async Task AsyncAsync_アクションの結果にnullが渡された場合は例外が発生する()
    {
        Func<Task> act = () => Result.Error(new Error()).AsTask().MapError(_ => Task.FromResult((IError)null));
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }
}

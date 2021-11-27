using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test;

public class T_FlatMapTest
{
    //T1 -> Void

    [Fact]
    public void NoReturn_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Ok("A").FlatMap(null as Func<string, Result>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
    }

    [Fact]
    public void NoReturn_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error);
        var executed = false;
        var result = errResult.FlatMap(_ =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public void NoReturn_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = Result.Ok("123").FlatMap(_ =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeTrue();
        result.Should().BeOk();
    }

    [Fact]
    public async Task NoReturn_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("A").AsTask().FlatMap(null as Func<string, Result>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task NoReturn_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Result.Ok());
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task NoReturn_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(_ =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task NoReturn_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("123").AsTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeTrue();
        result.Should().BeOk();
    }

    [Fact]
    public async Task NoReturn_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("A").FlatMap(null as Func<string, Task<Result>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task NoReturn_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error);
        var executed = false;
        var result = await errResult.FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok());
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task NoReturn_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("123").FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok());
        });

        executed.Should().BeTrue();
        result.Should().BeOk();
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("A").AsTask().FlatMap(null as Func<string, Task<Result>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Task.Run(() => Result.Ok()));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok());
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("123").AsTask().FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok());
        });

        executed.Should().BeTrue();
        result.Should().BeOk();
    }

    //T1 -> T2
    [Fact]
    public void ReturnT_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Ok("A").FlatMap(null as Func<string, Result<string>>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
    }

    [Fact]
    public void ReturnT_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error);
        var executed = false;
        var result = errResult.FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("ok");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public void ReturnT_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = Result.Ok("123").FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("ok");
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task ReturnT_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("A").AsTask().FlatMap(null as Func<string, Result<string>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ReturnT_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Result.Ok("ok"));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task ReturnT_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("ok");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ReturnT_AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("123").AsTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("ok");
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task ReturnT_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("A").FlatMap(null as Func<string, Task<Result<string>>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ReturnT_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error);
        var executed = false;
        var result = await errResult.FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok("ok"));
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ReturnT_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("123").FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok("ok"));
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("A").AsTask().FlatMap(null as Func<string, Task<Result<string>>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result<string>>, _ => Task.Run(() => Result.Ok("ok")));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok("ok"));
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("123").AsTask().FlatMap(_ =>
        {
            executed = true;
            return Task.Run(() => Result.Ok("ok"));
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == "ok");
    }
}

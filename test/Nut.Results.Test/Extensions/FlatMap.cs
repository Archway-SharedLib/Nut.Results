using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class FlatMap
{
    //Void -> T

    [Fact]
    public void ReturnT_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Ok().FlatMap(null as Func<Result<string>>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
    }

    [Fact]
    public void ReturnT_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error);
        var executed = false;
        var result = errResult.FlatMap(() =>
        {
            executed = true;
            return Result.Ok("value");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public void ReturnT_SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expectedValue = "ok";
        var result = Result.Ok().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(expectedValue);
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == expectedValue);
    }

    [Fact]
    public async Task ReturnT_AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().FlatMap(null as Func<Result<string>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ReturnT_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result>, () => Result.Ok("ok"));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task ReturnT_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(() =>
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
        var expectedValue = "ok";
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(expectedValue);
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == expectedValue);
    }

    [Fact]
    public async Task ReturnT_SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().FlatMap(null as Func<Task<Result<string>>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ReturnT_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error);
        var executed = false;
        var result = await errResult.FlatMap(() =>
        {
            executed = true;
            return Result.Ok("ok").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ReturnT_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expectValue = "ok";
        var result = await Result.Ok().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(expectValue).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == expectValue);
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().FlatMap(null as Func<Task<Result<string>>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result>, () => Task.Run(() => Result.Ok("ok")));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(() =>
        {
            executed = true;
            return Result.Ok("ok").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ReturnT_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expectValue = "ok";
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(expectValue).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == expectValue);
    }

    //Void -> Void

    [Fact]
    public void NoReturn_SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Ok().FlatMap(null as Func<Result>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
    }

    [Fact]
    public void NoReturn_SyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error);
        var executed = false;
        var result = errResult.FlatMap(() =>
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
        var result = Result.Ok().FlatMap(() =>
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
        Func<Task> act = () => Result.Ok().AsTask().FlatMap(null as Func<Result>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task NoReturn_AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result>, () => Result.Ok());
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task NoReturn_AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(() =>
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
        var result = await Result.Ok().AsTask().FlatMap(() =>
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
        Func<Task> act = () => Result.Ok().FlatMap(null as Func<Task<Result>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task NoReturn_SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error);
        var executed = false;
        var result = await errResult.FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task NoReturn_SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeOk();
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().FlatMap(null as Func<Task<Result>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMap(null as Task<Result>, () => Task.Run(() => Result.Ok()));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error(error).AsTask();
        var executed = false;
        var result = await errResult.FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task NoReturn_AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeOk();
    }

    // 書き心地
    public async Task Lambdaで呼び出す()
    {
        Task<Result> DoTask()
        {
            return Task.FromResult(Result.Ok());
        }
        ValueTask<Result> DoValueTask()
        {
            return new ValueTask<Result>();
        }
        Task<Result<int>> DoTaskV(int v)
        {
            return Task.FromResult(Result.Ok(v));
        }
        ValueTask<Result<int>> DoValueTaskV(int v)
        {
            return new ValueTask<Result<int>>(v);
        }

        Result.Ok().FlatMap(() => Result.Ok());
        await Result.Ok().FlatMap(() => Task.FromResult(Result.Ok()));
        await Result.Ok().FlatMap(async () => await DoTask());
        await Result.Ok().FlatMap(() => new ValueTask<Result>(Result.Ok()));
        await Result.Ok().FlatMap(async () => await DoValueTask());

        await Result.Ok().AsTask().FlatMap(() => Result.Ok());
        await Result.Ok().AsTask().FlatMap(() => Task.FromResult(Result.Ok()));
        await Result.Ok().AsTask().FlatMap(async () => await DoTask());
        await Result.Ok().AsTask().FlatMap(() => new ValueTask<Result>(Result.Ok()));
        await Result.Ok().AsTask().FlatMap(async () => await DoValueTask());

        await Result.Ok().AsValueTask().FlatMap(() => Result.Ok());
        await Result.Ok().AsValueTask().FlatMap(() => Task.FromResult(Result.Ok()));
        await Result.Ok().AsValueTask().FlatMap(async () => await DoTask());
        await Result.Ok().AsValueTask().FlatMap(() => new ValueTask<Result>(Result.Ok()));
        await Result.Ok().AsValueTask().FlatMap(async () => await DoValueTask());

        Result.Ok().FlatMap(() => Result.Ok(1));
        await Result.Ok().FlatMap(() => Task.FromResult(Result.Ok(1)));
        await Result.Ok().FlatMap(async () => await DoTaskV(1));
        await Result.Ok().FlatMap(() => new ValueTask<Result<int>>(Result.Ok(1)));
        await Result.Ok().FlatMap(async () => await DoValueTaskV(1));

        await Result.Ok().AsTask().FlatMap(() => Result.Ok(1));
        await Result.Ok().AsTask().FlatMap(() => Task.FromResult(Result.Ok(1)));
        await Result.Ok().AsTask().FlatMap(async () => await DoTaskV(1));
        await Result.Ok().AsTask().FlatMap(() => new ValueTask<Result<int>>(Result.Ok(1)));
        await Result.Ok().AsTask().FlatMap(async () => await DoValueTaskV(1));

        await Result.Ok().AsValueTask().FlatMap(() => Result.Ok(1));
        await Result.Ok().AsValueTask().FlatMap(() => Task.FromResult(Result.Ok()));
        await Result.Ok().AsValueTask().FlatMap(async () => await DoTaskV(1));
        await Result.Ok().AsValueTask().FlatMap(() => new ValueTask<Result<int>>(Result.Ok(1)));
        await Result.Ok().AsValueTask().FlatMap(async () => await DoValueTaskV(1));
    }

}

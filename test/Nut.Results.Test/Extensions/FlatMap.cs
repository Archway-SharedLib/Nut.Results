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
        var error = new Exception();
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
    public void ReturnT_SyncSync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = Result.Ok().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(""[0].ToString()); // throw exception
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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
    public async Task ReturnT_AsyncSync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(""[0].ToString()); // throw exception
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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
    public async Task ReturnT_SyncAsync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = await Result.Ok().FlatMap(new Func<Task<Result<string>>>(() =>
        {
            executed = true;
            return Result.Ok(""[0].ToString()).AsTask(); // throw exception
        }));

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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

    [Fact]
    public async Task ReturnT_AsyncAsync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok(""[0].ToString()).AsTask(); // throw exception
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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
    public void NoReturn_SyncSync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = Result.Ok().FlatMap(new Func<Result>(() =>
        {
            executed = true;
            ""[0].ToString(); // throw exception
            return Result.Ok();
        }));

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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
    public async Task NoReturn_AsyncSync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            ""[0].ToString(); // throw exception
            return Result.Ok();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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
    public async Task NoReturn_SyncAsync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = await Result.Ok().FlatMap(() =>
        {
            executed = true;
            ""[0].ToString(); // throw exception
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        var error = new Exception();
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

    [Fact]
    public async Task NoReturn_AsyncAsync_処理内で例外が発生した場合は失敗の結果が返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            ""[0].ToString(); // throw exception
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }
}

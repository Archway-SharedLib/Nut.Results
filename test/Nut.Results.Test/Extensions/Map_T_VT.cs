using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Map_T_VT
{
    [Fact]
    public async Task AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().Map(null as Func<string, string>).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsValueTask();
        var executed = false;
        var result = await errResult.Map(_ =>
        {
            executed = true;
            return "Foo";
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var resultValue = 123;
        var result = await Result.Ok("123").AsValueTask().Map(_ =>
        {
            executed = true;
            return resultValue;
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == resultValue);
    }

    [Fact]
    public async Task AsyncSync_戻り値がnullの場合は例外が発生する()
    {
        var act = () => Result.Ok("ok").AsValueTask().Map(_ => null as string).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    [Fact]
    public async Task SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").Map(null as Func<string, ValueTask<string>>).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error);
        var executed = false;
        var result = await errResult.Map(_ =>
        {
            executed = true;
            return new ValueTask<string>("Foo");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var resultValue = 123;
        var result = await Result.Ok("123").Map(_ =>
        {
            executed = true;
            return new ValueTask<int>(resultValue);
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == resultValue);
    }

    [Fact]
    public async Task SyncAsync_戻り値がnullの場合は例外が発生する()
    {
        var act = () => Result.Ok("ok").Map(_ => new ValueTask<string>(null as string)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //--------------

    [Fact]
    public async Task TaskAsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsTask().Map(null as Func<string, ValueTask<string>>).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task TaskAsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.Map(null as Task<Result<string>>, _ => new ValueTask<string>("v")).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task TaskAsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        var result = await errResult.Map(_ =>
        {
            executed = true;
            return new ValueTask<string>("Foo");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task TaskAsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var resultValue = 123;
        var result = await Result.Ok("123").AsTask().Map(_ =>
        {
            executed = true;
            return new ValueTask<int>(resultValue);
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == resultValue);
    }

    [Fact]
    public async Task TaskAsyncAsync_戻り値がnullの場合は例外が発生する()
    {
        var act = () => Result.Ok("ok").AsTask().Map(_ => new ValueTask<string>(null as string)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //--------------

    [Fact]
    public async Task AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().Map(null as Func<string, ValueTask<string>>).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsValueTask();
        var executed = false;
        var result = await errResult.Map(_ =>
        {
            executed = true;
            return new ValueTask<string>("Foo");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var resultValue = 123;
        var result = await Result.Ok("123").AsValueTask().Map(_ =>
        {
            executed = true;
            return new ValueTask<int>(resultValue);
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == resultValue);
    }

    [Fact]
    public async Task AsyncAsync_戻り値がnullの場合は例外が発生する()
    {
        var act = () => Result.Ok("ok").AsValueTask().Map(_ => new ValueTask<string>(null as string)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }

    //--------------

    [Fact]
    public async Task AsyncTaskAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().Map(null as Func<string, Task<string>>).AsTask();
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncTaskAsync_失敗の場合は同じActionは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var errResult = Result.Error<string>(error).AsValueTask();
        var executed = false;
        var result = await errResult.Map(_ =>
        {
            executed = true;
            return Task.FromResult("Foo");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task AsyncTaskAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var resultValue = 123;
        var result = await Result.Ok("123").AsValueTask().Map(_ =>
        {
            executed = true;
            return Task.FromResult(resultValue);
        });

        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == resultValue);
    }

    [Fact]
    public async Task AsyncTaskAsync_戻り値がnullの場合は例外が発生する()
    {
        var act = () => Result.Ok("ok").AsValueTask().Map(_ => Task.FromResult(null as string)).AsTask();
        await act.Should().ThrowAsync<InvalidReturnValueException>();
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Tap_T
{
    [Fact]
    public void SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("success").Tap(null as Action<string>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
    }

    [Fact]
    public void SyncSync_成功の場合はokのactionが値が渡されて実行され呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("success");
        var executed = false;
        var value = "";
        var result = expected.Tap((v) =>
        {
            executed = true;
            value = v;
        });
        executed.Should().BeTrue();
        value.Should().Be("success");
        result.Should().Be(expected).And.BeOk();
    }

    [Fact]
    public void SyncSync_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error());
        var result = expected.Tap((v) =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public async Task AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("string").AsTask().Tap(null as Action<string>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncSync_sourceが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.Tap(null as Task<Result<string>>, new Action<string>(_ => { }));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_成功の場合はokのactionが実行される()
    {
        var executed = false;
        var value = "";
        var result = await Result.Ok("success").AsTask().Tap(v =>
        {
            value = v;
            executed = true;
        });
        executed.Should().BeTrue();
        value.Should().Be("success");
        result.Should().BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
    {
        var executed = false;
        var error = new Error();
        var result = await Result.Error<string>(error).AsTask().Tap(v =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e == error);
    }

    [Fact]
    public async Task SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("success").Tap(null as Func<string, Task>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task SyncAsync_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("success");
        var executed = false;
        var value = "";
        var result = await expected.Tap(v =>
        {
            return Task.Run(() =>
            {
                executed = true;
                value = v;
            });
        });
        value.Should().Be("success");
        executed.Should().BeTrue();
        result.Should().Be(expected).And.BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error());
        var result = await expected.Tap(v =>
        {
            return Task.Run(() =>
            {
                executed = true;
            });
        });
        executed.Should().BeFalse();
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public async Task AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("success").AsTask().Tap(null as Func<string, Task>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncAsync_sourceが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.Tap(null as Task<Result<string>>, _ => { return Task.Run(() => { }); });
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はokのactionが実行される()
    {
        var executed = false;
        var value = "";
        var result = await Result.Ok("success").AsTask().Tap(v =>
        {
            return Task.Run(() =>
            {
                value = v;
                executed = true;
            });
        });
        value.Should().Be("success");
        executed.Should().BeTrue();
        result.Should().BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合はokのactionは実行されず失敗の値がそのまま帰る()
    {
        var executed = false;
        var error = new Error();
        var result = await Result.Error<string>(error).AsTask().Tap(_ =>
        {
            return Task.Run(() =>
            {
                executed = true;
            });
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e == error);
    }

    // 書き心地
    public async Task Lambdaで呼び出す()
    {
        Task DoTask()
        {
            return Task.CompletedTask;
        }
        ValueTask DoValueTask()
        {
            return new ValueTask();
        }

        Result.Ok(1).Tap((v) => { });
        await Result.Ok(1).Tap((v) => Task.CompletedTask);
        await Result.Ok(1).Tap(async (v) => await DoTask());
        await Result.Ok(1).Tap((v) => new ValueTask());
        await Result.Ok(1).Tap(async (v) => await DoValueTask());

        await Result.Ok(1).AsTask().Tap((v) => { });
        await Result.Ok(1).AsTask().Tap((v) => Task.CompletedTask);
        await Result.Ok(1).AsTask().Tap(async (v) => await DoTask());
        await Result.Ok(1).AsTask().Tap((v) => new ValueTask());
        await Result.Ok(1).AsTask().Tap(async (v) => await DoValueTask());

        await Result.Ok(1).AsValueTask().Tap((v) => { });
        await Result.Ok(1).AsValueTask().Tap((v) => Task.CompletedTask);
        await Result.Ok(1).AsValueTask().Tap(async (v) => await DoTask());
        await Result.Ok(1).AsValueTask().Tap((v) => new ValueTask());
        await Result.Ok(1).AsValueTask().Tap(async (v) => await DoValueTask());
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Tap_T_VT
{
    // ---------------

    [Fact]
    public async Task ValueTaskResT_Void_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().Tap(null as Action).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ValueTaskResT_Void_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("A").AsValueTask();
        var executed = false;
        var result = await expected.Tap(() =>
        {
            executed = true;
        });
        executed.Should().BeTrue();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_Void_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error()).AsValueTask();
        var result = await expected.Tap(() =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeError();
    }

    // ---------------

    [Fact]
    public async Task ResT_ValueTask_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").Tap(null as Func<ValueTask>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ResT_ValueTask_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("A");
        var executed = false;
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
        result.Should().Be(expected).And.BeOk();
    }

    [Fact]
    public async Task ResT_ValueTask_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error());
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().Be(expected).And.BeError();
    }

    // ---------------

    [Fact]
    public async Task ValueTaskResT_Task_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().Tap(null as Func<Task>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ValueTaskResT_Task_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("A").AsValueTask();
        var executed = false;
        var result = await expected.Tap(() =>
        {
            executed = true;
            return Task.CompletedTask;
        });
        executed.Should().BeTrue();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_Task_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error()).AsValueTask();
        var result = await expected.Tap(() =>
        {
            executed = true;
            return Task.CompletedTask;
        });
        executed.Should().BeFalse();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeError();
    }

    // ---------------

    [Fact]
    public async Task TaskResT_ValueTask_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsTask().Tap(null as Func<ValueTask>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task TaskResT_ValueTask_sourcceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.Tap((Task<Result<string>>)null, () => new ValueTask()).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task TaskResT_ValueTask_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("A").AsTask();
        var executed = false;
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeOk();
    }

    [Fact]
    public async Task TaskResT_ValueTask_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error()).AsTask();
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeError();
    }

    // ---------------

    [Fact]
    public async Task ValueTaskResT_ValueTask_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().Tap(null as Func<ValueTask>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ValueTaskResT_ValueTask_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok("A").AsValueTask();
        var executed = false;
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeOk();
    }

    [Fact]
    public async Task ValueTaskResT_ValueTask_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error<string>(new Error()).AsValueTask();
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeError();
    }
}

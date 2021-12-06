using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Tap_VT
{
    // ---------------

    [Fact]
    public async Task ValueTaskRes_Void_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().Tap(null as Action).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ValueTaskRes_Void_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok().AsValueTask();
        var executed = false;
        var result = await expected.Tap(() =>
        {
            executed = true;
        });
        executed.Should().BeTrue();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeOk();
    }

    [Fact]
    public async Task ValueTaskRes_Void_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error(new Error()).AsValueTask();
        var result = await expected.Tap(() =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeError();
    }

    // ---------------

    [Fact]
    public async Task Res_ValueTask_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().Tap(null as Func<ValueTask>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task Res_ValueTask_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok();
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
    public async Task Res_ValueTask_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error(new Error());
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
    public async Task ValueTaskRes_Task_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().Tap(null as Func<Task>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ValueTaskRes_Task_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok().AsValueTask();
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
    public async Task ValueTaskRes_Task_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error(new Error()).AsValueTask();
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
    public async Task TaskRes_ValueTask_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsTask().Tap(null as Func<ValueTask>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task TaskRes_ValueTask_sourcceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.Tap((Task<Result>)null, () => new ValueTask()).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task TaskRes_ValueTask_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok().AsTask();
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
    public async Task TaskRes_ValueTask_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error(new Error()).AsTask();
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
    public async Task ValueTaskRes_ValueTask_パラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().Tap(null as Func<ValueTask>).AsTask();
        var ex = await act.Should().ThrowAsync<ArgumentNullException>();
        ex.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task ValueTaskRes_ValueTask_成功の場合はokのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expected = Result.Ok().AsValueTask();
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
    public async Task ValueTaskRes_ValueTask_失敗の場合はokのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Error(new Error()).AsValueTask();
        var result = await expected.Tap(() =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().Be(await expected.ConfigureAwait(false)).And.BeError();
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class TapError_T_VT
{
    [Fact]
    public async Task ValueTaskResT_Void_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().TapError(null as Action<IError>).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ValueTaskResT_Void_失敗の場合はerrorのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError).AsValueTask();
        var executed = false;
        IError error = null;
        var result = await expected.TapError(e =>
        {
            error = e;
            executed = true;
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(await expected).And.BeError();
    }

    [Fact]
    public async Task ValueTaskResT_Void_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Ok("A").AsValueTask();
        var result = await expected.TapError(_ =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    //-------------------

    [Fact]
    public async Task ResT_ValueTask_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").TapError(null as Func<IError, ValueTask>).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ResT_ValueTask_失敗の場合はerrorのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError);
        var executed = false;
        IError error = null;
        var result = await expected.TapError(e =>
        {
            error = e;
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public async Task ResT_ValueTask_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Ok("A");
        var result = await expected.TapError(_ =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    //-------------------

    [Fact]
    public async Task ValueTaskResT_Task_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().TapError(null as Func<IError, Task>).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ValueTaskResT_Task_失敗の場合はerrorのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError).AsValueTask();
        var executed = false;
        IError error = null;
        var result = await expected.TapError(e =>
        {
            error = e;
            executed = true;
            return Task.CompletedTask;
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(await expected).And.BeError();
    }

    [Fact]
    public async Task ValueTaskResT_Task_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Ok("A").AsValueTask();
        var result = await expected.TapError(_ =>
        {
            executed = true;
            return Task.CompletedTask;
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    //-------------------

    [Fact]
    public async Task TaskResT_ValueTask_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsTask().TapError(null as Func<IError, ValueTask>).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task TaskResT_ValueTask_失敗の場合はerrorのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError).AsTask();
        var executed = false;
        IError error = null;
        var result = await expected.TapError(e =>
        {
            error = e;
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(await expected).And.BeError();
    }

    [Fact]
    public async Task TaskResT_ValueTask_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Ok("A").AsTask();
        var result = await expected.TapError(_ =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task TaskResT_ValueTask_Sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.TapError(null as Task<Result<string>>, e => new ValueTask()).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("source");
    }

    //-------------------

    [Fact]
    public async Task ValueTaskResT_ValueTask_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("A").AsValueTask().TapError(null as Func<IError, ValueTask>).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task ValueTaskResT_ValueTask_失敗の場合はerrorのactionが実行さ呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError).AsValueTask();
        var executed = false;
        IError error = null;
        var result = await expected.TapError(e =>
        {
            error = e;
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(await expected).And.BeError();
    }

    [Fact]
    public async Task ValueTaskResT_ValueTask_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Ok("A").AsValueTask();
        var result = await expected.TapError(_ =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

}

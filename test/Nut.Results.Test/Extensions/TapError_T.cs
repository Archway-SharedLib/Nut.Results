using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class TapError_T
{
    [Fact]
    public void SyncSync_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Ok("success").TapError(null as Action<IError>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
    }

    [Fact]
    public void SyncSync_失敗の場合はerrorのactionが実行され呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError);
        var executed = false;
        IError error = null;
        var result = expected.TapError(e =>
        {
            error = e;
            executed = true;
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public void SyncSync_成功の場合はerrorのactionは実行されず呼び出した値と同じ値が返る()
    {
        var executed = false;
        var expected = Result.Ok("success");
        var result = expected.TapError(_ =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task AsyncSync_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("success").AsTask().TapError(null as Action<IError>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncSync_sourceが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.TapError(null as Task<Result<string>>, _ => { });
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合はerrorのactionが実行される呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError);
        var executed = false;
        IError error = null;
        var result = await expected.AsTask().TapError(e =>
        {
            error = e;
            executed = true;
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public async Task AsyncSync_成功の場合はerrorのactionは実行されず失敗の値がそのまま帰る()
    {
        var executed = false;
        var result = await Result.Ok("success").AsTask().TapError(_ =>
        {
            executed = true;
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task SyncAsync_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("success").TapError(null as Func<IError, Task>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合はerrorのactionが実行される呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError);
        var executed = false;
        IError error = null;
        var result = await expected.TapError(e =>
        {
            return Task.Run(() =>
            {
                error = e;
                executed = true;
            });
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public async Task SyncAsync_成功の場合はerrorのactionは実行されず失敗の値がそのまま帰る()
    {
        var executed = false;
        var result = await Result.Ok("success").TapError(_ =>
        {
            return Task.Run(() =>
            {
                executed = true;
            });
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }


    [Fact]
    public async Task AsyncAsync_Errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("success").AsTask().TapError(null as Func<IError, Task>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncAsync_sourceが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.TapError(null as Task<Result<string>>, _ => Task.Run(() => { }));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合はerrorのactionが実行される呼び出した値と同じ値が返る()
    {
        var expectedError = new Error();
        var expected = Result.Error<string>(expectedError);
        var executed = false;
        IError error = null;
        var result = await expected.AsTask().TapError(e =>
        {
            return Task.Run(() =>
            {
                error = e;
                executed = true;
            });
        });
        executed.Should().BeTrue();
        expectedError.Should().Be(error);
        result.Should().Be(expected).And.BeError();
    }

    [Fact]
    public async Task AsyncAsync__成功の場合はerrorのactionは実行されず失敗の値がそのまま帰る()
    {
        var executed = false;
        var result = await Result.Ok("success").AsTask().TapError(_ =>
        {
            return Task.Run(() =>
            {
                executed = true;
            });
        });
        executed.Should().BeFalse();
        result.Should().BeOk();
    }
}
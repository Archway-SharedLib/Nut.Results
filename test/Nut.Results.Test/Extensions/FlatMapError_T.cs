using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;



namespace Nut.Results.Test;

public class FlatMapError_T
{
    //T -> T

    [Fact]
    public void SyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Error<string>(new Exception()).FlatMapError(null as Func<Exception, Result<string>>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
    }

    [Fact]
    public void SyncSync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok("ok");
        var executed = false;
        var result = errResult.FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("success");
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public void SyncSync_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = Result.Error<string>(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("success");
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public void SyncSync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = Result.Error<string>(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok(""[0].ToString()); // throw exception
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    [Fact]
    public async Task AsyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("ok").AsTask().FlatMapError(null as Func<Exception, Result<string>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMapError(null as Task<Result<string>>, e => Result.Ok("ok"));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合はActionが実行され値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        Exception param = null;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("success");
        });

        executed.Should().BeTrue();
        param.Should().Be(error);
        result.Should().BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public async Task AsyncSync_成功の場合はアクションが実行されず結果が成功で返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").AsTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("success");
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task AsyncSync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = await Result.Error<string>(expected).AsTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok(""[0].ToString()); // throw exception
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    [Fact]
    public async Task SyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("ok").FlatMapError(null as Func<Exception, Task<Result<string>>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合はActionが実行され結果が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<string>(error);
        var executed = false;
        Exception param = null;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("success").AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(error);
        result.Should().BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public async Task SyncAsync_成功の場合はアクションが実行されず成功のResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("success").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task SyncAsync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = await Result.Error<string>(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok(""[0].ToString()).AsTask(); // throw exception
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    [Fact]
    public async Task AsyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok("ok").AsTask().FlatMapError(null as Func<Exception, Task<Result<string>>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMapError(null as Task<Result<string>>, e => Task.Run(() => Result.Ok("ok")));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合はActionが実行され値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<string>(error).AsTask();
        var executed = false;
        Exception param = null;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok("success").AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(error);
        result.Should().BeOk().And.Match(v => v == "success");
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はアクションが実行されず結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").AsTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok("success").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk().And.Match(v => v == "ok");
    }

    [Fact]
    public async Task AsyncAsync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = await Result.Error<string>(expected).AsTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok(""[0].ToString()).AsTask(); // throw exception
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }
}

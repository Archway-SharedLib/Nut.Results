using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class FlatMapError
{
    //sync -> sync

    [Fact]
    public void SyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Error(new Exception()).FlatMapError(null as Func<Exception, Result>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("error");
    }

    [Fact]
    public void SyncSync_成功の場合は同じActionは実行されず成功の値が返る()
    {
        var errResult = Result.Ok();
        var executed = false;
        var result = errResult.FlatMapError(e =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public void SyncSync_失敗の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = Result.Error(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeOk();
    }

    [Fact]
    public void SyncSync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = Result.Error(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            ""[0].ToString(); // throw exception
            return Result.Ok();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    //async -> sync

    [Fact]
    public async Task AsyncSync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().FlatMapError(null as Func<Exception, Result>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMapError(null as Task<Result>, e => Result.Ok());
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合はActionが実行され値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error(error).AsTask();
        var executed = false;
        Exception param = null;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok();
        });

        executed.Should().BeTrue();
        param.Should().Be(error);
        result.Should().BeOk();
    }

    [Fact]
    public async Task AsyncSync_成功の場合はアクションが実行されず結果が成功で返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task AsyncSync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = await Result.Error(expected).AsTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            ""[0].ToString(); // throw exception
            return Result.Ok();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    //sync -> async

    [Fact]
    public async Task SyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().FlatMapError(null as Func<Exception, Task<Result>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合はActionが実行され結果が返る()
    {
        var error = new Exception();
        var errResult = Result.Error(error);
        var executed = false;
        Exception param = null;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(error);
        result.Should().BeOk();
    }

    [Fact]
    public async Task SyncAsync_成功の場合はアクションが実行されず成功のResultで返る()
    {
        var executed = false;
        var result = await Result.Ok().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task SyncAsync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = await Result.Error(expected).FlatMapError(e =>
        {
            executed = true;
            param = e;
            ""[0].ToString(); // throw exception
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    // async -> async

    [Fact]
    public async Task AsyncAsync_errorパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok().AsTask().FlatMapError(null as Func<Exception, Task<Result>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("error");
    }

    [Fact]
    public async Task AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.FlatMapError(null as Task<Result>, e => Task.Run(() => Result.Ok()));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合はActionが実行され値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error(error).AsTask();
        var executed = false;
        Exception param = null;
        var result = await errResult.FlatMapError(e =>
        {
            executed = true;
            param = e;
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(error);
        result.Should().BeOk();
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はアクションが実行されず結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMapError(e =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeOk();
    }

    [Fact]
    public async Task AsyncAsync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = false;
        var expected = new Exception();
        Exception param = null;
        var result = await Result.Error(expected).AsTask().FlatMapError(e =>
        {
            executed = true;
            param = e;
            ""[0].ToString(); // throw exception
            return Result.Ok().AsTask();
        });

        executed.Should().BeTrue();
        param.Should().Be(expected);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }
}

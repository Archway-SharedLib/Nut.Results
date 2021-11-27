using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;
using System.Threading.Tasks;
using System;

namespace Nut.Results.Test;

public class ResultBuilderTest_Try
{
    [Fact]
    public void NoReturn_Sync_例外が発生しない場合は成功が返る()
        => Result.Try(() => { }).Should().BeOk();

    [Fact]
    public void NoReturn_Sync_例外が発生した場合は失敗が返る()
        => Result.Try(() => RaiseException("Failed")).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");

    [Fact]
    public void NoReturn_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Action)null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void NoReturn_Sync_wizHandle_例外が発生しない場合は成功が返る()
        => Result.Try(() => { }, e => new TestError()).Should().BeOk();

    [Fact]
    public void NoReturn_Sync_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        Result.Try((Action)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        }).Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public void NoReturn_Sync_withHande_引数1がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Action)null!, e => new TestError());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void NoReturn_Sync_withHande_引数2がない場合は例外は発生する()
    {
        Action act = () => Result.Try(() => { }, null!);
        act.Should().Throw<ArgumentNullException>();
    }

    //----------------

    [Fact]
    public async Task NoReturn_Async_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => { }));
        (await result.ConfigureAwait(false)).Should().BeOk();
    }

    [Fact]
    public async Task NoReturn_Async_例外が発生した場合は失敗が返る()
    {
        var result = Result.Try(() => Task.Run(() => RaiseException("Failed")));
        (await result.ConfigureAwait(false)).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task NoReturn_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task NoReturn_Async_withHandle_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => { }), e => new TestError());
        (await result.ConfigureAwait(false)).Should().BeOk();
    }

    [Fact]
    public async Task NoReturn_Async_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        var res = await Result.Try((Func<Task>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        });
        res.Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public async Task NoReturn_Async_wizHandle_引数1がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task>)null!, e => new TestError());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task NoReturn_Async_wizHandle_引数2がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try(() => Task.CompletedTask, null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    //---------------------

    [Fact]
    public void T_Sync_例外が発生しない場合は成功が返る()
        => Result.Try(() => "Good").Should().BeOk().And.Match(v => v == "Good");

    [Fact]
    public void T_Sync_例外が発生した場合は失敗が返る()
        => Result.Try(() => RaiseException<string>("Failed")).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");

    [Fact]
    public void T_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<string>)null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T_Sync_wizHandle_例外が発生しない場合は成功が返る()
        => Result.Try(() => "Good", e => new TestError()).Should().BeOk().And.Match(v => v == "Good");

    [Fact]
    public void T_Sync_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        Result.Try((Func<string>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        }).Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public void T_Sync_withHande_引数1がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<string>)null!, e => new TestError());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T_Sync_withHande_引数2がない場合は例外は発生する()
    {
        Action act = () => Result.Try(() => "Good", null!);
        act.Should().Throw<ArgumentNullException>();
    }

    //----------------------

    [Fact]
    public async Task T_Async_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => "Good"));
        (await result.ConfigureAwait(false)).Should().BeOk().And.Match(v => v == "Good");
    }

    [Fact]
    public async Task T_Async_例外が発生した場合は失敗が返る()
    {
        var result = Result.Try(() => Task.Run(() => RaiseException<string>("Failed")));
        (await result).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task T_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<string>>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T_Async_withHandle_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => "Good"), e => new TestError());
        (await result).Should().BeOk();
    }

    [Fact]
    public async Task T_Async_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        var res = await Result.Try((Func<Task<string>>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        });
        res.Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public async Task T_Async_wizHandle_引数1がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<string>>)null!, e => new TestError());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T_Async_wizHandle_引数2がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try(() => Task.FromResult("success"), null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    //---------------------------

    [Fact]
    public void Result_Sync_例外が発生しない場合は結果が返る()
    {
        Result.Try(() => Result.Ok()).Should().BeOk();
        Result.Try(() => Result.Error("Error")).Should().BeError().And.WithMessage("Error");
    }

    [Fact]
    public void Result_Sync_例外が発生した場合は失敗が返る()
        => Result.Try(() => RaiseException<Result>("Failed")).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");

    [Fact]
    public void Result_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<Result>)null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Result_Sync_wizHandle_例外が発生しない場合は成功が返る()
        => Result.Try(() => Result.Ok(), e => new TestError()).Should().BeOk();

    [Fact]
    public void Result_Sync_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        Result.Try((Func<Result>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        }).Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public void Result_Sync_withHande_引数1がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<Result>)null!, e => new TestError());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Result_Sync_withHande_引数2がない場合は例外は発生する()
    {
        Action act = () => Result.Try(() => Result.Ok(), null!);
        act.Should().Throw<ArgumentNullException>();
    }

    //--------------------------------

    [Fact]
    public async Task Result_Async_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => Result.Ok()));
        (await result.ConfigureAwait(false)).Should().BeOk();
    }

    [Fact]
    public async Task Result_Async_例外が発生した場合は失敗が返る()
    {
        var result = Result.Try(() => Task.Run(() => RaiseException<Result>("Failed")));
        (await result.ConfigureAwait(false)).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task Result_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<Result>>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Result_Async_withHandle_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => Result.Ok()), e => new TestError());
        (await result).Should().BeOk();
    }

    [Fact]
    public async Task Result_Async_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        var res = await Result.Try((Func<Task<Result>>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        });
        res.Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public async Task Result_Async_wizHandle_引数1がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<Result>>)null!, e => new TestError());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Result_Async_wizHandle_引数2がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try(() => Result.Ok().AsTask(), null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    //-------------------------------

    [Fact]
    public void ResultT_Sync_例外が発生しない場合は結果が返る()
    {
        Result.Try(() => Result.Ok("Good")).Should().BeOk().And.Match(v => v == "Good");
        Result.Try(() => Result.Error<string>("Error")).Should().BeError().And.WithMessage("Error");
    }

    [Fact]
    public void ResultT_Sync_例外が発生した場合は失敗が返る()
        => Result.Try(() => RaiseException<Result<string>>("Failed")).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");

    [Fact]
    public void ResultT_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<Result<string>>)null!);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ResultT_Sync_wizHandle_例外が発生しない場合は成功が返る()
        => Result.Try(() => Result.Ok("Good"), e => new TestError()).Should().BeOk().And.Match((v => v == "Good"));

    [Fact]
    public void ResultT_Sync_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        Result.Try((Func<Result<string>>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        }).Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public void ResultT_Sync_withHande_引数1がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<Result<string>>)null!, e => new TestError());
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ResultT_Sync_withHande_引数2がない場合は例外は発生する()
    {
        Action act = () => Result.Try(() => Result.Ok("Good"), null!);
        act.Should().Throw<ArgumentNullException>();
    }

    //----------------------

    [Fact]
    public async Task ResultT_Async_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => Result.Ok("Good")));
        (await result.ConfigureAwait(false)).Should().BeOk().And.Match(v => v == "Good");
    }

    [Fact]
    public async Task ResultT_Async_例外が発生した場合は失敗が返る()
    {
        var result = Result.Try(() => Task.Run(() => RaiseException<Result<string>>("Failed")));
        (await result.ConfigureAwait(false)).Should().BeError().And.BeOfType<ExceptionalError>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task ResultT_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<Result<string>>>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResultT_Async_withHandle_例外が発生しない場合は成功が返る()
    {
        var result = Result.Try(() => Task.Run(() => Result.Ok("Good")), e => new TestError());
        (await result).Should().BeOk();
    }

    [Fact]
    public async Task ResultT_Async_wizHandle_例外が発生した場合は例外ハンドラが実行されそこで返されたエラーが返る()
    {
        var ex = new Exception();
        var er = new TestError();
        var res = await Result.Try((Func<Task<Result<string>>>)(() => throw ex), e =>
        {
            e.Should().Be(ex);
            return er;
        });
        res.Should().BeError().And.Match(e => e == er);
    }

    [Fact]
    public async Task ResultT_Async_wizHandle_引数1がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<Result<string>>>)null!, e => new TestError());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResultT_Async_wizHandle_引数2がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try(() => Result.Ok("Good").AsTask(), null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    //-----------------------------

    private void RaiseException(string message)
        => throw new Exception(message);

    private T RaiseException<T>(string message)
        => throw new Exception(message);

    private class TestError : IError
    {
        public string Message => "";
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Try
{
    [Fact]
    public void NoReturn_Sync_例外が発生しない場合は成功が返る()
        => Result.Try(() => { }).Should().BeOk();

    [Fact]
    public void NoReturn_Sync_例外が発生した場合は失敗が返る()
        => Result.Try(() => RaiseException("Failed")).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");

    [Fact]
    public void NoReturn_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Action)null!);
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
        (await result.ConfigureAwait(false)).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task NoReturn_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    //---------------------

    [Fact]
    public void T_Sync_例外が発生しない場合は成功が返る()
        => Result.Try(() => "Good").Should().BeOk().And.Match(v => v == "Good");

    [Fact]
    public void T_Sync_例外が発生した場合は失敗が返る()
        => Result.Try(() => RaiseException<string>("Failed")).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");

    [Fact]
    public void T_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<string>)null!);
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
        (await result).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task T_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<string>>)null!);
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
        => Result.Try(() => RaiseException<Result>("Failed")).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");

    [Fact]
    public void Result_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<Result>)null!);
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
        (await result.ConfigureAwait(false)).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task Result_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<Result>>)null!);
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
        => Result.Try(() => RaiseException<Result<string>>("Failed")).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");

    [Fact]
    public void ResultT_Sync_引数がない場合は例外は発生する()
    {
        Action act = () => Result.Try((Func<Result<string>>)null!);
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
        (await result.ConfigureAwait(false)).Should().BeError().And.BeOfType<Exception>().And.WithMessage("Failed");
    }

    [Fact]
    public async Task ResultT_Async_引数がない場合は例外は発生する()
    {
        Func<Task> act = () => Result.Try((Func<Task<Result<string>>>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    //-----------------------------

    private void RaiseException(string message)
        => throw new Exception(message);

    private T RaiseException<T>(string message)
        => throw new Exception(message);
}

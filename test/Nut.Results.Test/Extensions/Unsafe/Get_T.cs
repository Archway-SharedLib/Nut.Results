using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Get_T
{
    [Fact]
    public void 値を取得できる()
    {
        Result.Ok("OK").Get().Should().Be("OK");
    }

    [Fact]
    public async Task Async_値を取得できる()
    {
        var res = await Result.Ok("OK").AsTask().Get();
        res.Should().Be("OK");
    }

    [Fact]
    public void 失敗の場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Exception()).Get();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void 失敗の場合の例外にはもとの例外が含まれる()
    {
        var message = "Source Exception";
        var ex = new Exception(message);
        var act = () => Result.Error<string>(ex).Get();
        act.Should().Throw<InvalidOperationException>().WithInnerException<Exception>().WithMessage(message);
    }

    [Fact]
    public async Task Async_失敗の場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Exception()).AsTask().Get();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Async_失敗の場合の例外にはもとの例外が含まれる()
    {
        var message = "Source Exception";
        var ex = new Exception(message);
        var act = () => Result.Error<string>(ex).AsTask().Get();
        var exAssertion = await act.Should().ThrowAsync<InvalidOperationException>();
        exAssertion.WithInnerException<Exception>().WithMessage(message);
    }

    [Fact]
    public async Task Async_引数がnullの場合は例外が発生する()
    {
        var act = () => ResultUnsafeExtensions.Get((Task<Result<string>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

}

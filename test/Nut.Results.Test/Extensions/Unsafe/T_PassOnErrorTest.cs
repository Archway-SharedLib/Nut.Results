using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test;

public class T_PassOnErrorTest
{
    [Fact]
    public void TResult_成功の場合は例外が発生する()
    {
        Action act = () => Result.Ok(123).PassOnError<int, string>();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void TResult_エラーの値が引き継がれる()
    {
        var expect = new Error();
        Result.Error<int>(expect).PassOnError<int, string>().Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task TResult_Async_成功の場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok(123).AsTask().PassOnError<int, string>();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task TResult_Async_エラーの値が引き継がれる()
    {
        var expect = new Error();
        var error = await Result.Error<int>(expect).AsTask().PassOnError<int, string>();
        error.Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task TResult_Async_引数がnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultUnsafeExtensions.PassOnError<int, string>(null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public void 成功の場合は例外が発生する()
    {
        Action act = () => Result.Ok(123).PassOnError();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void エラーの値が引き継がれる()
    {
        var expect = new Error();
        Result.Error<int>(expect).PassOnError().Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task Async_成功の場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok(123).AsTask().PassOnError();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Async_エラーの値が引き継がれる()
    {
        var expect = new Error();
        var error = await Result.Error<int>(expect).AsTask().PassOnError();
        error.Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task Async_引数がnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultUnsafeExtensions.PassOnError((Task<Result<int>>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}

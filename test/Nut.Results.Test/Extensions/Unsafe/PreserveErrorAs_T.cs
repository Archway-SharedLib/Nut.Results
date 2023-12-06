using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class PreserveErrorAs_T_Ext
{
    [Fact]
    public async Task TResult_Async_成功の場合は例外が発生する()
    {
        var act = () => Result.Ok(123).AsTask().PreserveErrorAs<int, string>();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task TResult_Async_エラーの値が引き継がれる()
    {
        var expect = new Exception();
        var error = await Result.Error<int>(expect).AsTask().PreserveErrorAs<int, string>();
        error.Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task TResult_Async_引数がnullの場合は例外が発生する()
    {
        var act = () => ResultUnsafeExtensions.PreserveErrorAs<int, string>(null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_成功の場合は例外が発生する()
    {
        var act = () => Result.Ok(123).AsTask().PreserveErrorAs();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Async_エラーの値が引き継がれる()
    {
        var expect = new Exception();
        var error = await Result.Error<int>(expect).AsTask().PreserveErrorAs();
        error.Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task Async_引数がnullの場合は例外が発生する()
    {
        var act = () => ResultUnsafeExtensions.PreserveErrorAs((Task<Result<int>>)null!);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}

using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class PreserveErrorAs_Ext
{

    [Fact]
    public async Task Async_成功の場合は例外が発生する()
    {
        var act = () => Result.Ok().AsTask().PreserveErrorAs<string>();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Async_エラーの値が引き継がれる()
    {
        var expect = new Exception();
        var error = await Result.Error(expect).AsTask().PreserveErrorAs<string>();
        error.Should().BeError().And.Match(a => a == expect);
    }

    [Fact]
    public async Task Async_引数がnullの場合は例外が発生する()
    {
        var act = () => ResultUnsafeExtensions.PreserveErrorAs<string>((Task<Result>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }
}

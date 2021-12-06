using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class PassOnError_VT
{
    [Fact]
    public async Task Async_成功の場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().PassOnError<string>().AsTask();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task Async_エラーの値が引き継がれる()
    {
        var expect = new Error();
        var error = await Result.Error(expect).AsValueTask().PassOnError<string>();
        error.Should().BeError().And.Match(a => a == expect);
    }
}

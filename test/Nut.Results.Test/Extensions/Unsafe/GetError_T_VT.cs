using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class GetError_T_VT
{
    [Fact]
    public async Task Async_エラーの値を取得できる()
    {
        var expect = new Error();
        var error = await Result.Error<string>(expect).AsValueTask().GetError();
        error.Should().Be(expect);
    }

    [Fact]
    public async Task Async_成功の場合は例外が発生する()
    {
        var act = () => Result.Ok("ok").AsValueTask().GetError().AsTask();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}

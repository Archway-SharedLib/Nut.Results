using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;


namespace Nut.Results.Test;

public class VT_T_GetTest
{
    [Fact]
    public async Task Async_値を取得できる()
    {
        var res = await Result.Ok("OK").AsValueTask().Get();
        res.Should().Be("OK");
    }

    [Fact]
    public async Task Async_失敗の場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new Error()).AsValueTask().Get().AsTask();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }
}

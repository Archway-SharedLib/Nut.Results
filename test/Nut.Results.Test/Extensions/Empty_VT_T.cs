using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Empty_VT_T
{
    [Fact]
    public async Task Async_Okの場合は空のResultが返る()
    {
        var result = await Result.Ok("Hello").AsValueTask().Empty().ConfigureAwait(false);
        result.Should().BeOk();
    }

    [Fact]
    public async Task Async_Errorの場合は同じErrorが返る()
    {
        var result = await Result.Error<string>(new Error("Error")).AsValueTask().Empty().ConfigureAwait(false);
        result.Should().BeError()
            .And.Match(e => e.Message == "Error");
    }
}

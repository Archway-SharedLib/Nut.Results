using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Contains_VT_T
{

    [Fact]
    public async Task Async_Contains_値が一致する場合はtrueが返る()
    {
        (await Result.Ok("ABCDE").AsValueTask().Contains("ABCDE")).Should().BeTrue();
    }

    [Fact]
    public async Task Async_Contains_値が一致しない場合はfalseが返る()
    {
        (await Result.Ok("ABCDE").AsValueTask().Contains("12345")).Should().BeFalse();
    }

    [Fact]
    public async Task Async_Contains_失敗の場合はfalseが返る()
    {
        (await Result.Error<string>("Error").AsValueTask().Contains("Error"))
            .Should().BeFalse();
    }

    //----------------

    [Fact]
    public async Task Async_Eq_Contains_値が一致する場合はtrueが返る()
    {
        (await Result.Ok("ABCDE").AsValueTask()
            .Contains("abcde", StringComparer.InvariantCultureIgnoreCase))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Eq_Contains_値が一致しない場合はfalseが返る()
    {
        (await Result.Ok("ABCDE").AsValueTask()
            .Contains("abcde", StringComparer.InvariantCulture))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_Contains_失敗の場合はfalseが返る()
    {
        (await Result.Error<string>("Error").AsValueTask()
            .Contains("Error", StringComparer.Ordinal))
            .Should().BeFalse();
    }
}

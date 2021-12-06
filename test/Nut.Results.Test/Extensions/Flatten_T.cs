using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Flatten_T
{
    [Fact]
    public void WithT_成功の場合は値のResultが返る()
    {
        var source = Result.Ok(Result.Ok("Good"));
        source.Flatten().Should().BeOk().And.Match(v => v == "Good");
    }

    [Fact]
    public void WithT_成功の場合で中の値が失敗の場合は失敗が返る()
    {
        var source = Result.Ok(Result.Error<string>("NG"));
        source.Flatten().Should().BeError().And.Match(e => e.Message == "NG");
    }

    [Fact]
    public void WithT_失敗の場合は失敗が返る()
    {
        var source = Result.Error<Result<string>>(new Error("NG"));
        source.Flatten().Should().BeError().And.Match(e => e.Message == "NG");
    }

    [Fact]
    public void Plain_成功の場合は値のResultが返る()
    {
        var source = Result.Ok(Result.Ok());
        source.Flatten().Should().BeOk();
    }

    [Fact]
    public void Plain_成功の場合で中の値が失敗の場合は失敗が返る()
    {
        var source = Result.Ok(Result.Error("NG"));
        source.Flatten().Should().BeError().And.Match(e => e.Message == "NG");
    }

    [Fact]
    public void Plain_失敗の場合は失敗が返る()
    {
        var source = Result.Error<Result>(new Error("NG"));
        source.Flatten().Should().BeError().And.Match(e => e.Message == "NG");
    }
}

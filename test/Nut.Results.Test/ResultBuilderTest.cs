using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;
using System.Threading.Tasks;
using System;

namespace Nut.Results.Test;

public class ResultBuilderTest
{
    [Fact]
    public void Ok_成功の結果が返る()
    {
        var r = Result.Ok();
        r.IsOk.Should().BeTrue();
        r.IsError.Should().BeFalse();
    }

    [Fact]
    public void Error_失敗の結果が返る()
    {
        var r = Result.Error(new Error("message"));
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void Error_メッセージで失敗の結果が返る()
    {
        var r = Result.Error("message");
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
        r.Should().BeError().And.Match(e => e is Error).And.Match(e => e.Message == "message");
    }

    [Fact]
    public void T_Ok_成功の結果が返る()
    {
        var r = Result.Ok("Ok Message");
        r.IsOk.Should().BeTrue();
        r.IsError.Should().BeFalse();
    }

    [Fact]
    public void T_Error_失敗の結果が返る()
    {
        var r = Result.Error<string>(new Error("message"));
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void T_Error_メッセージで失敗の結果が返る()
    {
        var r = Result.Error<string>("message");
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
        r.Should().BeError().And.Match(e => e is Error).And.Match(e => e.Message == "message");
    }
}

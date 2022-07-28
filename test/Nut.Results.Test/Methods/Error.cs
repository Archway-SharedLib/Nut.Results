using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Error
{
    [Fact]
    public void Error_失敗の結果が返る()
    {
        var r = Result.Error(new Exception("message"));
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void Error_メッセージで失敗の結果が返る()
    {
        var r = Result.Error("message");
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
        r.Should().BeError().And.Match(e => e is Exception).And.WithMessage("message");
    }

    [Fact]
    public void T_Error_失敗の結果が返る()
    {
        var r = Result.Error<string>(new Exception("message"));
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void T_Error_メッセージで失敗の結果が返る()
    {
        var r = Result.Error<string>("message");
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
        r.Should().BeError().And.Match(e => e is Exception).And.WithMessage("message");
    }
}

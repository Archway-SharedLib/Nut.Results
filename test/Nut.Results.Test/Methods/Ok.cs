using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Ok
{
    [Fact]
    public void Ok_成功の結果が返る()
    {
        var r = Result.Ok();
        r.IsOk.Should().BeTrue();
        r.IsError.Should().BeFalse();
    }

    [Fact]
    public void T_Ok_成功の結果が返る()
    {
        var r = Result.Ok("Ok Message");
        r.IsOk.Should().BeTrue();
        r.IsError.Should().BeFalse();
    }
}

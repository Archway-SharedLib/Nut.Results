using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace Nut.Results.FluentAssertions.Test;

public class ResultTOkAssertions
{
    [Fact]
    public void Match_trueが返ってきたら成功する()
    {
        Result.Ok("ok").Should().BeOk().And.Match(_ => true);
    }

    [Fact]
    public void Match_falseが返ってきたら成功する()
    {
        Action act = () => Result.Ok("ok").Should().BeOk().And.Match(_ => false);
        act.Should().Throw<XunitException>();
    }
}

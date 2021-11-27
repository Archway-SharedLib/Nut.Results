using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nut.Results.FluentAssertions.Test;

public class ResultAssertionsExtensionsTest
{
    [Fact]
    public void Should_Assertinonのインスタンスを取得できる()
    {
        var instantce = ResultAssertionsExtensions.Should(Result.Ok());
        instantce.Should().NotBeNull();
    }

    [Fact]
    public void Should_AssertinonTのインスタンスを取得できる()
    {
        var instantce = ResultAssertionsExtensions.Should(Result.Ok("ok"));
        instantce.Should().NotBeNull();
    }
}

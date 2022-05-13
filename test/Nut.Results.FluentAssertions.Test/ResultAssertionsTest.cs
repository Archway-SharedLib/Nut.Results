using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using Xunit;
using Xunit.Sdk;

namespace Nut.Results.FluentAssertions.Test;

public class ResultAssertionsTest
{
    [Fact]
    public void Be_値が一致する場合は成功する()
    {
        Result.Ok().Should().Be(Result.Ok());
    }

    [Fact]
    public void Be_値が一致しない場合は失敗する()
    {
        Action act = () => Result.Ok().Should().Be(Result.Error(new Exception()));
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void BeOk_Okの場合は成功する()
    {
        Result.Ok().Should().BeOk();
    }

    [Fact]
    public void BeOk_Errorの場合は失敗する()
    {
        Action act = () => Result.Error(new Exception()).Should().BeOk();
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void BeOk_Errorの場合は失敗の内容が出力される()
    {
        Action act = () => Result.Error(new Exception("Raise Error")).Should().BeOk();
        act.Should().Throw<XunitException>().And.Message.Should().Be("Expected ok, but error.\nType: Exception\nMessage: Raise Error");
    }

    [Fact]
    public void BeError_Errorの場合は成功する()
    {
        Result.Error(new Exception()).Should().BeError();
    }

    [Fact]
    public void BeError_Okの場合は失敗する()
    {
        Action act = () => Result.Ok().Should().BeError();
        act.Should().Throw<XunitException>();
    }
}

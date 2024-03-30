using System;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ResultHelperAccessorTest
{
    [Fact]
    public void GetValue_参照型の値が取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<string>));
        var value = accessor.GetValue(Result.Ok("value"));
        value.Should().Be("value");
    }

    [Fact]
    public void GetValue_値型の値が取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<int>));
        var value = accessor.GetValue(Result.Ok(2));
        value.Should().Be(2);
    }

    [Fact]
    public void GetErrorValue_参照型の場合にエラーが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<string>));
        var expect = new Exception();
        var value = accessor.GetErrorValue(Result.Error<string>(expect));
        value.Should().Be(expect);
    }

    [Fact]
    public void GetErrorValue_値型の場合にエラーが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<int>));
        var expect = new Exception();
        var value = accessor.GetErrorValue(Result.Error<int>(expect));
        value.Should().Be(expect);
    }

    [Fact]
    public void GetIsOk_参照型でOkの場合にtrueが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<string>));
        var expect = new Exception();
        var value = accessor.GetIsOk(Result.Ok("value"));
        value.Should().BeTrue();
    }

    [Fact]
    public void GetIsOk_値型でOkの場合にtrueが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<int>));
        var expect = new Exception();
        var value = accessor.GetIsOk(Result.Ok(2));
        value.Should().BeTrue();
    }

    [Fact]
    public void GetIsOk_参照型でErrorの場合にfalseが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<string>));
        var expect = new Exception();
        var value = accessor.GetIsOk(Result.Error<string>(new Exception()));
        value.Should().BeFalse();
    }

    [Fact]
    public void GetIsOk_値型でErrorの場合にfalseが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<int>));
        var expect = new Exception();
        var value = accessor.GetIsOk(Result.Error<int>(new Exception()));
        value.Should().BeFalse();
    }

    [Fact]
    public void GetIsError_参照型でOkの場合にfalseが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<string>));
        var expect = new Exception();
        var value = accessor.GetIsError(Result.Ok("value"));
        value.Should().BeFalse();
    }

    [Fact]
    public void GetIsError_値型でOkの場合にfalseが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<int>));
        var expect = new Exception();
        var value = accessor.GetIsError(Result.Ok(2));
        value.Should().BeFalse();
    }

    [Fact]
    public void GetIsError_参照型でErrorの場合にtrueが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<string>));
        var expect = new Exception();
        var value = accessor.GetIsError(Result.Error<string>(new Exception()));
        value.Should().BeTrue();
    }

    [Fact]
    public void GetIsError_値型でErrorの場合にtrueが取得できる()
    {
        var accessor = new ResultHelper.Accessor(typeof(Result<int>));
        var expect = new Exception();
        var value = accessor.GetIsError(Result.Error<int>(new Exception()));
        value.Should().BeTrue();
    }
}

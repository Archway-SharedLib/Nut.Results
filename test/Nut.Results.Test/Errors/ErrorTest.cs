using Nut.Results;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nut.Results.Test.Errors;

public class ErrorTest
{
    [Fact]
    public void ctor_デフォルトコンストラクタではデフォルトメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var error = new Error();
        error.Message.Should().Be("An error has occured.");
    }

    [Fact]
    public void ctor_メッセージパラメーターにnullを渡したらデフォルトメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var error = new Error(null);
        error.Message.Should().Be("An error has occured.");
    }

    [Fact]
    public void ctor_コンストラクタで引き渡したメッセージがプロパティから取得できる()
    {
        var message = DateTime.Now.ToLongDateString();
        var error = new Error(message);
        error.Message.Should().Be(message);
    }

    [Fact]
    public void ToString_メッセージと同じ値が返る()
    {
        var message = DateTime.Now.ToLongDateString();
        var error = new Error(message);
        error.ToString().Should().Be(error.Message);
    }
}

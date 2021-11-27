using FluentAssertions;
using System;
using System.Runtime.InteropServices;
using Xunit;

namespace Nut.Results.Test.Errors;

public class ExceptionalErrorTest
{
    [Fact]
    public void ctor_例外のパラメーターがnullの場合は例外が発生する()
    {
        Action act = () => new ExceptionalError(null);
        act.Should().Throw<ArgumentNullException>();

        Action act2 = () => new ExceptionalError(null, "message");
        act2.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ctor_例外のメッセージがnullの場合はデフォルトメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var er = new ExceptionalError(new TestException());
        er.Message.Should().Be("An error has occured.");
    }

    private class TestException : Exception
    {
        public override string Message => null;
    }

    [Fact]
    public void ctor_メッセージのパラメーターがnullの場合はデフォルトメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var er = new ExceptionalError(new Exception(), null);
        er.Message.Should().Be("An error has occured.");
    }

    [Fact]
    public void Exception_コンストラクタで設定した例外がプロパティで取得できる()
    {
        var ex = new Exception();
        var er = new ExceptionalError(ex);
        er.Exception.Should().BeSameAs(ex);
    }

    [Fact]
    public void Message_コンストラクタで設定したメッセージがプロパティで取得できる()
    {
        var ex = new Exception();
        var message = "Hello";
        var er = new ExceptionalError(ex, message);
        er.Message.Should().Be(message);
    }

    [Fact]
    public void Message_コンストラクタでメッセージを指定しない例外のメッセージが設定される()
    {
        var ex = new Exception("Raise Exception");
        var message = ex.Message;
        var er = new ExceptionalError(ex);
        er.Message.Should().Be(message);
    }

    [Fact]
    public void ToException_設定した例外が返される()
    {
        var ex = new ArgumentException();
        var er = new ExceptionalError(ex) as IError;
        er.ToException().Should().BeSameAs(ex);

        var ex2 = new InvalidOperationException();
        var er2 = new ExceptionalError(ex2) as IError;
        er2.ToException().Should().BeSameAs(ex2);
    }
}

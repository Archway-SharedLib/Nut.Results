using System;
using System.Collections.Generic;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using Xunit;
using Xunit.Sdk;

namespace Nut.Results.FluentAssertions.Test;

public class ResultErrorAssertionsTest
{
    [Fact]
    public void ByOfType_エラーの型が同じ場合は成功する()
    {
        Result.Error(new TestError()).Should().BeError().And.BeOfType(typeof(TestError));
    }

    [Fact]
    public void ByOfType_エラーの型が違う場合は失敗する()
    {
        Action act = () => Result.Error(new TestError()).Should().BeError().And.BeOfType(typeof(TestError2));
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void ByOfType_T_エラーの型が同じ場合は成功する()
    {
        Result.Error(new TestError()).Should().BeError().And.BeOfType<TestError>();
    }

    [Fact]
    public void ByOfType_T_エラーの型が違う場合は失敗する()
    {
        Action act = () => Result.Error(new TestError()).Should().BeError().And.BeOfType<TestError2>();
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void WithMessage_メッセージが同じなら成功する()
    {
        Result.Error(new Error("Foo")).Should().BeError().And.WithMessage("Foo");
    }

    [Fact]
    public void WithMessage_メッセージが違えば失敗する()
    {
        Action act = () => Result.Error(new Error("Foo")).Should().BeError().And.WithMessage("Bar"); ;
        act.Should().Throw<XunitException>();
    }

    [Fact]
    public void Match_trueが返ってきたら成功する()
    {
        Result.Error(new Error("Foo")).Should().BeError().And.Match(_ => true);
    }

    [Fact]
    public void Match_falseが返ってきたら成功する()
    {
        Action act = () => Result.Error(new Error("Foo")).Should().BeError().And.Match(_ => false);
        act.Should().Throw<XunitException>();
    }

    private class TestError : Error { }

    private class TestError2 : Error { }
}

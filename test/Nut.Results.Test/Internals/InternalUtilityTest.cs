using FluentAssertions;
using Nut.Results.Internals;
using Xunit;

namespace Nut.Results.Test.Internals;

public class InternalUtilityTest
{
    [Fact]
    public void CheckReturnValueNotNull_指定された値がnullでない場合はそのままの値が返る()
    {
        InternalUtility.CheckReturnValueNotNull("Foo").Should().Be("Foo");
    }

    [Fact]
    public void CheckReturnValueNotNull_指定された値がnullの場合は例外が発生する()
    {
        var act = () => InternalUtility.CheckReturnValueNotNull<string>(null);
        act.Should().Throw<InvalidReturnValueException>();
    }

    [Fact]
    public void RaizeReturnValueNotNull_例外が発生する()
    {
        var act = () => InternalUtility.RaizeReturnValueNotNull();
        act.Should().Throw<InvalidReturnValueException>();
    }
}

using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class UnitTest
{
    [Fact]
    public void Unitの比較()
    {
        var unit = Unit.Default;
        var unit2 = Unit.Default;
        unit.Equals(unit2).Should().BeTrue();
        unit.Equals(null).Should().BeFalse();
        unit.Equals(new object()).Should().BeFalse();
        unit.CompareTo(unit2).Should().Be(0);
        unit.CompareTo(null).Should().Be(1);
        unit.CompareTo((object)unit2).Should().Be(0);
        unit.CompareTo(new object()).Should().Be(1);
        unit.GetHashCode().Should().Be(0);
        unit.ToString().Should().Be("()");
        (unit == unit2).Should().BeTrue();
        (unit != unit2).Should().BeFalse();
    }

    [Fact]
    public void Unitの比較_静的メソッド()
    {
        var unit = Unit.Default;
        var unit2 = Unit.Default;
        Unit.Equals(unit, unit2).Should().BeTrue();
        Unit.Equals(unit, (object)unit2).Should().BeTrue();
        Unit.Equals(unit, null).Should().BeFalse();
        Unit.Equals(unit, new object()).Should().BeFalse();
    }

    [Fact]
    public void newしたUnitの比較()
    {
        var unit = new Unit();
        var unit2 = new Unit();
        unit.Equals(unit2).Should().BeTrue();
        unit.Equals(null).Should().BeFalse();
        unit.Equals(new object()).Should().BeFalse();
        unit.CompareTo(unit2).Should().Be(0);
        unit.CompareTo((object)unit2).Should().Be(0);
        unit.CompareTo(null).Should().Be(1);
        unit.CompareTo(new object()).Should().Be(1);
        unit.GetHashCode().Should().Be(0);
        unit.ToString().Should().Be("()");
        (unit == unit2).Should().BeTrue();
        (unit != unit2).Should().BeFalse();
    }

    [Fact]
    public void newしたUnitの比較_静的メソッド()
    {
        var unit = new Unit();
        var unit2 = new Unit();
        Unit.Equals(unit, unit2).Should().BeTrue();
        Unit.Equals(unit, (object)unit2).Should().BeTrue();
        Unit.Equals(unit, null).Should().BeFalse();
        Unit.Equals(unit, new object()).Should().BeFalse();
    }

    [Fact]
    public void defaultとの比較()
    {
        var unit = Unit.Default;
        unit.Equals(default).Should().BeTrue();
        unit.Equals(default(Unit)).Should().BeTrue();
        unit.Equals(default(object)).Should().BeFalse();
        unit.CompareTo(default).Should().Be(0);
        unit.CompareTo((object)default).Should().Be(1);
        unit.CompareTo(default(Unit)).Should().Be(0);
        unit.CompareTo(default(object)).Should().Be(1);
        (unit == default).Should().BeTrue();
        (unit != default).Should().BeFalse();
    }
}

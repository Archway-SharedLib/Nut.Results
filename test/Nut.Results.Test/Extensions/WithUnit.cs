using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test.Extensions;
public class WithUnit
{
    [Fact]
    public void Unitに変換できる()
    {
        var ok = Result.Ok().WithUnit();
        ok.Should().BeOk();
        ok.Get().Should().Be(Unit.Default);
    }

    [Fact]
    public async Task Async_Unitに変換できる()
    {
        var ok = await Result.Ok().AsTask().WithUnit();
        ok.Should().BeOk();
        ok.Get().Should().Be(Unit.Default);
    }

    [Fact]
    public void 失敗の場合は失敗のまま()
    {
        var error = Result.Error(new Exception()).WithUnit();
        error.Should().BeError();
    }

    [Fact]
    public async Task Async_失敗の場合は失敗のまま()
    {
        var error = await Result.Error(new Exception()).AsTask().WithUnit();
        error.Should().BeError();
    }
}

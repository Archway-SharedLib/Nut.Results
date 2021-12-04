using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class T_ContainsTest
{
    [Fact]
    public void Contains_値が一致する場合はtrueが返る()
    {
        Result.Ok("ABCDE").Contains("ABCDE").Should().BeTrue();
    }

    [Fact]
    public void Contains_値が一致しない場合はfalseが返る()
    {
        Result.Ok("ABCDE").Contains("12345").Should().BeFalse();
    }

    [Fact]
    public void Contains_失敗の場合はfalseが返る()
    {
        Result.Error<string>("Error").Contains("Error").Should().BeFalse();
    }

    [Fact]
    public void Eq_Contains_値が一致する場合はtrueが返る()
    {
        Result.Ok("ABCDE")
            .Contains("abcde", StringComparer.InvariantCultureIgnoreCase)
            .Should().BeTrue();
    }

    [Fact]
    public void Eq_Contains_値が一致しない場合はfalseが返る()
    {
        Result.Ok("ABCDE")
            .Contains("abcde", StringComparer.InvariantCulture)
            .Should().BeFalse();
    }

    [Fact]
    public void Eq_Contains_失敗の場合はfalseが返る()
    {
        Result.Error<string>("Error")
            .Contains("Error", StringComparer.Ordinal)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Contains_nullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Contains(source: null, "ABCD");
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Contains_値が一致する場合はtrueが返る()
    {
        (await Result.Ok("ABCDE").AsTask().Contains("ABCDE")).Should().BeTrue();
    }

    [Fact]
    public async Task Async_Contains_値が一致しない場合はfalseが返る()
    {
        (await Result.Ok("ABCDE").AsTask().Contains("12345")).Should().BeFalse();
    }

    [Fact]
    public async Task Async_Contains_失敗の場合はfalseが返る()
    {
        (await Result.Error<string>("Error").AsTask().Contains("Error"))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_Contains_nullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Contains(source: null, "ABCD", StringComparer.Ordinal);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Eq_Contains_値が一致する場合はtrueが返る()
    {
        (await Result.Ok("ABCDE").AsTask()
            .Contains("abcde", StringComparer.InvariantCultureIgnoreCase))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Eq_Contains_値が一致しない場合はfalseが返る()
    {
        (await Result.Ok("ABCDE").AsTask()
            .Contains("abcde", StringComparer.InvariantCulture))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_Contains_失敗の場合はfalseが返る()
    {
        (await Result.Error<string>("Error").AsTask()
            .Contains("Error", StringComparer.Ordinal))
            .Should().BeFalse();
    }
}

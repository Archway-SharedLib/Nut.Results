using System;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Empty_T
{
    [Fact]
    public void Sync_Okの場合は空のResultが返る()
    {
        Result.Ok("Hello").Empty().Should().BeOk();
    }

    [Fact]
    public void Sync_Errorの場合は同じErrorが返る()
    {
        Result.Error<string>(new Exception("Error")).Empty().Should().BeError()
            .And.WithMessage("Error");
    }

    [Fact]
    public async Task Async_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Empty(null as Task<Result<string>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task Async_Okの場合は空のResultが返る()
    {
        var result = await Result.Ok("Hello").AsTask().Empty().ConfigureAwait(false);
        result.Should().BeOk();
    }

    [Fact]
    public async Task Async_Errorの場合は同じErrorが返る()
    {
        var result = await Result.Error<string>(new Exception("Error")).AsTask().Empty().ConfigureAwait(false);
        result.Should().BeError()
            .And.WithMessage("Error");
    }
}

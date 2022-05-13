using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ContainsError
{
    [Fact]
    public void 値が一致する場合はtrueが返る()
    {
        var error = new Exception();
        Result.Error(error).ContainsError(error).Should().BeTrue();
    }

    [Fact]
    public void 値が一致しない場合はfalseが返る()
    {
        var error = new Exception();
        Result.Error(error).ContainsError(new Exception()).Should().BeFalse();
    }

    [Fact]
    public void 成功の場合はfalseが返る()
    {
        Result.Ok().ContainsError(new Exception()).Should().BeFalse();
    }

    [Fact]
    public void Eq_値が一致する場合はtrueが返る()
    {
        var error = new Exception();

        Result.Error(error)
            .ContainsError(error, EqualityComparer<Exception>.Default)
            .Should().BeTrue();
    }

    [Fact]
    public void Eq_値が一致しない場合はfalseが返る()
    {
        var error = new Exception();

        Result.Error(error)
            .ContainsError(new Exception("Foo"), EqualityComparer<Exception>.Default)
            .Should().BeFalse();
    }

    [Fact]
    public void Eq_成功の場合はfalseが返る()
    {
        Result.Ok()
            .ContainsError(new Exception(), EqualityComparer<Exception>.Default)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_nullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.ContainsError(
            source: null as Task<Result>,
            new Exception());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_値が一致する場合はtrueが返る()
    {
        var err = new Exception();
        (await Result.Error(err).AsTask().ContainsError(err)).Should().BeTrue();
    }

    [Fact]
    public async Task Async_値が一致しない場合はfalseが返る()
    {
        var err = new Exception();
        (await Result.Error(err).AsTask().ContainsError(new Exception()))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_成功の場合はfalseが返る()
    {
        (await Result.Ok().AsTask().ContainsError(new Exception()))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_nullの場合は例外が発生する()
    {
        Func<Task> act = () =>
            ResultExtensions.ContainsError(
                source: null as Task<Result>,
                new Exception(),
                EqualityComparer<Exception>.Default);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Eq_値が一致する場合はtrueが返る()
    {
        var err = new Exception();
        (await Result.Error(err).AsTask()
            .ContainsError(err, EqualityComparer<Exception>.Default))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Eq_値が一致しない場合はfalseが返る()
    {
        var err = new Exception();
        (await Result.Error(err).AsTask()
                .ContainsError(new Exception(), EqualityComparer<Exception>.Default))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_成功の場合はfalseが返る()
    {
        (await Result.Ok().AsTask()
            .ContainsError(new Exception(), EqualityComparer<Exception>.Default))
            .Should().BeFalse();
    }
}

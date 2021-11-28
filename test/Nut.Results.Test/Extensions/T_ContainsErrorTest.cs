using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class T_ContainsErrorTest
{
    [Fact]
    public void 値が一致する場合はtrueが返る()
    {
        var error = new Error();
        Result.Error<string>(error).ContainsError(error).Should().BeTrue();
    }

    [Fact]
    public void 値が一致しない場合はfalseが返る()
    {
        var error = new Error();
        Result.Error<string>(error).ContainsError(new Error()).Should().BeFalse();
    }

    [Fact]
    public void 成功の場合はfalseが返る()
    {
        Result.Ok("Error").ContainsError(new Error()).Should().BeFalse();
    }

    [Fact]
    public void Eq_値が一致する場合はtrueが返る()
    {
        var error = new Error();

        Result.Error<string>(error)
            .ContainsError(error, EqualityComparer<IError>.Default)
            .Should().BeTrue();
    }

    [Fact]
    public void Eq_値が一致しない場合はfalseが返る()
    {
        var error = new Error();

        Result.Error<string>(error)
            .ContainsError(new Error("Foo"), EqualityComparer<IError>.Default)
            .Should().BeFalse();
    }

    [Fact]
    public void Eq_成功の場合はfalseが返る()
    {
        Result.Ok("Error")
            .ContainsError(new Error(), EqualityComparer<IError>.Default)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_nullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.ContainsError(
            source: null as Task<Result<string>>,
            new Error());
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_値が一致する場合はtrueが返る()
    {
        var err = new Error();
        (await Result.Error<string>(err).AsTask().ContainsError(err)).Should().BeTrue();
    }

    [Fact]
    public async Task Async_値が一致しない場合はfalseが返る()
    {
        var err = new Error();
        (await Result.Error<string>(err).AsTask().ContainsError(new Error()))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_成功の場合はfalseが返る()
    {
        (await Result.Ok("Error").AsTask().ContainsError(new Error()))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_nullの場合は例外が発生する()
    {
        Func<Task> act = () =>
            ResultExtensions.ContainsError(
                source: null as Task<Result<string>>,
                new Error(),
                EqualityComparer<IError>.Default);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Eq_値が一致する場合はtrueが返る()
    {
        var err = new Error();
        (await Result.Error<string>(err).AsTask()
            .ContainsError(err, EqualityComparer<IError>.Default))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Eq_値が一致しない場合はfalseが返る()
    {
        var err = new Error();
        (await Result.Error<string>(err).AsTask()
                .ContainsError(new Error(), EqualityComparer<IError>.Default))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_成功の場合はfalseが返る()
    {
        (await Result.Ok("Error").AsTask()
            .ContainsError(new Error(), EqualityComparer<IError>.Default))
            .Should().BeFalse();
    }
}

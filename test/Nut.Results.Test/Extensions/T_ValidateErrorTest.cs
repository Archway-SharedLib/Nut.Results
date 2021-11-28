using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class T_ValidateErrorTest
{
    [Fact]
    public void Predicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.ValidateError(
            source: Result.Error<string>(new Error()),
            predicate: (Func<IError, bool>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Predicateの引数には値が渡される()
    {
        var err = new Error();
        Result.Error<string>(err).ValidateError(e =>
        {
            e.Should().Be(err);
            return true;
        });
    }

    [Fact]
    public void Predicateがtrueを返せばtrueが返る()
    {
        Result.Error<string>(new Error())
            .ValidateError(v => true).Should().BeTrue();
    }

    [Fact]
    public void Predicateがfalseを返せばfalseが返る()
    {
        Result.Error<string>(new Error())
            .ValidateError(v => false).Should().BeFalse();
    }

    [Fact]
    public void 成功の場合はfalseが返る()
    {
        Result.Ok("Error")
            .ValidateError(_ => true)
            .Should().BeFalse();
    }

    //-------------

    [Fact]
    public async Task Async_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.ValidateError(
            source: null as Task<Result<string>>,
            predicate: _ => true);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Predicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.ValidateError(
            source: Result.Ok("A").AsTask(),
            predicate: (Func<IError, bool>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Predicateの引数には値が渡される()
    {
        var err = new Error();
        await Result.Error<string>(err).AsTask().ValidateError(e =>
        {
            e.Should().Be(err);
            return true;
        });
    }

    [Fact]
    public async Task Async_Predicateがtrueを返せばtrueが返る()
    {
        (await Result.Error<string>(new Error()).AsTask().ValidateError(v => true))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Predicateがfalseを返せばfalseが返る()
    {
        (await Result.Error<string>(new Error()).AsTask().ValidateError(v => false))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_成功の場合はfalseが返る()
    {
        (await Result.Ok("Error").AsTask()
            .ValidateError(_ => true))
            .Should().BeFalse();
    }

    //-------------

    [Fact]
    public async Task TaskPredicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.ValidateError(
            source: Result.Error<string>(new Error()),
            predicate: (Func<IError, Task<bool>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskPredicateの引数には値が渡される()
    {
        var err = new Error();
        await Result.Error<string>(err).ValidateError(e =>
        {
            e.Should().Be(err);
            return Task.FromResult(true);
        });
    }

    [Fact]
    public async Task TaskPredicateがtrueを返せばtrueが返る()
    {
        var result = await Result.Error<string>(new Error())
            .ValidateError(v => Task.FromResult(true));
        result.Should().BeTrue();
    }

    [Fact]
    public async Task TaskPredicateがfalseを返せばfalseが返る()
    {
        var result = await Result.Error<string>(new Error())
            .ValidateError(v => Task.FromResult(false));
        result.Should().BeFalse();
    }

    [Fact]
    public async Task TaskPredicate_成功の場合はfalseが返る()
    {
        var result = await Result.Ok("Error")
            .ValidateError(_ => Task.FromResult(true));
        result.Should().BeFalse();
    }

    //-------------

    [Fact]
    public async Task Async_TaskPredicateでsourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.ValidateError(
            source: null as Task<Result<string>>,
            predicate: _ => Task.FromResult(true));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_TaskPredicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.ValidateError(
            source: Result.Error<string>(new Error()).AsTask(),
            predicate: (Func<IError, Task<bool>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_TaskPredicateの引数には値が渡される()
    {
        var err = new Error();
        await Result.Error<string>(err).AsTask().ValidateError(e =>
        {
            e.Should().Be(err);
            return Task.FromResult(true);
        });
    }

    [Fact]
    public async Task Async_TaskPredicateがtrueを返せばtrueが返る()
    {
        var result = await Result.Error<string>(new Error()).AsTask()
            .ValidateError(v => Task.FromResult(true));
        result.Should().BeTrue();
    }

    [Fact]
    public async Task Async_TaskPredicateがfalseを返せばfalseが返る()
    {
        var result = await Result.Error<string>(new Error()).AsTask()
            .ValidateError(v => Task.FromResult(false));
        result.Should().BeFalse();
    }

    [Fact]
    public async Task Async_TaskPredicate_成功の場合はfalseが返る()
    {
        var result = await Result.Ok("Error").AsTask()
            .ValidateError(_ => Task.FromResult(true));
        result.Should().BeFalse();
    }
}

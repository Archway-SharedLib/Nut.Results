using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ValidateErrorTest
{
    [Fact]
    public void Predicate_Predicateがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.ValidateError(
            source: Result.Error(new Error()),
            predicate: (Func<IError, bool>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Predicate_Predicateの引数には値が渡される()
    {
        var err = new Error();
        Result.Error(err).ValidateError(e =>
        {
            e.Should().Be(err);
            return true;
        });
    }

    [Fact]
    public void Predicate_Predicateがtrueを返せばtrueが返る()
    {
        Result.Error(new Error())
            .ValidateError(v => true).Should().BeTrue();
    }

    [Fact]
    public void Predicate_Predicateがfalseを返せばfalseが返る()
    {
        Result.Error(new Error())
            .ValidateError(v => false).Should().BeFalse();
    }

    [Fact]
    public void Predicate_成功の場合はfalseが返る()
    {
        Result.Ok()
            .ValidateError(_ => true)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Predicate_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.ValidateError(
            source: null as Task<Result>,
            predicate: _ => true);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Predicate_Predicateがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.ValidateError(
            source: Result.Ok().AsTask(),
            predicate: (Func<IError, bool>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Predicate_Predicateの引数には値が渡される()
    {
        var err = new Error();
        await Result.Error(err).AsTask().ValidateError(e =>
        {
            e.Should().Be(err);
            return true;
        });
    }

    [Fact]
    public async Task Async_Predicate_Predicateがtrueを返せばtrueが返る()
    {
        (await Result.Error(new Error()).AsTask().ValidateError(v => true))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Predicate_Predicateがfalseを返せばfalseが返る()
    {
        (await Result.Error(new Error()).AsTask().ValidateError(v => false))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Predicate_成功の場合はfalseが返る()
    {
        (await Result.Ok().AsTask()
            .ValidateError(_ => true))
            .Should().BeFalse();
    }
}

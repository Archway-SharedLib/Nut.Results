using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class T_ValidateTest
{
    [Fact]
    public void Predicate_Validate_Predicateがnullの場合は例外が発生する()
    {
        Action act = () => ResultExtensions.Validate(
            source: Result.Ok("A"),
            predicate: (Func<string, bool>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Predicate_Validate_Predicateの引数には値が渡される()
    {
        Result.Ok("A").Validate(v =>
        {
            v.Should().Be("A");
            return true;
        });
    }

    [Fact]
    public void Predicate_Validate_Predicateがtrueを返せばtrueが返る()
    {
        Result.Ok("A").Validate(v => true).Should().BeTrue();
    }

    [Fact]
    public void Predicate_Validate_Predicateがfalseを返せばfalseが返る()
    {
        Result.Ok("A").Validate(v => false).Should().BeFalse();
    }

    [Fact]
    public void Predicate_Validate_失敗の場合はfalseが返る()
    {
        Result.Error<string>("Error")
            .Validate(_ => true)
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Predicate_Validate_sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Validate<string>(
            source: null,
            predicate: _ => true);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Predicate_Validate_Predicateがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Validate(
            source: Result.Ok("A").AsTask(),
            predicate: (Func<string, bool>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Predicate_Validate_Predicateの引数には値が渡される()
    {
        await Result.Ok("A").AsTask().Validate(v =>
        {
            v.Should().Be("A");
            return true;
        });
    }

    [Fact]
    public async Task Async_Predicate_Validate_Predicateがtrueを返せばtrueが返る()
    {
        (await Result.Ok("A").AsTask().Validate(v => true))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Predicate_Validate_Predicateがfalseを返せばfalseが返る()
    {
        (await Result.Ok("A").AsTask().Validate(v => false))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Predicate_Validate_失敗の場合はfalseが返る()
    {
        (await Result.Error<string>("Error").AsTask()
            .Validate(_ => true))
            .Should().BeFalse();
    }
}

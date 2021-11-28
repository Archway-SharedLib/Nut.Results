using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class T_ValidateTest
{
    [Fact]
    public void Validate_Predicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Validate(
            source: Result.Ok("A"),
            predicate: (Func<string, bool>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Validate_Predicateの引数には値が渡される()
    {
        Result.Ok("A").Validate(v =>
        {
            v.Should().Be("A");
            return true;
        });
    }

    [Fact]
    public void Validate_Predicateがtrueを返せばtrueが返る()
    {
        Result.Ok("A").Validate(v => true).Should().BeTrue();
    }

    [Fact]
    public void Validate_Predicateがfalseを返せばfalseが返る()
    {
        Result.Ok("A").Validate(v => false).Should().BeFalse();
    }

    [Fact]
    public void Validate_失敗の場合はfalseが返る()
    {
        Result.Error<string>("Error")
            .Validate(_ => true)
            .Should().BeFalse();
    }

    //--------------

    [Fact]
    public async Task Async_Validate_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Validate<string>(
            source: null,
            predicate: _ => true);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Validate_Predicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Validate(
            source: Result.Ok("A").AsTask(),
            predicate: (Func<string, bool>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Validate_Predicateの引数には値が渡される()
    {
        await Result.Ok("A").AsTask().Validate(v =>
        {
            v.Should().Be("A");
            return true;
        });
    }

    [Fact]
    public async Task Async_Validate_Predicateがtrueを返せばtrueが返る()
    {
        (await Result.Ok("A").AsTask().Validate(v => true))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Validate_Predicateがfalseを返せばfalseが返る()
    {
        (await Result.Ok("A").AsTask().Validate(v => false))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Validate_失敗の場合はfalseが返る()
    {
        (await Result.Error<string>("Error").AsTask()
            .Validate(_ => true))
            .Should().BeFalse();
    }

    //------

    [Fact]
    public async Task Validate_TaskPredicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Validate(
            source: Result.Ok("A"),
            predicate: (Func<string, Task<bool>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Validate_TaskPredicateの引数には値が渡される()
    {
        await Result.Ok("A").Validate(v =>
        {
            v.Should().Be("A");
            return Task.FromResult(true);
        });
    }

    [Fact]
    public async Task Validate_TaskPredicateがtrueを返せばtrueが返る()
    {
        var res = await Result.Ok("A").Validate(v => Task.FromResult(true));
        res.Should().BeTrue();
    }

    [Fact]
    public async Task Validate_TaskPredicateがfalseを返せばfalseが返る()
    {
        var res = await Result.Ok("A").Validate(v => Task.FromResult(false));
        res.Should().BeFalse();
    }

    [Fact]
    public async Task Validate_Task失敗の場合はfalseが返る()
    {
        var res = await Result.Error<string>("Error")
            .Validate(_ => Task.FromResult(true));
        res.Should().BeFalse();
    }

    //-------

    [Fact]
    public async Task Async_Validate_TaskPredicateがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Validate(
            source: Result.Ok("A").AsTask(),
            predicate: (Func<string, Task<bool>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Validate_TaskSourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Validate(
            source: (Task<Result<string>>)null,
            predicate: v => Task.FromResult(true));
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task Async_Validate_TaskPredicateの引数には値が渡される()
    {
        await Result.Ok("A").AsTask().Validate(v =>
        {
            v.Should().Be("A");
            return Task.FromResult(true);
        });
    }

    [Fact]
    public async Task Async_Validate_TaskPredicateがtrueを返せばtrueが返る()
    {
        var res = await Result.Ok("A").AsTask().Validate(v => Task.FromResult(true));
        res.Should().BeTrue();
    }

    [Fact]
    public async Task Async_Validate_TaskPredicateがfalseを返せばfalseが返る()
    {
        var res = await Result.Ok("A").AsTask().Validate(v => Task.FromResult(false));
        res.Should().BeFalse();
    }

    [Fact]
    public async Task Async_Validate_Task失敗の場合はfalseが返る()
    {
        var res = await Result.Error<string>("Error").AsTask()
            .Validate(_ => Task.FromResult(true));
        res.Should().BeFalse();
    }
}

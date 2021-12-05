using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class MergeTest
{
    [Fact]
    public void 引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((IEnumerable<Result>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void 全て成功の場合は成功になる()
    {
        var result = ResultExtensions.Merge(new[] { Result.Ok(), Result.Ok() });
        result.Should().BeOk();
    }

    [Fact]
    public void 失敗がある場合は失敗になる()
    {
        var result = ResultExtensions.Merge(new[] { Result.Ok(), Result.Error(new Error("1")), Result.Ok(), Result.Error(new Error("2")), Result.Ok() });
        result.Should().BeError().And.BeOfType<AggregateError>();
        var errors = result.GetError().As<AggregateError>();
        errors.Errors.Should().HaveCount(2);
    }

    [Fact]
    public async Task T_引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((IEnumerable<Task<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T_全て成功の場合は成功になる()
    {
        var result = await ResultExtensions.Merge(new[] { Result.Ok().AsTask(), Result.Ok().AsTask() });
        result.Should().BeOk();
    }

    [Fact]
    public async Task T_失敗がある場合は失敗になる()
    {
        var result = await ResultExtensions.Merge(new[] { Result.Ok().AsTask(), Result.Error(new Error("1")).AsTask(), Result.Ok().AsTask(), Result.Error(new Error("2")).AsTask(), Result.Ok().AsTask() });
        result.Should().BeError().And.BeOfType<AggregateError>();
        var errors = result.GetError().As<AggregateError>();
        errors.Errors.Should().HaveCount(2);
    }

    [Fact]
    public async Task A_引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((Task<IEnumerable<Result>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task A_引数のTaskの結果がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge(Task.FromResult<IEnumerable<Result>>(null));
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task A_全て成功の場合は成功になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] { Result.Ok(), Result.Ok() }.AsEnumerable()));
        result.Should().BeOk();
    }

    [Fact]
    public async Task A_失敗がある場合は失敗になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] { Result.Ok(), Result.Error(new Error("1")), Result.Ok(), Result.Error(new Error("2")), Result.Ok() }.AsEnumerable()));
        result.Should().BeError().And.BeOfType<AggregateError>();
        var errors = result.GetError().As<AggregateError>();
        errors.Errors.Should().HaveCount(2);
    }

    [Fact]
    public async Task TA_引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((Task<IEnumerable<Task<Result>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TA_引数のTaskの結果がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge(Task.FromResult<IEnumerable<Task<Result>>>(null));
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async Task TA_全て成功の場合は成功になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] { Result.Ok().AsTask(), Result.Ok().AsTask() }.AsEnumerable()));
        result.Should().BeOk();
    }

    [Fact]
    public async Task TA_失敗がある場合は失敗になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] { Result.Ok().AsTask(), Result.Error(new Error("1")).AsTask(), Result.Ok().AsTask(), Result.Error(new Error("2")).AsTask(), Result.Ok().AsTask() }.AsEnumerable()));
        result.Should().BeError().And.BeOfType<AggregateError>();
        var errors = result.GetError().As<AggregateError>();
        errors.Errors.Should().HaveCount(2);
    }
}

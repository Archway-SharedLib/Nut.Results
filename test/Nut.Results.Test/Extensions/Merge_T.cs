using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Merge_T
{
    [Fact]
    public void 引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((IEnumerable<Result<string>>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void 全て成功の場合は成功になる()
    {
        var result = ResultExtensions.Merge(new[] { Result.Ok("A"), Result.Ok("B") });
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public void 失敗がある場合は失敗になる()
    {
        var result = ResultExtensions.Merge(new[] {
                Result.Ok("1"),
                Result.Error<string>(new Exception("1")),
                Result.Ok("2"),
                Result.Error<string>(new Exception("2")),
                Result.Ok("3")
            });
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }

    [Fact]
    public async Task T_引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((IEnumerable<Task<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task T_全て成功の場合は成功になる()
    {
        var result = await ResultExtensions.Merge(new[] { Result.Ok("A").AsTask(), Result.Ok("B").AsTask() });
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public async Task T_失敗がある場合は失敗になる()
    {
        var result = await ResultExtensions.Merge(new[] {
                Result.Ok("1").AsTask(),
                Result.Error<string>(new Exception("1")).AsTask(),
                Result.Ok("2").AsTask(),
                Result.Error<string>(new Exception("2")).AsTask(),
                Result.Ok("3").AsTask()
            });
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }

    [Fact]
    public async Task T_タスクが失敗した場合は例外を含んだ失敗になる()
    {
        var result = await ResultExtensions.Merge(new[]
        {
            Result.Ok("A").AsTask(),
            Task.Run(() => Result.Ok(""[0].ToString())) // throw exception
        });
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    [Fact]
    public async Task A_引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((Task<IEnumerable<Result<string>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task A_引数のTaskの結果がnullの場合は例外が発生するべき()
    {
        var result = await ResultExtensions.Merge(Task.FromResult<IEnumerable<Result<string>>>(null));
        result.Should().BeError().And.BeOfType<InvalidOperationException>();
    }

    [Fact]
    public async Task A_全て成功の場合は成功になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] { Result.Ok("A"), Result.Ok("B") }.AsEnumerable()));
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public async Task A_失敗がある場合は失敗になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] {
                Result.Ok("1"),
                Result.Error<string>(new Exception("1")),
                Result.Ok("2"),
                Result.Error<string>(new Exception("2")),
                Result.Ok("3")
            }.AsEnumerable()));
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }

    [Fact]
    public async Task TA_引数がnullの場合は例外が発生するべき()
    {
        var act = () => ResultExtensions.Merge((Task<IEnumerable<Task<Result<string>>>>)null);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TA_引数のTaskの結果がnullの場合は例外が発生するべき()
    {
        var result = await ResultExtensions.Merge(Task.FromResult<IEnumerable<Task<Result<string>>>>(null));
        result.Should().BeError().And.BeOfType<InvalidOperationException>();
    }

    [Fact]
    public async Task TA_全て成功の場合は成功になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] { Result.Ok("A").AsTask(), Result.Ok("B").AsTask() }.AsEnumerable()));
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public async Task TA_失敗がある場合は失敗になる()
    {
        var result = await ResultExtensions.Merge(Task.FromResult(new[] {
                Result.Ok("1").AsTask(),
                Result.Error<string>(new Exception("1")).AsTask(),
                Result.Ok("2").AsTask(),
                Result.Error<string>(new Exception("2")).AsTask(),
                Result.Ok("3").AsTask()
            }.AsEnumerable()));
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }
}

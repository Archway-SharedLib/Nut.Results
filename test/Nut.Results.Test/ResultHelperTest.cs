using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ResultHelperTest
{
    [Fact]
    public void IsWithValueResultType_nullならfalseが返るべき()
    {
        ResultHelper.IsWithValueResultType(null).Should().BeFalse();
    }

    [Fact]
    public void IsNoValueResultType_nullならfalseが返るべき()
    {
        ResultHelper.IsNoValueResultType(null).Should().BeFalse();
    }

    [Fact]
    public void IsResultType_nullならfalseが返るべき()
    {
        ResultHelper.IsResultType(null).Should().BeFalse();
    }

    [Fact]
    public void IsWithValueResultType_ResultTならtrueが返るべき()
    {
        ResultHelper.IsWithValueResultType(typeof(Result<string>))
            .Should().BeTrue();
    }

    [Fact]
    public void IsWithValueResultType_ResultTでないならfalseが返るべき()
    {
        ResultHelper.IsWithValueResultType(typeof(Result))
            .Should().BeFalse();

        ResultHelper.IsWithValueResultType(typeof(string))
                        .Should().BeFalse();
    }

    [Fact]
    public void IsNoValueResultType_Resultならtrueが返るべき()
    {
        ResultHelper.IsNoValueResultType(typeof(Result))
            .Should().BeTrue();
    }

    [Fact]
    public void IsNoValueResultType_Resultでないならfalseが返るべき()
    {
        ResultHelper.IsNoValueResultType(typeof(Result<string>))
            .Should().BeFalse();

        ResultHelper.IsNoValueResultType(typeof(string))
                        .Should().BeFalse();
    }

    [Fact]
    public void IsResultType_ResultかReslutTならtrueが返るべき()
    {
        ResultHelper.IsResultType(typeof(Result)).Should().BeTrue();
        ResultHelper.IsResultType(typeof(Result<string>)).Should().BeTrue();
    }

    [Fact]
    public void IsResultType_ResultかReslutTでないならfalseが返るべき()
    {
        ResultHelper.IsResultType(typeof(string)).Should().BeFalse();
    }

    [Fact]
    public void GetOkType_ResultTのTの型が取得できるべき()
    {
        ResultHelper.GetOkType(typeof(Result<string>)).Should().Be(typeof(string));
    }

    [Fact]
    public void GetOkType_ResultTでない場合は例外が発生するべき()
    {
        Action act = () => ResultHelper.GetOkType(typeof(Result));
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void TryGetOkType_trueが返りResultTのTの型が取得できるべき()
    {
        var result = ResultHelper.TryGetOkType(typeof(Result<string>), out var type);
        result.Should().BeTrue();
        type.Should().Be(typeof(string));
    }

    [Fact]
    public void TryGetOkType_ResultTでない場合はfalseが返りnullになるべき()
    {
        var result = ResultHelper.TryGetOkType(typeof(Result), out var type);
        result.Should().BeFalse();
        type.Should().BeNull();
    }

    [Fact]
    public void CreateErrorResult_型パラメーターがResultもしくはResultTでない場合は例外が発生するべき()
    {
        Action act = () => ResultHelper.CreateErrorResult<string>(new Exception());
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void CreateErrorResult_型パラメーターがResultの場合はResult型でエラーを持っている値が返るべき()
    {
        var error = new Exception();
        var result = ResultHelper.CreateErrorResult<Result>(error);
        result.Should().BeError();
        result.GetError().Should().BeSameAs(error);
    }

    [Fact]
    public void CreateErrorResult_型パラメーターがResultTの場合はResultT型でエラーを持っている値が返るべき()
    {
        var error = new Exception();
        var result = ResultHelper.CreateErrorResult<Result<string>>(error);
        result.Should().BeError();
        result.GetError().Should().BeSameAs(error);
    }

    [Fact]
    public void TryGetOkValue_Okの値が取得できて結果はtrueが返るべき()
    {
        var result = ResultHelper.TryGetOkValue(Result.Ok("Hello"), out string value);
        result.Should().BeTrue();
        value.Should().Be("Hello");
    }

    [Fact]
    public void TryGetOkValue_ResultTでない場合はfalseが返りデフォルト値になる()
    {
        var result = ResultHelper.TryGetOkValue(Result.Ok(), out string value);
        result.Should().BeFalse();
        value.Should().Be(default);
    }

    [Fact]
    public void TryGetOkValue_ResultTで失敗の場合はfalseが返りデフォルト値になる()
    {
        var result = ResultHelper.TryGetOkValue(Result.Error<string>(new Exception()), out string value);
        result.Should().BeFalse();
        value.Should().Be(default);
    }

    [Fact]
    public void TryGetOkValue_sourceがnullの場合はfalseが返りデフォルト値になる()
    {
        var result = ResultHelper.TryGetOkValue(null, out string value);
        result.Should().BeFalse();
        value.Should().Be(default);
    }

    [Fact]
    public void TryGetOkValue_設定されている型とoutの型が一致しない場合はfalseが返りデフォルト値になる()
    {
        var result = ResultHelper.TryGetOkValue(Result.Ok("Hello"), out int value);
        result.Should().BeFalse();
        value.Should().Be(default);
    }

    [Fact]
    public void TryGetErrorValue_失敗の値が取得できて結果はtrueが返るべき()
    {
        var error = new Exception();
        var result = ResultHelper.TryGetErrorValue(Result.Error(error), out var value);
        result.Should().BeTrue();
        value.Should().Be(error);
    }

    [Fact]
    public void TryGetErrorValue_成功の場合は結果はfalseが返るべき()
    {
        var result = ResultHelper.TryGetErrorValue(Result.Ok(), out var value);
        result.Should().BeFalse();
        value.Should().BeNull();
    }

    [Fact]
    public void T_TryGetErrorValue_失敗の値が取得できて結果はtrueが返るべき()
    {
        var error = new Exception();
        var result = ResultHelper.TryGetErrorValue(Result.Error<string>(error), out var value);
        result.Should().BeTrue();
        value.Should().Be(error);
    }

    [Fact]
    public void T_TryGetErrorValue_成功の場合は結果はfalseが返るべき()
    {
        var result = ResultHelper.TryGetErrorValue(Result.Ok("Hello"), out var value);
        result.Should().BeFalse();
        value.Should().BeNull();
    }

    [Fact]
    public void TryGetErrorValue_Resultでない場合はfalseが返るべき()
    {
        var result = ResultHelper.TryGetErrorValue(new Exception(), out var value);
        result.Should().BeFalse();
        value.Should().BeNull();
    }

    [Fact]
    public void TryGetErrorValue_nullの場合はfalseが返るべき()
    {
        var result = ResultHelper.TryGetErrorValue(null, out var value);
        result.Should().BeFalse();
        value.Should().BeNull();
    }

    [Fact]
    public void Merge_引数がnullの場合は例外が発生するべき()
    {
        Action act = () => ResultHelper.Merge(null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void Merge_全て成功の場合は成功になる()
    {
        var result = ResultHelper.Merge(new[] { Result.Ok(), Result.Ok() });
        result.Should().BeOk();
    }

    [Fact]
    public void Merge_失敗がある場合は失敗になる()
    {
        var result = ResultHelper.Merge(new[] { Result.Ok(), Result.Error(new Exception("1")), Result.Ok(), Result.Error(new Exception("2")), Result.Ok() });
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }

    [Fact]
    public async Task MergeAsync_引数がnullの場合は例外が発生するべき()
    {
        Func<Task> act = () => ResultHelper.MergeAsync(null as IEnumerable<Task<Result>>);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task MergeAsync_全て成功の場合は成功になる()
    {
        var result = await ResultHelper.MergeAsync(new[] { Result.Ok().AsTask(), Result.Ok().AsTask() } as IEnumerable<Task<Result>>);
        result.Should().BeOk();
    }

    [Fact]
    public async Task MergeAsync_失敗がある場合は失敗になる()
    {
        var result = await ResultHelper.MergeAsync(new[] {
            Result.Ok().AsTask(),
            Result.Error(new Exception("1")).AsTask(),
            Result.Ok().AsTask(),
            Result.Error(new Exception("2")).AsTask(),
            Result.Ok().AsTask()
        } as IEnumerable<Task<Result>>);
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }

    [Fact]
    public void MergeT_引数がnullの場合は例外が発生するべき()
    {
        Action act = () => ResultHelper.Merge<string>(null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void MergeT_全て成功の場合は成功になる()
    {
        var result = ResultHelper.Merge(new[] { Result.Ok("A"), Result.Ok("B") });
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public void MergeT_全て成功の場合は成功になる_for_codecoverage()
    {
        var result = ResultHelper.Merge(new List<Result<string>>{ Result.Ok("A"), Result.Ok("B") });
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public void MergeT_失敗がある場合は失敗になる()
    {
        var result = ResultHelper.Merge(new[] {
            Result.Ok("1"),
            Result.Error<string>(new Exception("1")),
            Result.Ok("2"),
            Result.Error<string>(new Exception("2")),
            Result.Ok("3")
        } as IEnumerable<Result<string>>);
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }

    [Fact]
    public async Task MergeAsyncT_引数がnullの場合は例外が発生するべき()
    {
        Func<Task> act = () => ResultHelper.MergeAsync(null as IEnumerable<Task<Result<string>>>);
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task MergeAsyncT_全て成功の場合は成功になる()
    {
        var result = await ResultHelper.MergeAsync(new[] {
            Result.Ok("A").AsTask(),
            Result.Ok("B").AsTask()
        } as IEnumerable<Task<Result<string>>>);
        result.Should().BeOk();
        result.Get().Should().HaveCount(2);
    }

    [Fact]
    public async Task MergeAsyncT_失敗がある場合は失敗になる()
    {
        var result = await ResultHelper.MergeAsync(new[] {
            Result.Ok("1").AsTask(),
            Result.Error<string>(new Exception("1")).AsTask(),
            Result.Ok("2").AsTask(),
            Result.Error<string>(new Exception("2")).AsTask(),
            Result.Ok("3").AsTask()
        } as IEnumerable<Task<Result<string>>>);
        result.Should().BeError().And.BeOfType<AggregateException>();
        var errors = result.GetError().As<AggregateException>();
        errors.InnerExceptions.Should().HaveCount(2);
    }
}

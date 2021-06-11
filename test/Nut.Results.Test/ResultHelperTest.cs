using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Nut.Results.FluentAssertions;

namespace Nut.Results.Test
{
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
            Action act = () => ResultHelper.CreateErrorResult<string>(new Error());
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void CreateErrorResult_型パラメーターがResultの場合はResult型でエラーを持っている値が返るべき()
        {
            var error = new Error();
            var result = ResultHelper.CreateErrorResult<Result>(error);
            result.Should().BeError();
            result.GetError().Should().BeSameAs(error);
        }

        [Fact]
        public void CreateErrorResult_型パラメーターがResultTの場合はResultT型でエラーを持っている値が返るべき()
        {
            var error = new Error();
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
            var result = ResultHelper.TryGetOkValue(Result.Error<string>(new Error()), out string value);
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
            var error = new Error();
            var result = ResultHelper.TryGetErrorValue(Result.Error(error), out IError value);
            result.Should().BeTrue();
            value.Should().Be(error);
        }
        
        [Fact]
        public void TryGetErrorValue_成功の場合は結果はfalseが返るべき()
        {
            var result = ResultHelper.TryGetErrorValue(Result.Ok(), out IError value);
            result.Should().BeFalse();
            value.Should().BeNull();
        }
        
        [Fact]
        public void T_TryGetErrorValue_失敗の値が取得できて結果はtrueが返るべき()
        {
            var error = new Error();
            var result = ResultHelper.TryGetErrorValue(Result.Error<string>(error), out IError value);
            result.Should().BeTrue();
            value.Should().Be(error);
        }
        
        [Fact]
        public void T_TryGetErrorValue_成功の場合は結果はfalseが返るべき()
        {
            var result = ResultHelper.TryGetErrorValue(Result.Ok("Hello"), out IError value);
            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Fact]
        public void TryGetErrorValue_Resultでない場合はfalseが返るべき()
        {
            var result = ResultHelper.TryGetErrorValue(new Error(), out IError value);
            result.Should().BeFalse();
            value.Should().BeNull();
        }

        [Fact]
        public void TryGetErrorValue_nullの場合はfalseが返るべき()
        {
            var result = ResultHelper.TryGetErrorValue(null, out IError value);
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
            var result = ResultHelper.Merge(Result.Ok(), Result.Ok());
            result.Should().BeOk();
        }

        [Fact]
        public void Merge_失敗がある場合は失敗になる()
        {
            var result = ResultHelper.Merge(Result.Ok(), Result.Error(new Error("1")), Result.Ok(), Result.Error(new Error("2")), Result.Ok());
            result.Should().BeError().And.BeOfType<AggregateError>();
            var errors = result.GetError().As<AggregateError>();
            errors.Errors.Should().HaveCount(2);
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
            var result = ResultHelper.Merge(Result.Ok("A"), Result.Ok("B"));
            result.Should().BeOk();
            result.Get().Should().HaveCount(2);
        }

        [Fact]
        public void MergeT_失敗がある場合は失敗になる()
        {
            var result = ResultHelper.Merge(Result.Ok("1"), 
                Result.Error<string>(new Error("1")), 
                Result.Ok("2"), 
                Result.Error<string>(new Error("2")),
                Result.Ok("3"));
            result.Should().BeError().And.BeOfType<AggregateError>();
            var errors = result.GetError().As<AggregateError>();
            errors.Errors.Should().HaveCount(2);
        }
    }
}

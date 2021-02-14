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
    }
}

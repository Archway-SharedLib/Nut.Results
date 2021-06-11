using FluentAssertions;
using Nut.Results.FluentAssertions;
using System;
using Xunit;

namespace Nut.Results.Test
{
    public class T_MergeTest
    {
        [Fact]
        public void 引数がnullの場合は例外が発生するべき()
        {
            Action act = () => ResultExtensions.Merge<string>(null);
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
                Result.Error<string>(new Error("1")),
                Result.Ok("2"),
                Result.Error<string>(new Error("2")),
                Result.Ok("3")
            });
            result.Should().BeError().And.BeOfType<AggregateError>();
            var errors = result.GetError().As<AggregateError>();
            errors.Errors.Should().HaveCount(2);
        }
    }
}

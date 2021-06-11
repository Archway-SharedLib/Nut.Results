using FluentAssertions;
using Nut.Results.FluentAssertions;
using System;
using Xunit;

namespace Nut.Results.Test
{
    public class MergeTest
    {
        [Fact]
        public void 引数がnullの場合は例外が発生するべき()
        {
            Action act = () => ResultExtensions.Merge(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void Merge_全て成功の場合は成功になる()
        {
            var result = ResultExtensions.Merge(new[] { Result.Ok(), Result.Ok() });
            result.Should().BeOk();
        }

        [Fact]
        public void Merge_失敗がある場合は失敗になる()
        {
            var result = ResultExtensions.Merge(new[] { Result.Ok(), Result.Error(new Error("1")), Result.Ok(), Result.Error(new Error("2")), Result.Ok() });
            result.Should().BeError().And.BeOfType<AggregateError>();
            var errors = result.GetError().As<AggregateError>();
            errors.Errors.Should().HaveCount(2);
        }
    }
}

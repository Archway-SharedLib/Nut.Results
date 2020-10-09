using System;
using FluentAssertions;
using Xunit;

namespace Archway.Results.Test
{
    public class ResultUnsafeExtensionsTest
    {
        [Fact]
        public void GetError_エラーの値を取得できる()
        {
            var expect = new Error();
            Result.Error(expect).GetError().Should().Be(expect);
        }

        [Fact]
        public void GetError_成功の場合は例外が発生する()
        {
            Action act = () => Result.Ok().GetError();
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
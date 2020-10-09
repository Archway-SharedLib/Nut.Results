using System;
using FluentAssertions;
using Xunit;

namespace Archway.Results.Test
{
    public class ResultTUnsafeExtensionsTest
    {
        [Fact]
        public void Get_値を取得できる()
        {
            var expect = new Error();
            Result.Ok("OK").Get().Should().Be("OK");
        }

        [Fact]
        public void Get_失敗の場合は例外が発生する()
        {
            Action act = () => Result.Error<string>(new Error()).Get();
            act.Should().Throw<InvalidOperationException>();
        }
        
        [Fact]
        public void GetError_エラーの値を取得できる()
        {
            var expect = new Error();
            Result.Error<string>(expect).GetError().Should().Be(expect);
        }

        [Fact]
        public void GetError_成功の場合は例外が発生する()
        {
            Action act = () => Result.Ok("ok").GetError();
            act.Should().Throw<InvalidOperationException>();
        }
    }
}
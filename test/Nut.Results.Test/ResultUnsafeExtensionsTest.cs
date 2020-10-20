using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test
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

        [Fact]
        public async Task async_GetError_エラーの値を取得できる()
        {
            var expect = new Error();
            var error = await Task.FromResult(Result.Error(expect)).GetError();
            error.Should().Be(expect);
        }

        [Fact]
        public void async_GetError_成功の場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok()).GetError();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void async_GetError_引数がnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultUnsafeExtensions.GetError((Task<Result>)null);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
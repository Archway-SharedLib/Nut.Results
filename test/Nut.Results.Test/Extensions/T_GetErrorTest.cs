using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test
{
    public class T_GetErrorTest
    {
        [Fact]
        public void GetError_エラーの値を取得できる()
        {
            var expect = new Error();
            Result.Error<string>(expect).GetError().Should().Be(expect);
        }

        [Fact]
        public async Task async_GetError_エラーの値を取得できる()
        {
            var expect = new Error();
            var error = await Task.FromResult(Result.Error<string>(expect)).GetError();
            error.Should().Be(expect);
        }

        [Fact]
        public void GetError_成功の場合は例外が発生する()
        {
            Action act = () => Result.Ok("ok").GetError();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void async_GetError_成功の場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Ok("ok")).GetError();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void async_GetError_引数がnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultExtensions.GetError((Task<Result<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
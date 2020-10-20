using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test
{
    public class ResultTUnsafeExtensionsTest
    {
        [Fact]
        public void Get_値を取得できる()
        {
            Result.Ok("OK").Get().Should().Be("OK");
        }

        [Fact]
        public async Task async_Get_値を取得できる()
        {
            var res = await Task.FromResult(Result.Ok("OK")).Get();
            res.Should().Be("OK");
        }

        [Fact]
        public void Get_失敗の場合は例外が発生する()
        {
            Action act = () => Result.Error<string>(new Error()).Get();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void async_Get_失敗の場合は例外が発生する()
        {
            Func<Task> act = () => Task.FromResult(Result.Error<string>(new Error())).Get();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void async_Get_引数がnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultTUnsafeExtensions.Get((Task<Result<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }

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
            Func<Task> act = () => ResultTUnsafeExtensions.GetError((Task<Result<string>>)null);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
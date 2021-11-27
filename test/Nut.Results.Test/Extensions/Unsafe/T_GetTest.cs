using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class T_GetTest
    {
        [Fact]
        public void 値を取得できる()
        {
            Result.Ok("OK").Get().Should().Be("OK");
        }

        [Fact]
        public async Task Async_値を取得できる()
        {
            var res = await Result.Ok("OK").AsTask().Get();
            res.Should().Be("OK");
        }

        [Fact]
        public void 失敗の場合は例外が発生する()
        {
            Action act = () => Result.Error<string>(new Error()).Get();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task Async_失敗の場合は例外が発生する()
        {
            Func<Task> act = () =>Result.Error<string>(new Error()).AsTask().Get();
            await act.Should().ThrowAsync<InvalidOperationException>();
        }

        [Fact]
        public async Task Async_引数がnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultUnsafeExtensions.Get((Task<Result<string>>)null);
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

    }
}
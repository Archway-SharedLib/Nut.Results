using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class GetErrorTest
    {
        [Fact]
        public void エラーの値を取得できる()
        {
            var expect = new Error();
            Result.Error(expect).GetError().Should().Be(expect);
        }

        [Fact]
        public void 成功の場合は例外が発生する()
        {
            Action act = () => Result.Ok().GetError();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task Async_エラーの値を取得できる()
        {
            var expect = new Error();
            var error = await Result.Error(expect).AsTask().GetError();
            error.Should().Be(expect);
        }

        [Fact]
        public void Async_成功の場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().AsTask().GetError();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void Async_引数がnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultUnsafeExtensions.GetError((Task<Result>)null);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
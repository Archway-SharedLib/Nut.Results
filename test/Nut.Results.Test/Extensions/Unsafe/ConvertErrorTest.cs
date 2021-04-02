using System;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

// ReSharper disable CheckNamespace

namespace Nut.Results.Test
{
    public class ConvertErrorTest
    {
        [Fact]
        public void 成功の場合は例外が発生する()
        {
            Action act = () => Result.Ok().ConvertError<string>();
            act.Should().Throw<InvalidOperationException>();
        }
        
        [Fact]
        public void エラーの値が引き継がれる()
        {
            var expect = new Error();
            Result.Error(expect).ConvertError<string>().Should().BeError().And.Match(a => a == expect);
        }
        
        [Fact]
        public void Async_成功の場合は例外が発生する()
        {
            Func<Task> act = () => Result.Ok().AsTask().ConvertError<string>();
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public async Task Async_エラーの値が引き継がれる()
        {
            var expect = new Error();
            var error = await Result.Error(expect).AsTask().ConvertError<string>();
            error.Should().BeError().And.Match(a => a == expect);
        }
        
        [Fact]
        public void Async_引数がnullの場合は例外が発生する()
        {
            Func<Task> act = () => ResultUnsafeExtensions.ConvertError<string>((Task<Result>)null);
            act.Should().Throw<ArgumentNullException>();
        }
    }
}
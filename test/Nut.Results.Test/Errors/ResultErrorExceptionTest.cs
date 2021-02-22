using System;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test.Errors
{
    public class ResultErrorExceptionTest
    {
        [Fact]
        public void ctor_コンストラクタで渡したErrorがSourceErrorで取得できる()
        {
            var err = new Error();
            var ex = new ResultErrorException(err);
            ex.SourceError.Should().Be(err);
        }
        
        [Fact]
        public void ctor_コンストラクタで渡したErrorのメッセージがMessageに設定される()
        {
            var message = "Exception Message";
            var ex = new ResultErrorException(new Error(message));
            ex.Message.Should().Be(message);
        }

        [Fact]
        public void ctor_コンストラクタでsourceErrorにnullを渡すと例外が発生する()
        {
            Action act = () => new ResultErrorException(null);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ctor2_コンストラクタで渡したErrorがSourceErrorで取得できる()
        {
            var err = new Error();
            var ex = new ResultErrorException(err, "Error");
            ex.SourceError.Should().Be(err);
        }
        
        [Fact]
        public void ctor2_コンストラクタで渡したメッセージがMessageに設定される()
        {
            var message = "Exception Message";
            var ex = new ResultErrorException(new Error("Foo"), message);
            ex.Message.Should().Be(message);
        }
        
        [Fact]
        public void ctor2_コンストラクタでsourceErrorにnullを渡すと例外が発生する()
        {
            Action act = () => new ResultErrorException(null, "Error");
            act.Should().Throw<ArgumentNullException>();
        }
        
    }
}
using FluentAssertions;
using Xunit;

namespace Archway.Results.Test
{
    public class InvalidReturnValueExceptionTest
    {
        [Fact]
        public void ctor_デフォルトコンストラクタではデフォルトのメッセージが設定される()
        {
            var ex = new InvalidReturnValueException();
            ex.Message.Should().Be("Invalid return value.");
        }

        [Fact]
        public void ctor_コンストラクタにメッセージを設定したらmessageプロパティで所得できる()
        {
            var expect = "this is error message";
            var ex = new InvalidReturnValueException(expect);
            ex.Message.Should().Be(expect);
        }
    }
}
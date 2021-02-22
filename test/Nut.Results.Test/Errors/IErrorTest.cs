using Nut.Results;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Nut.Results.Test.Errors
{
    public class IErrorTest
    {
        [Fact]
        public void ToException_Exceptionのインスタンスが返される()
        {
            IError error = new TestErrorWithMessage();
            var ex = error.ToException();
            ex.Should().BeOfType(typeof(ResultErrorException));
        }
        
        [Fact]
        public void ToException_ResultErrorExceptionのSourceErrorには自信のインスタンスが設定されている()
        {
            IError error = new TestErrorWithMessage();
            var ex = error.ToException();
            ex.Should().BeOfType(typeof(ResultErrorException));
            ((ResultErrorException) ex).SourceError.Should().Be(error);
        }

        [Fact]
        public void ToException_メッセージが設定されている場合はそのメッセージを利用したExceptionが生成される()
        {
            IError error = new TestErrorWithMessage();
            var ex = error.ToException();
            ex.Message.Should().Be("Error Message For test");
        }

        [Fact]
        public void ToException_オーバーライドされている場合はそちらが利用される()
        {
            IError error = new ToExceptionOverrideError();
            var ex = error.ToException();
            ex.Should().BeOfType(typeof(Exception));
        }
        
        private class TestErrorWithMessage : IError
        {

            public string Message => "Error Message For test";
        }

        private class ToExceptionOverrideError : IError
        {
            public string Message => "message";

            public Exception ToException()
            {
                return new Exception();
            }
        }
    }
}

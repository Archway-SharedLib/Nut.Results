using Archway.Results;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Archway.Results.Test
{
    public class IErrorTest
    {
        [Fact]
        public void ToException_Exceptionのインスタンスが返される()
        {
            IError error = new TestErrorWithMessage();
            var ex = error.ToException();
            ex.Should().BeOfType(typeof(Exception));
        }

        [Fact]
        public void ToException_メッセージが設定されている場合はそのメッセージを利用したExceptionが生成される()
        {
            IError error = new TestErrorWithMessage();
            var ex = error.ToException();
            ex.Message.Should().Be("Error Message For test");
        }

        [Fact]
        public void ToException_メッセージが空の場合はメッセージが空のExceptionが生成される()
        {
            IError error = new TestEerorWithEmptyMessage();
            var ex = error.ToException();
            ex.Message.Should().BeEmpty();
        }

        [Fact]
        public void ToException_メッセージがnullの場合はExceptionのデフォルトのメッセージが設定される()
        {
            IError error = new TestErrorWithNullMessage();
            var ex = error.ToException();
            ex.Message.Should().Be((new Exception()).Message);
        }

        private class TestErrorWithNullMessage : IError
        {
            public string Message => null;
        }

        private class TestEerorWithEmptyMessage : IError
        {
            public string Message => string.Empty;
        }

        private class TestErrorWithMessage : IError
        {

            public string Message => "Error Message For test";
        }
    }
}

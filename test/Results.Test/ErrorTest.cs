using Archway.Results;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Archway.Results.Test
{
    public class ErrorTest
    {
        [Fact]
        public void ctor_デフォルトコンストラクタではメッセージは空が設定される()
        {
            var error = new Error();
            error.Message.Should().BeEmpty();
        }

        [Fact]
        public void ctor_コンストラクタで引き渡したメッセージがプロパティから取得できる()
        {
            var message = DateTime.Now.ToLongDateString();
            var error = new Error(message);
            error.Message.Should().Be(message);
        }

        [Fact]
        public void ToString_メッセージと同じ値が返る()
        {
            var message = DateTime.Now.ToLongDateString();
            var error = new Error(message);
            error.ToString().Should().Be(error.Message);
        }
    }
}

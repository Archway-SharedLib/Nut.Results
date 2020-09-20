using Archway.Results;
using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Xunit;

namespace Archway.Results.Test
{
    public class ResultBuilderTest
    {
        [Fact]
        public void Ok_成功の結果が返る()
        {
            var r = Result.Ok();
            r.IsOk.Should().BeTrue();
            r.IsError.Should().BeFalse();
        }

        [Fact]
        public void Error_失敗の結果が返る()
        {
            var r = Result.Error(new Error("message"));
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
        }

        [Fact]
        public void T_Ok_成功の結果が返る()
        {
            var r = Result.Ok("Ok Message");
            r.IsOk.Should().BeTrue();
            r.IsError.Should().BeFalse();
        }

        [Fact]
        public void T_Error_失敗の結果が返る()
        {
            var r = Result.Error<string>(new Error("message"));
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
        }
    }
}

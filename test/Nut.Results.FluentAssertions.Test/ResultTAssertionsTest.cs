using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using Xunit.Sdk;

namespace Nut.Results.FluentAssertions.Test
{
    public class ResultTAssertionsTest
    {
        [Fact]
        public void Be_値が一致する場合は成功する()
        {
            Result.Ok("Ok").Should().Be(Result.Ok("Ok"));
        }

        [Fact]
        public void Be_値が一致しない場合は失敗する()
        {
            Action act = () => Result.Ok("Ok").Should().Be(Result.Ok("Success"));
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void BeOk_Okの場合は成功する()
        {
            Result.Ok("Ok").Should().BeOk();
        }

        [Fact]
        public void BeOk_Errorの場合は失敗する()
        {
            Action act = () => Result.Error<string>(new Error()).Should().BeOk();
            act.Should().Throw<XunitException>();
        }

        [Fact]
        public void BeError_Errorの場合は成功する()
        {
            Result.Error<string>(new Error()).Should().BeError();
        }

        [Fact]
        public void BeError_Okの場合は失敗する()
        {
            Action act = () => Result.Ok("Ok").Should().BeError();
            act.Should().Throw<XunitException>();
        }
    }
}

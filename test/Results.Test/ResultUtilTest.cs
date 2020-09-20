using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Archway.Results.Test
{
    public class ResultUtilTest
    {
        [Fact]
        public void IsVoidResultType_Resultの場合はtrue()
        {
            Result.IsVoidResultType(typeof(Result)).Should().BeTrue();
        }

        [Fact]
        public void IsVoidResultType_Resultでない場合はfalse()
        {
            Result.IsVoidResultType(typeof(Result<string>)).Should().BeFalse();
        }

        [Fact]
        public void IsGenericResultType_ResultTの場合はtrue()
        {
            Result.IsGenericResultType(typeof(Result<string>)).Should().BeTrue();
        }

        [Fact]
        public void IsGenericResultType_ResultTでGeneric型が指定されていない場合もtrue()
        {
            Result.IsGenericResultType(typeof(Result<>)).Should().BeTrue();
        }

        [Fact]
        public void IsGenericResultType_ResultTでない場合はfalse()
        {
            Result.IsGenericResultType(typeof(Result)).Should().BeFalse();
        }

        [Fact]
        public void IsResultType_Resultの場合はtrue()
        {
            Result.IsResultType(typeof(Result)).Should().BeTrue();
        }

        [Fact]
        public void IsResultType_ResultTの場合はtrue()
        {
            Result.IsResultType(typeof(Result<string>)).Should().BeTrue();
        }

        [Fact]
        public void IsResultType_ResultTでGeneric型が指定されていない場合もtrue()
        {
            Result.IsResultType(typeof(Result<>)).Should().BeTrue();
        }

        [Fact]
        public void GetOkType_ResultTのTが取得できる()
        {
            Result.GetOkType(typeof(Result<string>)).Should().Be(typeof(string));
        }

        [Fact]
        public void GetOkType_ResultTで無い場合は例外が発生する()
        {
            Action act = () => Result.GetOkType(typeof(Result));
            act.Should().Throw<ArgumentException>();
        }
    }
}

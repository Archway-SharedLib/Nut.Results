using FluentAssertions;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Archway.Results.Test
{
    public class ResultTest
    {
        [Fact]
        public void ctor_デフォルトコンストラクタで生成した場合は失敗()
        {
            var r = new Result();
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
            r.errorValue.Should().BeNull();
        }

        [Fact]
        public void ctor_Errorがnullでokがfalseの場合は例外が発生してインスタンスを作れない()
        {
            Action act = () => new Result(null, false);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void ctor_Errorがnullでokがtrueの場合は成功のインスタンスが作成される()
        {
            var r = new Result(null, true);
            r.IsOk.Should().BeTrue();
        }

        [Fact]
        public void ctor_Errorがnullでなくokがfalseの場合は失敗のインスタンスが作成される()
        {
            var r = new Result(new Error(), false);
            r.IsError.Should().BeTrue();
        }

        [Fact]
        public void ctor_Errorがnullでなくokがtrueの場合は成功のインスタンスになる()
        {
            var r = new Result(new Error(), true);
            r.IsOk.Should().BeTrue();
        }

        [Fact]
        public void Equals_両方とも成功の場合はtrue()
        {
            var r1 = Result.Ok();
            var r2 = Result.Ok();
            r1.Equals(r2).Should().BeTrue();
        }

        [Fact]
        public void Equals_成功と失敗の場合はfalse()
        {
            var r1 = Result.Ok();
            var r2 = Result.Error(new Error());
            r1.Equals(r2).Should().BeFalse();
        }

        [Fact]
        public void Equals_両方とも失敗の場合は失敗がEqualならtrue()
        {
            var er = new Error();
            var r1 = Result.Error(er);
            var r2 = Result.Error(er);
            r1.Equals(r2).Should().BeTrue();
        }

        [Fact]
        public void Equals_両方とも失敗の場合は失敗がEqualでないならfalse()
        {
            var er1 = new Error();
            var er2 = new Error();
            var r1 = Result.Error(er1);
            var r2 = Result.Error(er2);
            r1.Equals(r2).Should().BeFalse();
        }

        [Fact]
        public void GetHashCode_成功の場合は同じ値になる()
        {
            var r1 = Result.Ok();
            var r2 = Result.Ok();
            r1.GetHashCode().Should().Be(r2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_成功と失敗の場合は違う値になる()
        {
            var r1 = Result.Ok();
            var r2 = Result.Error(new Error());
            r1.GetHashCode().Should().NotBe(r2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致する場合は一致する()
        {
            var er = new Error();
            var r1 = Result.Error(er);
            var r2 = Result.Error(er);
            r1.GetHashCode().Should().Be(r2.GetHashCode());
        }

        [Fact]
        public void GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致しない場合は一致しない()
        {
            var er1 = new Error();
            var er2 = new Error();
            var r1 = Result.Error(er1);
            var r2 = Result.Error(er2);
            er1.GetHashCode().Should().NotBe(er2.GetHashCode());
            r1.GetHashCode().Should().NotBe(r2.GetHashCode());
        }

        [Fact]
        public void ToString_成功の場合はokと返される()
        {
            var r = Result.Ok();
            r.ToString().Should().Be("ok");
        }

        [Fact]
        public void ToString_失敗の場合は失敗オブジェクトのToStringに接頭辞でerrorが付いた値が返される()
        {
            var r = Result.Error(new TestError());
            r.ToString().Should().Be("error: Test Error");
        }

        [Fact]
        public void ToString_失敗で失敗オブジェクトのToStringがnullの場合はerror_nullと返される()
        {
            var r = Result.Error(new TestNullError());
            r.ToString().Should().Be("error: (null)");
        }

        [Fact]
        public void for_test_coverage_ToString_失敗で失敗オブジェクトがnullの場合はerror_nullと返される()
        {
            var r = new Result();
            r.ToString().Should().Be("error: (null)");
        }

        [Fact]
        public void T_ctor_デフォルトコンストラクタで生成した場合は失敗()
        {
            var r = new Result<string>();
            r.IsOk.Should().BeFalse();
            r.IsError.Should().BeTrue();
            r.errorValue.Should().BeNull();
        }

        [Fact]
        public void T_ctor_Errorがnullでokがfalseの場合は例外が発生してインスタンスを作れない()
        {
            Action act = () => new Result<string>(null, null, false);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void T_ctor_Valueがnullでokがtrueの場合は例外が発生してインスタンスを作れない()
        {
            Action act = () => new Result<string>(null, null, true);
            act.Should().Throw<ArgumentNullException>();
        }

        [Fact]
        public void T_ctor_Errorがnullでなくokがfalseの場合は失敗のインスタンスが作られる()
        {
            var r = new Result<string>(null, new Error(), false);
            r.IsError.Should().BeTrue();
        }

        [Fact]
        public void T_ctor_Valueがnullでなくokがtrueの場合は成功のインスタンスが作られる()
        {
            var r = new Result<string>("success", new Error(), true);
            r.IsOk.Should().BeTrue();
        }

        [Fact]
        public void T_ctor_Errorがnullでなくokがtrueの場合は成功のインスタンスになる()
        {
            var r = new Result<int>(1, new Error(), true);
            r.IsOk.Should().BeTrue();
        }

        [Fact]
        public void T_ctor_Valueがnullでなくokがfalseの場合は失敗のインスタンスになる()
        {
            var r = new Result<int>(1, new Error(), false);
            r.IsError.Should().BeTrue();
        }

        [Fact]
        public void T_operator_値をResultTに代入できる()
        {
            Result<string> r = "ABC";
            r.IsOk.Should().BeTrue();
            r.value.Should().Be("ABC");
        }

        [Fact]
        public void T_Equals_両方とも成功の場合は値がequalならtrue()
        {
            var r1 = Result.Ok("OK");
            var r2 = Result.Ok("OK");
            r1.Equals(r2).Should().BeTrue();
        }

        [Fact]
        public void T_Equals_両方とも成功の場合でも値がnotequalならfalse()
        {
            var r1 = Result.Ok("OK1");
            var r2 = Result.Ok("OK2");
            r1.Equals(r2).Should().BeFalse();
        }

        [Fact]
        public void T_Equals_成功と失敗の場合は値がequalでもfalse()
        {
            var error = new Error();
            var r1 = Result.Ok(error);
            var r2 = Result.Error(error);
            r1.Equals(r2).Should().BeFalse();
        }

        [Fact]
        public void T_Equals_両方とも失敗の場合は値がEqualならtrue()
        {
            var er = new Error();
            var r1 = Result.Error(er);
            var r2 = Result.Error(er);
            r1.Equals(r2).Should().BeTrue();
        }

        [Fact]
        public void T_Equals_両方とも失敗の場合は値がEqualでないならfalse()
        {
            var er1 = new Error();
            var er2 = new Error();
            var r1 = Result.Error(er1);
            var r2 = Result.Error(er2);
            r1.Equals(r2).Should().BeFalse();
        }

        [Fact]
        public void T_GetHashCode_成功の場合は成功のハッシュコードが同じ場合は一致する()
        {
            var r1 = Result.Ok("OK");
            var r2 = Result.Ok("OK");
            r1.GetHashCode().Should().Be(r2.GetHashCode());
        }

        [Fact]
        public void T_GetHashCode_成功の場合は成功のハッシュコードが違う場合は一致しない()
        {
            var r1 = Result.Ok("OK1");
            var r2 = Result.Ok("OK2");
            r1.GetHashCode().Should().NotBe(r2.GetHashCode());
        }

        [Fact]
        public void T_GetHashCode_成功と失敗の場合は違う値になる()
        {
            var er = new Error();
            var r1 = Result.Ok(er);
            var r2 = Result.Error(er);
            r1.GetHashCode().Should().NotBe(r2.GetHashCode());
        }

        [Fact]
        public void T_GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致する場合は一致する()
        {
            var er = new Error();
            var r1 = Result.Error(er);
            var r2 = Result.Error(er);
            r1.GetHashCode().Should().Be(r2.GetHashCode());
        }

        [Fact]
        public void T_GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致しない場合は一致しない()
        {
            var er1 = new Error();
            var er2 = new Error();
            var r1 = Result.Error(er1);
            var r2 = Result.Error(er2);
            er1.GetHashCode().Should().NotBe(er2.GetHashCode());
            r1.GetHashCode().Should().NotBe(r2.GetHashCode());
        }

        [Fact]
        public void T_ToString_成功の場合はokと値のToStringが返される()
        {
            var r = Result.Ok("Test");
            r.ToString().Should().Be("ok: Test");
        }

        [Fact]
        public void T_ToString_成功で値のToStringがnullの場合はok_nullと返される()
        {
            var r = Result.Ok(new TestOk());
            r.ToString().Should().Be("ok: (null)");
        }

        [Fact]
        public void T_ToString_失敗の場合は失敗オブジェクトのToStringに接頭辞でerrorが付いた値が返される()
        {
            var r = Result.Error(new TestError());
            r.ToString().Should().Be("error: Test Error");
        }

        [Fact]
        public void T_ToString_失敗で失敗オブジェクトのToStringがnullの場合はerror_nullと返される()
        {
            var r = Result.Error(new TestNullError());
            r.ToString().Should().Be("error: (null)");
        }

        private class TestOk {
            public override string ToString() => null;
        }

        private class TestError : IError
        {
            public string Message => "";
            public override string ToString() => "Test Error";
        }

        private class TestNullError : IError
        {
            public string Message => "";
            public override string ToString() => null;
        }
    }
}

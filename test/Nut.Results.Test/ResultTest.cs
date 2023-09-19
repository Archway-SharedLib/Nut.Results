using System;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ResultTest
{
    [Fact]
    public void ctor_デフォルトコンストラクタで生成した場合は失敗()
    {
        var r = new Result();
        r.IsOk.Should().BeFalse();
        r.IsError.Should().BeTrue();
        r._capturedError.Should().BeNull();
    }

    [Fact]
    public void ctor_Errorがnullでokがfalseの場合は例外が発生してインスタンスを作れない()
    {
        Action act = () => new Result((Exception)null, false);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ctor_Errorがnullでokがtrueの場合は成功のインスタンスが作成される()
    {
        var r = new Result((Exception)null, true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void ctor_Errorがnullでなくokがfalseの場合は失敗のインスタンスが作成される()
    {
        var r = new Result(new Exception(), false);
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void ctor_Errorがnullでなくokがtrueの場合は成功のインスタンスになる()
    {
        var r = new Result(new Exception(), true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void ctor_EDIがnullでokがfalseの場合は例外が発生してインスタンスを作れない()
    {
        Action act = () => new Result((ExceptionDispatchInfo)null, false);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ctor_EDIがnullでokがtrueの場合は成功のインスタンスが作成される()
    {
        var r = new Result((ExceptionDispatchInfo)null, true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void ctor_EDIがnullでなくokがfalseの場合は失敗のインスタンスが作成される()
    {
        var r = new Result( ExceptionDispatchInfo.Capture(new Exception()), false);
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void ctor_EDIがnullでなくokがtrueの場合は成功のインスタンスになる()
    {
        var r = new Result(ExceptionDispatchInfo.Capture(new Exception()), true);
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
        var r2 = Result.Error(new Exception());
        r1.Equals(r2).Should().BeFalse();
    }

    [Fact]
    public void Equals_両方とも失敗の場合は失敗がEqualならtrue()
    {
        var er = new Exception();
        var r1 = Result.Error(er);
        var r2 = Result.Error(er);
        r1.Equals(r2).Should().BeTrue();
    }

    [Fact]
    public void Equals_両方とも失敗の場合は失敗がEqualでないならfalse()
    {
        var er1 = new Exception();
        var er2 = new Exception();
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
        var r2 = Result.Error(new Exception());
        r1.GetHashCode().Should().NotBe(r2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致する場合は一致する()
    {
        var er = new Exception();
        var r1 = Result.Error(er);
        var r2 = Result.Error(er);
        r1.GetHashCode().Should().Be(r2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致しない場合は一致しない()
    {
        var er1 = new Exception();
        var er2 = new Exception();
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
        var r = Result.Error(new TestException());
        r.ToString().Should().Be("error: Test Error");
    }

    [Fact]
    public void ToString_失敗で失敗オブジェクトのToStringがnullの場合はerror_nullと返される()
    {
        var r = Result.Error(new TestNullException());
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
        r._capturedError.Should().BeNull();
    }

    [Fact]
    public void T_ctor_Errorがnullでokがfalseの場合は例外が発生してインスタンスを作れない()
    {
        Action act = () => new Result<string>(null, (Exception)null, false);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T_ctor_Valueがnullでokがtrueの場合は例外が発生してインスタンスを作れない()
    {
        Action act = () => new Result<string>(null, (Exception)null, true);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T_ctor_Errorがnullでなくokがfalseの場合は失敗のインスタンスが作られる()
    {
        var r = new Result<string>(null, new Exception(), false);
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_Valueがnullでなくokがtrueの場合は成功のインスタンスが作られる()
    {
        var r = new Result<string>("success", new Exception(), true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_Errorがnullでなくokがtrueの場合は成功のインスタンスになる()
    {
        var r = new Result<int>(1, new Exception(), true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_Valueがnullでなくokがfalseの場合は失敗のインスタンスになる()
    {
        var r = new Result<int>(1, new Exception(), false);
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_EDIがnullでokがfalseの場合は例外が発生してインスタンスを作れない()
    {
        Action act = () => new Result<string>(null, (ExceptionDispatchInfo)null, false);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T_ctor_EDI_Valueがnullでokがtrueの場合は例外が発生してインスタンスを作れない()
    {
        Action act = () => new Result<string>(null, (ExceptionDispatchInfo)null, true);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void T_ctor_EDIがnullでなくokがfalseの場合は失敗のインスタンスが作られる()
    {
        var r = new Result<string>(null,  ExceptionDispatchInfo.Capture(new Exception()), false);
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_EDI_Valueがnullでなくokがtrueの場合は成功のインスタンスが作られる()
    {
        var r = new Result<string>("success", ExceptionDispatchInfo.Capture(new Exception()), true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_EDIがnullでなくokがtrueの場合は成功のインスタンスになる()
    {
        var r = new Result<int>(1, ExceptionDispatchInfo.Capture(new Exception()), true);
        r.IsOk.Should().BeTrue();
    }

    [Fact]
    public void T_ctor_EDI_Valueがnullでなくokがfalseの場合は失敗のインスタンスになる()
    {
        var r = new Result<int>(1, ExceptionDispatchInfo.Capture(new Exception()), false);
        r.IsError.Should().BeTrue();
    }

    [Fact]
    public void T_operator_値をResultTに代入できる()
    {
        Result<string> r = "ABC";
        r.IsOk.Should().BeTrue();
        r._value.Should().Be("ABC");
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
        var error = new Exception();
        var r1 = Result.Ok(error);
        var r2 = Result.Error<Exception>(error);
        r1.Equals(r2).Should().BeFalse();
    }

    [Fact]
    public void T_Equals_両方とも失敗の場合は値がEqualならtrue()
    {
        var er = new Exception();
        var r1 = Result.Error<string>(er);
        var r2 = Result.Error<string>(er);
        r1.Equals(r2).Should().BeTrue();
    }

    [Fact]
    public void T_Equals_両方とも失敗の場合は値がEqualでないならfalse()
    {
        var er1 = new Exception();
        var er2 = new Exception();
        var r1 = Result.Error<string>(er1);
        var r2 = Result.Error<string>(er2);
        r1.Equals(r2).Should().BeFalse();
    }

    [Fact]
    public void T_Equals_失敗と成功のfalse()
    {
        var er = new Exception();
        var r1 = Result.Error<string>(er);
        var r2 = Result.Ok("Success");
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
        var er = new Exception();
        var r1 = Result.Ok(er);
        var r2 = Result.Error<string>(er);
        r1.GetHashCode().Should().NotBe(r2.GetHashCode());
    }

    [Fact]
    public void T_GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致する場合は一致する()
    {
        var er = new Exception();
        var r1 = Result.Error<string>(er);
        var r2 = Result.Error<string>(er);
        r1.GetHashCode().Should().Be(r2.GetHashCode());
    }

    [Fact]
    public void T_GetHashCode_両方とも失敗の場合は失敗のハッシュコードが一致しない場合は一致しない()
    {
        var er1 = new Exception();
        var er2 = new Exception();
        var r1 = Result.Error<string>(er1);
        var r2 = Result.Error<string>(er2);
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
        var r = Result.Error<string>(new TestException());
        r.ToString().Should().Be("error: Test Error");
    }

    [Fact]
    public void T_ToString_失敗で失敗オブジェクトのToStringがnullの場合はerror_nullと返される()
    {
        var r = Result.Error<string>(new TestNullException());
        r.ToString().Should().Be("error: (null)");
    }

    [Fact]
    public void T_ToString_失敗で失敗オブジェクトがnullの場合はerror_nullと返される()
    {
        var r = new Result<string>();
        r.ToString().Should().Be("error: (null)");
    }

    [Fact]
    public void op_true_成功の場合はtrue()
    {
        var r = Result.Ok();
        if (r) { }
        else throw new Exception();
    }

    [Fact]
    public void op_true_失敗の場合はfalse()
    {
        var r = Result.Error(new Exception());
        if (r ) throw new Exception();
    }

    [Fact]
    public void T_op_true_成功の場合はtrue()
    {
        var r = Result.Ok("string");
        if (r) { }
        else throw new Exception();
    }

    [Fact]
    public void T_op_true_失敗の場合はfalse()
    {
        var r = Result.Error<string>(new Exception());
        if (r) throw new Exception();
    }

    private class TestOk
    {
        public override string ToString() => null;
    }

    private class TestException : Exception
    {
        public override string ToString() => "Test Error";
    }

    private class TestNullException : Exception
    {
        public override string ToString() => null;
    }
}

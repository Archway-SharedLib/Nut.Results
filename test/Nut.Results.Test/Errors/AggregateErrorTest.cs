using FluentAssertions;
using FluentAssertions.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Nut.Results.Test.Errors;

public class AggregateErrorTest
{
    [Fact]
    public void ctor_デフォルトコンストラクタではデフォルトのメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var er = new AggregateError();
        er.Message.Should().Be("Raise multiple errors.");
    }

    [Fact]
    public void ctor_エラーパラメーターだけのコンストラクタではデフォルトのメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var er = new AggregateError(Enumerable.Empty<IError>());
        er.Message.Should().Be("Raise multiple errors.");
    }

    [Fact]
    public void ctor_Paramsのエラーパラメーターだけのコンストラクタではデフォルトのメッセージが設定される()
    {
        using var cul = TestHelper.SetEnglishCulture();
        var er = new AggregateError(new Error(), new Error());
        er.Message.Should().Be("Raise multiple errors.");
    }

    [Fact]
    public void ctor_エラーパラメーターにnullが設定されたら例外が発生する()
    {
        Action act = () => new AggregateError((IEnumerable<IError>)null);
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void ctor_Paramsエラーパラメーターにnullが設定されたら例外が発生する()
    {
        Action act = () => new AggregateError(new Error(), (IError)null, new Error());
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void ctor_エラーパラメーターにnullが含まれていたら例外が発生する()
    {
        Action act = () => new AggregateError(new[] { new Error(), null, new Error() });
        act.Should().Throw<ArgumentException>();
    }

    [Fact]
    public void Errors_コンストラクタで設定した値が取得できる()
    {
        var e1 = new Error();
        var e2 = new Error();
        var e3 = new Error();

        var er = new AggregateError(e1, e2, e3);

        er.Errors.IndexOf(e1).Should().Be(0);
        er.Errors.IndexOf(e2).Should().Be(1);
        er.Errors.IndexOf(e3).Should().Be(2);
    }
}

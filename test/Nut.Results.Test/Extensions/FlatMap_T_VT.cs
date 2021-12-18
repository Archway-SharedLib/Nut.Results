using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class FlatMap_T_VT
{
    // VT<R> to R...

    [Fact]
    public async Task ValTaskResT_ReturnRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<string, Result>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error(error);
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnRes_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsValueTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error(error);
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public async Task ValTaskResT_ReturnTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<string, Task<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error(error).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnTaskRes_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsValueTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error(error).AsTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public async Task ValTaskResT_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<string, ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnValTaskRes_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsValueTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error(error).AsValueTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }

    // ...

    [Fact]
    public async Task ResT_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").FlatMap((Func<string, ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResT_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).FlatMap(_ =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ResT_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").FlatMap(_ =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ResT_ReturnValTaskRes_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).FlatMap(v =>
        {
            actual = v;
            return Result.Error(error).AsValueTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }


    //---

    [Fact]
    public async Task TaskResT_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsTask().FlatMap((Func<string, ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskRes_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMap((Task<Result<string>>)null, _ => Result.Ok().AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskRes_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error(error).AsValueTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }


    // ...

    [Fact]
    public async Task ValTaskResT_ReturnResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<string, Result<string>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("text");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error<string>(error);
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public async Task ValTaskResT_ReturnTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<string, Task<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("text").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error<string>(error).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnTaskResT_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsValueTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error<string>(error).AsTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public async Task ValTaskResT_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<string, ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ValTaskResT_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ValTaskResT_ReturnValTaskResT_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsValueTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error<string>(error).AsValueTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }


    // R to VT<R>...

    [Fact]
    public async Task ResT_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").FlatMap((Func<string, ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task ResT_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ResT_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").FlatMap(_ =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task ResT_ReturnValTaskResT_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).FlatMap(v =>
        {
            actual = v;
            return Result.Error<string>(error).AsValueTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }

    //---

    [Fact]
    public async Task TaskResT_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsTask().FlatMap((Func<string, ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskResT_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMap((Task<Result<string>>)null, _ => Result.Ok("text").AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsTask().FlatMap(_ =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task TaskResT_ReturnValTaskResT_okの引数には成功の値が設定される()
    {
        var error = new Error();
        var expected = "text";
        string actual = null;
        var result = await Result.Ok(expected).AsTask().FlatMap(v =>
        {
            actual = v;
            return Result.Error<string>(error).AsValueTask();
        });

        actual.Should().Be(expected);
        result.Should().BeError().And.Match(v => v == error);
    }
}

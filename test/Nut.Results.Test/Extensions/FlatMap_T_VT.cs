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
    public static async Task ValTaskResT_ReturnRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<Result>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskResT_ReturnRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskResT_ReturnRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error);
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskResT_ReturnTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<Task<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskResT_ReturnTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskResT_ReturnTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskResT_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskResT_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskResT_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    // R to VT<R>...

    [Fact]
    public static async Task ResT_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").FlatMap((Func<ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ResT_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ResT_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //---

    [Fact]
    public static async Task TaskResT_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsTask().FlatMap((Func<ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskResT_ReturnValTaskRes_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMap((Task<Result<string>>)null, () => Result.Ok().AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskResT_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task TaskResT_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    // VT<R> to R<T>...

    [Fact]
    public static async Task ValTaskResT_ReturnResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<Result<string>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskResT_ReturnResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskResT_ReturnResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error);
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskResT_ReturnTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<Task<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskResT_ReturnTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskResT_ReturnTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskResT_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsValueTask().FlatMap((Func<ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskResT_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskResT_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    // R to VT<R>...

    [Fact]
    public static async Task ResT_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").FlatMap((Func<ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ResT_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ResT_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //---

    [Fact]
    public static async Task TaskResT_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok("text").AsTask().FlatMap((Func<ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskResT_ReturnValTaskResT_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMap((Task<Result<string>>)null, () => Result.Ok("text").AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskResT_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error<string>(error).AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task TaskResT_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok("text").AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }
}

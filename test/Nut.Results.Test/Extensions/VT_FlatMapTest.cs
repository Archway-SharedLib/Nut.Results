using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class VT_FlatMapTest
{
    // VT<R> to R...

    [Fact]
    public static async Task ValTaskRes_ReturnRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().FlatMap((Func<Result>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskRes_ReturnRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskRes_ReturnRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error);
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskRes_ReturnTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().FlatMap((Func<Task<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskRes_ReturnTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskRes_ReturnTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskRes_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().FlatMap((Func<ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskRes_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskRes_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    // R to VT<R>...

    [Fact]
    public static async Task Res_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().FlatMap((Func<ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task Res_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task Res_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //---

    [Fact]
    public static async Task TaskRes_ReturnValTaskRes_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsTask().FlatMap((Func<ValueTask<Result>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskRes_ReturnValTaskRes_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMap((Task<Result>)null, () => Result.Ok().AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskRes_ReturnValTaskRes_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok().AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task TaskRes_ReturnValTaskRes_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    // VT<R> to R<T>...

    [Fact]
    public static async Task ValTaskRes_ReturnResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().FlatMap((Func<Result<string>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskRes_ReturnResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text");
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskRes_ReturnResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error);
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskRes_ReturnTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().FlatMap((Func<Task<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskRes_ReturnTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskRes_ReturnTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //--

    [Fact]
    public static async Task ValTaskRes_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsValueTask().FlatMap((Func<ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task ValTaskRes_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task ValTaskRes_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsValueTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    // R to VT<R>...

    [Fact]
    public static async Task Res_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().FlatMap((Func<ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task Res_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task Res_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }

    //---

    [Fact]
    public static async Task TaskRes_ReturnValTaskResT_okパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => Result.Ok().AsTask().FlatMap((Func<ValueTask<Result<string>>>)null).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskRes_ReturnValTaskResT_sourceパラメーターが指定されていない場合は例外が発生する()
    {
        var act = () => ResultExtensions.FlatMap((Task<Result>)null, () => Result.Ok("text").AsValueTask()).AsTask();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }

    [Fact]
    public static async Task TaskRes_ReturnValTaskResT_失敗の場合はOkアクションは実行されず同じErrorの値が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Error(error).AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Ok("text").AsValueTask();
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public static async Task TaskRes_ReturnValTaskResT_成功の場合はOkアクションが実行されて結果が返る()
    {
        var error = new Error();
        var executed = false;
        var result = await Result.Ok().AsTask().FlatMap(() =>
        {
            executed = true;
            return Result.Error<string>(error).AsValueTask();
        });

        executed.Should().BeTrue();
        result.Should().BeError().And.Match(v => v == error);
    }
}

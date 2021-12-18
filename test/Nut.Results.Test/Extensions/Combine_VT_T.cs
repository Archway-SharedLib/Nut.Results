using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Combine_VT_T
{
    [Fact]
    public async Task ValTaskResT_ResT_rightFuncがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(
            Result.Ok("ok").AsValueTask(),
            (Func<Result<int>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("rightFunc");
    }

    [Fact]
    public async Task ValTaskResT_ResT_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).AsValueTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError());
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task ValTaskResT_ResT_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").AsValueTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError());
        });
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task ValTaskResT_ResT_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok(leftExpect).AsValueTask().Combine(() => Result.Ok<int>(rightExpect));
        result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
    }

    // ------------------------------

    [Fact]
    public async Task ResT_ValueTaskResT_rightFuncがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(
            Result.Ok("ok"),
            (Func<ValueTask<Result<int>>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("rightFunc");
    }

    [Fact]
    public async Task ResT_ValueTaskResT_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsValueTask();
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task ResT_ValueTaskResT_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsValueTask();
        });
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task ResT_ValueTaskResT_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok(leftExpect).Combine(
            () => Result.Ok(rightExpect).AsValueTask());
        result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
    }

    // ------------------------------

    [Fact]
    public async Task ValTaskResT_ValueTaskResT_rightFuncがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(
            Result.Ok("ok").AsValueTask(),
            (Func<ValueTask<Result<int>>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("rightFunc");
    }

    [Fact]
    public async Task ValTaskResT_ValueTaskResT_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).AsValueTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsValueTask();
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task ValTaskResT_ValueTaskResT_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").AsValueTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsValueTask();
        });
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task ValTaskResT_ValueTaskResT_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok(leftExpect).AsValueTask().Combine(
            () => Result.Ok(rightExpect).AsValueTask());
        result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
    }

    // ------------------------------

    [Fact]
    public async Task ValTaskResT_TaskResT_rightFuncがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(
            Result.Ok("ok").AsValueTask(),
            (Func<Task<Result<int>>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("rightFunc");
    }

    [Fact]
    public async Task ValTaskResT_TaskResT_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).AsValueTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsTask();
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task ValTaskResT_TaskResT_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").AsValueTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsTask();
        });
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task ValTaskResT_TaskResT_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok(leftExpect).AsValueTask().Combine(
            () => Result.Ok(rightExpect).AsTask());
        result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
    }

    // ------------------------------

    [Fact]
    public async Task TaskResT_ValTaskResT_sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(
            (Task<Result<string>>)null,
            () => Result.Ok(1).AsValueTask()).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task TaskResT_ValTaskResT_rightFuncがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(
            Result.Ok("ok").AsTask(),
            (Func<ValueTask<Result<int>>>)null).AsTask();
        var res = await act.Should().ThrowAsync<ArgumentNullException>();
        res.And.ParamName.Should().Be("rightFunc");
    }

    [Fact]
    public async Task TaskResT_ValTaskResT_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).AsTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsValueTask();
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task TaskResT_ValTaskResT_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok("ok").AsTask().Combine(() =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsValueTask();
        });
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task TaskResT_ValTaskResT_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok(leftExpect).AsTask().Combine(
            () => Result.Ok(rightExpect).AsValueTask());
        result.Should().BeOk().And.Match(v => v.Left == leftExpect && v.Right == rightExpect);
    }

    public class SourceError : Error { }

    public class DestError : Error { }
}

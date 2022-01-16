using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Combine_T
{
    [Fact]
    public void Func_SyncSync_Destがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(Result.Ok("ok"), (Func<string, Result<int>>)null);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("dest");
    }

    [Fact]
    public void Func_SyncSync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = Result.Error<string>(new SourceError()).Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError());
        });
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public void Func_SyncSync_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = Result.Ok<string>("ok").Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError());
        });
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public void Func_SyncSync_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = Result.Ok<string>(leftExpect).Combine(_ => Result.Ok<int>(rightExpect));
        result.Should().BeOk().And.Match(v => v.Item1 == leftExpect && v.Item2 == rightExpect);
    }

    [Fact]
    public async Task Func_AsyncSync_Sourceがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(null as Task<Result<string>>, _ => Result.Ok("ok"));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task Func_AsyncSync_Destがnullの場合は例外が発生する()
    {
        var act = () => ResultExtensions.Combine(Result.Ok("Ok").AsTask(), (Func<string, Result<int>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("dest");
    }

    [Fact]
    public async Task Func_AsyncSync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).AsTask().Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError());
        }).ConfigureAwait(false);
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task Func_AsyncSync_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok<string>("ok").AsTask().Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError());
        }).ConfigureAwait(false);
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task Func_AsyncSync_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok<string>(leftExpect).AsTask()
            .Combine(_ => Result.Ok<int>(rightExpect)).ConfigureAwait(false);
        result.Should().BeOk().And.Match(v => v.Item1 == leftExpect && v.Item2 == rightExpect);
    }

    [Fact]
    public async Task Func_SyncAsync_Destがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok"), (Func<string, Task<Result<int>>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("dest");
    }

    [Fact]
    public async Task Func_SyncAsync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsTask();
        }).ConfigureAwait(false);
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task Func_SyncAsync_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok<string>("ok").Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsTask();
        }).ConfigureAwait(false);
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task Func_SyncAsync_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok<string>(leftExpect)
            .Combine(_ => Result.Ok<int>(rightExpect).AsTask()).ConfigureAwait(false);
        result.Should().BeOk().And.Match(v => v.Item1 == leftExpect && v.Item2 == rightExpect);
    }

    [Fact]
    public async Task Func_AsyncAsync_Sourceがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Combine(null as Task<Result<string>>, _ => Result.Ok("ok").AsTask());
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task Func_AsyncAsync_Destがnullの場合は例外が発生する()
    {
        Func<Task> act = () => ResultExtensions.Combine(Result.Ok("Ok").AsTask(), (Func<string, Task<Result<int>>>)null);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("dest");
    }

    [Fact]
    public async Task Func_AsyncAsync_Sourceがエラーの場合でDestは実行されずSourceのエラーが返る()
    {
        var executed = false;
        var result = await Result.Error<string>(new SourceError()).AsTask().Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsTask();
        }).ConfigureAwait(false);
        executed.Should().BeFalse();
        result.Should().BeError().And.Match(e => e is SourceError);
    }

    [Fact]
    public async Task Func_AsyncAsync_Destがエラーの場合はDestのエラーが返る()
    {
        var executed = false;
        var result = await Result.Ok<string>("ok").AsTask().Combine(_ =>
        {
            executed = true;
            return Result.Error<int>(new DestError()).AsTask();
        }).ConfigureAwait(false);
        executed.Should().BeTrue();
        result.Should().BeError().And.Match(e => e is DestError);
    }

    [Fact]
    public async Task Func_AsyncAsync_両方ともOkの場合は両方の値のタプルが返る()
    {
        var leftExpect = "ok";
        var rightExpect = 123;
        var result = await Result.Ok<string>(leftExpect).AsTask()
            .Combine(_ => Result.Ok<int>(rightExpect).AsTask()).ConfigureAwait(false);
        result.Should().BeOk().And.Match(v => v.Item1 == leftExpect && v.Item2 == rightExpect);
    }

    public class SourceError : Error { }

    public class DestError : Error { }

}

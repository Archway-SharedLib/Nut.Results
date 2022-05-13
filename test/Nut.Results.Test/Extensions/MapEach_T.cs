using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class MapEach_T
{
    [Fact]
    public void SyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Action act = () => Result.Ok(Enumerable.Range(0, 2)).MapEach(null as Func<int, string>);
        act.Should().Throw<ArgumentNullException>().And.ParamName.Should().Be("ok");
    }

    [Fact]
    public void SyncSync_失敗の場合はActionは実行されず同じErrorの値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<IEnumerable<int>>(error);
        var executed = false;
        var result = errResult.MapEach(_ =>
        {
            executed = true;
            return "Foo";
        });

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public void SyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = Result.Ok(Enumerable.Range(0, 2)).MapEach(v =>
         {
             executed = true;
             return v.ToString();
         });

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }

    [Fact]
    public async Task AsyncSync_sourceパラメーターが指定されていない場合は例外が発生する()
    {

        Func<Task> act = () => (null as Task<Result<IEnumerable<int>>>).MapEach(v => v);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncSync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok(Enumerable.Range(0, 2)).AsTask().MapEach(null as Func<int, string>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncSync_失敗の場合はActionは実行されず同じErrorの値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<IEnumerable<int>>(error).AsTask();
        var executed = false;
        var result = await errResult.MapEach(_ =>
        {
            executed = true;
            return "Foo";
        }).ConfigureAwait(false);

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task AsyncSync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok(Enumerable.Range(0, 2)).AsTask().MapEach(v =>
        {
            executed = true;
            return v.ToString();
        }).ConfigureAwait(false);

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }

    [Fact]
    public async Task SyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok(Enumerable.Range(0, 2)).MapEach(null as Func<int, Task<string>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task SyncAsync_失敗の場合はActionは実行されず同じErrorの値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<IEnumerable<int>>(error);
        var executed = false;
        var result = await errResult.MapEach(_ =>
        {
            executed = true;
            return Task.FromResult("Foo");
        }).ConfigureAwait(false);

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task SyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok(Enumerable.Range(0, 2)).MapEach(v =>
        {
            executed = true;
            return Task.FromResult(v.ToString());
        }).ConfigureAwait(false);

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }

    [Fact]
    public async Task AsyncAsync_sourceパラメーターが指定されていない場合は例外が発生する()
    {

        Func<Task> act = () => (null as Task<Result<IEnumerable<int>>>).MapEach(v => Task.FromResult(v));
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("source");
    }

    [Fact]
    public async Task AsyncAsync_Okパラメーターが指定されていない場合は例外が発生する()
    {
        Func<Task> act = () => Result.Ok(Enumerable.Range(0, 2)).AsTask().MapEach(null as Func<int, Task<string>>);
        var result = await act.Should().ThrowAsync<ArgumentNullException>();
        result.And.ParamName.Should().Be("ok");
    }

    [Fact]
    public async Task AsyncAsync_失敗の場合はActionは実行されず同じErrorの値が返る()
    {
        var error = new Exception();
        var errResult = Result.Error<IEnumerable<int>>(error).AsTask();
        var executed = false;
        var result = await errResult.MapEach(_ =>
        {
            executed = true;
            return Task.FromResult("Foo");
        }).ConfigureAwait(false);

        executed.Should().BeFalse();
        result.Should().BeError().And.Match(v => v == error);
    }

    [Fact]
    public async Task AsyncAsync_成功の場合はアクションが実行されて結果がResultで返る()
    {
        var executed = false;
        var result = await Result.Ok(Enumerable.Range(0, 2)).AsTask().MapEach(v =>
        {
            executed = true;
            return Task.FromResult(v.ToString());
        }).ConfigureAwait(false);

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }
}

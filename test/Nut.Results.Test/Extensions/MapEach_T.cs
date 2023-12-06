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
    public void SyncSync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = 0;
        var result = Result.Ok(Enumerable.Range(0, 2)).MapEach(v =>
        {
            executed += 1;
            return ""[0].ToString(); // throw exception;
        });

        executed.Should().Be(1);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        });

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
        });

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }

    [Fact]
    public async Task AsyncSync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = 0;
        var result = await Result.Ok(Enumerable.Range(0, 2)).AsTask().MapEach(v =>
        {
            executed += 1;
            return ""[0].ToString(); // throw exception;
        });

        executed.Should().Be(1);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    [Fact]
    public async Task AsyncSync_Taskで例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = 0;
        var result = await Task.Run(() =>
        {
            ""[0].ToString(); // throw exception;
            return Result.Ok(Enumerable.Range(0, 2));
        }).MapEach(v =>
        {
            executed += 1;
            return v;
        });

        executed.Should().Be(0);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        });

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
        });

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }

    [Fact]
    public async Task SyncAsync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = 0;
        var result = await Result.Ok(Enumerable.Range(0, 2)).MapEach(v =>
        {
            executed += 1;
            return Task.FromResult(""[0].ToString()); // throw exception;
        });

        executed.Should().Be(1);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
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
        });

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
        });

        result.Should().BeOk();
        executed.Should().BeTrue();
        var values = result.Get();
        values.Should().HaveCount(2).And.BeEquivalentTo("0", "1");
    }

    [Fact]
    public async Task AsyncAsync_処理内で例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = 0;
        var result = await Result.Ok(Enumerable.Range(0, 2)).AsTask().MapEach(v =>
        {
            executed += 1;
            return Task.FromResult(""[0].ToString()); // throw exception;
        });

        executed.Should().Be(1);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

    [Fact]
    public async Task AsyncAsync_Taskで例外が発生した場合は新しい失敗の結果が返る()
    {
        var executed = 0;
        var result = await Task.Run(() =>
        {
            ""[0].ToString(); // throw exception;
            return Result.Ok(Enumerable.Range(0, 2));
        }).MapEach(v =>
        {
            executed += 1;
            return Task.FromResult(v);
        });

        executed.Should().Be(0);
        result.Should().BeError().And.BeOfType<IndexOutOfRangeException>();
    }

}

using System;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ThrowIfError_T
{
    [Fact]
    public void 失敗の場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new InvalidOperationException()).ThrowIfError();
        act.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void 成功の場合は例外が発生せず値がそのまま返る()
    {
        var act = () => Result.Ok("abc").ThrowIfError();
        act.Should().NotThrow().And.Subject().Should().BeOk();
    }

    [Fact]
    public async Task Async_失敗の場合は例外が発生する()
    {
        var act = () => Result.Error<string>(new InvalidOperationException()).AsTask().ThrowIfError();
        await act.Should().ThrowAsync<InvalidOperationException>();
    }

    [Fact]
    public async void Async_成功の場合は例外が発生せず値がそのまま返る()
    {
        var act = () => Result.Ok("abc").AsTask().ThrowIfError();
        await act.Should().NotThrowAsync();
    }

    [Fact]
    public async void Async_nullの場合は例外が発生する()
    {
        var act = () => ((Task<Result<string>>)null)!.ThrowIfError();
        await act.Should().ThrowAsync<ArgumentNullException>();
    }


    [Fact]
    public void 例外のスタックトレースはキャプチャされる()
    {
        var act = () => Throw1(Result.Error<string>(Create1()));
        var exception = act.Should().Throw<InvalidCastException>().Subject.First();
        exception.StackTrace.Split('\n')[0].Should().Contain(nameof(Create4));
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private Exception Create1() => Create2();

    [MethodImpl(MethodImplOptions.NoInlining)]
    private Exception Create2() => Create3();

    [MethodImpl(MethodImplOptions.NoInlining)]
    private Exception Create3() => Create4();

    [MethodImpl(MethodImplOptions.NoInlining)]
    private Exception Create4()
    {
        try
        {
            throw new InvalidCastException();
        }
        catch (Exception e)
        {
            return e;
        }
    }

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Throw1(Result<string> result) => Throw2(result);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Throw2(Result<string> result) => Throw3(result);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Throw3(Result<string> result) => Throw4(result);

    [MethodImpl(MethodImplOptions.NoInlining)]
    private void Throw4(Result<string> result) => result.ThrowIfError();

}

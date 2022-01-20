using System;
using System.Linq;
using FluentAssertions;
using Nut.Results.FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class Merge_Tuple
{
    [Fact]
    public void 全て成功の場合は成功が返る()
    {
        var result = Result.Ok("1").Merge(Result.Ok(1), Result.Ok(true));
        result.Should().BeOk().And.Match(v => v.Item1 == "1" && v.Item2 == 1 && v.Item3);
    }

    [Fact]
    public void エラーが一つ以上あった場合は失敗が返る()
    {
        var result = Result.Ok("1").Merge(Result.Error<int>("IntError"), Result.Ok(true), Result.Error<DateTime>("DateTimeError"));
        result.Should().BeError().And.Match(e => e is AggregateError);
        var err = result.GetError() as AggregateError;
        err.Errors.Should().HaveCount(2);
        err.Errors.First().Message.Should().Be("IntError");
        err.Errors.Skip(1).First().Message.Should().Be("DateTimeError");
    }
}

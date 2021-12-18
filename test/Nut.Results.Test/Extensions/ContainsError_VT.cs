using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Nut.Results.Test;

public class ContainsError_VT
{


    [Fact]
    public async Task Async_値が一致する場合はtrueが返る()
    {
        var err = new Error();
        (await Result.Error(err).AsValueTask().ContainsError(err)).Should().BeTrue();
    }

    [Fact]
    public async Task Async_値が一致しない場合はfalseが返る()
    {
        var err = new Error();
        (await Result.Error(err).AsValueTask().ContainsError(new Error()))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_成功の場合はfalseが返る()
    {
        (await Result.Ok().AsValueTask().ContainsError(new Error()))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_値が一致する場合はtrueが返る()
    {
        var err = new Error();
        (await Result.Error(err).AsValueTask()
            .ContainsError(err, EqualityComparer<IError>.Default))
            .Should().BeTrue();
    }

    [Fact]
    public async Task Async_Eq_値が一致しない場合はfalseが返る()
    {
        var err = new Error();
        (await Result.Error(err).AsValueTask()
                .ContainsError(new Error(), EqualityComparer<IError>.Default))
            .Should().BeFalse();
    }

    [Fact]
    public async Task Async_Eq_成功の場合はfalseが返る()
    {
        (await Result.Ok().AsValueTask()
            .ContainsError(new Error(), EqualityComparer<IError>.Default))
            .Should().BeFalse();
    }
}

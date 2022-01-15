using FluentAssertions;
using Xunit;
using Nut.Results.FluentAssertions;

namespace Nut.Results.Test;

public class DisallowOperation
{
    [Fact]
    public void Resultのdefault()
    {
        void Check(Result res)
        {
            res.Should().BeError();
            res.GetError().Should().BeNull();
        }

        Check(default);
    }

    [Fact]
    public void Resultのnew()
    {
        void Check(Result res)
        {
            res.Should().BeError();
            res.GetError().Should().BeNull();
        }

        Check(new Result());
    }

    [Fact]
    public void ResultTのdefault()
    {
        void Check<T>(Result<T> res)
        {
            res.Should().BeError();
            res.GetError().Should().BeNull();
        }

        Check<string>(default);
        Check<int>(default);
    }

    [Fact]
    public void ResultTのnew()
    {
        void Check<T>(Result<T> res)
        {
            res.Should().BeError();
            res.GetError().Should().BeNull();
        }

        Check(new Result<string>());
        Check<int>(new Result<int>());
    }
}

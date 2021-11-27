using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Nut.Results.FluentAssertions;

public class ResultAssertions<T>
{
    private readonly Result<T> _instance;

    public ResultAssertions(Result<T> instance)
    {
        _instance = instance;
    }

    public AndConstraint<ResultOkAssertions<T>> BeOk(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.IsOk)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected ok, but error.");
        return new AndConstraint<ResultOkAssertions<T>>(new ResultOkAssertions<T>(_instance));
    }

    public AndConstraint<ResultAssertions<T>> Be(Result<T> expected,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("not equal values");
        return new AndConstraint<ResultAssertions<T>>(new ResultAssertions<T>(_instance));
    }

    public AndConstraint<ResultErrorAssertions> BeError(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.IsError)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected error, but ok.");
        return new AndConstraint<ResultErrorAssertions>(new ResultErrorAssertions(_instance.GetError()));
    }
}

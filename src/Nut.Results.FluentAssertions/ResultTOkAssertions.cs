using FluentAssertions;
using FluentAssertions.Execution;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Nut.Results.FluentAssertions;

public class ResultOkAssertions<T>
{
    private readonly Result<T> _instance;

    public ResultOkAssertions(Result<T> instance)
    {
        _instance = instance;
    }

    public AndConstraint<ResultOkAssertions<T>> Match(Expression<Func<T, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(predicate.Compile()(_instance.Get()))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected value to match {0}{reason}, but found {1}.", (object)predicate.Body, (object)_instance.Get()!);
        return new AndConstraint<ResultOkAssertions<T>>(this);
    }
}

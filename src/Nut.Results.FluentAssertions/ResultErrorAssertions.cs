using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Nut.Results.FluentAssertions;

public class ResultErrorAssertions
{
    private readonly IError _instance;

    public ResultErrorAssertions(IError instance)
    {
        _instance = instance;
    }

    public AndConstraint<ResultErrorAssertions> Match(Expression<Func<IError, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(predicate.Compile()(_instance))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected value to match {0}{reason}, but found {1}.", (object)predicate.Body, (object)_instance);
        return new AndConstraint<ResultErrorAssertions>(this);
    }

    public AndConstraint<ResultErrorAssertions> BeOfType<T>(
        string because = "",
        params object[] becauseArgs)
    {
        BeOfType(typeof(T), because, becauseArgs);
        return new AndConstraint<ResultErrorAssertions>(this);
    }

    public AndConstraint<ResultErrorAssertions> BeOfType(
        Type expectedType,
        string because = "",
        params object[] becauseArgs)
    {
        (new ObjectAssertions(_instance)).BeOfType(expectedType, because, becauseArgs);
        return new AndConstraint<ResultErrorAssertions>(this);
    }

    public virtual AndConstraint<ResultErrorAssertions> WithMessage(
        string expectedWildcardPattern,
        string because = "",
        params object[] becauseArgs)
    {
        (new StringAssertions(_instance.Message)).Be(expectedWildcardPattern, because, because);

        return new AndConstraint<ResultErrorAssertions>(this);
    }
}

using FluentAssertions;
using FluentAssertions.Execution;

namespace Nut.Results.FluentAssertions;

public class ResultAssertions
{
    private readonly Result _instance;

    public ResultAssertions(Result instance)
    {
        _instance = instance;
    }

    public AndConstraint<ResultAssertions> Be(Result expected,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("not equal values.");
        return new AndConstraint<ResultAssertions>(new ResultAssertions(_instance));
    }

    public AndConstraint<ResultAssertions> BeOk(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.IsOk)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected ok, but error.");
        return new AndConstraint<ResultAssertions>(new ResultAssertions(_instance));
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

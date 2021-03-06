using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Nut.Results.FluentAssertions
{

    public class ResultAssertions
    {
        private readonly Result instance;
    
        public ResultAssertions(Result instance)
        {
            this.instance = instance;
        }

        public AndConstraint<ResultAssertions> Be(Result expected,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(instance.Equals(expected))
                .BecauseOf(because, becauseArgs)
                .FailWith("not equal values.");
            return new AndConstraint<ResultAssertions>(new ResultAssertions(instance));
        }
        
        public AndConstraint<ResultAssertions> BeOk(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(this.instance.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected ok, but error.");
            return new AndConstraint<ResultAssertions>(new ResultAssertions(instance));
        }
    
        public AndConstraint<ResultErrorAssertions> BeError(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(this.instance.IsError)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected error, but ok.");
            return new AndConstraint<ResultErrorAssertions>(new ResultErrorAssertions(instance.GetError()));
        }
    }
}
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace Nut.Results.FluentAssertions
{
    public class ResultErrorAssertions
    {
        private readonly IError instance;

        public ResultErrorAssertions(IError instance)
        {
            this.instance = instance;
        }

        public AndConstraint<ResultErrorAssertions> Match(Expression<Func<IError, bool>> predicate,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(predicate.Compile()(this.instance))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected value to match {0}{reason}, but found {1}.", (object)predicate.Body, (object)this.instance);
            return new AndConstraint<ResultErrorAssertions>(this);
        }

        public AndConstraint<ResultErrorAssertions> BeOfType<T>(
            string because = "",
            params object[] becauseArgs)
        {
            this.BeOfType(typeof(T), because, becauseArgs);
            return new AndConstraint<ResultErrorAssertions>(this);
        }

        public AndConstraint<ResultErrorAssertions> BeOfType(
            Type expectedType,
            string because = "",
            params object[] becauseArgs)
        {
            (new ObjectAssertions(instance)).BeOfType(expectedType, because, becauseArgs);
            return new AndConstraint<ResultErrorAssertions>(this);
        }

        public virtual AndConstraint<ResultErrorAssertions> WithMessage(
            string expectedWildcardPattern,
            string because = "",
            params object[] becauseArgs)
        {
            (new StringAssertions(instance.Message)).Be(expectedWildcardPattern, because, because);

            return new AndConstraint<ResultErrorAssertions>(this);
        }
    }
}

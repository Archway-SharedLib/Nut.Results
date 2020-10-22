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
    
    public class ResultAssertions<T>
    {
        private readonly Result<T> instance;

        public ResultAssertions(Result<T> instance)
        {
            this.instance = instance;
        }

        public AndConstraint<ResultOkAssertions<T>> BeOk(
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(this.instance.IsOk)
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected ok, but error.");
            return new AndConstraint<ResultOkAssertions<T>>(new ResultOkAssertions<T>(instance));
        }
        
        public AndConstraint<ResultAssertions<T>> Be(Result<T> expected,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(instance.Equals(expected))
                .BecauseOf(because, becauseArgs)
                .FailWith("not equal values");
            return new AndConstraint<ResultAssertions<T>>(new ResultAssertions<T>(instance));
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

    public class ResultOkAssertions<T>
    {
        private readonly Result<T> instance;

        public ResultOkAssertions(Result<T> instance)
        {
            this.instance = instance;
        }

        public AndConstraint<ResultOkAssertions<T>> Match(Expression<Func<T, bool>> predicate,
            string because = "",
            params object[] becauseArgs)
        {
            Execute.Assertion.ForCondition(predicate.Compile()(this.instance.Get()))
                .BecauseOf(because, becauseArgs)
                .FailWith("Expected value to match {0}{reason}, but found {1}.", (object) predicate.Body, (object) this.instance.Get()!);
            return new AndConstraint<ResultOkAssertions<T>>(this);
        }
    }
    
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
                .FailWith("Expected value to match {0}{reason}, but found {1}.", (object) predicate.Body, (object) this.instance);
            return new AndConstraint<ResultErrorAssertions>(this);
        }
        
        public AndConstraint<ResultErrorAssertions> BeOfType<T>(
            string because = "",
            params object[] becauseArgs)
        {
            this.BeOfType(typeof (T), because, becauseArgs);
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
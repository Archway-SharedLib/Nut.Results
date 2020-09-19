using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public static Result Ok() => new Result(null, true);

        public static Result Error(IError error) => new Result(error, false);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, default!, true);

        public static Result<T> Error<T>(IError error) => new Result<T>(default!, error, false);

        

        public static bool IsResultType(Type target) => IsVoidResultType(target) || IsGenericResultType(target);

        public static bool IsVoidResultType(Type target) => target == typeof(Result);

        public static bool IsGenericResultType(Type target) =>
            target.IsGenericType && target.GetGenericTypeDefinition() == typeof(Result<>);


        public static Type GetOkType(Type target)
        {
            if (!IsGenericResultType(target))
            {
                throw new InvalidOperationException("Parameter is not Result<> type");
            }
            return target.GetGenericArguments().First();
        }

        //public static Result<T> AggregateErrors<T>(Func<IErrorAggregator, T> func)
        //{
        //    var aggregator = new ErrorAggregator();
        //    var result = func(aggregator);
        //    return !aggregator.Errors.Any() ? Ok(result) : Error<T>(new AggregateError(aggregator.Errors));
        //}
    }

    //public interface IErrorAggregator
    //{
    //    Result<T> Try<T>(Func<T> func);
    //    Result<T> Run<T>(Func<Result<T>> func);
    //}

    //public class ErrorAggregator : IErrorAggregator
    //{
    //    public List<IError> Errors { get; } = new List<IError>();

    //    public Result<T> Try<T>(Func<T> func)
    //    {
    //        try
    //        {
    //            return Result.Ok(func());
    //        }
    //        catch (Exception e)
    //        {
    //            var error = new ExceptionError(e);
    //            Errors.Add(error);
    //            return Result.Error<T>(error);
    //        }
    //    }

    //    public Result<T> Run<T>(Func<Result<T>> func)
    //    {
    //        var result = func();
    //        if (!result.IsError) return result;
    //        var error = result.GetError();
    //        Errors.Add(error);
    //        return result;
    //    }
    //}

    
}

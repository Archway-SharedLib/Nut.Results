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
    }
}

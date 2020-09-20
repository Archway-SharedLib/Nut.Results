using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public static bool IsResultType(Type target) => IsVoidResultType(target) || IsGenericResultType(target);

        public static bool IsVoidResultType(Type target) => target == typeof(Result);

        public static bool IsGenericResultType(Type target) =>
            target.IsGenericType && target.GetGenericTypeDefinition() == typeof(Result<>);


        public static Type GetOkType(Type target)
        {
            if (!IsGenericResultType(target))
            {
                throw new ArgumentException("Parameter is not Result<T> type");
            }
            return target.GetGenericArguments().First();
        }
    }
}

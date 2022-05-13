using System;
using System.Linq.Expressions;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results.Internals;

internal static class ErrorResultFactory<T>
{
    public static readonly Func<Exception, T> Create = CreateFactory();

    private static Func<Exception, T> CreateFactory()
    {
        if (!ResultHelper.IsResultType(typeof(T)))
        {
            throw new InvalidOperationException(SR.Exception_TypeParameterNeedResultType);
        }

        if (ResultHelper.IsWithValueResultType(typeof(T)))
        {
            // Result<T>
            var genericArg = ResultHelper.GetOkType(typeof(T));
            var parameter = Expression.Parameter(typeof(Exception));
            var call = Expression.Call(typeof(Result), nameof(Result.Error),
                new Type[] { genericArg }, parameter);
            return Expression.Lambda<Func<Exception, T>>(call, parameter).Compile();
        }
        else
        {
            // Result
            var parameter = Expression.Parameter(typeof(Exception));
            var call = Expression.Call(typeof(Result), nameof(Result.Error), null, parameter);
            return Expression.Lambda<Func<Exception, T>>(call, parameter).Compile();
        }
    }
}

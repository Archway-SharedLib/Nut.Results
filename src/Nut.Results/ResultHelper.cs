using Nut.Results.Internals;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nut.Results
{
    public static class ResultHelper
    {
        public static bool IsResultType(Type target) =>
            IsWithValueResultType(target) || IsNoValueResultType(target);
        public static bool IsWithValueResultType(Type target) =>
            target.IsGenericType && target.GetGenericTypeDefinition() == typeof(Result<>);

        public static bool IsNoValueResultType(Type target) =>
            target == typeof(Result);

        public static Type GetOkType(Type target)
        {
            if (!IsWithValueResultType(target))
            {
                throw new InvalidOperationException(SR.Exception_ParameterIsNotResultT);
            }
            return target.GetGenericArguments().First();
        }

        public static T CreateErrorResult<T>(IError errorValue)
        {
            try
            {
                return ErrorResultFactory<T>.Create(errorValue);
            } 
            catch(TypeInitializationException e) when (e.InnerException is InvalidOperationException)
            {
                throw e.InnerException;
            }
        }
    }
}

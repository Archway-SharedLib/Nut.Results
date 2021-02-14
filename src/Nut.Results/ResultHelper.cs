using Nut.Results.Internals;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Nut.Results
{
    public static class ResultHelper
    {
        public static bool IsResultType([NotNullWhen(true)]Type? target) =>
            IsWithValueResultType(target) || IsNoValueResultType(target);
        
        public static bool IsWithValueResultType([NotNullWhen(true)]Type? target) =>
            target is not null &&
            target.IsGenericType && 
            target.GetGenericTypeDefinition() == typeof(Result<>);

        public static bool IsNoValueResultType([NotNullWhen(true)]Type? target) =>
            target == typeof(Result);

        public static Type GetOkType(Type target)
        {
            if (!TryGetOkType(target, out var result))
            {
                throw new InvalidOperationException(SR.Exception_ParameterIsNotResultT);
            }
            return result;
        }

        public static bool TryGetOkType(Type target, [NotNullWhen(true)]out Type? okType)
        {
            okType = null;
            if (!IsWithValueResultType(target)) return false;
            okType = target.GetGenericArguments().First();
            return true;
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

        public static bool TryGetOkValue<T>(object source, [NotNullWhen(true)]out T? value)
        {
            value = default;
            var sourceType = source?.GetType();
            if (!IsWithValueResultType(sourceType)) return false;
            
            // cache?
            var isOkProp = sourceType.GetProperty("IsOk", BindingFlags.Instance | BindingFlags.Public);
            if (!(bool)isOkProp!.GetValue(source))
            {
                return false;
            }
            
            // cache?
            var valueField = sourceType.GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldValue = valueField!.GetValue(source);
            if (fieldValue is not T castValue) return false;

            value = castValue;
            return true;
        }
        
        public static bool TryGetErrorValue(object source, [NotNullWhen(true)]out IError? value)
        {
            value = null;
            var sourceType = source?.GetType();
            if (!IsResultType(sourceType)) return false;
            
            // cache?
            var isErrorProp = sourceType.GetProperty("IsError", BindingFlags.Instance | BindingFlags.Public);
            if (!(bool)isErrorProp!.GetValue(source))
            {
                return false;
            }
            
            // cache?
            var errValueField = sourceType.GetField("errorValue", BindingFlags.Instance | BindingFlags.NonPublic);
            var fieldValue = errValueField!.GetValue(source);
            value = (IError)fieldValue;
            return true;
        }
    }
}

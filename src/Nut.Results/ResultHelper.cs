using Nut.Results.Internals;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
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

        private static readonly ConcurrentDictionary<Type, Accessor> cache = new ();
        public static bool TryGetOkValue<T>(object source, [NotNullWhen(true)]out T? value)
        {
            value = default;
            var sourceType = source?.GetType();
            if (!IsWithValueResultType(sourceType)) return false;
            var accessor = cache.GetOrAdd(sourceType, t => new Accessor(t));
            if (!accessor.GetIsOk(source!)) return false;
            var fieldValue = accessor.GetValue!(source!);
            if (fieldValue is not T castValue) return false;
            value = castValue!;
            return true;
        }
        
        public static bool TryGetErrorValue(object source, [NotNullWhen(true)]out IError? value)
        {
            value = null;
            var sourceType = source?.GetType();
            if (!IsResultType(sourceType)) return false;
            var accessor = cache.GetOrAdd(sourceType, t => new Accessor(t));
            if (!accessor.GetIsError(source!)) return false;
            value = accessor.GetErrorValue(source!);
            return true;
        }
        
        private class Accessor
        {
            public Accessor(Type target)
            {
                this.GetValue = GetValueExpression(target);
                this.GetErrorValue = GetErrorValueExpression(target);
                this.GetIsOk = GetBoolPropertyExpression(target, "IsOk");
                this.GetIsError = GetBoolPropertyExpression(target, "IsError");
            }
            public Func<object, object>? GetValue { get; }
            
            public Func<object, IError> GetErrorValue { get; }
            
            public Func<object, bool> GetIsOk { get; }
            
            public Func<object, bool> GetIsError { get; }

            private static Func<object, object>? GetValueExpression(Type sourceType)
            {
                var fieldInfo = sourceType.GetField("value", BindingFlags.Instance | BindingFlags.NonPublic);
                if (fieldInfo is null) return null;
                var sourceParam = Expression.Parameter(typeof(object));
                var returnExpression = Expression.Field(Expression.Convert(sourceParam, sourceType), fieldInfo!);
                var lambda = Expression.Lambda(returnExpression, sourceParam);
                return (Func<object, object>)lambda.Compile();
            }
            
            private static Func<object, IError> GetErrorValueExpression(Type sourceType)
            {
                var fieldInfo = sourceType.GetField("errorValue", BindingFlags.Instance | BindingFlags.NonPublic);
                var sourceParam = Expression.Parameter(typeof(object));
                var returnExpression = Expression.Field(Expression.Convert(sourceParam, sourceType), fieldInfo!);
                var lambda = Expression.Lambda(returnExpression, sourceParam);
                return (Func<object, IError>)lambda.Compile();
            }
        
            private static Func<object, bool> GetBoolPropertyExpression(Type sourceType, string propertyName)
            {
                var propertyInfo = sourceType.GetProperty(propertyName, BindingFlags.Instance | BindingFlags.Public);
                var sourceObjectParam = Expression.Parameter(typeof(object));
                var returnExpression = 
                    Expression.Call(Expression.Convert
                        (sourceObjectParam, sourceType), propertyInfo!.GetMethod);
                return (Func<object, bool>)Expression.Lambda
                    (returnExpression, sourceObjectParam).Compile();
            }
        }
    }
}

﻿using Nut.Results.Internals;
using System;
using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results
{
    /// <summary>
    /// <see cref="Result"/> および <see cref="Result{T}"/> を操作するためのヘルパーメソッドを提供します。
    /// </summary>
    public static class ResultHelper
    {
        /// <summary>
        /// 指定された型が <see cref="Result"/> または <see cref="Result{T}"/> かどうかを取得します。
        /// </summary>
        /// <param name="target">検証する型</param>
        /// <returns><see cref="Result"/> または <see cref="Result{T}"/>の場合は true 、そうでない場合は false</returns>
        public static bool IsResultType([NotNullWhen(true)]Type? target) =>
            IsWithValueResultType(target) || IsNoValueResultType(target);

        /// <summary>
        /// 指定された型が または <see cref="Result{T}"/> かどうかを取得します。
        /// </summary>
        /// <param name="target">検証する型</param>
        /// <returns><see cref="Result{T}"/>の場合は true 、そうでない場合は false</returns>
        public static bool IsWithValueResultType([NotNullWhen(true)]Type? target) =>
            target is not null &&
            target.IsGenericType && 
            target.GetGenericTypeDefinition() == typeof(Result<>);

        /// <summary>
        /// 指定された型が <see cref="Result"/>かどうかを取得します。
        /// </summary>
        /// <param name="target">検証する型</param>
        /// <returns><see cref="Result"/>の場合は true 、そうでない場合は false</returns>
        public static bool IsNoValueResultType([NotNullWhen(true)]Type? target) =>
            target == typeof(Result);

        /// <summary>
        /// 指定された <see cref="Type"/> が <see cref="Result{T}"/> だった場合に型パラメーターの型を取得します。
        /// </summary>
        /// <param name="target">取得する型</param>
        /// <returns><see cref="Result{T}"/> だった場合の型パラメーターの型</returns>
        public static Type GetOkType(Type target)
        {
            if (!TryGetOkType(target, out var result))
            {
                throw new InvalidOperationException(SR.Exception_ParameterIsNotResultT);
            }
            return result;
        }

        /// <summary>
        /// 指定された <see cref="Type"/> が <see cref="Result{T}"/> だった場合に型パラメーターの型を取得します。
        /// </summary>
        /// <param name="target">取得する型</param>
        /// <param name="okType"><see cref="Result{T}"/> だった場合の型パラメーターの型</param>
        /// <returns>型パラメーターの型が取得できた場合は true 、そうでない場合は false</returns>
        public static bool TryGetOkType(Type target, [NotNullWhen(true)]out Type? okType)
        {
            okType = null;
            if (!IsWithValueResultType(target)) return false;
            okType = target.GetGenericArguments().First();
            return true;
        }

        /// <summary>
        /// 指定された成功の型で失敗の結果を作成します。
        /// </summary>
        /// <typeparam name="T">成功の値の型</typeparam>
        /// <param name="errorValue">失敗の値</param>
        /// <returns>作成した失敗の結果</returns>
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

        /// <summary>
        /// 複数の <see cref="Result"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <param name="results">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static Result Merge(params Result[] results)
        {
            if (results is null) throw new ArgumentNullException(nameof(results));
            
            var errors = results.Where(r => r.IsError).Select(r => r.GetError());
            return errors.Any() ? Result.Error(new AggregateError(errors)) : Result.Ok();
        }

        /// <summary>
        /// 複数の <see cref="Result"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <param name="results">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static async Task<Result> MergeAsync(params Task<Result>[] results)
        {
            if (results is null) throw new ArgumentNullException(nameof(results));
            var r = await Task.WhenAll(results).ConfigureAwait(false);
            return Merge(r);
        }

        /// <summary>
        /// 複数の <see cref="Result{T}"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <typeparam name="T">結果の値の型</typeparam>
        /// <param name="results">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static Result<T[]> Merge<T>(params Result<T>[] results)
        {
            if (results is null) throw new ArgumentNullException(nameof(results));

            var errors = results.Where(r => r.IsError).Select(r => r.GetError());
            return errors.Any() ? Result.Error<T[]>(new AggregateError(errors)) : Result.Ok(results.Select(r => r.Get()).ToArray());
        }

        /// <summary>
        /// 複数の <see cref="Result{T}"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <typeparam name="T">結果の値の型</typeparam>
        /// <param name="results">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static async Task<Result<T[]>> MergeAsync<T>(params Task<Result<T>>[] results)
        {
            if (results is null) throw new ArgumentNullException(nameof(results));

            var r = await Task.WhenAll(results).ConfigureAwait(false);
            return Merge(r);
        }

        private static readonly ConcurrentDictionary<Type, Accessor> cache = new ();

        /// <summary>
        /// 指定されたオブジェクトが <see cref="Result{T}"/> で結果が成功だった場合に成功の値を取得します。
        /// </summary>
        /// <typeparam name="T">成功の値の型</typeparam>
        /// <param name="source">元となるオブジェクトの値</param>
        /// <param name="value">取得した成功の値</param>
        /// <returns>成功の値が取得できた場合は true 、そうでない場合は false</returns>
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

        /// <summary>
        /// 指定されたオブジェクトが <see cref="Result"/> または <see cref="Result{T}"/> で結果が成功だった場合に成功の値を取得します。
        /// </summary>
        /// <param name="source">元となるオブジェクトの値</param>
        /// <param name="value">取得した失敗の値</param>
        /// <returns>失敗の値が取得できた場合は true 、そうでない場合は false</returns>
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

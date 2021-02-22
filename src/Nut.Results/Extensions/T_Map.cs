using System;
using System.Threading.Tasks;
using Nut.Results.Internals;

// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        //sync - sync T1 -> T2
        public static Result<TResult> Map<T, TResult>(this in Result<T> source, Func<T, TResult> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);
        
            var newValue = ok(source.value);
            
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        
        //async - sync T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source, Func<T, TResult> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            
            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result.errorValue);
        
            var newValue = ok(result.value);
           
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        
        //sync - async T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Result<T> source, Func<T, Task<TResult>> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);

            var newValue = await ok(source.value).ConfigureAwait(false);
            ;
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        
        //async - async T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source, Func<T, Task<TResult>> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            
            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result.errorValue);
        
            var newValue = await ok(result.value).ConfigureAwait(false);
 
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
    }
}
using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultTMapExtensions
    {
        //sync - sync T1 -> T2
        public static Result<TResult> Map<T, TResult>(this Result<T> source, Func<T, TResult> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);
        
            var newValue = ok(source.value);
            return Result.Ok(newValue);
        }
        
        //async - sync T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source, Func<T, TResult> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            
            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result.errorValue);
        
            var newValue = ok(result.value);
            return Result.Ok(newValue);
        }
        
        //sync - async T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Result<T> source, Func<T, Task<TResult>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);

            var newValue = await ok(source.value);
            return Result.Ok(newValue);
        }
        
        //async - async T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source, Func<T, Task<TResult>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            
            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result.errorValue);
        
            var newValue = await ok(result.value).ConfigureAwait(false);
            return Result.Ok(newValue);
        }
        
        // rename to omit
        
        //sync - sync T1 -> Void
        // public static Result Map<T>(this Result<T> source, Action<T> ok)
        // {
        //     if (ok == null) throw new ArgumentNullException(nameof(ok));
        //     if (!source.IsOk) return Result.Error(source.errorValue);
        //
        //     ok(source.value);
        //     return Result.Ok();
        // }
        //
        // //async - sync T1 -> Void
        // public static async Task<Result> Map<T>(this Task<Result<T>> source, Action<T> ok)
        // {
        //     if (source == null) throw new ArgumentNullException(nameof(source));
        //     if (ok == null) throw new ArgumentNullException(nameof(ok));
        //     
        //     var result = await source.ConfigureAwait(false);
        //     if (!result.IsOk) return Result.Error(result.errorValue);
        //
        //     ok(result.value);
        //     return Result.Ok();
        // }
        //
        // //sync - async T1 -> Void
        // public static async Task<Result> Map<T>(this Result<T> source, Func<T, Task> ok)
        // {
        //     if (ok == null) throw new ArgumentNullException(nameof(ok));
        //     if (!source.IsOk) return Result.Error(source.errorValue);
        //
        //     await ok(source.value);
        //     return Result.Ok();
        // }
        //
        // //async - async T1 -> Void 
        // public static async Task<Result> Map<T>(this Task<Result<T>> source, Func<T, Task> ok)
        // {
        //     if (source == null) throw new ArgumentNullException(nameof(source));
        //     if (ok == null) throw new ArgumentNullException(nameof(ok));
        //     
        //     var result = await source.ConfigureAwait(false);
        //     if (!result.IsOk) return Result.Error(result.errorValue);
        //
        //     await ok(result.value).ConfigureAwait(false);
        //     return Result.Ok();
        // }
    }
}
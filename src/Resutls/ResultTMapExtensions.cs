using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultTMapExtensions
    {
        //sync - sync T1 -> T2
        public static Result<TResult> Map<T, TResult>(this in Result<T> source, Func<T, TResult> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);
        
            var newValue = ok(source.value);
            CheckReturnValueNotNull(newValue);
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
            CheckReturnValueNotNull(newValue);
            return Result.Ok(newValue);
        }
        
        //sync - async T1 -> T2
        public static async Task<Result<TResult>> Map<T, TResult>(this Result<T> source, Func<T, Task<TResult>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);

            var newValue = await ok(source.value).ConfigureAwait(false);
            CheckReturnValueNotNull(newValue);
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
            CheckReturnValueNotNull(newValue);
            return Result.Ok(newValue);
        }

        private static void CheckReturnValueNotNull<T>(T returnValue)
        {
            if(returnValue is null) throw new InvalidReturnValueException("cannot set null to return value");
        }
    }
}
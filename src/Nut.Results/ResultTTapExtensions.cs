using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static class ResultTTapExtensions
    {
        //sync - sync
        public static Result<T> Tap<T>(this in Result<T> source, Action<T> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) ok(source.value);
            return source;
        }
        
        //async - sync
        public static async Task<Result<T>> Tap<T>(this Task<Result<T>> source, Action<T> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) ok(result.value);
            return result;
        }
        
        //sync - async
        public static async Task<Result<T>> Tap<T>(this Result<T> source, Func<T, Task> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) await ok(source.value).ConfigureAwait(false);
            return source;
        }
        
        //async - async
        public static async Task<Result<T>> Tap<T>(this Task<Result<T>> source, Func<T, Task> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) await ok(result.value).ConfigureAwait(false);
            return result;
        }
    }
}
using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        //sync - sync
        public static Result<T> Tap<T>(this in Result<T> source, Action<T> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) ok(source.value);
            return source;
        }
        
        //async - sync
        public static async Task<Result<T>> Tap<T>(this Task<Result<T>> source, Action<T> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) ok(result.value);
            return result;
        }
        
        //sync - async
        public static async Task<Result<T>> Tap<T>(this Result<T> source, Func<T, Task> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) await ok(source.value).ConfigureAwait(false);
            return source;
        }
        
        //async - async
        public static async Task<Result<T>> Tap<T>(this Task<Result<T>> source, Func<T, Task> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) await ok(result.value).ConfigureAwait(false);
            return result;
        }
    }
}
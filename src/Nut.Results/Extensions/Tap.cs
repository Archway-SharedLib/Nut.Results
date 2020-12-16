using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        //sync - sync
        public static Result Tap(this in Result source, Action ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) ok();
            return source;
        }
        
        //async - sync
        public static async Task<Result> Tap(this Task<Result> source, Action ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) ok();
            return result;
        }
        
        //sync - async
        public static async Task<Result> Tap(this Result source, Func<Task> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) await ok().ConfigureAwait(false);
            return source;
        }
        
        //async - async
        public static async Task<Result> Tap(this Task<Result> source, Func<Task> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) await ok().ConfigureAwait(false);
            return result;
        }
    }
}
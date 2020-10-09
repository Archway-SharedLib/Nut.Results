using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultTapExtensions
    {
        //sync - sync
        public static Result Tap(this Result source, Action ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) ok();
            return source;
        }
        
        //async - sync
        public static async Task<Result> Tap(this Task<Result> source, Action ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) ok();
            return result;
        }
        
        //sync - async
        public static async Task<Result> Tap(this Result source, Func<Task> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (source.IsOk) await ok().ConfigureAwait(false);
            return source;
        }
        
        //async - async
        public static async Task<Result> Tap(this Task<Result> source, Func<Task> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            var result = await source.ConfigureAwait(false);
            
            if (result.IsOk) await ok().ConfigureAwait(false);
            return result;
        }
    }
}
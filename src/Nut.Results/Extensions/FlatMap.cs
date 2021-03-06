using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        // Void -> T
        
        // sync - sync Void -> T
        public static Result<T> FlatMap<T>(this in Result source, Func<Result<T>> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            return !source.IsOk ? Result.Error<T>(source.errorValue) : ok();
        }

        //async - sync Void -> T
        public static async Task<Result<T>> FlatMap<T>(this Task<Result> source, Func<Result<T>> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            return !result.IsOk ? Result.Error<T>(result.errorValue) : ok();
        }

        //sync - async Void -> T
        public static async Task<Result<T>> FlatMap<T>(this Result source, Func<Task<Result<T>>> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<T>(source.errorValue);

            return await ok().ConfigureAwait(false);
        }

        //async - async Void -> T
        public static async Task<Result<T>> FlatMap<T>(this Task<Result> source, Func<Task<Result<T>>> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<T>(result.errorValue);

            return await ok().ConfigureAwait(false);
        }
        
        // Void -> Void
        
        // sync - sync Void -> Void
        public static Result FlatMap(this in Result source, Func<Result> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            return !source.IsOk ? source : ok();
        }

        //async - sync Void -> Void
        public static async Task<Result> FlatMap(this Task<Result> source, Func<Result> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            return !result.IsOk ? result : ok();
        }

        //sync - async Void -> Void
        public static async Task<Result> FlatMap(this Result source, Func<Task<Result>> ok)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return source;

            return await ok().ConfigureAwait(false);
        }

        //async - async Void -> Void
        public static async Task<Result> FlatMap(this Task<Result> source, Func<Task<Result>> ok)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return result;

            return await ok().ConfigureAwait(false);
        }
    }
}
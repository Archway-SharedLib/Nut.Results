using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultFlatMapExtensions
    {
        // Void -> T
        
        // sync - sync Void -> T
        public static Result<T> FlatMap<T>(this Result source, Func<Result<T>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<T>(source.errorValue);

            return ok();
        }

        //async - sync Void -> T
        public static async Task<Result<T>> FlatMap<T>(this Task<Result> source, Func<Result<T>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<T>(result.errorValue);

            return ok();
        }

        //sync - async Void -> T
        public static async Task<Result<T>> FlatMap<T>(this Result source, Func<Task<Result<T>>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<T>(source.errorValue);

            return await ok().ConfigureAwait(false);
        }

        //async - async Void -> T
        public static async Task<Result<T>> FlatMap<T>(this Task<Result> source, Func<Task<Result<T>>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<T>(result.errorValue);

            return await ok().ConfigureAwait(false);
        }
        
        // Void -> Void
        
        // sync - sync Void -> Void
        public static Result FlatMap(this Result source, Func<Result> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error(source.errorValue);

            return ok();
        }

        //async - sync Void -> Void
        public static async Task<Result> FlatMap(this Task<Result> source, Func<Result> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error(result.errorValue);

            return ok();
        }

        //sync - async Void -> Void
        public static async Task<Result> FlatMap(this Result source, Func<Task<Result>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error(source.errorValue);

            return await ok().ConfigureAwait(false);
        }

        //async - async Void -> Void
        public static async Task<Result> FlatMap(this Task<Result> source, Func<Task<Result>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error(result.errorValue);

            return await ok().ConfigureAwait(false);
        }
    }
}
using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static class ResultTFlatMapExtensions
    {
        //T1 -> Void

        // sync - sync T1 -> Void
        public static Result FlatMap<T>(this Result<T> source, Func<T, Result> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error(source.errorValue);

            return ok(source.value);
        }

        //async - sync T1 -> Void
        public static async Task<Result> FlatMap<T>(this Task<Result<T>> source, Func<T, Result> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error(result.errorValue);

            return ok(result.value);
        }

        //sync - async T1 -> Void
        public static async Task<Result> FlatMap<T>(this Result<T> source, Func<T, Task<Result>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error(source.errorValue);

            return await ok(source.value).ConfigureAwait(false);
        }

        //async - async T1 -> Void 
        public static async Task<Result> FlatMap<T>(this Task<Result<T>> source, Func<T, Task<Result>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error(result.errorValue);

            return await ok(result.value).ConfigureAwait(false);
        }

        //T1 -> T2

        // sync - sync T1 -> T2
        public static Result<TResult> FlatMap<T, TResult>(this Result<T> source, Func<T, Result<TResult>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);

            return ok(source.value);
        }

        //async - sync T1 -> T2
        public static async Task<Result<TResult>> FlatMap<T, TResult>(this Task<Result<T>> source, Func<T, Result<TResult>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result.errorValue);

            return ok(result.value);
        }

        //sync - async T1 -> T2
        public static async Task<Result<TResult>> FlatMap<T, TResult>(this Result<T> source, Func<T, Task<Result<TResult>>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!source.IsOk) return Result.Error<TResult>(source.errorValue);

            return await ok(source.value).ConfigureAwait(false);
        }

        //async - async T1 -> T2 
        public static async Task<Result<TResult>> FlatMap<T, TResult>(this Task<Result<T>> source, Func<T, Task<Result<TResult>>> ok)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (ok == null) throw new ArgumentNullException(nameof(ok));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result.errorValue);

            return await ok(result.value).ConfigureAwait(false);
        }
    }
}
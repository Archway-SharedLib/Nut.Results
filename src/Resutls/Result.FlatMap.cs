using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public Result FlatMap(Func<Result> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            return !IsOk ? Result.Error(errorValue) : ok();
        }

        public Result FlatMap(Func<Task<Result>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error(errorValue);

            return AsyncHelper.RunSync(() => ok());
        }

        public Result<TResult> FlatMap<TResult>(Func<Result<TResult>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            return !IsOk ? Result.Error<TResult>(errorValue) : ok();
        }

        public Result<TResult> FlatMap<TResult>(Func<Task<Result<TResult>>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error<TResult>(errorValue);

            return AsyncHelper.RunSync(() => ok());
        }

        public async Task<Result> FlatMapAsync(Func<Task<Result>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error(errorValue);
            return await ok().ConfigureAwait(false);
        }

        public async Task<Result<TResult>> FlatMapAsync<TResult>(Func<Task<Result<TResult>>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error<TResult>(errorValue);
            return await ok().ConfigureAwait(false);
        }
    }

    public readonly partial struct Result<T>
    {
        public Result<TResult> FlatMap<TResult>(Func<T, Result<TResult>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            return !IsOk ? Result.Error<TResult>(errorValue) : ok(value);
        }

        public Result<TResult> FlatMap<TResult>(Func<T, Task<Result<TResult>>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error<TResult>(errorValue);

            var okValue = value;
            return AsyncHelper.RunSync(() => ok(okValue));
        }

        public Result FlatMap(Func<T, Result> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            return !IsOk ? Result.Error(errorValue) : ok(value);
        }

        public Result FlatMap(Func<T, Task<Result>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error(errorValue);

            var okValue = value;
            return AsyncHelper.RunSync(() => ok(okValue));
        }

        public async Task<Result<TResult>> FlatMapAsync<TResult>(Func<T, Task<Result<TResult>>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error<TResult>(errorValue);
            return await ok(value).ConfigureAwait(false);
        }

        public async Task<Result> FlatMapAsync(Func<T, Task<Result>> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (!IsOk) return Result.Error(errorValue);
            return await ok(value).ConfigureAwait(false);
        }
    }
}

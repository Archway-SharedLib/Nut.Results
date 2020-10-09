using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    
    
    // public readonly partial struct Result
    // {
    //     public async Task<Result> MapAsync(Func<Task> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //         if (!IsOk) return Result.Error(errorValue);
    //         await ok().ConfigureAwait(false);
    //         return Result.Ok();
    //     }
    //
    //     public async Task<Result<TResult>> MapAsync<TResult>(Func<Task<TResult>> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //         if (!IsOk) return Result.Error<TResult>(errorValue);
    //         var result = await ok().ConfigureAwait(false);
    //         return Result.Ok(result);
    //     }
    // }
    //
    // public readonly partial struct Result<T>
    // {
    //     public Result<TResult> Map<TResult>(Func<T, TResult> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //
    //         if (!IsOk) return Result.Error<TResult>(errorValue);
    //         var result = ok(value);
    //         return Result.Ok(result);
    //     }
    //
    //     public Result<TResult> Map<TResult>(Func<T, Task<TResult>> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //         if (!IsOk) return Result.Error<TResult>(errorValue);
    //
    //         var okValue = value;
    //         var result = AsyncHelper.RunSync(() => ok(okValue));
    //         return Result.Ok(result);
    //     }
    //
    //     public Result Map(Action<T> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //
    //         if (!IsOk) return Result.Error(errorValue);
    //         ok(value);
    //         return Result.Ok();
    //     }
    //
    //     public Result Map(Func<T, Task> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //         if (!IsOk) return Result.Error(errorValue);
    //
    //         var okValue = value;
    //         AsyncHelper.RunSync(() => ok(okValue));
    //         return Result.Ok();
    //     }
    //
    //     public async Task<Result<TResult>> MapAsync<TResult>(Func<T, Task<TResult>> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //         if (!IsOk) return Result.Error<TResult>(errorValue);
    //         var result = await ok(value).ConfigureAwait(false);
    //         return Result.Ok(result);
    //     }
    //
    //     public async Task<Result> MapAsync(Func<T, Task> ok)
    //     {
    //         if (ok == null) throw new ArgumentNullException(nameof(ok));
    //         if (!IsOk) return Result.Error(errorValue);
    //         await ok(value).ConfigureAwait(false);
    //         return Result.Ok();
    //     }
    // }
}

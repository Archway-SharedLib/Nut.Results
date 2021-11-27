using System;
using System.Threading.Tasks;
using Nut.Results.Internals;

namespace Nut.Results;

public static partial class ResultExtensions
{
    public static async ValueTask<Result<TResult>> Map<T, TResult>(this Result<T> source,
        Func<T, ValueTask<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return Result.Error<TResult>(source.errorValue);
        return await ok(source.value).ConfigureAwait(false);
    }
        
    public static async ValueTask<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source,
        Func<T, ValueTask<TResult>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
            
        var result = await source.ConfigureAwait(false);
            
        if (!result.IsOk) return Result.Error<TResult>(result.errorValue);
        return await ok(result.value).ConfigureAwait(false);
    }
        
    public static async ValueTask<Result<TResult>> Map<T, TResult>(this ValueTask<Result<T>> source, Func<T, TResult> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var value = await source;
            
        if (!value.IsOk) return Result.Error<TResult>(value.errorValue);
        
        var newValue = ok(value.value);
            
        return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
    }

    public static async ValueTask<Result<TResult>> Map<T, TResult>(this ValueTask<Result<T>> source, Func<T, ValueTask<TResult>> ok)
    {
        var result = await source.ConfigureAwait(false);

        if (!result.IsOk) return Result.Error<TResult>(result.errorValue);
        return await ok(result.value).ConfigureAwait(false);
    }
}

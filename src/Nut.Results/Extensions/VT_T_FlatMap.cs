using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // VT<R> to R...

    public static async ValueTask<Result> FlatMap<T>(this ValueTask<Result<T>> source, Func<Result> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : ok();
    }

    public static async ValueTask<Result> FlatMap<T>(this ValueTask<Result<T>> source, Func<Task<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : await ok();
    }

    public static async ValueTask<Result> FlatMap<T>(this ValueTask<Result<T>> source, Func<ValueTask<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : await ok();
    }

    // R to VT<R>...

    public static async ValueTask<Result> FlatMap<T>(this Result<T> source, Func<ValueTask<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error(source._errorValue) : await ok();
    }

    public static async ValueTask<Result> FlatMap<T>(this Task<Result<T>> source, Func<ValueTask<Result>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : await ok();
    }

    // VT<R> to R<T>...

    public static async ValueTask<Result<TResult>> FlatMap<T, TResult>(this ValueTask<Result<T>> source, Func<Result<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);    
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : ok();
    }

    public static async ValueTask<Result<TResult>> FlatMap<T, TResult>(this ValueTask<Result<T>> source, Func<Task<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : await ok();
    }

    public static async ValueTask<Result<TResult>> FlatMap<T, TResult>(this ValueTask<Result<T>> source, Func<ValueTask<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : await ok();
    }

    // R to VT<R<T>>...

    public static async ValueTask<Result<TResult>> FlatMap<T, TResult>(this Result<T> source, Func<ValueTask<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error<TResult>(source._errorValue) : await ok();
    }

    public static async ValueTask<Result<TResult>> FlatMap<T, TResult>(this Task<Result<T>> source, Func<ValueTask<Result<TResult>>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : await ok();
    }
}

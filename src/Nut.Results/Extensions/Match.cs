// ReSharper disable CheckNamespace

using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // result to result

    public static Result Match(this in Result source, Func<Result> ok, Func<IError, Result> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok() : err(source._errorValue);
    }

    public static Task<Result> Match(this in Result source, Func<Task<Result>> ok, Func<IError, Result> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok() : Task.FromResult(err(source._errorValue));
    }

    public static Task<Result> Match(this in Result source, Func<Result> ok, Func<IError, Task<Result>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? Task.FromResult(ok()) : err(source._errorValue);
    }

    public static Task<Result> Match(this in Result source, Func<Task<Result>> ok, Func<IError, Task<Result>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok() : err(source._errorValue);
    }

    public static async Task<Result> Match(this Task<Result> source, Func<Result> ok, Func<IError, Result> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok() : err(result._errorValue);
    }

    public static async Task<Result> Match(this Task<Result> source, Func<Task<Result>> ok, Func<IError, Result> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok() : err(result._errorValue);
    }

    public static async Task<Result> Match(this Task<Result> source, Func<Result> ok, Func<IError, Task<Result>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok() : await err(result._errorValue);
    }

    public static async Task<Result> Match(this Task<Result> source, Func<Task<Result>> ok, Func<IError, Task<Result>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok() : await err(result._errorValue);
    }

    // result to result<T>

    public static Result<T> Match<T>(this in Result source, Func<Result<T>> ok, Func<IError, Result<T>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok() : err(source._errorValue);
    }

    public static Task<Result<T>> Match<T>(this in Result source, Func<Task<Result<T>>> ok, Func<IError, Result<T>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok() : Task.FromResult(err(source._errorValue));
    }

    public static Task<Result<T>> Match<T>(this in Result source, Func<Result<T>> ok, Func<IError, Task<Result<T>>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? Task.FromResult(ok()) : err(source._errorValue);
    }

    public static Task<Result<T>> Match<T>(this in Result source, Func<Task<Result<T>>> ok, Func<IError, Task<Result<T>>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok() : err(source._errorValue);
    }

    public static async Task<Result<T>> Match<T>(this Task<Result> source, Func<Result<T>> ok, Func<IError, Result<T>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok() : err(result._errorValue);
    }

    public static async Task<Result<T>> Match<T>(this Task<Result> source, Func<Task<Result<T>>> ok, Func<IError, Result<T>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok() : err(result._errorValue);
    }

    public static async Task<Result<T>> Match<T>(this Task<Result> source, Func<Result<T>> ok, Func<IError, Task<Result<T>>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok() : await err(result._errorValue);
    }

    public static async Task<Result<T>> Match<T>(this Task<Result> source, Func<Task<Result<T>>> ok, Func<IError, Task<Result<T>>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok() : await err(result._errorValue);
    }
}

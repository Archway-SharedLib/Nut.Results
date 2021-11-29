using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    // T -> T

    // sync - sync T -> T
    public static Result<T> FlatMapError<T>(this in Result<T> source, Func<IError, Result<T>> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        return !source.IsError ? source : error(source._errorValue);
    }

    //async - sync T -> T
    public static async Task<Result<T>> FlatMapError<T>(this Task<Result<T>> source, Func<IError, Result<T>> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        return !result.IsError ? result : error(result._errorValue);
    }

    //sync - async T -> T
    public static async Task<Result<T>> FlatMapError<T>(this Result<T> source, Func<IError, Task<Result<T>>> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        return await error(source._errorValue).ConfigureAwait(false);
    }

    //async - async T -> T
    public static async Task<Result<T>> FlatMapError<T>(this Task<Result<T>> source, Func<IError, Task<Result<T>>> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        if (!result.IsError) return result;

        return await error(result._errorValue).ConfigureAwait(false);
    }
}

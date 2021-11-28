// ReSharper disable CheckNamespace

using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    public static bool ValidateError<T>(this in Result<T> source, Func<IError, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && predicate(source._errorValue);
    }

    public static async Task<bool> ValidateError<T>(this Result<T> source, Func<IError, Task<bool>> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && await predicate(source._errorValue);
    }

    public static async Task<bool> ValidateError<T>(this Task<Result<T>> source, Func<IError, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && predicate(s._errorValue);
    }

    public static async Task<bool> ValidateError<T>(this Task<Result<T>> source, Func<IError, Task<bool>> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && await predicate(s._errorValue);
    }
}

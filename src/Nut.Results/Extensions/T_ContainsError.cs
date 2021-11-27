// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    public static bool ContainsError<T>(this in Result<T> source, IError error)
        => ContainsError(source, error, null);

    public static bool ContainsError<T>(this in Result<T> source, IError error, IEqualityComparer<IError>? comparer)
    {
        if (source.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(source._errorValue, error);
    }

    public static bool ContainsError<T>(this in Result<T> source, Func<IError, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && predicate(source._errorValue);
    }

    public static Task<bool> ContainsError<T>(this Task<Result<T>> source, IError error)
        => ContainsError(source, error, null);

    public static async Task<bool> ContainsError<T>(this Task<Result<T>> source, IError error, IEqualityComparer<IError>? comparer)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        var s = await source.ConfigureAwait(false);
        if (s.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(s._errorValue, error);
    }

    public static async Task<bool> ContainsError<T>(this Task<Result<T>> source, Func<IError, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && predicate(s._errorValue);
    }
}

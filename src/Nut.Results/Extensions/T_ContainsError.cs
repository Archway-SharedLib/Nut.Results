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
}

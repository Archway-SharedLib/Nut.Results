using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    public static bool Contains<T>(this in Result<T> source, T value)
        => Contains(source, value, null);

    public static bool Contains<T>(this in Result<T> source, T value, IEqualityComparer<T>? comparer)
    {
        if (source.IsError) return false;
        comparer ??= EqualityComparer<T>.Default;
        return comparer.Equals(source._value, value);
    }

    public static Task<bool> Contains<T>(this Task<Result<T>> source, T value)
        => Contains(source, value, null);

    public static async Task<bool> Contains<T>(this Task<Result<T>> source, T value, IEqualityComparer<T>? comparer)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        var s = await source.ConfigureAwait(false);
        if (s.IsError) return false;
        comparer ??= EqualityComparer<T>.Default;
        return comparer.Equals(s._value, value);
    }
}

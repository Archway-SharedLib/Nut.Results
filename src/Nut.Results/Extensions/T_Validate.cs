using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    public static bool Validate<T>(this in Result<T> source, Func<T, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsError && predicate(source._value);
    }

    public static async Task<bool> Validate<T>(this Result<T> source, Func<T, Task<bool>> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsError && await predicate(source._value);
    }

    public static async Task<bool> Validate<T>(this Task<Result<T>> source, Func<T, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsError && predicate(s._value);
    }

    public static async Task<bool> Validate<T>(this Task<Result<T>> source, Func<T, Task<bool>> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsError && await predicate(s._value);
    }
}

using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている成功の値をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が失敗の場合は false が返ります。</remarks>
    /// <param name="source">成功の値を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static bool Validate<T>(this in Result<T> source, Func<T, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsError && predicate(source._value);
    }

    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている成功の値をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が失敗の場合は false が返ります。</remarks>
    /// <param name="source">成功の値を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static async Task<bool> Validate<T>(this Result<T> source, Func<T, Task<bool>> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsError && await predicate(source._value);
    }

    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている成功の値をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が失敗の場合は false が返ります。</remarks>
    /// <param name="source">成功の値を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static async Task<bool> Validate<T>(this Task<Result<T>> source, Func<T, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsError && predicate(s._value);
    }

    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている成功の値をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が失敗の場合は false が返ります。</remarks>
    /// <param name="source">成功の値を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static async Task<bool> Validate<T>(this Task<Result<T>> source, Func<T, Task<bool>> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsError && await predicate(s._value);
    }
}



using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static bool ValidateError<T>(this in Result<T> source, Func<Exception, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && predicate(source._errorValue);
    }

    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static async Task<bool> ValidateError<T>(this Result<T> source, Func<Exception, Task<bool>> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && await predicate(source._errorValue);
    }

    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static async Task<bool> ValidateError<T>(this Task<Result<T>> source, Func<Exception, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && predicate(s._errorValue);
    }

    /// <summary>
    /// 指定した <see cref="Result{T}"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result{T}"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    /// <typeparam name="T">成功の値の型</typeparam>
    public static async Task<bool> ValidateError<T>(this Task<Result<T>> source, Func<Exception, Task<bool>> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && await predicate(s._errorValue);
    }
}

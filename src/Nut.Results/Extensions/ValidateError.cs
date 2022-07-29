using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 指定した <see cref="Result"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    public static bool ValidateError(this in Result source, Func<Exception, bool> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && predicate(source._capturedError.SourceException);
    }

    /// <summary>
    /// 指定した <see cref="Result"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    public static async Task<bool> ValidateError(this Result source, Func<Exception, Task<bool>> predicate)
    {
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        return !source.IsOk && await predicate(source._capturedError.SourceException);
    }

    /// <summary>
    /// 指定した <see cref="Result"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    public static async Task<bool> ValidateError(this Task<Result> source, Func<Exception, bool> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && predicate(s._capturedError.SourceException);
    }

    /// <summary>
    /// 指定した <see cref="Result"/> に含まれている <see cref="Exception"/> をチェックします。
    /// </summary>
    /// <remarks><paramref name="source"/> の値が成功の場合は false が返ります。</remarks>
    /// <param name="source"><see cref="Exception"/> を持っている <see cref="Result"/></param>
    /// <param name="predicate">チェックする処理</param>
    /// <returns>チェックする処理の結果</returns>
    public static async Task<bool> ValidateError(this Task<Result> source, Func<Exception, Task<bool>> predicate)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (predicate is null) throw new ArgumentNullException(nameof(predicate));
        var s = await source.ConfigureAwait(false);
        return !s.IsOk && await predicate(s._capturedError.SourceException);
    }
}

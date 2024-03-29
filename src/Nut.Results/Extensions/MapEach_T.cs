using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 結果の列挙可能の値ごとに別の値にマップする処理を実行します。
    /// </summary>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <returns>別の値にマップされた結果</returns>
    public static Result<IEnumerable<TResult>> MapEach<T, TResult>(this in Result<IEnumerable<T>> source, Func<T, TResult> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        return source.Map(v => v.Select(ok).ToList().AsEnumerable());
    }

    /// <summary>
    /// 結果の列挙可能の値ごとに別の値にマップする処理を実行します。
    /// </summary>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <returns>別の値にマップされた結果</returns>
    public static async Task<Result<IEnumerable<TResult>>> MapEach<T, TResult>(this Task<Result<IEnumerable<T>>> source, Func<T, TResult> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        try
        {
            var value = await source.ConfigureAwait(false);
            return MapEach(value, ok);
        }
        catch (Exception e)
        {
            return Result.Error<IEnumerable<TResult>>(e);
        }
    }

    /// <summary>
    /// 結果の列挙可能の値ごとに別の値にマップする処理を実行します。
    /// </summary>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <returns>別の値にマップされた結果</returns>
    public static async Task<Result<IEnumerable<TResult>>> MapEach<T, TResult>(this Result<IEnumerable<T>> source, Func<T, Task<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (source.IsError) return Result.Error<IEnumerable<TResult>>(source._capturedError);

        try
        {
            var items = source._value;
            var tasks = items.Select(ok);

            var result = await Task.WhenAll(tasks).ConfigureAwait(false);
            return Result.Ok(result.AsEnumerable());
        }
        catch (Exception e)
        {
            return Result.Error<IEnumerable<TResult>>(e);
        }
    }

    /// <summary>
    /// 結果の列挙可能の値ごとに別の値にマップする処理を実行します。
    /// </summary>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <returns>別の値にマップされた結果</returns>
    public static async Task<Result<IEnumerable<TResult>>> MapEach<T, TResult>(this Task<Result<IEnumerable<T>>> source, Func<T, Task<TResult>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        try
        {
            var value = await source.ConfigureAwait(false);
            return await MapEach(value, ok).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return Result.Error<IEnumerable<TResult>>(e);
        }
    }
}

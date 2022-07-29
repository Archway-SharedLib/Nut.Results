using System;
using System.Threading.Tasks;
using Nut.Results.Internals;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 成功の値を別の値にマップします。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <returns>別の値にマップされた結果</returns>
    public static Result<TResult> Map<T, TResult>(this in Result<T> source, Func<T, TResult> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return Result.Error<TResult>(source._capturedError);

        try
        {
            var newValue = ok(source._value);
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        catch (Exception e)
        {
            return Result.Error<TResult>(e);
        }
    }

    /// <summary>
    /// 成功の値を別の値にマップします。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <returns>別の値にマップされた結果</returns>
    public static async Task<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source, Func<T, TResult> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        try
        {
            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result._capturedError);

            var newValue = ok(result._value);
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        catch (Exception e)
        {
            return Result.Error<TResult>(e);
        }
    }

    /// <summary>
    /// 成功の値を別の値にマップします。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <returns>別の値にマップされた結果</returns>
    public static async Task<Result<TResult>> Map<T, TResult>(this Result<T> source, Func<T, Task<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return Result.Error<TResult>(source._capturedError);

        try
        {
            var newValue = await ok(source._value).ConfigureAwait(false);
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        catch (Exception e)
        {
            return Result.Error<TResult>(e);
        }
    }

    /// <summary>
    /// 成功の値を別の値にマップします。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">マップする値</param>
    /// <param name="ok">マップする処理</param>
    /// <typeparam name="T">現在の成功の型</typeparam>
    /// <typeparam name="TResult">マップする成功の型</typeparam>
    /// <returns>別の値にマップされた結果</returns>
    public static async Task<Result<TResult>> Map<T, TResult>(this Task<Result<T>> source, Func<T, Task<TResult>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        try
        {
            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) return Result.Error<TResult>(result._capturedError);
            var newValue = await ok(result._value).ConfigureAwait(false);
            return Result.Ok(InternalUtility.CheckReturnValueNotNull(newValue));
        }
        catch (Exception e)
        {
            return Result.Error<TResult>(e);
        }
    }
}

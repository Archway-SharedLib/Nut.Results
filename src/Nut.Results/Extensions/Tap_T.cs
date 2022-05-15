using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>もとの値</returns>
    public static Result<T> Tap<T>(this in Result<T> source, Action<T> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        try
        {
            if (source.IsOk) ok(source._value);
            return source;
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>もとの値</returns>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> source, Action<T> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        try
        {
            var result = await source.ConfigureAwait(false);
            if (result.IsOk) ok(result._value);
            return result;
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>もとの値</returns>
    public static async Task<Result<T>> Tap<T>(this Result<T> source, Func<T, Task> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        try
        {
            if (source.IsOk) await ok(source._value).ConfigureAwait(false);
            return source;
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>もとの値</returns>
    public static async Task<Result<T>> Tap<T>(this Task<Result<T>> source, Func<T, Task> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        try
        {
            var result = await source.ConfigureAwait(false);
            if (result.IsOk) await ok(result._value).ConfigureAwait(false);
            return result;
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }
}

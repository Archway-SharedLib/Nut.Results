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
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> Tap<T>(this ValueTask<Result<T>> source, Action ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);

        if (result.IsOk) ok();
        return result;
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> Tap<T>(this Result<T> source, Func<ValueTask> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (source.IsOk) await ok().ConfigureAwait(false);
        return source;
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> Tap<T>(this ValueTask<Result<T>> source, Func<Task> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);

        if (result.IsOk) await ok().ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> Tap<T>(this Task<Result<T>> source, Func<ValueTask> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);

        if (result.IsOk) await ok().ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// 成功の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="ok">特定の処理</param>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> Tap<T>(this ValueTask<Result<T>> source, Func<ValueTask> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);

        if (result.IsOk) await ok().ConfigureAwait(false);
        return result;
    }
}
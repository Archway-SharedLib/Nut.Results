using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> TapError<T>(this ValueTask<Result<T>> source, Action<IError> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        var result = await source.ConfigureAwait(false);
        if (result.IsError) error(result._errorValue);
        return result;
    }

    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> TapError<T>(this Result<T> source, Func<IError, ValueTask> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));

        if (source.IsError) await error(source._errorValue);
        return source;
    }

    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> TapError<T>(this ValueTask<Result<T>> source, Func<IError, Task> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        var result = await source.ConfigureAwait(false);

        if (result.IsError) await error(result._errorValue).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> TapError<T>(this Task<Result<T>> source, Func<IError, ValueTask> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        if (result.IsError) await error(result._errorValue).ConfigureAwait(false);
        return result;
    }

    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>もとの値</returns>
    public static async ValueTask<Result<T>> TapError<T>(this ValueTask<Result<T>> source, Func<IError, ValueTask> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        var result = await source.ConfigureAwait(false);

        if (result.IsError) await error(result._errorValue).ConfigureAwait(false);
        return result;
    }
}

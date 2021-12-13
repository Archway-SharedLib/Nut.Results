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
    /// <returns>もとの値</returns>
    public static Result TapError(this in Result source, Action<IError> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (source.IsError) error(source._errorValue);
        return source;
    }

    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <returns>もとの値</returns>
    public static async Task<Result> TapError(this Task<Result> source, Action<IError> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
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
    /// <returns>もとの値</returns>
    public static async Task<Result> TapError(this Result source, Func<IError, Task> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (source.IsError) await error(source._errorValue).ConfigureAwait(false);
        return source;
    }

    /// <summary>
    /// 失敗の場合に、指定された処理を行いもとの値を返します。
    /// </summary>
    /// <param name="source">処理を実行するかどうかの値</param>
    /// <param name="error">特定の処理</param>
    /// <returns>もとの値</returns>
    public static async Task<Result> TapError(this Task<Result> source, Func<IError, Task> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));
        var result = await source.ConfigureAwait(false);

        if (result.IsError) await error(result._errorValue).ConfigureAwait(false);
        return result;
    }
}

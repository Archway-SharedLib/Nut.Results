using System;
using System.Threading.Tasks;
using Nut.Results.Internals;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // sync - sync
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static Result MapError<TError>(this in Result source, Func<IError, TError> error) where TError : IError
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        return !source.IsError ? source : Result.Error(InternalUtility.CheckReturnValueNotNull(error(source._errorValue)));
    }

    //async - sync
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result> MapError<TError>(this Task<Result> source, Func<IError, TError> error) where TError : IError
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        return !result.IsError ? result : Result.Error(InternalUtility.CheckReturnValueNotNull(error(result._errorValue)));
    }

    //sync - async
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result> MapError<TError>(this Result source, Func<IError, Task<TError>> error) where TError : IError
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        var errorCallbackResult = error(source._errorValue);
        if (errorCallbackResult == null) InternalUtility.RaizeReturnValueNotNull();
        var result = await errorCallbackResult!.ConfigureAwait(false);
        return Result.Error(InternalUtility.CheckReturnValueNotNull(result));
    }

    //async - async
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result> MapError<TError>(this Task<Result> source, Func<IError, Task<TError>> error) where TError : IError
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        if (!result.IsError) return result;

        var errorCallbackResult = error(result._errorValue);
        if (errorCallbackResult == null) InternalUtility.RaizeReturnValueNotNull();
        var newReturnValue = await errorCallbackResult!.ConfigureAwait(false);
        return Result.Error(InternalUtility.CheckReturnValueNotNull(newReturnValue));
    }
}

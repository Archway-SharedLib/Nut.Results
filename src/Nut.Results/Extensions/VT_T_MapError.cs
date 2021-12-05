using System;
using System.Threading.Tasks;
using Nut.Results.Internals;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async ValueTask<Result<T>> MapError<T,TError>(this ValueTask<Result<T>> source, Func<IError, TError> error) where TError : IError
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        var value = await source.ConfigureAwait(false);
        return !value.IsError ? value : Result.Error<T>(InternalUtility.CheckReturnValueNotNull(error(value._errorValue)));
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async ValueTask<Result<T>> MapError<T, TError>(this ValueTask<Result<T>> source, Func<IError, Task<TError>> error) where TError : IError
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        var value = await source.ConfigureAwait(false);
        if (!value.IsError) return value;

        var errorCallbackResult = error(value._errorValue);
        if (errorCallbackResult is null) InternalUtility.RaizeReturnValueNotNull();
        var result = await errorCallbackResult.ConfigureAwait(false);
        return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(result));
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async ValueTask<Result<T>> MapError<T, TError>(this ValueTask<Result<T>> source, Func<IError, ValueTask<TError>> error) where TError : IError
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        var value = await source.ConfigureAwait(false);
        if (!value.IsError) return value;

        var errorCallbackResult = error(value._errorValue);
        var result = await errorCallbackResult.ConfigureAwait(false);
        return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(result));
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async ValueTask<Result<T>> MapError<T, TError>(this Result<T> source, Func<IError, ValueTask<TError>> error) where TError : IError
    {
        if (error is null) throw new ArgumentNullException(nameof(error));

        if (!source.IsError) return source;

        var errorCallbackResult = error(source._errorValue);
        var newReturnValue = await errorCallbackResult.ConfigureAwait(false);
        return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(newReturnValue));
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async ValueTask<Result<T>> MapError<T, TError>(this Task<Result<T>> source, Func<IError, ValueTask<TError>> error) where TError : IError
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        if (!result.IsError) return result;
        var errorCallbackResult = error(result._errorValue);
        var newReturnValue = await errorCallbackResult.ConfigureAwait(false);
        return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(newReturnValue));
    }
}

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
    public static Result MapError<TError>(this in Result source, Func<Exception, TError> error) where TError : Exception
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        try
        {
            return !source.IsError ? source : Result.Error(InternalUtility.CheckReturnValueNotNull(error(source._capturedError.SourceException)));
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }

    //async - sync
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result> MapError<TError>(this Task<Result> source, Func<Exception, TError> error) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        try
        {
            var result = await source.ConfigureAwait(false);
            return !result.IsError ? result : Result.Error(InternalUtility.CheckReturnValueNotNull(error(result._capturedError.SourceException)));
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }

    //sync - async
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result> MapError<TError>(this Result source, Func<Exception, Task<TError>> error) where TError : Exception
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        try
        {
            var errorCallbackResult = error(source._capturedError.SourceException);
            if (errorCallbackResult == null) InternalUtility.RaizeReturnValueNotNull();
            var result = await errorCallbackResult!.ConfigureAwait(false);
            return Result.Error(InternalUtility.CheckReturnValueNotNull(result));
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }

    //async - async
    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result> MapError<TError>(this Task<Result> source, Func<Exception, Task<TError>> error) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        try
        {
            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            var errorCallbackResult = error(result._capturedError.SourceException);
            if (errorCallbackResult == null) InternalUtility.RaizeReturnValueNotNull();
            var newReturnValue = await errorCallbackResult!.ConfigureAwait(false);
            return Result.Error(InternalUtility.CheckReturnValueNotNull(newReturnValue));
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }
}

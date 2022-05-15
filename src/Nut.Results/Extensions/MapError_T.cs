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
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static Result<T> MapError<T, TError>(this in Result<T> source, Func<Exception, TError> error) where TError : Exception
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        try
        {
            return !source.IsError ? source : Result.Error<T>(InternalUtility.CheckReturnValueNotNull(error(source._errorValue)));
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result<T>> MapError<T, TError>(this Task<Result<T>> source, Func<Exception, TError> error) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        try
        {
            var result = await source.ConfigureAwait(false);
            return !result.IsError ? result : Result.Error<T>(InternalUtility.CheckReturnValueNotNull(error(result._errorValue)));
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result<T>> MapError<T, TError>(this Result<T> source, Func<Exception, Task<TError>> error) where TError : Exception
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        try
        {
            var returnValue = error(source._errorValue);
            if (returnValue == null) InternalUtility.RaizeReturnValueNotNull();
            var result = await returnValue!.ConfigureAwait(false);
            return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(result));
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    /// <summary>
    /// 失敗だった場合に設定されている失敗の値を新しい失敗の値に変換します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい失敗の値を作成する処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">失敗の型</typeparam>
    /// <returns>新しい失敗の値を持った結果</returns>
    public static async Task<Result<T>> MapError<T, TError>(this Task<Result<T>> source, Func<Exception, Task<TError>> error) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        try
        {
            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            var returnValue = error(result._errorValue);
            if (returnValue is null) InternalUtility.RaizeReturnValueNotNull();
            var newReturnValue = await returnValue!.ConfigureAwait(false);
            return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(newReturnValue));
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }
}

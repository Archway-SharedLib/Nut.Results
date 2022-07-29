using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // T -> T

    // sync - sync T -> T

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static Result<T> FlatMapError<T>(this in Result<T> source, Func<Exception, Result<T>> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        try
        {
            return !source.IsError ? source : error(source._capturedError.SourceException);
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    //async - sync T -> T

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result<T>> FlatMapError<T>(this Task<Result<T>> source, Func<Exception, Result<T>> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        try
        {
            var result = await source.ConfigureAwait(false);
            return !result.IsError ? result : error(result._capturedError.SourceException);
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    //sync - async T -> T

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result<T>> FlatMapError<T>(this Result<T> source, Func<Exception, Task<Result<T>>> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        try
        {
            return await error(source._capturedError.SourceException).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }

    //async - async T -> T

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result<T>> FlatMapError<T>(this Task<Result<T>> source, Func<Exception, Task<Result<T>>> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        try
        {
            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;
            return await error(result._capturedError.SourceException).ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return Result.Error<T>(e);
        }
    }
}

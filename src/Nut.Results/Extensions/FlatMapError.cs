using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // Void -> Void

    // sync - sync Void -> Void

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static Result FlatMapError(this in Result source, Func<IError, Result> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        return !source.IsError ? source : error(source._errorValue);
    }

    //async - sync Void -> Void

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result> FlatMapError(this Task<Result> source, Func<IError, Result> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        return !result.IsError ? result : error(result._errorValue);
    }

    //sync - async Void -> Void

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result> FlatMapError(this Result source, Func<IError, Task<Result>> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        return await error(source._errorValue).ConfigureAwait(false);
    }

    //async - async Void -> Void

    /// <summary>
    /// 失敗だった場合に新しい値を持った結果を作成します。成功の場合は、成功がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="error">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result> FlatMapError(this Task<Result> source, Func<IError, Task<Result>> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        if (!result.IsError) return result;

        return await error(result._errorValue).ConfigureAwait(false);
    }
}

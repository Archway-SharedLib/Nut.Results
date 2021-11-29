using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    // Void -> T

    // sync - sync Void -> T

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static Result<TResult> FlatMap<TResult>(this in Result source, Func<Result<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error<TResult>(source._errorValue) : ok();
    }

    //async - sync Void -> T

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result<TResult>> FlatMap<TResult>(this Task<Result> source, Func<Result<TResult>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : ok();
    }

    //sync - async Void -> T

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result<TResult>> FlatMap<TResult>(this Result source, Func<Task<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return Result.Error<TResult>(source._errorValue);

        return await ok().ConfigureAwait(false);
    }

    //async - async Void -> T

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async Task<Result<TResult>> FlatMap<TResult>(this Task<Result> source, Func<Task<Result<TResult>>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        if (!result.IsOk) return Result.Error<TResult>(result._errorValue);

        return await ok().ConfigureAwait(false);
    }

    // Void -> Void

    // sync - sync Void -> Void

    /// <summary>
    /// 成功だった場合に新しい結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <returns>新しい結果</returns>
    public static Result FlatMap(this in Result source, Func<Result> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? source : ok();
    }

    //async - sync Void -> Void

    /// <summary>
    /// 成功だった場合に新しい結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <returns>新しい結果</returns>
    public static async Task<Result> FlatMap(this Task<Result> source, Func<Result> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? result : ok();
    }

    //sync - async Void -> Void

    /// <summary>
    /// 成功だった場合に新しい結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <returns>新しい結果</returns>
    public static async Task<Result> FlatMap(this Result source, Func<Task<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return source;

        return await ok().ConfigureAwait(false);
    }

    //async - async Void -> Void

    /// <summary>
    /// 成功だった場合に新しい結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <returns>新しい結果</returns>
    public static async Task<Result> FlatMap(this Task<Result> source, Func<Task<Result>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        if (!result.IsOk) return result;

        return await ok().ConfigureAwait(false);
    }
}

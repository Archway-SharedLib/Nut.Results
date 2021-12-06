using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // VT<R> to R...

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result> FlatMap(this ValueTask<Result> source, Func<Result> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : ok();
    }

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result> FlatMap(this ValueTask<Result> source, Func<Task<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : await ok();
    }

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result> FlatMap(this ValueTask<Result> source, Func<ValueTask<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : await ok();
    }

    // R to VT<R>...

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result> FlatMap(this Result source, Func<ValueTask<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error(source._errorValue) : await ok();
    }

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result> FlatMap(this Task<Result> source, Func<ValueTask<Result>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : await ok();
    }

    // VT<R> to R<T>...

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result<TResult>> FlatMap<TResult>(this ValueTask<Result> source, Func<Result<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : ok();
    }

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result<TResult>> FlatMap<TResult>(this ValueTask<Result> source, Func<Task<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : await ok();
    }

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result<TResult>> FlatMap<TResult>(this ValueTask<Result> source, Func<ValueTask<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : await ok();
    }

    // R to VT<R<T>>...

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result<TResult>> FlatMap<TResult>(this Result source, Func<ValueTask<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error<TResult>(source._errorValue) : await ok();
    }

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ok">新しい結果の値を作成する処理</param>
    /// <typeparam name="TResult">新しい成功の型</typeparam>
    /// <returns>新しい値を持った結果</returns>
    public static async ValueTask<Result<TResult>> FlatMap<TResult>(this Task<Result> source, Func<ValueTask<Result<TResult>>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : await ok();
    }
}

using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    //T1 -> Void

    // sync - sync T1 -> Void

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <returns>新しい結果</returns>
    public static Result FlatMap<T>(this Result<T> source, Func<T, Result> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error(source._errorValue) : ok(source._value);
    }

    //async - sync T1 -> Void

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <returns>新しい結果</returns>
    public static async Task<Result> FlatMap<T>(this Task<Result<T>> source, Func<T, Result> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error(result._errorValue) : ok(result._value);
    }

    //sync - async T1 -> Void

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <returns>新しい結果</returns>
    public static async Task<Result> FlatMap<T>(this Result<T> source, Func<T, Task<Result>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return Result.Error(source._errorValue);

        return await ok(source._value).ConfigureAwait(false);
    }

    //async - async T1 -> Void

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <returns>新しい結果</returns>
    public static async Task<Result> FlatMap<T>(this Task<Result<T>> source, Func<T, Task<Result>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        if (!result.IsOk) return Result.Error(result._errorValue);

        return await ok(result._value).ConfigureAwait(false);
    }

    //T1 -> T2

    // sync - sync T1 -> T2

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい値をもった結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <typeparam name="TResult">新しい結果の型</typeparam>
    /// <returns>新しい値をもった結果</returns>
    public static Result<TResult> FlatMap<T, TResult>(this Result<T> source, Func<T, Result<TResult>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        return !source.IsOk ? Result.Error<TResult>(source._errorValue) : ok(source._value);
    }

    //async - sync T1 -> T2

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい値をもった結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <typeparam name="TResult">新しい結果の型</typeparam>
    /// <returns>新しい値をもった結果</returns>
    public static async Task<Result<TResult>> FlatMap<T, TResult>(this Task<Result<T>> source, Func<T, Result<TResult>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        return !result.IsOk ? Result.Error<TResult>(result._errorValue) : ok(result._value);
    }

    //sync - async T1 -> T2

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい値をもった結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <typeparam name="TResult">新しい結果の型</typeparam>
    /// <returns>新しい値をもった結果</returns>
    public static async Task<Result<TResult>> FlatMap<T, TResult>(this Result<T> source, Func<T, Task<Result<TResult>>> ok)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (!source.IsOk) return Result.Error<TResult>(source._errorValue);

        return await ok(source._value).ConfigureAwait(false);
    }

    //async - async T1 -> T2

    /// <summary>
    /// 成功だった場合に新しい値を持った結果を作成します。失敗の場合は、失敗がそのまま返ります。
    /// </summary>
    /// <param name="source">もととなる値を持った結果</param>
    /// <param name="ok">新しい値をもった結果を作成する処理</param>
    /// <typeparam name="T">成功の結果の型</typeparam>
    /// <typeparam name="TResult">新しい結果の型</typeparam>
    /// <returns>新しい値をもった結果</returns>
    public static async Task<Result<TResult>> FlatMap<T, TResult>(this Task<Result<T>> source, Func<T, Task<Result<TResult>>> ok)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));

        var result = await source.ConfigureAwait(false);
        if (!result.IsOk) return Result.Error<TResult>(result._errorValue);

        return await ok(result._value).ConfigureAwait(false);
    }
}

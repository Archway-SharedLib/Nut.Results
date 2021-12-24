

using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    // reulst<T> to result<T>

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Result<TResult> Match<T, TResult>(this in Result<T> source, Func<T, Result<TResult>> ok, Func<IError, Result<TResult>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok(source._value) : err(source._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Task<Result<TResult>> Match<T, TResult>(this in Result<T> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Result<TResult>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok(source._value) : Task.FromResult(err(source._errorValue));
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Task<Result<TResult>> Match<T, TResult>(this in Result<T> source, Func<T, Result<TResult>> ok, Func<IError, Task<Result<TResult>>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? Task.FromResult(ok(source._value)) : err(source._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Task<Result<TResult>> Match<T, TResult>(this in Result<T> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Task<Result<TResult>>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok(source._value) : err(source._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Result<TResult>> ok, Func<IError, Result<TResult>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok(result._value) : err(result._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Result<TResult>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok(result._value) : err(result._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Result<TResult>> ok, Func<IError, Task<Result<TResult>>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok(result._value) : await err(result._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result{TResult}"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TResult">返される結果の成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Task<Result<TResult>>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok(result._value) : await err(result._errorValue);
    }

    // reulst<T> to result

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Result Match<T>(this in Result<T> source, Func<T, Result> ok, Func<IError, Result> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok(source._value) : err(source._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Task<Result> Match<T>(this in Result<T> source, Func<T, Task<Result>> ok, Func<IError, Result> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok(source._value) : Task.FromResult(err(source._errorValue));
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Task<Result> Match<T>(this in Result<T> source, Func<T, Result> ok, Func<IError, Task<Result>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? Task.FromResult(ok(source._value)) : err(source._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static Task<Result> Match<T>(this in Result<T> source, Func<T, Task<Result>> ok, Func<IError, Task<Result>> err)
    {
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        return source.IsOk ? ok(source._value) : err(source._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Result> ok, Func<IError, Result> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok(result._value) : err(result._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Task<Result>> ok, Func<IError, Result> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok(result._value) : err(result._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Result> ok, Func<IError, Task<Result>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ok(result._value) : await err(result._errorValue);
    }

    /// <summary>
    /// 結果が成功の場合は、<paramref name="ok"/> の結果を返し、失敗の場合は <paramref name="err"/> の結果を返します。
    /// </summary>
    /// <param name="source">もととなる <see cref="Result"/></param>
    /// <param name="ok">成功の場合に実行される処理</param>
    /// <param name="err">失敗の場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>処理の結果</returns>
    public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Task<Result>> ok, Func<IError, Task<Result>> err)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ok is null) throw new ArgumentNullException(nameof(ok));
        if (err is null) throw new ArgumentNullException(nameof(err));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? await ok(result._value) : await err(result._errorValue);
    }
}

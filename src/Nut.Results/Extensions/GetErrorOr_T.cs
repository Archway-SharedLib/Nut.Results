using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static Exception GetErrorOr<T, TError>(this in Result<T> source, Func<T, TError> ifOk) where TError : Exception
    {
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        return source.IsOk ? ifOk(source._value) : source._capturedError.SourceException;
    }

    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static async Task<Exception> GetErrorOr<T, TError>(this Task<Result<T>> source, Func<T, TError> ifOk) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ifOk(result._value) : result._capturedError.SourceException;
    }

    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static async Task<Exception> GetErrorOr<T, TError>(this Result<T> source, Func<T, Task<TError>> ifOk) where TError : Exception
    {
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        if (source.IsOk) return await ifOk(source._value);
        return source._capturedError.SourceException;
    }

    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static async Task<Exception> GetErrorOr<T, TError>(this Task<Result<T>> source, Func<T, Task<TError>> ifOk) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        var result = await source.ConfigureAwait(false);
        if (result.IsOk) return await ifOk(result._value);
        return result._capturedError.SourceException;
    }
}

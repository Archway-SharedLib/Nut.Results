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
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static Exception GetErrorOr<TError>(this in Result source, Func<TError> ifOk) where TError : Exception
    {
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        return source.IsOk ? ifOk() : source._capturedError.SourceException;
    }

    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static async Task<Exception> GetErrorOr<TError>(this Task<Result> source, Func<TError> ifOk) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ifOk() : result._capturedError.SourceException;
    }

    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static async Task<Exception> GetErrorOr<TError>(this Result source, Func<Task<TError>> ifOk) where TError : Exception
    {
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        if (source.IsOk) return await ifOk();
        return source._capturedError.SourceException;
    }

    /// <summary>
    /// 失敗の値を取得します。成功の場合は<paramref name="ifOk"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifOk">結果が成功だった場合に実行される処理</param>
    /// <typeparam name="TError">エラーの型</typeparam>
    /// <returns>失敗の値</returns>
    public static async Task<Exception> GetErrorOr<TError>(this Task<Result> source, Func<Task<TError>> ifOk) where TError : Exception
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        var result = await source.ConfigureAwait(false);
        if (result.IsOk) return await ifOk();
        return result._capturedError.SourceException;
    }
}

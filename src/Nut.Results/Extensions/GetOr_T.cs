using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 成功の値を取得します。失敗の場合は<paramref name="ifError"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifError">結果が失敗だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の値</returns>
    public static T GetOr<T>(this in Result<T> source, Func<Exception, T> ifError)
    {
        if (ifError is null) throw new ArgumentNullException(nameof(ifError));

        return source.IsError ? ifError(source._capturedError.SourceException) : source._value;
    }

    /// <summary>
    /// 成功の値を取得します。失敗の場合は<paramref name="ifError"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifError">結果が失敗だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の値</returns>
    public static async Task<T> GetOr<T>(this Task<Result<T>> source, Func<Exception, T> ifError)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifError is null) throw new ArgumentNullException(nameof(ifError));

        var result = await source.ConfigureAwait(false);
        return result.IsError ? ifError(result._capturedError.SourceException) : result._value;
    }

    /// <summary>
    /// 成功の値を取得します。失敗の場合は<paramref name="ifError"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifError">結果が失敗だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の値</returns>
    public static async Task<T> GetOr<T>(this Result<T> source, Func<Exception, Task<T>> ifError)
    {
        if (ifError is null) throw new ArgumentNullException(nameof(ifError));

        if (source.IsError) return await ifError(source._capturedError.SourceException);
        return source._value;
    }

    /// <summary>
    /// 成功の値を取得します。失敗の場合は<paramref name="ifError"/>の結果が返されます。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <param name="ifError">結果が失敗だった場合に実行される処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の値</returns>
    public static async Task<T> GetOr<T>(this Task<Result<T>> source, Func<Exception, Task<T>> ifError)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifError is null) throw new ArgumentNullException(nameof(ifError));

        var result = await source.ConfigureAwait(false);
        if (result.IsError) return await ifError(result._capturedError.SourceException);
        return result._value;
    }
}

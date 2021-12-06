using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results;

public static partial class ResultUnsafeExtensions
{

    /// <summary>
    /// 失敗の結果を使って、新しい<see cref="Result{T}"/>を作成します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <typeparam name="T">もととなる成功の値の型</typeparam>
    /// <typeparam name="TResult">結果となる成功の値の型</typeparam>
    /// <returns>新しい<see cref="Result{T}"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    public static async ValueTask<Result<TResult>> PassOnError<T, TResult>(this ValueTask<Result<T>> source)
    {
        var res = await source.ConfigureAwait(false);
        if (res.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Result.Error<TResult>(res._errorValue);
    }

    /// <summary>
    /// 失敗の結果を使って、新しい<see cref="Result"/>を作成します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <typeparam name="T">もととなる成功の値の型</typeparam>
    /// <returns>新しい<see cref="Result"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    public static async ValueTask<Result> PassOnError<T>(this ValueTask<Result<T>> source)
    {
        var res = await source.ConfigureAwait(false);
        if (res.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Result.Error(res._errorValue);
    }
}

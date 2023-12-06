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
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>新しい<see cref="Result{T}"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    [Obsolete("このメソッドは将来廃止される予定です。 PreserveErrorAs<T>() を使用してください。", false)]
    public static Result<TResult> PassOnError<TResult>(this in Result source)
    {
        if (source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Result.Error<TResult>(source._capturedError);
    }

    /// <summary>
    /// 失敗の結果を使って、新しい<see cref="Result{T}"/>を作成します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>新しい<see cref="Result{T}"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    [Obsolete("このメソッドは将来廃止される予定です。 PreserveErrorAs<T>() を使用してください。", false)]
    public static async Task<Result<TResult>> PassOnError<TResult>(this Task<Result> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        var res = await source.ConfigureAwait(false);
        if (res.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Result.Error<TResult>(res._capturedError);
    }
}

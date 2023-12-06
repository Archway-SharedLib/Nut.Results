using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results;

public readonly partial struct Result
{
    /// <summary>
    /// 失敗の結果を使って、新しい<see cref="Result{T}"/>を作成します。
    /// </summary>
    /// <typeparam name="TResult">成功の値の型</typeparam>
    /// <returns>新しい<see cref="Result{T}"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    public Result<TResult> PreserveErrorAs<TResult>()
    {
        if (IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Error<TResult>(_capturedError);
    }
}

public readonly partial struct Result<T>
{
    /// <summary>
    /// 失敗の結果を使って、新しい<see cref="Result{T}"/>を作成します。
    /// </summary>
    /// <typeparam name="TResult">結果となる成功の値の型</typeparam>
    /// <returns>新しい<see cref="Result{T}"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    public Result<TResult> PreserveErrorAs<TResult>()
    {
        if (IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Result.Error<TResult>(_capturedError);
    }

    /// <summary>
    /// 失敗の結果を使って、新しい<see cref="Result"/>を作成します。
    /// </summary>
    /// <returns>新しい<see cref="Result"/></returns>
    /// <exception cref="InvalidOperationException">もととなる結果が成功だった場合に発生します。</exception>
    public Result PreserveErrorAs()
    {
        if (IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return Result.Error(_capturedError);
    }
}

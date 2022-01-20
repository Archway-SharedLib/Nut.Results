using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// ネストしている<see cref="Result{T}"/>の値を解除します。
    /// </summary>
    /// <param name="source">ネストしている<see cref="Result{T}"/></param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>ネストが解除された<see cref="Result{T}"/></returns>
    public static Result<T> Flatten<T>(this Result<Result<T>> source)
        => source.IsError ? Result.Error<T>(source._errorValue) : source._value;

    /// <summary>
    /// ネストしている<see cref="Result{Result}"/>の値を解除します。
    /// </summary>
    /// <param name="source">ネストしている<see cref="Result{Result}"/></param>
    /// <returns>ネストが解除された<see cref="Result"/></returns>
    public static Result Flatten(this Result<Result> source)
        => source.IsError ? Result.Error(source._errorValue) : source._value;
}

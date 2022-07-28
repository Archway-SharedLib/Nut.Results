using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results;

public readonly partial struct Result
{
    /// <summary>
    /// <see cref="Result"/> の成功を返します。
    /// </summary>
    /// <returns>成功の結果</returns>
    public static Result Ok() => new(null, true);

    /// <summary>
    /// <see cref="Result{T}"/> の成功を返します。
    /// </summary>
    /// <param name="value">成功の値</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の結果</returns>
    public static Result<T> Ok<T>(T value) => new(value, default!, true);
}

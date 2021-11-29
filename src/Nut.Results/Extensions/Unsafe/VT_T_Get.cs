using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

// ReSharper disable CheckNamespace

namespace Nut.Results;

public static partial class ResultUnsafeExtensions
{
    //this is unsafe method.

    /// <summary>
    /// 成功の値を取得します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の値</returns>
    /// <exception cref="InvalidOperationException">結果が失敗だった場合に発生します。</exception>
    public static async ValueTask<T> Get<T>(this ValueTask<Result<T>> source)
    {
        var result = await source.ConfigureAwait(false);
        if (!result.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotOkBeforeCheck);
        return result._value;
    }
}

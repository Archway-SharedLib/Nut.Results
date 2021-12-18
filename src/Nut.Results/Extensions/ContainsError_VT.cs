

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 結果が失敗の場合に指定した <see cref="IError"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="error">チェックする <see cref="IError"/></param>
    /// <returns>失敗の値が一致した場合は true 、そうでない場合または結果が成功の場合は false</returns>
    public static ValueTask<bool> ContainsError(this ValueTask<Result> source, IError error)
        => ContainsError(source, error, null);

    /// <summary>
    /// 結果が失敗の場合に指定した <see cref="IError"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="error">チェックする <see cref="IError"/></param>
    /// <param name="comparer">比較する処理</param>
    /// <returns>失敗の値が一致した場合は true 、そうでない場合または結果が成功の場合は false</returns>
    public static async ValueTask<bool> ContainsError(this ValueTask<Result> source, IError error, IEqualityComparer<IError>? comparer)
    {
        var s = await source.ConfigureAwait(false);
        if (s.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(s._errorValue, error);
    }
}

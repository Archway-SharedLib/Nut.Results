

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 結果が失敗の場合に指定した <see cref="Exception"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="error">チェックする <see cref="Exception"/></param>
    /// <returns>失敗の値が一致した場合は true 、そうでない場合または結果が成功の場合は false</returns>
    public static bool ContainsError(this in Result source, Exception error)
        => ContainsError(source, error, null);

    /// <summary>
    /// 結果が失敗の場合に指定した <see cref="Exception"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="error">チェックする <see cref="Exception"/></param>
    /// <param name="comparer">比較する処理</param>
    /// <returns>失敗の値が一致した場合は true 、そうでない場合または結果が成功の場合は false</returns>
    public static bool ContainsError(this in Result source, Exception error, IEqualityComparer<Exception>? comparer)
    {
        if (source.IsOk) return false;
        comparer ??= EqualityComparer<Exception>.Default;
        return comparer.Equals(source._errorValue, error);
    }

    /// <summary>
    /// 結果が失敗の場合に指定した <see cref="Exception"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="error">チェックする <see cref="Exception"/></param>
    /// <returns>失敗の値が一致した場合は true 、そうでない場合または結果が成功の場合は false</returns>
    public static Task<bool> ContainsError(this Task<Result> source, Exception error)
        => ContainsError(source, error, null);

    /// <summary>
    /// 結果が失敗の場合に指定した <see cref="Exception"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="error">チェックする <see cref="Exception"/></param>
    /// <param name="comparer">比較する処理</param>
    /// <returns>失敗の値が一致した場合は true 、そうでない場合または結果が成功の場合は false</returns>
    public static async Task<bool> ContainsError(this Task<Result> source, Exception error, IEqualityComparer<Exception>? comparer)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        var s = await source.ConfigureAwait(false);
        if (s.IsOk) return false;
        comparer ??= EqualityComparer<Exception>.Default;
        return comparer.Equals(s._errorValue, error);
    }
}

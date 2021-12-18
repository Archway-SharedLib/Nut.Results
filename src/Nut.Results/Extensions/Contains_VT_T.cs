using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 結果が成功の場合に指定された比較する値と比較して結果を消します。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="value">比較する値</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>成功の値が、指定された比較する値と一致した場合はtrue、そうでない場合または結果が失敗の場合はfalse</returns>
    public static ValueTask<bool> Contains<T>(this ValueTask<Result<T>> source, T value)
        => Contains(source, value, null);

    /// <summary>
    /// 結果が成功の場合に指定された比較する値と比較して結果を消します。
    /// </summary>
    /// <param name="source">結果</param>
    /// <param name="value">比較する値</param>
    /// <param name="comparer">比較する処理</param>
    /// <typeparam name="T">成功の型</typeparam>
    /// <returns>成功の値が、指定された比較する値と一致した場合はtrue、そうでない場合または結果が失敗の場合はfalse</returns>
    public static async ValueTask<bool> Contains<T>(this ValueTask<Result<T>> source, T value, IEqualityComparer<T>? comparer)
    {
        var s = await source.ConfigureAwait(false);
        if (s.IsError) return false;
        comparer ??= EqualityComparer<T>.Default;
        return comparer.Equals(s._value, value);
    }
}

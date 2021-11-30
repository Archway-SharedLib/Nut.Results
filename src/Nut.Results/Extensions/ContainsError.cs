// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// 指定した <see cref="Result"/> に指定した <see cref="IError"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source"><see cref="IError"/> を持っている <see cref="Result"/></param>
    /// <param name="error">チェックする <see cref="IError"/></param>
    /// <returns>含まれている場合は true 、そうでない場合は false</returns>
    public static bool ContainsError(this in Result source, IError error)
        => ContainsError(source, error, null);

    /// <summary>
    /// 指定した <see cref="Result"/> に指定した <see cref="IError"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source"><see cref="IError"/> を持っている <see cref="Result"/></param>
    /// <param name="error">チェックする <see cref="IError"/></param>
    /// <param name="comparer">比較する処理</param>
    /// <returns>含まれている場合は true 、そうでない場合は false</returns>
    public static bool ContainsError(this in Result source, IError error, IEqualityComparer<IError>? comparer)
    {
        if (source.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(source._errorValue, error);
    }

    /// <summary>
    /// 指定した <see cref="Task{Result}"/> に指定した <see cref="IError"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source"><see cref="IError"/> を持っている <see cref="Task{Result}"/></param>
    /// <param name="error">チェックする <see cref="IError"/></param>
    /// <returns>含まれている場合は true 、そうでない場合は false</returns>
    public static Task<bool> ContainsError(this Task<Result> source, IError error)
        => ContainsError(source, error, null);

    /// <summary>
    /// 指定した <see cref="Task{Result}"/> に指定した <see cref="IError"/> が含まれるかどうかをチェックします。
    /// </summary>
    /// <param name="source"><see cref="IError"/> を持っている <see cref="Task{Result}"/></param>
    /// <param name="error">チェックする <see cref="IError"/></param>
    /// <param name="comparer">比較する処理</param>
    /// <returns>含まれている場合は true 、そうでない場合は false</returns>
    public static async Task<bool> ContainsError(this Task<Result> source, IError error, IEqualityComparer<IError>? comparer)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        var s = await source.ConfigureAwait(false);
        if (s.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(s._errorValue, error);
    }
}

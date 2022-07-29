using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultUnsafeExtensions
{
    /// <summary>
    /// 失敗の場合は例外を発行します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <returns>もととなる結果</returns>
    public static Result ThrowIfError(this in Result source)
    {
        if(!source.IsOk) source._capturedError.Throw();
        return source;
    }

    /// <summary>
    /// 失敗の場合は例外を発行します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <returns>もととなる結果</returns>
    public static async Task<Result> ThrowIfError(this Task<Result> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        var result = await source.ConfigureAwait(false);
        if(!result.IsOk) result._capturedError.Throw();
        return result;
    }
}

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// <see cref="Result"/> の結果をマージします。
    /// </summary>
    /// <remarks>
    /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateException"/> にまとめられます。
    /// </remarks>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static Result Merge(this IEnumerable<Result> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        return ResultHelper.Merge(source);
    }

    /// <summary>
    /// <see cref="Result"/> の結果をマージします。
    /// </summary>
    /// <remarks>
    /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateException"/> にまとめられます。
    /// </remarks>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static async Task<Result> Merge(this IEnumerable<Task<Result>> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        try
        {
            return await ResultHelper.MergeAsync(source);
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }

    /// <summary>
    /// <see cref="Result"/> の結果をマージします。
    /// </summary>
    /// <remarks>
    /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateException"/> にまとめられます。
    /// </remarks>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static async Task<Result> Merge(this Task<IEnumerable<Result>> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        try
        {
            var value = await source.ConfigureAwait(false);
            if (value is null)
            {
                throw new InvalidOperationException(Resources.Strings.Exception_CannotMergeNullReuslts);
            }
            return ResultHelper.Merge(value);
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }

    /// <summary>
    /// <see cref="Result"/> の結果をマージします。
    /// </summary>
    /// <remarks>
    /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateException"/> にまとめられます。
    /// </remarks>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static async Task<Result> Merge(this Task<IEnumerable<Task<Result>>> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        try
        {
            var value = await source.ConfigureAwait(false);
            if (value is null)
            {
                throw new InvalidOperationException(Resources.Strings.Exception_CannotMergeNullReuslts);
            }
            return await ResultHelper.MergeAsync(value);
        }
        catch (Exception e)
        {
            return Result.Error(e);
        }
    }
}

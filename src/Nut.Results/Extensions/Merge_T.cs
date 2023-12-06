using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <typeparam name="T">結果の値の型</typeparam>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static Result<T[]> Merge<T>(this IEnumerable<Result<T>> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        return ResultHelper.Merge(source);
    }

    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <typeparam name="T">結果の値の型</typeparam>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static async Task<Result<T[]>> Merge<T>(this IEnumerable<Task<Result<T>>> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        try
        {
            return await ResultHelper.MergeAsync(source);
        }
        catch (Exception e)
        {
            return Result.Error<T[]>(e);
        }
    }

    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <typeparam name="T">結果の値の型</typeparam>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static async Task<Result<T[]>> Merge<T>(this Task<IEnumerable<Result<T>>> source)
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
            return Result.Error<T[]>(e);
        }
    }

    /// <summary>
    /// <see cref="Result{T}"/> の結果をマージします。
    /// </summary>
    /// <typeparam name="T">結果の値の型</typeparam>
    /// <param name="source">マージする結果</param>
    /// <returns>マージした結果</returns>
    public static async Task<Result<T[]>> Merge<T>(this Task<IEnumerable<Task<Result<T>>>> source)
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
            return Result.Error<T[]>(e);
        }
    }
}

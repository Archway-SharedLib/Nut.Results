using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// もとの結果が成功の場合に、引数で指定された処理を実行し、その結果ともとの結果の値を混ぜた値を返します。
    /// </summary>
    /// <param name="source">もとの結果</param>
    /// <param name="rightFunc">処理</param>
    /// <typeparam name="TLeft">もとの結果の成功の型</typeparam>
    /// <typeparam name="TRight">処理の結果の成功の型</typeparam>
    /// <returns>もとの結果の成功の値と、処理の結果の成功の値を混ぜた値</returns>
    public static async ValueTask<Result<(TLeft Left, TRight Right)>> Combine<TLeft, TRight>(
        this ValueTask<Result<TLeft>> source,
        Func<Result<TRight>> rightFunc)
    {
        if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));

        var sourceResult = await source.ConfigureAwait(false);
        return Combine(sourceResult, rightFunc);
    }

    /// <summary>
    /// もとの結果が成功の場合に、引数で指定された処理を実行し、その結果ともとの結果の値を混ぜた値を返します。
    /// </summary>
    /// <param name="source">もとの結果</param>
    /// <param name="rightFunc">処理</param>
    /// <typeparam name="TLeft">もとの結果の成功の型</typeparam>
    /// <typeparam name="TRight">処理の結果の成功の型</typeparam>
    /// <returns>もとの結果の成功の値と、処理の結果の成功の値を混ぜた値</returns>
    public static async ValueTask<Result<(TLeft Left, TRight Right)>> Combine<TLeft, TRight>(
        this Result<TLeft> source,
        Func<ValueTask<Result<TRight>>> rightFunc)
    {
        if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));

        if (source.IsError)
        {
            return Result.Error<(TLeft Left, TRight Right)>(source.GetError());
        }
        var dest = await rightFunc().ConfigureAwait(false);
        return dest.IsError ?
            Result.Error<(TLeft Left, TRight Right)>(dest.GetError()) :
            Result.Ok((source.Get(), dest.Get()));
    }

    /// <summary>
    /// もとの結果が成功の場合に、引数で指定された処理を実行し、その結果ともとの結果の値を混ぜた値を返します。
    /// </summary>
    /// <param name="source">もとの結果</param>
    /// <param name="rightFunc">処理</param>
    /// <typeparam name="TLeft">もとの結果の成功の型</typeparam>
    /// <typeparam name="TRight">処理の結果の成功の型</typeparam>
    /// <returns>もとの結果の成功の値と、処理の結果の成功の値を混ぜた値</returns>
    public static async ValueTask<Result<(TLeft Left, TRight Right)>> Combine<TLeft, TRight>(
        this ValueTask<Result<TLeft>> source,
        Func<ValueTask<Result<TRight>>> rightFunc)
    {
        if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));

        var sourceResult = await source.ConfigureAwait(false);
        return await Combine(sourceResult, rightFunc);
    }

    //---------------

    /// <summary>
    /// もとの結果が成功の場合に、引数で指定された処理を実行し、その結果ともとの結果の値を混ぜた値を返します。
    /// </summary>
    /// <param name="source">もとの結果</param>
    /// <param name="rightFunc">処理</param>
    /// <typeparam name="TLeft">もとの結果の成功の型</typeparam>
    /// <typeparam name="TRight">処理の結果の成功の型</typeparam>
    /// <returns>もとの結果の成功の値と、処理の結果の成功の値を混ぜた値</returns>
    public static async ValueTask<Result<(TLeft Left, TRight Right)>> Combine<TLeft, TRight>(
        this ValueTask<Result<TLeft>> source,
        Func<Task<Result<TRight>>> rightFunc)
    {
        if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));

        var sourceResult = await source.ConfigureAwait(false);
        if (sourceResult.IsError)
        {
            return Result.Error<(TLeft Left, TRight Right)>(sourceResult.GetError());
        }

        var rightResult = await rightFunc().ConfigureAwait(false);
        if (rightResult.IsError)
        {
            return Result.Error<(TLeft Left, TRight Right)>(rightResult.GetError());
        }
        return Result.Ok((sourceResult.Get(), rightResult.Get()));
    }

    /// <summary>
    /// もとの結果が成功の場合に、引数で指定された処理を実行し、その結果ともとの結果の値を混ぜた値を返します。
    /// </summary>
    /// <param name="source">もとの結果</param>
    /// <param name="rightFunc">処理</param>
    /// <typeparam name="TLeft">もとの結果の成功の型</typeparam>
    /// <typeparam name="TRight">処理の結果の成功の型</typeparam>
    /// <returns>もとの結果の成功の値と、処理の結果の成功の値を混ぜた値</returns>
    public static async ValueTask<Result<(TLeft Left, TRight Right)>> Combine<TLeft, TRight>(
        this Task<Result<TLeft>> source,
        Func<ValueTask<Result<TRight>>> rightFunc)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (rightFunc is null) throw new ArgumentNullException(nameof(rightFunc));

        var sourceResult = await source.ConfigureAwait(false);
        if (sourceResult.IsError)
        {
            return Result.Error<(TLeft Left, TRight Right)>(sourceResult.GetError());
        }
        var dest = await rightFunc().ConfigureAwait(false);
        return dest.IsError ?
            Result.Error<(TLeft Left, TRight Right)>(dest.GetError()) :
            Result.Ok((sourceResult.Get(), dest.Get()));
    }
}

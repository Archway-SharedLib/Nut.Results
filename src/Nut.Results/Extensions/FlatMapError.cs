using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    // Void -> Void

    // sync - sync Void -> Void
    public static Result FlatMapError(this in Result source, Func<IError, Result> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        return !source.IsError ? source : error(source._errorValue);
    }

    //async - sync Void -> Void
    public static async Task<Result> FlatMapError(this Task<Result> source, Func<IError, Result> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        return !result.IsError ? result : error(result._errorValue);
    }

    //sync - async Void -> Void
    public static async Task<Result> FlatMapError(this Result source, Func<IError, Task<Result>> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (!source.IsError) return source;

        return await error(source._errorValue).ConfigureAwait(false);
    }

    //async - async Void -> Void
    public static async Task<Result> FlatMapError(this Task<Result> source, Func<IError, Task<Result>> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);
        if (!result.IsError) return result;

        return await error(result._errorValue).ConfigureAwait(false);
    }
}

using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    //sync - sync
    public static Result<T> TapError<T>(this in Result<T> source, Action<IError> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (source.IsError) error(source._errorValue);
        return source;
    }

    //async - sync
    public static async Task<Result<T>> TapError<T>(this Task<Result<T>> source, Action<IError> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));

        var result = await source.ConfigureAwait(false);

        if (result.IsError) error(result._errorValue);
        return result;
    }

    //sync - async
    public static async Task<Result<T>> TapError<T>(this Result<T> source, Func<IError, Task> error)
    {
        if (error is null) throw new ArgumentNullException(nameof(error));
        if (source.IsError) await error(source._errorValue).ConfigureAwait(false);
        return source;
    }

    //async - async
    public static async Task<Result<T>> TapError<T>(this Task<Result<T>> source, Func<IError, Task> error)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (error is null) throw new ArgumentNullException(nameof(error));
        var result = await source.ConfigureAwait(false);

        if (result.IsError) await error(result._errorValue).ConfigureAwait(false);
        return result;
    }
}

using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results;

public static partial class ResultExtensions
{
    //sync - sync
    public static IError GetErrorOr<T, TError>(this in Result<T> source, Func<T, TError> ifOk) where TError : IError
    {
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        return source.IsOk ? ifOk(source._value) : source._errorValue;
    }

    //async - sync
    public static async Task<IError> GetErrorOr<T, TError>(this Task<Result<T>> source, Func<T, TError> ifOk) where TError : IError
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        var result = await source.ConfigureAwait(false);
        return result.IsOk ? ifOk(result._value) : result._errorValue;
    }

    //sync - async
    public static async Task<IError> GetErrorOr<T, TError>(this Result<T> source, Func<T, Task<TError>> ifOk) where TError : IError
    {
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        if (source.IsOk) return await ifOk(source._value);
        return source._errorValue;
    }

    //async - async
    public static async Task<IError> GetErrorOr<T, TError>(this Task<Result<T>> source, Func<T, Task<TError>> ifOk) where TError : IError
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

        var result = await source.ConfigureAwait(false);
        if (result.IsOk) return await ifOk(result._value);
        return result._errorValue;
    }
}

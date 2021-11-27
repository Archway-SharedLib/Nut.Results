using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

// ReSharper disable CheckNamespace

namespace Nut.Results;

public static partial class ResultUnsafeExtensions
{
    public static IError GetError<T>(this in Result<T> source)
    {
        if (source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return source._errorValue;
    }

    public static async Task<IError> GetError<T>(this Task<Result<T>> source)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));

        var result = await source.ConfigureAwait(false);
        if (result.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return result._errorValue;
    }
}

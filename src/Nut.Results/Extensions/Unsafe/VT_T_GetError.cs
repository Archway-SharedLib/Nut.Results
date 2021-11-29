using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

// ReSharper disable CheckNamespace

namespace Nut.Results;

public static partial class ResultUnsafeExtensions
{
    public static async ValueTask<IError> GetError<T>(this ValueTask<Result<T>> source)
    {
        var result = await source.ConfigureAwait(false);
        if (result.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return result._errorValue;
    }
}

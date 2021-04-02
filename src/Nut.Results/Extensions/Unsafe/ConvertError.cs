using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results
{
    public static partial class ResultUnsafeExtensions
    {

        public static Result<T> ConvertError<T>(this in Result source)
        {
            if (source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return Result.Error<T>(source.errorValue);
        }

        public static async Task<Result<T>> ConvertError<T>(this Task<Result> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            
            var res = await source.ConfigureAwait(false);
            if (res.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return Result.Error<T>(res.errorValue);
        }
    }
}
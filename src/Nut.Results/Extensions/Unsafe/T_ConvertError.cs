using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results
{
    public static partial class ResultUnsafeExtensions
    {
        public static Result<TResult> ConvertError<T, TResult>(this in Result<T> source)
        {
            if (source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return Result.Error<TResult>(source.errorValue);
        }

        public static async Task<Result<TResult>> ConvertError<T, TResult>(this Task<Result<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            
            var res = await source.ConfigureAwait(false);
            if (res.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return Result.Error<TResult>(res.errorValue);
        }
        
        public static Result ConvertError<T>(this in Result<T> source)
        {
            if (source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return Result.Error(source.errorValue);
        }

        public static async Task<Result> ConvertError<T>(this Task<Result<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            
            var res = await source.ConfigureAwait(false);
            if (res.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return Result.Error(res.errorValue);
        }
    }
}
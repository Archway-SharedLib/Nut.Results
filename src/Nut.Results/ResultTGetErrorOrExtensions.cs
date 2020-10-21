using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static class ResultTGetErrorOrExtensions
    {
        //sync - sync
        public static IError GetErrorOr<T, TError>(this in Result<T> source, Func<T, TError> ifOk) where TError : IError
        {
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            if (source.IsOk) return ifOk(source.value);
            return source.errorValue;
        }

        //async - sync
        public static async Task<IError> GetErrorOr<T, TError>(this Task<Result<T>> source, Func<T, TError> ifOk) where TError : IError
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) return ifOk(result.value);
            return result.errorValue;
        }

        //sync - async
        public static async Task<IError> GetErrorOr<T, TError>(this Result<T> source, Func<T, Task<TError>> ifOk) where TError : IError
        {
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            if (source.IsOk) return await ifOk(source.value);
            return source.errorValue;
        }

        //async - async
        public static async Task<IError> GetErrorOr<T, TError>(this Task<Result<T>> source, Func<T, Task<TError>> ifOk) where TError : IError
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) return await ifOk(result.value);
            return result.errorValue;
        }
    }
}

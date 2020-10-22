using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        //sync - sync
        public static T GetOr<T>(this in Result<T> source, Func<IError, T> ifError)
        {
            if (ifError is null) throw new ArgumentNullException(nameof(ifError));

            if (source.IsError) return ifError(source.errorValue);
            return source.value;
        }

        //async - sync
        public static async Task<T> GetOr<T>(this Task<Result<T>> source, Func<IError, T> ifError)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ifError is null) throw new ArgumentNullException(nameof(ifError));

            var result = await source.ConfigureAwait(false);
            if (result.IsError) return ifError(result.errorValue);
            return result.value;
        }

        //sync - async
        public static async Task<T> GetOr<T>(this Result<T> source, Func<IError, Task<T>> ifError)
        {
            if (ifError is null) throw new ArgumentNullException(nameof(ifError));

            if (source.IsError) return await ifError(source.errorValue);
            return source.value;
        }

        //async - async
        public static async Task<T> GetOr<T>(this Task<Result<T>> source, Func<IError, Task<T>> ifError)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ifError is null) throw new ArgumentNullException(nameof(ifError));

            var result = await source.ConfigureAwait(false);
            if (result.IsError) return await ifError(result.errorValue);
            return result.value;
        }
    }
}

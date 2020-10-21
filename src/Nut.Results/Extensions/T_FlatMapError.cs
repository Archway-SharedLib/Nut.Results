using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        // T -> T
        
        // sync - sync T -> T
        public static Result<T> FlatMapError<T>(this in Result<T> source, Func<IError, Result<T>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            return error(source.errorValue);
        }

        //async - sync T -> T
        public static async Task<Result<T>> FlatMapError<T>(this Task<Result<T>> source, Func<IError, Result<T>> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            return error(result.errorValue);
        }

        //sync - async T -> T
        public static async Task<Result<T>> FlatMapError<T>(this Result<T> source, Func<IError, Task<Result<T>>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            return await error(source.errorValue).ConfigureAwait(false);
        }

        //async - async T -> T
        public static async Task<Result<T>> FlatMapError<T>(this Task<Result<T>> source, Func<IError, Task<Result<T>>> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            return await error(result.errorValue).ConfigureAwait(false);
        }
    }
}
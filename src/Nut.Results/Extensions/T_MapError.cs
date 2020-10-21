using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        // sync - sync
        public static Result<T> MapError<T, TError>(this in Result<T> source, Func<IError, TError> error) where TError : IError
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(error(source.errorValue)));
        }

        //async - sync
        public static async Task<Result<T>> MapError<T, TError>(this Task<Result<T>> source, Func<IError, TError> error) where TError : IError
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(error(result.errorValue)));
        }

        //sync - async
        public static async Task<Result<T>> MapError<T, TError>(this Result<T> source, Func<IError, Task<TError>> error) where TError: IError
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            var returnValue = error(source.errorValue);
            if (returnValue == null) InternalUtility.RaizeReturnValueNotNull();
            var result = await returnValue!.ConfigureAwait(false);
            return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(result));
        }

        //async - async
        public static async Task<Result<T>> MapError<T, TError>(this Task<Result<T>> source, Func<IError, Task<TError>> error) where TError : IError
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            var returnValue = error(result.errorValue);
            if (returnValue == null) InternalUtility.RaizeReturnValueNotNull();
            var newReturnValue = await returnValue!.ConfigureAwait(false);
            return Result.Error<T>(InternalUtility.CheckReturnValueNotNull(newReturnValue));
        }
    }
}
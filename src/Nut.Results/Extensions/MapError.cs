using System;
using System.Threading.Tasks;
using Nut.Results.Internals;

// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        // sync - sync
        public static Result MapError<TError>(this in Result source, Func<IError, TError> error) where TError: IError
        {
            if (error is null) throw new ArgumentNullException(nameof(error));
            return !source.IsError ? source : Result.Error(InternalUtility.CheckReturnValueNotNull(error(source.errorValue)));
        }

        //async - sync
        public static async Task<Result> MapError<TError>(this Task<Result> source, Func<IError, TError> error) where TError : IError
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (error is null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            return !result.IsError ? result : Result.Error(InternalUtility.CheckReturnValueNotNull(error(result.errorValue)));
        }

        //sync - async
        public static async Task<Result> MapError<TError>(this Result source, Func<IError, Task<TError>> error) where TError : IError
        {
            if (error is null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            var errorCallbackResult = error(source.errorValue);
            if (errorCallbackResult == null) InternalUtility.RaizeReturnValueNotNull();
            var result = await errorCallbackResult!.ConfigureAwait(false);
            return Result.Error(InternalUtility.CheckReturnValueNotNull(result));
        }

        //async - async
        public static async Task<Result> MapError<TError>(this Task<Result> source, Func<IError, Task<TError>> error) where TError : IError
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (error is null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            var errorCallbackResult = error(result.errorValue);
            if (errorCallbackResult == null) InternalUtility.RaizeReturnValueNotNull();
            var newReturnValue = await errorCallbackResult!.ConfigureAwait(false);
            return Result.Error(InternalUtility.CheckReturnValueNotNull(newReturnValue));
        }
    }
}
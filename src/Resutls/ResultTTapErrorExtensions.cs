using System;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultTTapErrorExtensions
    {
        //sync - sync
        public static Result<T> TapError<T>(this in Result<T> source, Action<IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (source.IsError) error(source.errorValue);
            return source;
        }
        
        //async - sync
        public static async Task<Result<T>> TapError<T>(this Task<Result<T>> source, Action<IError> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            
            if (result.IsError) error(result.errorValue);
            return result;
        }
        
        //sync - async
        public static async Task<Result<T>> TapError<T>(this Result<T> source, Func<IError, Task> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (source.IsError) await error(source.errorValue).ConfigureAwait(false);
            return source;
        }
        
        //async - async
        public static async Task<Result<T>> TapError<T>(this Task<Result<T>> source, Func<IError, Task> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));
            var result = await source.ConfigureAwait(false);
            
            if (result.IsError) await error(result.errorValue).ConfigureAwait(false);
            return result;
        }
    }
}
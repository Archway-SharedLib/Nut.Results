using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        //sync - sync
        public static Result TapError(this in Result source, Action<IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (source.IsError) error(source.errorValue);
            return source;
        }
        
        //async - sync
        public static async Task<Result> TapError(this Task<Result> source, Action<IError> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            
            if (result.IsError) error(result.errorValue);
            return result;
        }
        
        //sync - async
        public static async Task<Result> TapError(this Result source, Func<IError, Task> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (source.IsError) await error(source.errorValue).ConfigureAwait(false);
            return source;
        }
        
        //async - async
        public static async Task<Result> TapError(this Task<Result> source, Func<IError, Task> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));
            var result = await source.ConfigureAwait(false);
            
            if (result.IsError) await error(result.errorValue).ConfigureAwait(false);
            return result;
        }
    }
}
using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        // Void -> Void
        
        // sync - sync Void -> Void
        public static Result FlatMapError(this in Result source, Func<IError, Result> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            return error(source.errorValue);
        }

        //async - sync Void -> Void
        public static async Task<Result> FlatMapError(this Task<Result> source, Func<IError, Result> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            return error(result.errorValue);
        }

        //sync - async Void -> Void
        public static async Task<Result> FlatMapError(this Result source, Func<IError, Task<Result>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!source.IsError) return source;

            return await error(source.errorValue).ConfigureAwait(false);
        }

        //async - async Void -> Void
        public static async Task<Result> FlatMapError(this Task<Result> source, Func<IError, Task<Result>> error)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));
            if (error == null) throw new ArgumentNullException(nameof(error));

            var result = await source.ConfigureAwait(false);
            if (!result.IsError) return result;

            return await error(result.errorValue).ConfigureAwait(false);
        }
    }
}
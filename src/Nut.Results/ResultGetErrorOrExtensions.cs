using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static class ResultGetErrorOrExtensions
    {
        //sync - sync
        public static IError GetErrorOr(this in Result source, Func<IError> ifOk)
        {
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            if (source.IsOk) return ifOk();
            return source.errorValue;
        }

        //async - sync
        public static async Task<IError> GetErrorOr(this Task<Result> source, Func<IError> ifOk)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) return ifOk();
            return result.errorValue;
        }

        //sync - async
        public static async Task<IError> GetErrorOr(this Result source, Func<Task<IError>> ifOk)
        {
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            if (source.IsOk) return await ifOk();
            return source.errorValue;
        }

        //async - async
        public static async Task<IError> GetErrorOr(this Task<Result> source, Func<Task<IError>> ifOk)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ifOk is null) throw new ArgumentNullException(nameof(ifOk));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) return await ifOk();
            return result.errorValue;
        }
    }
}

using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static class ResultTUnsafeExtensions
    {
        private const string NotOkMessage = "Result is not ok. You must check before.";
        private const string NotErrorMessage = "Result is not error. You must check before.";

        //this is unsafe method.
        public static T Get<T>(this in Result<T> source)
        {
            if (!source.IsOk) throw new InvalidOperationException(NotOkMessage);
            return source.value;
        }

        public static async Task<T> Get<T>(this Task<Result<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) throw new InvalidOperationException(NotOkMessage);
            return result.value;
        }

        public static IError GetError<T>(this in Result<T> source)
        {
            if (source.IsOk) throw new InvalidOperationException(NotErrorMessage);
            return source.errorValue;
        }

        public static async Task<IError> GetError<T>(this Task<Result<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) throw new InvalidOperationException(NotErrorMessage);
            return result.errorValue;
        }
    }
}
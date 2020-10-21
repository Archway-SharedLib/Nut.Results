using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        private const string NotErrorMessage = "Result is not error. You must check before.";

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
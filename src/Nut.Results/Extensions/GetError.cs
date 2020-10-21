using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        private const string ErrorMessage = "Result is not error. You must check before.";

        //this is unsafe method.
        public static IError GetError(this in Result source)
        {
            if (source.IsOk) throw new InvalidOperationException(ErrorMessage);
            return source.errorValue;
        }

        public static async Task<IError> GetError(this Task<Result> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) throw new InvalidOperationException(ErrorMessage);
            return result.errorValue;
        }
    }
}

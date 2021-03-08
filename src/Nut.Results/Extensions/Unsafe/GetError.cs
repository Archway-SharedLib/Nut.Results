using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;

// ReSharper disable CheckNamespace

namespace Nut.Results
{
    public static partial class ResultUnsafeExtensions
    {
        //this is unsafe method.
        public static IError GetError(this in Result source)
        {
            if (source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return source.errorValue;
        }

        public static async Task<IError> GetError(this Task<Result> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var result = await source.ConfigureAwait(false);
            if (result.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
            return result.errorValue;
        }
    }
}

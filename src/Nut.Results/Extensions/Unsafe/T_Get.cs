using System;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace

namespace Nut.Results
{
    public static partial class ResultUnsafeExtensions
    {
        //this is unsafe method.
        public static T Get<T>(this in Result<T> source)
        {
            if (!source.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotOkBeforeCheck);
            return source.value;
        }

        public static async Task<T> Get<T>(this Task<Result<T>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));

            var result = await source.ConfigureAwait(false);
            if (!result.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotOkBeforeCheck);
            return result.value;
        }
    }
}
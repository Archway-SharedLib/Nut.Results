using System;

namespace Archway.Results
{
    public static class ResultTUnsafeExtensions
    {
        //this is unsafe method.
        public static T Get<T>(this Result<T> source)
        {
            if (!source.IsOk) throw new InvalidOperationException("Result is not ok. You must check before.");
            return source.value;
        }

        public static IError GetError<T>(this Result<T> source)
        {
            if (source.IsOk) throw new InvalidOperationException("Result is not error. You must check before.");
            return source.errorValue;
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public static class ResultUnsafeExtensions
    {
        //this is unsafe method.
        public static IError GetError(this Result source)
        {
            if (source.IsOk) throw new InvalidOperationException("Result is not error. You must check before.");
            return source.errorValue;
        }
    }
}

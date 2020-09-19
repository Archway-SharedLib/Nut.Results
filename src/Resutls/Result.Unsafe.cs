using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        //this is unsafe method.
        public IError GetError()
        {
            if (IsOk) throw new InvalidOperationException("Result is not error. You must check before.");
            return errorValue;
        }
    }

    public readonly partial struct Result<T>
    {
        //this is unsafe method.
        public T Get()
        {
            if (!IsOk) throw new InvalidOperationException("Result is not ok. You must check before.");
            return value;
        }

        public IError GetError()
        {
            if (IsOk) throw new InvalidOperationException("Result is not error. You must check before.");
            return errorValue;
        }
    }
}

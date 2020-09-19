using System;
using System.Collections.Generic;
using System.Text;

namespace Archway.Results
{
    public interface IError
    {
        string Message { get; }

        Exception ToException()
        {
            return string.IsNullOrEmpty(Message) ? new Exception() : new Exception(Message);
        }
    }
}

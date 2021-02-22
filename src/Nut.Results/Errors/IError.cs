using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    public interface IError
    {
        string? Message { get; }

        Exception ToException()
            => new ResultErrorException(this, Message);
    }
}

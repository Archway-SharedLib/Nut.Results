using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    public class Error : IError
    {
        public Error()
        {
            Message = string.Empty;
        }

        public Error(string message)
        {
            Message = message;
        }

        public string Message { get; }

        public override string ToString()
        {
            return Message;
        }
    }
}

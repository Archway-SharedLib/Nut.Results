using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    public class Error : IError
    {
        public Error()
        {
            Message = SR.Error_DefaultErrorMessage;
        }

        public Error(string message)
        {
            Message = message ?? SR.Error_DefaultErrorMessage;
        }

        public string Message { get; }

        public override string ToString()
        {
            return Message;
        }
    }
}

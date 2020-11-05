using System;

namespace Nut.Results
{
    public class InvalidReturnValueException: Exception
    {
        public InvalidReturnValueException(): base(SR.Exception_InvalidReturnValue){}
        
        public InvalidReturnValueException(string message): base(message){}
    }
}
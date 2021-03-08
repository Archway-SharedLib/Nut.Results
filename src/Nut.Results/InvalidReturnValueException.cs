using System;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results
{
    public class InvalidReturnValueException: Exception
    {
        public InvalidReturnValueException(): base(SR.Exception_InvalidReturnValue){}
        
        public InvalidReturnValueException(string message): base(message){}
    }
}
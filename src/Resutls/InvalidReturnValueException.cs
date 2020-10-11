using System;

namespace Archway.Results
{
    public class InvalidReturnValueException: Exception
    {
        private const string DefaultErrorMessage = "Invalid return value.";
        
        public InvalidReturnValueException(): base(DefaultErrorMessage){}
        
        public InvalidReturnValueException(string message): base(message){}
    }
}
using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    public class ExceptionalError : IError
    {

        public ExceptionalError(Exception sourceException)
        {
            this.Exception = sourceException ?? throw new ArgumentNullException(nameof(sourceException));
            this.Message = Exception.Message ?? SR.Error_DefaultErrorMessage;
        }

        public ExceptionalError(Exception sourceException, string message)
        {
            this.Exception = sourceException ?? throw new ArgumentNullException(nameof(sourceException));
            this.Message = message ?? SR.Error_DefaultErrorMessage;
        }

        public string Message { get; }

        public Exception Exception { get; }

        Exception IError.ToException()
        {
            return this.Exception;
        }
    }
}

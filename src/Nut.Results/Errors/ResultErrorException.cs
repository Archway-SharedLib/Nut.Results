using System;

namespace Nut.Results
{
    public class ResultErrorException: Exception
    {
        public IError SourceError { get; }

        public ResultErrorException(IError sourceError) :
            this(sourceError, sourceError?.Message)
        {
        }

        public ResultErrorException(IError sourceError, string? message) :
            base(message)
        {
            SourceError = sourceError ?? throw new ArgumentNullException(nameof(sourceError));
        }
        
    }
}
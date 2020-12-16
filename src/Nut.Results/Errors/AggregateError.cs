using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Nut.Results
{
    public class AggregateError : Error
    {
        public AggregateError() : this(SR.Error_DefaultAggregateErrorMessage) { }

        public AggregateError(string message) : this(message, Enumerable.Empty<IError>())
        {
        }

        public AggregateError(params IError[] errors) : this(SR.Error_DefaultAggregateErrorMessage, errors) { }

        public AggregateError(IEnumerable<IError> errors) : this(SR.Error_DefaultAggregateErrorMessage, errors) { }

        public AggregateError(string message, IEnumerable<IError> errors) : base(message)
        {
            if (errors is null) throw new ArgumentNullException(nameof(errors));
            var errorList = errors.ToList();
            if (errorList.Any(err => err is null))
            {
                throw new ArgumentException(SR.Exception_AggregateErrorIncludeNullInErrors);
            }
            Errors = new ReadOnlyCollection<IError>(errorList);
        }

        public ReadOnlyCollection<IError> Errors { get; }
    }
}

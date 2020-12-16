using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nut.Results
{
    public readonly partial struct Result
    {
        public static Result Ok() => new (null, true);

        public static Result Error(IError error) => new (error, false);

        public static Result Error(string message) => new (new Error(message), false);

        public static Result<T> Ok<T>(T value) => new (value, default!, true);

        public static Result<T> Error<T>(IError error) => new (default!, error, false);

        public static Result<T> Error<T>(string message) => new (default!, new Error(message), false);
    }
}

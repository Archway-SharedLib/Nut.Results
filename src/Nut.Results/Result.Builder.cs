﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Nut.Results
{
    public readonly partial struct Result
    {
        public static Result Ok() => new Result(null, true);

        public static Result Error(IError error) => new Result(error, false);

        public static Result Error(string message) => new Result(new Error(message), false);

        public static Result<T> Ok<T>(T value) => new Result<T>(value, default!, true);

        public static Result<T> Error<T>(IError error) => new Result<T>(default!, error, false);

        public static Result<T> Error<T>(string message) => new Result<T>(default!, new Error(message), false);
    }
}
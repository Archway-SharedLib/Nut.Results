using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results;

public static partial class ResultExtensions
{
    public static Result<T> Flatten<T>(this Result<Result<T>> source)
        => source.IsError ? Result.Error<T>(source.GetError()) : source.Get();

    public static Result Flatten(this Result<Result> source)
        => source.IsError ? Result.Error(source.GetError()) : source.Get();
}

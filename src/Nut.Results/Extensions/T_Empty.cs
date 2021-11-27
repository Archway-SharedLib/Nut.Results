using System;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    public static Result Empty<T>(this in Result<T> source)
        => source.FlatMap(_ => Result.Ok());

    public static Task<Result> Empty<T>(this Task<Result<T>> source)
        => source.FlatMap(_ => Result.Ok());
}

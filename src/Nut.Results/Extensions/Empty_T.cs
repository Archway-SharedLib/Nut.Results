using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// <see cref="Result{T}"/>を<see cref="Result"/>に変換します。
    /// </summary>
    /// <param name="source">変換元の値</param>
    /// <typeparam name="T">変換元の値の型</typeparam>
    /// <returns>変換された<see cref="Result"/></returns>
    public static Result Empty<T>(this in Result<T> source)
        => source.FlatMap(_ => Result.Ok());

    /// <summary>
    /// <see cref="Result{T}"/>を<see cref="Result"/>に変換します。
    /// </summary>
    /// <param name="source">変換元の値</param>
    /// <typeparam name="T">変換元の値の型</typeparam>
    /// <returns>変換された<see cref="Result"/></returns>
    public static Task<Result> Empty<T>(this Task<Result<T>> source)
        => source.FlatMap(_ => Result.Ok());
}

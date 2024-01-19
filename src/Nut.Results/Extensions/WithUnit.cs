using System.Threading.Tasks;

namespace Nut.Results;

/// <summary>
/// <see cref="Result"/> および <see cref="Result{T}"/> の拡張メソッドを定義します。
/// </summary>
public static partial class ResultExtensions
{
    /// <summary>
    /// <see cref="Result{Unit}"/> に変換した値を返します。
    /// </summary>
    /// <param name="source"><see cref="Result{Unit}"/> に変換する<see cref="Result"/></param>
    /// <returns><see cref="Result{Unit}"/> に変換された入力</returns>
    public static Result<Unit> WithUnit(this in Result source)
        => source.FlatMap(() => Result.Ok(Unit.Default));

    /// <summary>
    /// <see cref="Result{Unit}"/> に変換した値を返します。
    /// </summary>
    /// <param name="source"><see cref="Result{Unit}"/> に変換する<see cref="Result"/></param>
    /// <returns><see cref="Result{Unit}"/> に変換された入力</returns>
    public static Task<Result<Unit>> WithUnit(this Task<Result> source)
        => source.FlatMap(() => Result.Ok(Unit.Default));
}

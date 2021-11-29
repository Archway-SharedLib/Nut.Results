// ReSharper disable CheckNamespace

using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// <see cref="Result{T}"/>をもった<see cref="Task{T}"/>として型指定された入力を返します。
    /// </summary>
    /// <typeparam name="T"><see cref="Result{T}"/>の値の型</typeparam>
    /// <param name="source"><see cref="Result{T}"/>をもった<see cref="Task{T}"/>として型指定する<see cref="Result{T}"/></param>
    /// <returns><see cref="Result{T}"/>をもった<see cref="Task{T}"/>として型指定された入力</returns>
    public static Task<Result<T>> AsTask<T>(this Result<T> source) => Task.FromResult(source);
}

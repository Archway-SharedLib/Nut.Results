// ReSharper disable CheckNamespace

using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// <see cref="Task{T}"/> として型指定された入力を返します。
        /// </summary>
        /// <param name="source"><see cref="Task{T}"/>として型指定する<see cref="Result"/></param>
        /// <returns><see cref="Task{T}"/> として型指定された入力</returns>
        public static Task<Result> AsTask(this Result source) => Task.FromResult(source);
    }
}
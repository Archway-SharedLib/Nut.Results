// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Linq;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// <see cref="Result{T}"/> の結果をマージします。
        /// </summary>
        /// <typeparam name="T">結果の値の型</typeparam>
        /// <param name="source">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static Result<T[]> Merge<T>(this IEnumerable<Result<T>> source) => ResultHelper.Merge(source.ToArray());
    }
}

// ReSharper disable CheckNamespace

using System.Collections.Generic;
using System.Linq;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        /// <summary>
        /// <see cref="Result"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <param name="source">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static Result Merge(this IEnumerable<Result> source) => ResultHelper.Merge(source.ToArray());
    }
}

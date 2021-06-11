// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
        public static Result Merge(this IEnumerable<Result> source) => ResultHelper.Merge(source?.ToArray()!);

        /// <summary>
        /// <see cref="Result"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <param name="source">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static Task<Result> Merge(this IEnumerable<Task<Result>> source) => ResultHelper.MergeAsync(source?.ToArray()!);

        /// <summary>
        /// <see cref="Result"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <param name="source">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static async Task<Result> Merge(this Task<IEnumerable<Result>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            var value = await source.ConfigureAwait(false);
            return ResultHelper.Merge(value?.ToArray()!);
        }

        /// <summary>
        /// <see cref="Result"/> の結果をマージします。
        /// </summary>
        /// <remarks>
        /// 全ての結果が成功している場合は、成功になります。一つでも失敗があると失敗になり、エラーは <see cref="AggregateError"/> にまとめられます。
        /// </remarks>
        /// <param name="source">マージする結果</param>
        /// <returns>マージした結果</returns>
        public static async Task<Result> Merge(this Task<IEnumerable<Task<Result>>> source)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            var value = await source.ConfigureAwait(false);
            return await ResultHelper.MergeAsync(value?.ToArray()!);
        }
    }
}

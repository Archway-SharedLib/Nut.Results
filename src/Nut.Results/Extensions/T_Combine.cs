using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {

        public static Result<(TSource Left, TDest Right)> Combine<TSource, TDest>(
            this in Result<TSource> source,
            Func<Result<TDest>> destFunc)
        {

            if (destFunc is null) throw new ArgumentNullException(nameof(destFunc));

            if (source.IsError)
            {
                return Result.Error<(TSource Left, TDest Right)>(source.GetError());
            }
            var dest = destFunc();
            if (dest.IsError) return Result.Error<(TSource Left, TDest Right)>(dest.GetError());
            return Result.Ok((source.Get(), dest.Get()));
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Task<Result<TSource>> source,
            Func<Result<TDest>> destFunc)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (destFunc is null) throw new ArgumentNullException(nameof(destFunc));

            var sourceResult = await source.ConfigureAwait(false);
            return Combine(sourceResult, destFunc);
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Task<Result<TSource>> source,
            Func<Task<Result<TDest>>> destFunc)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (destFunc is null) throw new ArgumentNullException(nameof(destFunc));

            var sourceResult = await source.ConfigureAwait(false);
            return await Combine(sourceResult, destFunc).ConfigureAwait(false);
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Result<TSource> source,
            Func<Task<Result<TDest>>> destFunc)
        {
            if (destFunc is null) throw new ArgumentNullException(nameof(destFunc));
            
            if (source.IsError)
            {
                return Result.Error<(TSource Left, TDest Right)>(source.GetError());
            }
            var dest = await destFunc().ConfigureAwait(false);
            if (dest.IsError) return Result.Error<(TSource Left, TDest Right)>(dest.GetError());
            return Result.Ok((source.Get(), dest.Get()));
        }
    }
}

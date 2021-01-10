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
            in Result<TDest> dest)
        {
            if (source.IsError || dest.IsError)
            {
                if (source.IsError && dest.IsError)
                {
                    return Result.Error<(TSource Left, TDest Right)>(
                        new AggregateError(GetErrors(source.GetError()).Concat(GetErrors(dest.GetError())).ToArray()));
                }
                else
                {
                    return Result.Error<(TSource Left, TDest Right)>(source.IsError ? source.GetError() : dest.GetError());
                }
            }
            return Result.Ok((source.Get(), dest.Get()));
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Task<Result<TSource>> source,
            Task<Result<TDest>> dest)
        {
            var sourceResult = await source.ConfigureAwait(false);
            var destResult = await dest.ConfigureAwait(false);
            return Combine(sourceResult, destResult);
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Result<TSource> source,
            Task<Result<TDest>> dest)
        {
            var destResult = await dest.ConfigureAwait(false);
            return Combine(source, destResult);
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Task<Result<TSource>> source,
            Result<TDest> dest)
        {
            var sourceResult = await source.ConfigureAwait(false);
            return Combine(sourceResult, dest);
        }

        //--------------

        public static Result<(TSource Left, TDest Right)> Combine<TSource, TDest>(
            this in Result<TSource> source,
            Func<Result<TDest>> destFunc)
        {
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
            var sourceResult = await source.ConfigureAwait(false);
            return Combine(sourceResult, destFunc);
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Task<Result<TSource>> source,
            Func<Task<Result<TDest>>> destFunc)
        {
            var sourceResult = await source.ConfigureAwait(false);
            return await Combine(sourceResult, destFunc).ConfigureAwait(false);
        }

        public static async Task<Result<(TSource Left, TDest Right)>> Combine<TSource, TDest>(
            this Result<TSource> source,
            Func<Task<Result<TDest>>> destFunc)
        {
            if (source.IsError)
            {
                return Result.Error<(TSource Left, TDest Right)>(source.GetError());
            }
            var dest = await destFunc().ConfigureAwait(false);
            if (dest.IsError) return Result.Error<(TSource Left, TDest Right)>(dest.GetError());
            return Result.Ok((source.Get(), dest.Get()));
        }

        private static IEnumerable<IError> GetErrors(IError err)
            => err is AggregateError aggrErr ? aggrErr.Errors : new[] { err };

    }
}

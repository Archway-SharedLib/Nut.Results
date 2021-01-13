// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        public static bool ContainsError(this in Result source, IError error)
            => ContainsError(source, error, null);

        public static bool ContainsError(this in Result source, IError error, IEqualityComparer<IError>? comparer)
        {
            if (source.IsOk) return false;
            comparer ??= EqualityComparer<IError>.Default;
            return comparer.Equals(source.errorValue, error);
        }

        public static bool ContainsError(this in Result source, Func<IError, bool> predicate)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            return !source.IsOk && predicate(source.errorValue);
        }

        public static Task<bool> ContainsError(this Task<Result> source, IError error)
            => ContainsError(source, error, null);
        
        public static async Task<bool> ContainsError(this Task<Result> source, IError error, IEqualityComparer<IError>? comparer)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            var s = await source.ConfigureAwait(false);
            if (s.IsOk) return false;
            comparer ??= EqualityComparer<IError>.Default;
            return comparer.Equals(s.errorValue, error);
        }
        
        public static async Task<bool> ContainsError(this Task<Result> source, Func<IError, bool> predicate)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            var s = await source.ConfigureAwait(false);
            return !s.IsOk && predicate(s.errorValue);
        }
    }
}
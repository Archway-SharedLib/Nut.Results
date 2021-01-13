using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
// ReSharper disable CheckNamespace
namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        public static bool Contains<T>(this in Result<T> source, T value)
            => Contains(source, value, null);

        public static bool Contains<T>(this in Result<T> source, T value, IEqualityComparer<T>? comparer)
        {
            if (source.IsError) return false;
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(source.value, value);
        }

        public static bool Contains<T>(this in Result<T> source, Func<T, bool> predicate)
        {
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            return !source.IsError && predicate(source.value);
        }

        public static Task<bool> Contains<T>(this Task<Result<T>> source, T value)
            => Contains(source, value, null);
        
        public static async Task<bool> Contains<T>(this Task<Result<T>> source, T value, IEqualityComparer<T>? comparer)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            var s = await source.ConfigureAwait(false);
            if (s.IsError) return false;
            comparer ??= EqualityComparer<T>.Default;
            return comparer.Equals(s.value, value);
        }
        
        public static async Task<bool> Contains<T>(this Task<Result<T>> source, Func<T, bool> predicate)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (predicate is null) throw new ArgumentNullException(nameof(predicate));
            var s = await source.ConfigureAwait(false);
            return !s.IsError && predicate(s.value);
        }
    }
}
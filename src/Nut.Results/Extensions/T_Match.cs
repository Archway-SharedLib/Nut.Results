// ReSharper disable CheckNamespace

using System;
using System.Threading.Tasks;

namespace Nut.Results
{
    public static partial class ResultExtensions
    {
        // reulst<T> to result<T>

        public static Result<TResult> Match<T, TResult>(this in Result<T> source, Func<T, Result<TResult>> ok, Func<IError, Result<TResult>> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
        
            return source.IsOk ? ok(source.value) : err(source.errorValue);
        }

        public static Task<Result<TResult>> Match<T, TResult>(this in Result<T> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Result<TResult>> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? ok(source.value) : Task.FromResult(err(source.errorValue));
        }

        public static Task<Result<TResult>> Match<T, TResult>(this in Result<T> source, Func<T, Result<TResult>> ok, Func<IError, Task<Result<TResult>>> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? Task.FromResult(ok(source.value)) : err(source.errorValue);
        }

        public static Task<Result<TResult>> Match<T, TResult>(this in Result<T> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Task<Result<TResult>>> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? ok(source.value) : err(source.errorValue);
        }

        public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Result<TResult>> ok, Func<IError, Result<TResult>> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? ok(result.value) : err(result.errorValue);
        }

        public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Result<TResult>> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? await ok(result.value) : err(result.errorValue);
        }

        public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Result<TResult>> ok, Func<IError, Task<Result<TResult>>> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            var result = await source.ConfigureAwait(false);
            return result.IsOk ? ok(result.value) : await err(result.errorValue);
        }

        public static async Task<Result<TResult>> Match<T, TResult>(this Task<Result<T>> source, Func<T, Task<Result<TResult>>> ok, Func<IError, Task<Result<TResult>>> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? await ok(result.value) : await err(result.errorValue);
        }

        // reulst<T> to result

        public static Result Match<T>(this in Result<T> source, Func<T, Result> ok, Func<IError, Result> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? ok(source.value) : err(source.errorValue);
        }

        public static Task<Result> Match<T>(this in Result<T> source, Func<T, Task<Result>> ok, Func<IError, Result> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? ok(source.value) : Task.FromResult(err(source.errorValue));
        }

        public static Task<Result> Match<T>(this in Result<T> source, Func<T, Result> ok, Func<IError, Task<Result>> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? Task.FromResult(ok(source.value)) : err(source.errorValue);
        }

        public static Task<Result> Match<T>(this in Result<T> source, Func<T, Task<Result>> ok, Func<IError, Task<Result>> err)
        {
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));
            
            return source.IsOk ? ok(source.value) : err(source.errorValue);
        }

        public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Result> ok, Func<IError, Result> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? ok(result.value) : err(result.errorValue);
        }

        public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Task<Result>> ok, Func<IError, Result> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? await ok(result.value) : err(result.errorValue);
        }

        public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Result> ok, Func<IError, Task<Result>> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? ok(result.value) : await err(result.errorValue);
        }

        public static async Task<Result> Match<T>(this Task<Result<T>> source, Func<T, Task<Result>> ok, Func<IError, Task<Result>> err)
        {
            if (source is null) throw new ArgumentNullException(nameof(source));
            if (ok is null) throw new ArgumentNullException(nameof(ok));
            if (err is null) throw new ArgumentNullException(nameof(err));

            var result = await source.ConfigureAwait(false);
            return result.IsOk ? await ok(result.value) : await err(result.errorValue);
        }
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public Result MapError(Func<IError, IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));

            if (IsOk) return Result.Ok();
            var result = error(errorValue);
            return Result.Error(result);
        }

        public Result MapError(Func<IError, Task<IError>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!IsOk) return Result.Ok();

            var errorVal = errorValue;
            var result = AsyncHelper.RunSync(() => error(errorVal));
            return Result.Error(result);
        }

        public Result MapErrorResolve(Action<IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsOk) return Result.Ok();
            
            error(errorValue);
            return Result.Ok();
        }

        public Result MapErrorResolve(Func<IError, Task> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsOk) return Result.Ok();

            var errorVal = errorValue;
            AsyncHelper.RunSync(() => error(errorVal));
            return Result.Ok();
        }

        public Result<TResult> MapError<TResult>(Func<IError, IError> error, Func<TResult> defaultValue)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (defaultValue == null) throw new ArgumentNullException(nameof(defaultValue));

            if (IsOk) return Result.Ok(defaultValue());
            var result = error(errorValue);
            return Result.Error<TResult>(result);
        }

        public Result<TResult> MapError<TResult>(Func<IError, Task<IError>> error, Func<TResult> defaultValue)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (!IsOk) return Result.Ok(defaultValue());

            var errorVal = errorValue;
            var result = AsyncHelper.RunSync(() => error(errorVal));
            return Result.Error<TResult>(result);
        }

        public Result<TResult> MapErrorResolve<TResult>(Func<IError, TResult> toOk, Func<TResult> defaultValue)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));

            if (IsOk) return Result.Ok(defaultValue());
            var result = toOk(errorValue);
            return Result.Ok(result);
        }

        public Result<TResult> MapErrorResolve<TResult>(Func<IError, Task<TResult>> toOk, Func<TResult> defaultValue)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));
            if (IsOk) return Result.Ok(defaultValue());

            var errorVal = errorValue;
            var result = AsyncHelper.RunSync(() => toOk(errorVal));
            return Result.Ok(result);
        }

        public async Task<Result> MapErrorAsync(Func<IError, Task<IError>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsOk) return Result.Ok();
            
            var result = await error(errorValue).ConfigureAwait(false);
            return Result.Error(result);
        }

        public async Task<Result> MapErrorResolveAsync(Func<IError, Task> toOk)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));
            if (IsOk) return Result.Ok();

            await toOk(errorValue).ConfigureAwait(false);
            return Result.Ok();
        }

        public async Task<Result<TResult>> MapErrorAsync<TResult>(Func<IError, Task<IError>> error, Func<TResult> defaultValue)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (defaultValue == null) throw new ArgumentNullException(nameof(defaultValue));
            if (IsOk) return Result.Ok(defaultValue());
            
            var result = await error(errorValue).ConfigureAwait(false);
            return Result.Error<TResult>(result);
        }

        public async Task<Result<TResult>> MapErrorResolveAsync<TResult>(Func<IError, Task<TResult>> toOk, Func<TResult> defaultValue)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));
            if (IsOk) return Result.Ok(defaultValue());

            var result = await toOk(errorValue).ConfigureAwait(false);
            return Result.Ok(result);
        }
    }

    public readonly partial struct Result<T>
    {
        public Result<T> MapError(Func<IError, IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));

            if (IsOk) return Result.Ok(value);
            var result = error(errorValue);
            return Result.Error<T>(result);
        }

        public Result<T> MapError(Func<IError, Task<IError>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsOk) return Result.Ok(value);

            var errorVal = errorValue;
            var result = AsyncHelper.RunSync(() => error(errorVal));
            return Result.Error<T>(result);
        }

        public Result<TResult> MapErrorResolve<TResult>(Func<IError, TResult> toOk, Func<TResult> defaultValue)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));

            if (IsOk) return Result.Ok(defaultValue());
            var result = toOk(errorValue);
            return Result.Ok(result);
        }

        public Result<TResult> MapErrorResolve<TResult>(Func<IError, Task<TResult>> toOk, Func<TResult> defaultValue)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));
            if (IsOk) return Result.Ok(defaultValue());

            var errorVal = errorValue;
            var result = AsyncHelper.RunSync(() => toOk(errorVal));
            return Result.Ok(result);
        }

        public Result MapErrorResolve(Action<IError> toOk)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));

            if (IsOk) return Result.Ok();
            toOk(errorValue);
            return Result.Ok();
        }

        public Result MapErrorResolve(Func<IError, Task> toOk)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));
            if (IsOk) return Result.Ok();

            var errorVal = errorValue;
            AsyncHelper.RunSync(() => toOk(errorVal));
            return Result.Ok();
        }

        public async Task<Result<T>> MapErrorAsync(Func<IError, Task<IError>> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsOk) return Result.Ok(value);
            
            var result = await error(errorValue).ConfigureAwait(false);
            return Result.Error<T>(result);
        }

        public async Task<Result<TResult>> MapErrorResolveAsync<TResult>(Func<IError, Task<TResult>> toOk, Func<TResult> defaultValue)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));
            if (IsOk) return Result.Ok(defaultValue());
            
            var result = await toOk(errorValue).ConfigureAwait(false);
            return Result.Ok(result);
        }

        public async Task<Result> MapErrorResolveAsync(Func<IError, Task> toOk)
        {
            if (toOk == null) throw new ArgumentNullException(nameof(toOk));

            if (IsOk) return Result.Ok();
            
            await toOk(errorValue).ConfigureAwait(false);
            return Result.Ok();
        }
    }
}

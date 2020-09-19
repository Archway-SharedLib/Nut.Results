using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public void Match(Action ok, Action<IError> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            if (IsOk) ok();
            else error(errorValue);
        }

        public Task MatchAsync(Func<Task> ok, Func<IError, Task> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            return IsOk ? ok() : error(errorValue);
        }

        public TResult Match<TResult>(Func<TResult> ok, Func<IError, TResult> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            return IsOk ? ok() : error(errorValue);
        }

        public Task<TResult> MatchAsync<TResult>(Func<Task<TResult>> ok,
            Func<IError, Task<TResult>> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            return IsOk ? ok() : error(errorValue);
        }
    }

    public readonly partial struct Result<T>
    {
        public void Match(Action<T> ok, Action<IError> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            if (IsOk) ok(value);
            else error(errorValue);
        }

        public Task MatchAsync(Func<T, Task> ok, Func<IError, Task> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            return IsOk ? ok(value) : error(errorValue);
        }

        public TResult Match<TResult>(Func<T, TResult> ok, Func<IError, TResult> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            return IsOk ? ok(value) : error(errorValue);
        }

        public Task<TResult> MatchAsync<TResult>(Func<T, Task<TResult>> ok,
            Func<IError, Task<TResult>> error)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (error == null) throw new ArgumentNullException(nameof(error));

            return IsOk ? ok(value) : error(errorValue);
        }
    }

}

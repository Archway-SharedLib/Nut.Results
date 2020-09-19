using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public void IfOk(Action ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (IsOk) ok();
        }

        public async Task IfOkAsync(Func<Task> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (IsOk) await ok().ConfigureAwait(false);
        }

        public void IfError(Action<IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsError) error(errorValue);
        }

        public async Task IfErrorAsync(Func<IError, Task> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsError) await error(errorValue).ConfigureAwait(false);
        }
    }

    public readonly partial struct Result<T>
    {
        public void IfOk(Action<T> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (IsOk) ok(value);
        }

        public async Task IfOkAsync(Func<T, Task> ok)
        {
            if (ok == null) throw new ArgumentNullException(nameof(ok));
            if (IsOk) await ok(value).ConfigureAwait(false);
        }

        public void IfError(Action<IError> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsError) error(errorValue);
        }

        public async Task IfErrorAsync(Func<IError, Task> error)
        {
            if (error == null) throw new ArgumentNullException(nameof(error));
            if (IsError) await error(errorValue).ConfigureAwait(false);
        }
    }
}

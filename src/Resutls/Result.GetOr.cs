using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Archway.Results
{
    public readonly partial struct Result
    {
        public IError GetErrorOr(Func<IError> defaultError)
        {
            if (defaultError == null) throw new ArgumentNullException(nameof(defaultError));
            return IsError ? errorValue : defaultError();
        }

        public Task<IError> GetErrorOrAsync(Func<Task<IError>> defaultError)
        {
            if (defaultError == null) throw new ArgumentNullException(nameof(defaultError));
            return IsError ? Task.FromResult<IError>(errorValue) : defaultError();
        }
    }

    public readonly partial struct Result<T>
    {
        public T GetOr(Func<T> defaultValue)
        {
            if (defaultValue == null) throw new ArgumentNullException(nameof(defaultValue));
            return IsOk ? value : defaultValue();
        }

        public Task<T> GetOrAsync(Func<Task<T>> defaultValue)
        {
            if (defaultValue == null) throw new ArgumentNullException(nameof(defaultValue));
            return IsOk ? Task.FromResult<T>(value) : defaultValue();
        }

        public T GetOrDefault()
        {
            return GetOr(() => default!);
        }

        public IError GetErrorOr(Func<IError> defaultError)
        {
            if (defaultError == null) throw new ArgumentNullException(nameof(defaultError));
            return IsError ? errorValue : defaultError();
        }

        public Task<IError> GetErrorOrAsync(Func<Task<IError>> defaultError)
        {
            if (defaultError == null) throw new ArgumentNullException(nameof(defaultError));
            return IsError ? Task.FromResult<IError>(errorValue) : defaultError();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results
{
    public readonly partial struct Result
    {
        public static Result Ok() => new (null, true);

        public static Result Error(IError error) => new (error, false);

        public static Result Error(string message) => new (new Error(message), false);

        public static Result<T> Ok<T>(T value) => new (value, default!, true);

        public static Result<T> Error<T>(IError error) => new (default!, error, false);

        public static Result<T> Error<T>(string message) => new (default!, new Error(message), false);

        public static Result Try(Action action)
        {
            try
            {
                action();
                return Ok();
            }
            catch(Exception e)
            {
                return Error(new ExceptionalError(e));
            }
        }

        public static async Task<Result> Try(Func<Task> action)
        {
            try
            {
                await action().ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return Error(new ExceptionalError(e));
            }
        }

        public static Result<T> Try<T>(Func<T> action)
        {
            try
            {
                return Ok(action());
            }
            catch(Exception e)
            {
                return Error<T>(new ExceptionalError(e));
            }
        }

        public static async Task<Result<T>> Try<T>(Func<Task<T>> action)
        {
            try
            {
                var result = await action().ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Error<T>(new ExceptionalError(e));
            }
        }

        public static Result Try(Func<Result> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                return Error(new ExceptionalError(e));
            }
        }

        public static async Task<Result> Try(Func<Task<Result>> action)
        {
            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return Error(new ExceptionalError(e));
            }
        }

        public static Result<T> Try<T>(Func<Result<T>> action)
        {
            try
            {
                return action();
            }
            catch (Exception e)
            {
                return Error<T>(new ExceptionalError(e));
            }
        }

        public static async Task<Result<T>> Try<T>(Func<Task<Result<T>>> action)
        {
            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return Error<T>(new ExceptionalError(e));
            }
        }
    }
}

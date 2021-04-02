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

        private static IError DefualtErrorHanler(Exception e)
            => new ExceptionalError(e);

        public static Result Try(Action action)
            => Try(action, DefualtErrorHanler);
        
        public static Result Try(Action action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                action();
                return Ok();
            }
            catch(Exception e)
            {
                return Error(errorHandler(e));
            }
        }

        public static Task<Result> Try(Func<Task> action)
            => Try(action, DefualtErrorHanler);
        
        public static async Task<Result> Try(Func<Task> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                await action().ConfigureAwait(false);
                return Ok();
            }
            catch (Exception e)
            {
                return Error(errorHandler(e));
            }
        }

        public static Result<T> Try<T>(Func<T> action)
            => Try(action, DefualtErrorHanler);
        
        public static Result<T> Try<T>(Func<T> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                return Ok(action());
            }
            catch(Exception e)
            {
                return Error<T>(errorHandler(e));
            }
        }

        public static Task<Result<T>> Try<T>(Func<Task<T>> action)
            => Try(action, DefualtErrorHanler);
        
        public static async Task<Result<T>> Try<T>(Func<Task<T>> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                var result = await action().ConfigureAwait(false);
                return Ok(result);
            }
            catch (Exception e)
            {
                return Error<T>(errorHandler(e));
            }
        }

        public static Result Try(Func<Result> action)
            => Try(action, DefualtErrorHanler);

        public static Result Try(Func<Result> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                return action();
            }
            catch (Exception e)
            {
                return Error(errorHandler(e));
            }
        }

        public static Task<Result> Try(Func<Task<Result>> action)
            => Try(action, DefualtErrorHanler);

        public static async Task<Result> Try(Func<Task<Result>> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return Error(errorHandler(e));
            }
        }

        public static Result<T> Try<T>(Func<Result<T>> action)
            => Try(action, DefualtErrorHanler);

        public static Result<T> Try<T>(Func<Result<T>> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                return action();
            }
            catch (Exception e)
            {
                return Error<T>(errorHandler(e));
            }
        }

        public static Task<Result<T>> Try<T>(Func<Task<Result<T>>> action)
            => Try(action, DefualtErrorHanler);

        public static async Task<Result<T>> Try<T>(Func<Task<Result<T>>> action, Func<Exception, IError> errorHandler)
        {
            if (action is null) throw new ArgumentNullException(nameof(action));
            if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));
            
            try
            {
                return await action().ConfigureAwait(false);
            }
            catch (Exception e)
            {
                return Error<T>(errorHandler(e));
            }
        }
    }
}

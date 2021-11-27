using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results;

public readonly partial struct Result
{
    /// <summary>
    /// <see cref="Result"/> の成功を返します。
    /// </summary>
    /// <returns>成功の結果</returns>
    public static Result Ok() => new(null, true);

    /// <summary>
    /// <see cref="Result"/> の失敗を返します。
    /// </summary>
    /// <param name="error">失敗の値</param>
    /// <returns>失敗の結果</returns>
    public static Result Error(IError error) => new(error, false);

    /// <summary>
    /// 指定されたメッセージが設定された <see cref="Nut.Results.Error"/> をもつ <see cref="Result"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <returns>失敗の結果</returns>
    public static Result Error(string message) => new(new Error(message), false);

    /// <summary>
    /// <see cref="Result{T}"/> の成功を返します。
    /// </summary>
    /// <param name="value">成功の値</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>成功の結果</returns>
    public static Result<T> Ok<T>(T value) => new(value, default!, true);

    /// <summary>
    /// <see cref="Result{T}"/> の失敗を返します。
    /// </summary>
    /// <param name="error">失敗の値</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>失敗の結果</returns>
    public static Result<T> Error<T>(IError error) => new(default!, error, false);

    /// <summary>
    /// 指定されたメッセージが設定された <see cref="Nut.Results.Error"/> をもつ <see cref="Result{T}"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>失敗の結果</returns>
    public static Result<T> Error<T>(string message) => new(default!, new Error(message), false);

    private static IError DefualtErrorHanler(Exception e)
        => new ExceptionalError(e);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Result Try(Action action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
    public static Result Try(Action action, Func<Exception, IError> errorHandler)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));
        if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));

        try
        {
            action();
            return Ok();
        }
        catch (Exception e)
        {
            return Error(errorHandler(e));
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Task<Result> Try(Func<Task> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
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

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Result<T> Try<T>(Func<T> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
    public static Result<T> Try<T>(Func<T> action, Func<Exception, IError> errorHandler)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));
        if (errorHandler is null) throw new ArgumentNullException(nameof(errorHandler));

        try
        {
            return Ok(action());
        }
        catch (Exception e)
        {
            return Error<T>(errorHandler(e));
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Task<Result<T>> Try<T>(Func<Task<T>> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
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

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Result Try(Func<Result> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
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

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Task<Result> Try(Func<Task<Result>> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
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

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Result<T> Try<T>(Func<Result<T>> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
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

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="ExceptionalError"/> を持った失敗の結果</returns>
    public static Task<Result<T>> Try<T>(Func<Task<Result<T>>> action)
        => Try(action, DefualtErrorHanler);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <param name="errorHandler">例外が発生した場合に、 <see cref="IError"/> を作成する処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <paramref name="errorHandler"/> を実行した結果の <see cref="IError"/> を持った失敗の結果</returns>
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

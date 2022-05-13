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
    public static Result Error(Exception error) => new(error, false);

    /// <summary>
    /// 指定されたメッセージが設定された <see cref="Exception"/> をもつ <see cref="Result"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <returns>失敗の結果</returns>
    public static Result Error(string message) => new(new Exception(message), false);

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
    public static Result<T> Error<T>(Exception error) => new(default!, error, false);

    /// <summary>
    /// 指定されたメッセージが設定された <see cref="Exception"/> をもつ <see cref="Result{T}"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>失敗の結果</returns>
    public static Result<T> Error<T>(string message) => new(default!, new Exception(message), false);

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は  <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は成功の結果を返します。例外が発生した場合はその <see cref="Exception"/> を持った失敗の結果</returns>
    public static Result Try(Action action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));

        try
        {
            action();
            return Ok();
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は成功の結果を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static async Task<Result> Try(Func<Task> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));

        try
        {
            await action().ConfigureAwait(false);
            return Ok();
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static Result<T> Try<T>(Func<T> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));

        try
        {
            return Ok(action());
        }
        catch (Exception e)
        {
            return Error<T>(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を持った成功の結果を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static async Task<Result<T>> Try<T>(Func<Task<T>> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));

        try
        {
            var result = await action().ConfigureAwait(false);
            return Ok(result);
        }
        catch (Exception e)
        {
            return Error<T>(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static Result Try(Func<Result> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));
        try
        {
            return action();
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static async Task<Result> Try(Func<Task<Result>> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));
        try
        {
            return await action().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return Error(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static Result<T> Try<T>(Func<Result<T>> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));

        try
        {
            return action();
        }
        catch (Exception e)
        {
            return Error<T>(e);
        }
    }

    /// <summary>
    /// 引数で渡された処理を実行し、例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果を返します。
    /// </summary>
    /// <param name="action">処理</param>
    /// <returns>例外が発生しなかった場合は処理の戻り値を返します。例外が発生した場合は <see cref="Exception"/> を持った失敗の結果</returns>
    public static async Task<Result<T>> Try<T>(Func<Task<Result<T>>> action)
    {
        if (action is null) throw new ArgumentNullException(nameof(action));

        try
        {
            return await action().ConfigureAwait(false);
        }
        catch (Exception e)
        {
            return Error<T>(e);
        }
    }
}

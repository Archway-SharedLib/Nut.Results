using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace Nut.Results;

public readonly partial struct Result
{
    /// <summary>
    /// <see cref="Result"/> の失敗を返します。
    /// </summary>
    /// <param name="error">失敗の値</param>
    /// <returns>失敗の結果</returns>
    public static Result Error(Exception error) => new(error, false);

    /// <summary>
    /// 失敗に設定する例外を発生させる処理を指定します。
    /// </summary>
    /// <param name="errorAction">失敗に設定する例外を発生させる処理</param>
    /// <returns>失敗の結果</returns>
    /// <exception cref="InvalidOperationException"><paramref name="errorAction"/>の実行で例外が発生しなかった場合に発生します。</exception>
    public static Result Error(Action errorAction)
    {
        try
        {
            errorAction();
        }
        catch (Exception e)
        {
            return Error(e);
        }
        throw new InvalidOperationException();
    }

    /// <summary>
    /// 指定されたメッセージが設定された <see cref="Exception"/> をもつ <see cref="Result"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <returns>失敗の結果</returns>
    public static Result Error(string message) => Error(() => throw new Exception(message));

    internal static Result Error(ExceptionDispatchInfo info) => new(info, false);

    /// <summary>
    /// <see cref="Result{T}"/> の失敗を返します。
    /// </summary>
    /// <param name="error">失敗の値</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>失敗の結果</returns>
    public static Result<T> Error<T>(Exception error) => new(default!, error, false);

    /// <summary>
    /// 失敗に設定する例外を発生させる処理を指定します。
    /// </summary>
    /// <param name="errorAction">失敗に設定する例外を発生させる処理</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>失敗の結果</returns>
    /// <exception cref="InvalidOperationException"><paramref name="errorAction"/>の実行で例外が発生しなかった場合に発生します。</exception>
    public static Result<T> Error<T>(Action errorAction)
    {
        try
        {
            errorAction();
        }
        catch (Exception e)
        {
            return Error<T>(e);
        }
        throw new InvalidOperationException();
    }

    /// <summary>
    /// 指定されたメッセージが設定された <see cref="Exception"/> をもつ <see cref="Result{T}"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns>失敗の結果</returns>
    public static Result<T> Error<T>(string message) => Error<T>(() => throw new Exception(message));

    internal static Result<T> Error<T>(ExceptionDispatchInfo info) => new(default!, info, false);
}

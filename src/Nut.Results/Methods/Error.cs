using System;
using System.Collections.Generic;
using System.Linq;
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
    /// 指定されたメッセージが設定された <see cref="Exception"/> をもつ <see cref="Result"/> の失敗を返します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <returns>失敗の結果</returns>
    public static Result Error(string message) => new(new Exception(message), false);

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
}

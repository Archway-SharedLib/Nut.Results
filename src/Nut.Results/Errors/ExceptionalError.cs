using System;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results;

/// <summary>
/// エラーのもととなる例外をラップした失敗の結果を定義します。
/// </summary>
public class ExceptionalError : IError
{
    /// <summary>
    /// エラーのもととなる例外を指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="sourceException">もととなった例外</param>
    public ExceptionalError(Exception sourceException)
    {
        Exception = sourceException ?? throw new ArgumentNullException(nameof(sourceException));
        Message = Exception.Message ?? SR.Error_DefaultErrorMessage;
    }

    /// <summary>
    /// エラーのもととなる例外とメッセージを指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="sourceException">もととなった例外</param>
    /// <param name="message">メッセージ</param>
    public ExceptionalError(Exception sourceException, string message)
    {
        Exception = sourceException ?? throw new ArgumentNullException(nameof(sourceException));
        Message = message ?? SR.Error_DefaultErrorMessage;
    }

    /// <summary>
    /// メッセージを取得します。
    /// </summary>
    public string Message { get; }

    /// <summary>
    /// もととなる例外を取得します。
    /// </summary>
    public Exception Exception { get; }

    /// <summary>
    /// もととなった例外を返します。
    /// </summary>
    /// <returns>もととなった例外</returns>
    Exception IError.ToException()
    {
        return Exception;
    }
}

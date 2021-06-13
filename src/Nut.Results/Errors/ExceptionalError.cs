using System;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results
{
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
            this.Exception = sourceException ?? throw new ArgumentNullException(nameof(sourceException));
            this.Message = Exception.Message ?? SR.Error_DefaultErrorMessage;
        }

        /// <summary>
        /// エラーのもととなる例外とメッセージを指定してインスタンスを初期化します。
        /// </summary>
        /// <param name="sourceException">もととなった例外</param>
        /// <param name="message">メッセージ</param>
        public ExceptionalError(Exception sourceException, string message)
        {
            this.Exception = sourceException ?? throw new ArgumentNullException(nameof(sourceException));
            this.Message = message ?? SR.Error_DefaultErrorMessage;
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
            return this.Exception;
        }
    }
}

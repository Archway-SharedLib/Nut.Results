using System;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results
{
    /// <summary>
    /// <see cref="Result"/> または <see cref="Result{T}"/> に値を設定するコールバック処理で不正な値を返したときに発生する例外です。
    /// </summary>
    public class InvalidReturnValueException: Exception
    {
        /// <summary>
        /// インスタンスを初期化します。
        /// </summary>
        public InvalidReturnValueException(): base(SR.Exception_InvalidReturnValue){}
        
        /// <summary>
        /// メッセージを指定してインスタンスを初期化します。
        /// </summary>
        /// <param name="message">メッセージ</param>
        public InvalidReturnValueException(string message): base(message){}
    }
}
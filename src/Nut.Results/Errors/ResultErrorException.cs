using System;

namespace Nut.Results
{
    /// <summary>
    /// <see cref="IError"/> を例外に変換する際のデフォルトの例外です。
    /// </summary>
    public class ResultErrorException: Exception
    {
        /// <summary>
        /// この例外のもととなった <see cref="IError"/> を取得します。
        /// </summary>
        public IError SourceError { get; }

        /// <summary>
        /// もととなる <see cref="IError"/> を指定してインスタンスを初期化します。
        /// </summary>
        /// <param name="sourceError">もととなる <see cref="IError"/></param>
        public ResultErrorException(IError sourceError) :
            this(sourceError, sourceError?.Message)
        {
        }

        /// <summary>
        /// もととなる <see cref="IError"/> とメッセージを指定してインスタンスを初期化します。
        /// </summary>
        /// <param name="sourceError">もととなる <see cref="IError"/></param>
        /// <param name="message">メッセージ</param>
        public ResultErrorException(IError sourceError, string? message) :
            base(message)
        {
            SourceError = sourceError ?? throw new ArgumentNullException(nameof(sourceError));
        }
        
    }
}
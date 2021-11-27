using SR = Nut.Results.Resources.Strings;

namespace Nut.Results;

/// <summary>
/// 失敗の結果の詳細を表す基本的な型を定義します。
/// </summary>
public class Error : IError
{
    /// <summary>
    /// インスタンスを初期化します。
    /// </summary>
    public Error()
    {
        Message = SR.Error_DefaultErrorMessage;
    }

    /// <summary>
    /// メッセージを指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    public Error(string message)
    {
        Message = message ?? SR.Error_DefaultErrorMessage;
    }

    /// <summary>
    /// メッセージを取得します。
    /// </summary>
    public string Message { get; }

    /// <inheritdoc />
    public override string ToString()
    {
        return Message;
    }
}

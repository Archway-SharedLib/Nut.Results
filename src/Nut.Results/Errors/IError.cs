using System;

namespace Nut.Results;

/// <summary>
/// 失敗の結果の詳細を表すインターフェイスを定義します。
/// </summary>
public interface IError
{
    /// <summary>
    /// メッセージを取得します。
    /// </summary>
    string? Message { get; }

    /// <summary>
    /// エラーを例外に変換します。
    /// </summary>
    /// <returns>変換された例外</returns>
    Exception ToException()
        => new ResultErrorException(this, Message);
}

using System;
using System.Runtime.ExceptionServices;

namespace Nut.Results;

/// <summary>
/// 値の無い結果を表します。
/// </summary>
public readonly partial struct Result : IEquatable<Result>
{

    internal readonly ExceptionDispatchInfo _capturedError;

    internal Result(Exception? errorValue, bool isOk)
    {
        if (errorValue is null && !isOk) throw new ArgumentNullException(nameof(errorValue));
        _capturedError = (isOk ? null : ExceptionDispatchInfo.Capture(errorValue!))!;
        IsOk = isOk;
    }

    internal Result(ExceptionDispatchInfo? errorValue, bool isOk)
    {
        if (errorValue is null && !isOk) throw new ArgumentNullException(nameof(errorValue));
        _capturedError = (isOk ? null : errorValue)!;
        IsOk = isOk;
    }

    /// <summary>
    /// 成功かどうかを取得します。
    /// </summary>
    /// <example>
    /// var value = Result.Ok();
    /// value.IsOk; // true
    /// </example>
    public bool IsOk { get; }

    /// <summary>
    /// 失敗かどうかを取得します。
    /// </summary>
    /// <example>
    /// var value = Result.Ok();
    /// value.IsError; // false
    /// </example>
    public bool IsError => !IsOk;

    /// <summary>
    /// 両方とも成功か、または失敗の場合に true を返します。失敗の場合は値の比較も行われます。
    /// </summary>
    /// <param name="obj">比較する値</param>
    /// <returns>両方とも成功か、または失敗の場合に true</returns>
    public override bool Equals(object obj) =>
        obj is Result result && Equals(result);

    /// <summary>
    /// 両方とも成功か、または失敗の場合に true を返します。失敗の場合は値の比較も行われます。
    /// </summary>
    /// <param name="other">比較する値</param>
    /// <returns>両方とも成功か、または失敗の場合に true</returns>
    public bool Equals(Result other)
        => IsOk ? other.IsOk : _capturedError.SourceException.Equals(other._capturedError.SourceException);

    /// <summary>
    /// ハッシュコードを取得します。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
        => IsOk ? IsOk.GetHashCode() : HashCode.Combine(_capturedError.SourceException, IsOk);

    /// <summary>
    /// 文字列表現を取得します。
    /// </summary>
    /// <returns>文字列表現</returns>
    public override string ToString()
        => IsOk ? "ok" : $"error: {_capturedError?.SourceException.ToString() ?? "(null)"}";

    /// <summary>
    /// 結果が成功していることを示します。
    /// </summary>
    /// <param name="result">チェックする結果</param>
    /// <returns>成功の場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator true(in Result result) => result.IsOk;

    /// <summary>
    /// 結果が失敗していることを示します。
    /// </summary>
    /// <param name="result">チェックする結果</param>
    /// <returns>失敗の場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator false(in Result result) => !result.IsOk;

    /// <summary>
    /// 結果と値が一致するかどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する結果</param>
    /// <returns>結果と値が一致する場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator ==(in Result left, in Result right) => left.Equals(right);

    /// <summary>
    /// 結果と値が一致しないどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する結果</param>
    /// <returns>結果と値が一致しない場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator !=(in Result left, in Result right) => !left.Equals(right);

    /// <summary>
    /// 結果と真偽値が一致するかどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する真偽値</param>
    /// <returns>結果が成功で真偽値が <see langword="true"/> 、もしくは結果が失敗で真偽値が  <see langword="false"/> の場合は  <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator ==(in Result left, in bool right) => left.IsOk == right;

    /// <summary>
    /// 結果と真偽値が一致しないどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する真偽値</param>
    /// <returns>結果が成功で真偽値が <see langword="false"/> 、もしくは結果が失敗で真偽値が  <see langword="true"/> の場合は  <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator !=(in Result left, in bool right) => left.IsOk != right;

    /// <summary>
    /// 結果が成功ではないかを返します。
    /// </summary>
    /// <param name="result">チェックする結果</param>
    /// <returns>結果が成功でない場合は <see langword="true"/>そうでない場合は <see langword="false"/></returns>
    public static bool operator !(in Result result) => !result.IsOk;
}

/// <summary>
/// 成功の値がある結果を表します。
/// </summary>
/// <typeparam name="T">成功の値の型</typeparam>
public readonly partial struct Result<T> : IEquatable<Result<T>>
{
    internal readonly T _value;
    internal readonly ExceptionDispatchInfo _capturedError;

    internal Result(T value, Exception errorValue, bool isOk)
    {
        if (value is null && isOk) throw new ArgumentNullException(nameof(value));
        if (errorValue is null && !isOk) throw new ArgumentNullException(nameof(errorValue));
        _value = (isOk ? value : default)!;
        _capturedError = (isOk ? null : ExceptionDispatchInfo.Capture(errorValue!))!;
        IsOk = isOk;
    }

    internal Result(T value, ExceptionDispatchInfo errorValue, bool isOk)
    {
        if (value is null && isOk) throw new ArgumentNullException(nameof(value));
        if (errorValue is null && !isOk) throw new ArgumentNullException(nameof(errorValue));
        _value = (isOk ? value : default)!;
        _capturedError = (isOk ? null : errorValue)!;
        IsOk = isOk;
    }

    /// <summary>
    /// 成功かどうかを取得します。
    /// </summary>
    /// <example>
    /// var value = Result.Ok("Success");
    /// value.IsOk; // true
    /// </example>
    public bool IsOk { get; }

    /// <summary>
    /// 失敗かどうかを取得します。
    /// </summary>
    /// <example>
    /// var value = Result.Ok("Success");
    /// value.IsError; // false
    /// </example>
    public bool IsError => !IsOk;

    /// <summary>
    /// 指定された値を成功の結果にマッピングします。
    /// </summary>
    /// <param name="value">マッピングする値</param>
    /// <returns>成功の結果</returns>
    public static implicit operator Result<T>(T value) => Result.Ok(value);

    /// <summary>
    /// 両方とも成功か、または失敗の場合に値の比較を行い一致しているかどうかを返します。
    /// </summary>
    /// <param name="obj">比較する値</param>
    /// <returns>両方とも成功か、または失敗の場合に値の比較を行い一致しているかどうか</returns>
    public override bool Equals(object obj) => obj is Result<T> result && Equals(result);

    /// <summary>
    /// 両方とも成功か、または失敗の場合に値の比較を行い一致しているかどうかを返します。
    /// </summary>
    /// <param name="other">比較する値</param>
    /// <returns>両方とも成功か、または失敗の場合に値の比較を行い一致しているかどうか</returns>
    public bool Equals(Result<T> other)
        => IsOk ?
            other.IsOk && _value!.Equals(other._value) :
            other.IsError && _capturedError.SourceException.Equals(other._capturedError.SourceException);

    /// <summary>
    /// 文字列表現を返します。
    /// </summary>
    /// <returns>文字列表現</returns>
    public override string ToString()
        => IsOk ? $"ok: {PrepareNullText(_value!.ToString())}" :
            $"error: {PrepareNullText(_capturedError?.SourceException.ToString())}";

    /// <summary>
    /// ハッシュコードを取得します。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
        => IsOk ? HashCode.Combine(_value, IsOk) : HashCode.Combine(_capturedError.SourceException, IsOk);

    private string PrepareNullText(string? text) => text ?? "(null)";

    /// <summary>
    /// 結果が成功していることを示します。
    /// </summary>
    /// <param name="result">チェックする結果</param>
    /// <returns>成功の場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator true(in Result<T> result) => result.IsOk;

    /// <summary>
    /// 結果が失敗していることを示します。
    /// </summary>
    /// <param name="result">チェックする結果</param>
    /// <returns>失敗の場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator false(in Result<T> result) => !result.IsOk;

    /// <summary>
    /// 結果と値が一致するかどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する結果</param>
    /// <returns>結果と値が一致する場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator ==(in Result<T> left, in Result<T> right) => left.Equals(right);

    /// <summary>
    /// 結果と値が一致しないどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する結果</param>
    /// <returns>結果と値が一致しない場合は <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator !=(in Result<T> left, in Result<T> right) => !left.Equals(right);

    /// <summary>
    /// 結果と真偽値が一致するかどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する真偽値</param>
    /// <returns>結果が成功で真偽値が <see langword="true"/> 、もしくは結果が失敗で真偽値が  <see langword="false"/> の場合は  <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator ==(in Result<T> left, in bool right) => left.IsOk == right;

    /// <summary>
    /// 結果と真偽値が一致しないどうかを返します。
    /// </summary>
    /// <param name="left">チェックする結果</param>
    /// <param name="right">比較する真偽値</param>
    /// <returns>結果が成功で真偽値が <see langword="false"/> 、もしくは結果が失敗で真偽値が  <see langword="true"/> の場合は  <see langword="true"/> そうでない場合は <see langword="false"/></returns>
    public static bool operator !=(in Result<T> left, in bool right) => left.IsOk != right;

    /// <summary>
    /// 結果が成功ではないかを返します。
    /// </summary>
    /// <param name="result">チェックする結果</param>
    /// <returns>結果が成功でない場合は <see langword="true"/>そうでない場合は <see langword="false"/></returns>
    public static bool operator !(in Result<T> result) => !result.IsOk;

}

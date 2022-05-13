using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results;

/// <summary>
/// 値の無い結果を表します。
/// </summary>
public readonly partial struct Result : IEquatable<Result>
{
    internal readonly Exception _errorValue;

    internal Result(Exception? errorValue, bool isOk)
    {
        if (errorValue is null && !isOk) throw new ArgumentNullException(nameof(errorValue));
        _errorValue = (isOk ? null : errorValue)!;
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
    /// <param name="other">比較する値</param>
    /// <returns>両方とも成功か、または失敗の場合に true</returns>
    public bool Equals(Result other)
        => IsOk ? other.IsOk : _errorValue.Equals(other._errorValue);

    /// <summary>
    /// ハッシュコードを取得します。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
        => IsOk ? IsOk.GetHashCode() : HashCode.Combine(_errorValue, IsOk);

    /// <summary>
    /// 文字列表現を取得します。
    /// </summary>
    /// <returns>文字列表現</returns>
    public override string ToString()
        => IsOk ? "ok" : $"error: {_errorValue?.ToString() ?? "(null)"}";
}

/// <summary>
/// 成功の値がある結果を表します。
/// </summary>
/// <typeparam name="T">成功の値の型</typeparam>
public readonly partial struct Result<T> : IEquatable<Result<T>>
{
    internal readonly T _value;
    internal readonly Exception _errorValue;

    internal Result(T value, Exception errorValue, bool isOk)
    {
        if (value is null && isOk) throw new ArgumentNullException(nameof(value));
        if (errorValue is null && !isOk) throw new ArgumentNullException(nameof(errorValue));
        _value = (isOk ? value : default)!;
        _errorValue = (isOk ? null : errorValue)!;
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
    /// <param name="other">比較する値</param>
    /// <returns>両方とも成功か、または失敗の場合に値の比較を行い一致しているかどうか</returns>
    public bool Equals(Result<T> other)
        => IsOk ?
            other.IsOk && _value!.Equals(other._value) :
            other.IsError && _errorValue.Equals(other._errorValue);

    /// <summary>
    /// 文字列表現を返します。
    /// </summary>
    /// <returns>文字列表現</returns>
    public override string ToString()
        => IsOk ? $"ok: {PrepareNullText(_value!.ToString())}" :
            $"error: {PrepareNullText(_errorValue?.ToString())}";

    /// <summary>
    /// ハッシュコードを取得します。
    /// </summary>
    /// <returns>ハッシュコード</returns>
    public override int GetHashCode()
        => IsOk ? HashCode.Combine(_value, IsOk) : HashCode.Combine(_errorValue, IsOk);

    private string PrepareNullText(string? text) => text ?? "(null)";
}

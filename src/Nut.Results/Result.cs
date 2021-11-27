using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results;

/// <summary>
/// 値の無い結果を表します。
/// </summary>
public readonly partial struct Result : IEquatable<Result>
{
    internal readonly IError _errorValue;

    internal Result(IError? errorValue, bool isOk)
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

    public bool Equals(Result other)
        => IsOk ? other.IsOk : _errorValue.Equals(other._errorValue);

    public override int GetHashCode()
        => IsOk ? IsOk.GetHashCode() : HashCode.Combine(_errorValue, IsOk);

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
    internal readonly IError _errorValue;

    internal Result(T value, IError errorValue, bool isOk)
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

    public static implicit operator Result<T>(T value) => Result.Ok(value);

    public bool Equals(Result<T> other)
        => IsOk ?
            other.IsOk && _value!.Equals(other._value) :
            other.IsError && _errorValue.Equals(other._errorValue);

    public override string ToString()
        => IsOk ? $"ok: {PrepareNullText(_value!.ToString())}" :
            $"error: {PrepareNullText(_errorValue?.ToString())}";

    public override int GetHashCode()
        => IsOk ? HashCode.Combine(_value, IsOk) : HashCode.Combine(_errorValue, IsOk);

    private string PrepareNullText(string? text) => text ?? "(null)";
}

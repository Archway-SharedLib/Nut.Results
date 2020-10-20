using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    /// <summary>
    /// 値の無い結果を表します。
    /// </summary>
    public readonly partial struct Result : IEquatable<Result>
    {
        internal readonly IError errorValue;
        
        internal Result(IError? errorValue, bool isOk)
        {
            if (errorValue == null && !isOk) throw new ArgumentNullException(nameof(errorValue));
            this.errorValue = (isOk ? null : errorValue)!;
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
            => IsOk ? other.IsOk : errorValue.Equals(other.errorValue);

        public override int GetHashCode()
            => IsOk ? IsOk.GetHashCode() : HashCode.Combine(errorValue, IsOk);

        public override string ToString()
            => IsOk ? "ok" :  $"error: {errorValue?.ToString() ?? "(null)"}";
    }

    /// <summary>
    /// 成功の値がある結果を表します。
    /// </summary>
    /// <typeparam name="T">成功の値の型</typeparam>
    public readonly partial struct Result<T> : IEquatable<Result<T>>
    {
        internal readonly T value;
        internal readonly IError errorValue;

        internal Result(T value, IError errorValue, bool isOk)
        {
            if (value == null && isOk) throw new ArgumentNullException(nameof(value));
            if (errorValue == null && !isOk) throw new ArgumentNullException(nameof(errorValue));
            this.value = (isOk ? value : default)!;
            this.errorValue = (isOk ? null : errorValue)!;
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
                other.IsOk && value!.Equals(other.value) :
                other.IsError && errorValue.Equals(other.errorValue);
        
        public override string ToString()
            => IsOk ? $"ok: {PrepareNullText(value!.ToString())}" : 
                $"error: {PrepareNullText(errorValue?.ToString())}";

        public override int GetHashCode()
            => IsOk ? HashCode.Combine(value, IsOk) : HashCode.Combine(errorValue, IsOk);

        private string PrepareNullText(string? text) => text ?? "(null)";
    }
}

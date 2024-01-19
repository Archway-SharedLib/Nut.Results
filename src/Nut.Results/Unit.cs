using System;

namespace Nut.Results;

/// <summary>
/// 唯一の値の型を表します。
/// </summary>
public readonly struct Unit : IEquatable<Unit>, IComparable<Unit>, IComparable
{
    private static readonly Unit s_value = new();

    /// <summary>
    /// 唯一の値を取得します。
    /// </summary>
    public static ref readonly Unit Default => ref s_value;

    /// <summary>
    /// <see cref="Unit"/> の比較を行います。この処理は常に<c>0</c>を返します。
    /// </summary>
    /// <param name="other">比較する <see cref="Unit"/></param>
    /// <returns>同値であることを表す<c>0</c>を返します</returns>
    public int CompareTo(Unit other) => 0;

    /// <summary>
    /// <see cref="Unit"/> の比較を行います。この処理は指定された値が <see cref="Unit"/> である場合に <c>0</c>を返します。そうでない場合は<c>1</c>を返します。
    /// </summary>
    /// <param name="obj">比較する値</param>
    /// <returns>指定された値が <see cref="Unit"/> である場合に<c>0</c>を返します。そうでない場合は<c>1</c>を返します</returns>
    public int CompareTo(object obj) => obj is Unit ? 0 : 1;

    /// <summary>
    /// 指定された値が同じ値かどうかを判定します。
    /// </summary>
    /// <param name="obj">判定する値</param>
    /// <returns>指定された値が <see cref="Unit"/> の場合は <c>true</c>、そうでない場合は <c>false</c> を返します。</returns>
    public override bool Equals(object? obj) => obj is Unit;

    /// <summary>
    /// 指定された値が同じ値かどうかを判定します。この処理は常に<c>true</c>を返します。
    /// </summary>
    /// <param name="other">判定する値</param>
    /// <returns>常に <c>true</c> を返します。</returns>
    public bool Equals(Unit other) => true;

    /// <summary>
    /// ハッシュ値を返します。この処理は常に<c>0</c>を返します。
    /// </summary>
    /// <remarks>常に <c>0</c> を返します。</remarks>
    public override int GetHashCode() => 0;

    /// <summary>
    /// <see cref="Unit"/> の文字列表現を返します。常に <c>"()"</c>を返します。
    /// </summary>
    /// <returns>常に <c>"()"</c>を返します。</returns>
    public override string ToString() => "()";

    /// <summary>
    /// 指定された値が等しいかどうかを判定します。この処理は常に<c>true</c>を返します。
    /// </summary>
    /// <param name="left">比較する一つ目の値</param>
    /// <param name="right">比較する二つ目の値</param>
    /// <returns>この処理は常に<c>true</c>を返します。</returns>
    public static bool operator ==(Unit left, Unit right) => true;

    /// <summary>
    /// 指定された値が等しくないかどうかを判定します。この処理は常に<c>false</c>を返します。
    /// </summary>
    /// <param name="left">比較する一つ目の値</param>
    /// <param name="right">比較する二つ目の値</param>
    /// <returns>この処理は常に<c>false</c>を返します。</returns>
    public static bool operator !=(Unit left, Unit right) => false;
}

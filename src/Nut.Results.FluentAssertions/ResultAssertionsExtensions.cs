namespace Nut.Results.FluentAssertions;

/// <summary>
/// <see cref="Result"/>および<see cref="Result{T}"/>をアサーションするための拡張メソッドを定義します。
/// </summary>
public static class ResultAssertionsExtensions
{
    /// <summary>
    /// アサーションするための <see cref="ResultAssertions"/> を返します。
    /// </summary>
    /// <param name="instance">アサーションする対象の <see cref="Result"/> インスタンス</param>
    /// <returns><see cref="ResultAssertions"/></returns>
    public static ResultAssertions Should(this in Result instance)
    {
        return new(instance);
    }

    /// <summary>
    /// アサーションするための <see cref="ResultAssertions{T}"/> を返します。
    /// </summary>
    /// <param name="instance">アサーションする対象の <see cref="Result{T}"/> インスタンス</param>
    /// <typeparam name="T">成功の値の型</typeparam>
    /// <returns><see cref="ResultAssertions{T}"/></returns>
    public static ResultAssertions<T> Should<T>(this in Result<T> instance)
    {
        return new(instance);
    }
}

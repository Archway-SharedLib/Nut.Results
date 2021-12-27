using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Nut.Results.FluentAssertions;

/// <summary>
/// <see cref="Result{T}"/>をアサーションします。
/// </summary>
/// <typeparam name="T"></typeparam>
public class ResultOkAssertions<T>
{
    private readonly Result<T> _instance;

    /// <summary>
    /// アサーションする対象の <see cref="Result{T}"/> を指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="instance">アサーションする対象の <see cref="Result{T}"/></param>
    public ResultOkAssertions(Result<T> instance)
    {
        _instance = instance;
    }

    /// <summary>
    /// 値が指定された条件に一致することをアサーションします。
    /// </summary>
    /// <param name="predicate">成功の値と比較する条件</param>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultOkAssertions<T>> Match(Expression<Func<T, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(predicate.Compile()(_instance.Get()))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected value to match {0}{reason}, but found {1}.", (object)predicate.Body, (object)_instance.Get()!);
        return new AndConstraint<ResultOkAssertions<T>>(this);
    }
}

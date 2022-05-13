using System;
using System.Linq.Expressions;
using FluentAssertions;
using FluentAssertions.Execution;
using FluentAssertions.Primitives;

namespace Nut.Results.FluentAssertions;

/// <summary>
/// <see cref="Exception"/>をアサーションします。
/// </summary>
public class ResultErrorAssertions
{
    private readonly Exception _instance;

    /// <summary>
    /// アサーションする対象の <see cref="Exception"/> を指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="instance">アサーションする対象の <see cref="Exception"/> </param>
    public ResultErrorAssertions(Exception instance)
    {
        _instance = instance;
    }

    /// <summary>
    /// <see cref="Exception"/> が指定された条件に一致することをアサーションします。
    /// </summary>
    /// <param name="predicate">条件</param>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultErrorAssertions> Match(Expression<Func<Exception, bool>> predicate,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(predicate.Compile()(_instance))
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected value to match {0}{reason}, but found {1}.", (object)predicate.Body, (object)_instance);
        return new AndConstraint<ResultErrorAssertions>(this);
    }

    /// <summary>
    /// <see cref="Exception"/> の型が指定された型と一致することをアサーションします。
    /// </summary>
    /// <typeparam name="T">期待する <see cref="Exception"/> の値の型</typeparam>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultErrorAssertions> BeOfType<T>(
        string because = "",
        params object[] becauseArgs)
    {
        BeOfType(typeof(T), because, becauseArgs);
        return new AndConstraint<ResultErrorAssertions>(this);
    }

    /// <summary>
    /// <see cref="Exception"/> の型が指定された型と一致することをアサーションします。
    /// </summary>
    /// <param name="expectedType">期待する <see cref="Exception"/> の値の型</param>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultErrorAssertions> BeOfType(
        Type expectedType,
        string because = "",
        params object[] becauseArgs)
    {
        (new ObjectAssertions(_instance)).BeOfType(expectedType, because, becauseArgs);
        return new AndConstraint<ResultErrorAssertions>(this);
    }

    /// <summary>
    /// <see cref="Exception"/> に設定されているメッセージが指定された値と一致することをアサーションします。
    /// </summary>
    /// <param name="expectedWildcardPattern">メッセージのパターン</param>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public virtual AndConstraint<ResultErrorAssertions> WithMessage(
        string expectedWildcardPattern,
        string because = "",
        params object[] becauseArgs)
    {
        (new StringAssertions(_instance.Message)).Be(expectedWildcardPattern, because, because);

        return new AndConstraint<ResultErrorAssertions>(this);
    }
}

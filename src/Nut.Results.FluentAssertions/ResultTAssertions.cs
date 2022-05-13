using System;
using FluentAssertions;
using FluentAssertions.Execution;

namespace Nut.Results.FluentAssertions;

/// <summary>
/// <see cref="Result{T}"/>のアサーション定義します。
/// </summary>
/// <typeparam name="T">成功の値の型</typeparam>
public class ResultAssertions<T>
{
    private readonly Result<T> _instance;

    /// <summary>
    /// <see cref="Result{T}"/>を指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="instance">アサーションするインスタンス</param>
    public ResultAssertions(Result<T> instance)
    {
        _instance = instance;
    }

    /// <summary>
    /// <see cref="Result{T}"/>が成功かどうかをアサーションします。
    /// </summary>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultOkAssertions<T>> BeOk(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.IsOk)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected ok, but error.\n{ToErrorMessage(_instance._errorValue)}");
        return new AndConstraint<ResultOkAssertions<T>>(new ResultOkAssertions<T>(_instance));
    }

    private string ToErrorMessage(Exception? error)
    {
        if (error is null) return string.Empty;
        return $"Type: {error.GetType().Name}\nMessage: {error.Message}";
    }

    /// <summary>
    /// <see cref="Result{T}"/>が指定された<see cref="Result{T}"/>と一致するかどうかをアサーションします。
    /// </summary>
    /// <param name="expected">期待する<see cref="Result{T}"/></param>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultAssertions<T>> Be(Result<T> expected,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("not equal values");
        return new AndConstraint<ResultAssertions<T>>(new ResultAssertions<T>(_instance));
    }

    /// <summary>
    /// <see cref="Result{T}"/>が失敗かどうかをアサーションします。
    /// </summary>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultErrorAssertions> BeError(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.IsError)
            .BecauseOf(because, becauseArgs)
            .FailWith("Expected error, but ok.");
        return new AndConstraint<ResultErrorAssertions>(new ResultErrorAssertions(_instance.GetError()));
    }
}

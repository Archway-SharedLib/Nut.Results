using FluentAssertions;
using FluentAssertions.Execution;

namespace Nut.Results.FluentAssertions;

/// <summary>
/// <see cref="Result"/>のアサーション定義します。
/// </summary>
public class ResultAssertions
{
    private readonly Result _instance;

    /// <summary>
    /// <see cref="Result"/>を指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="instance">アサーションするインスタンス</param>
    public ResultAssertions(Result instance)
    {
        _instance = instance;
    }

    /// <summary>
    /// <see cref="Result"/>が指定された<see cref="Result"/>と一致するかどうかをアサーションします。
    /// </summary>
    /// <param name="expected">期待する<see cref="Result"/></param>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultAssertions> Be(Result expected,
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.Equals(expected))
            .BecauseOf(because, becauseArgs)
            .FailWith("not equal values.");
        return new AndConstraint<ResultAssertions>(new ResultAssertions(_instance));
    }

    /// <summary>
    /// <see cref="Result"/>が成功かどうかをアサーションします。
    /// </summary>
    /// <param name="because">理由のメッセージを設定します。</param>
    /// <param name="becauseArgs">理由のメッセージの引数を設定します。</param>
    /// <returns>チェインしてアサーションするための <see cref="AndConstraint{T}"/> を返します。</returns>
    public AndConstraint<ResultAssertions> BeOk(
        string because = "",
        params object[] becauseArgs)
    {
        Execute.Assertion.ForCondition(_instance.IsOk)
            .BecauseOf(because, becauseArgs)
            .FailWith($"Expected ok, but error.\n{ToErrorMessage(_instance._errorValue)}");
        return new AndConstraint<ResultAssertions>(new ResultAssertions(_instance));
    }

    private string ToErrorMessage(IError? error)
    {
        if (error is null) return string.Empty;
        return $"Type: {error.GetType().Name}\nMessage: {error.Message}";
    }

    /// <summary>
    /// <see cref="Result"/>が失敗かどうかをアサーションします。
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

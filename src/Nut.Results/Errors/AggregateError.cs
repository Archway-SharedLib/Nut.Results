using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SR = Nut.Results.Resources.Strings;

namespace Nut.Results;

/// <summary>
/// 複数の <see cref="IError"/> を纏めるためのエラーを定義します。
/// </summary>
public class AggregateError : Error
{
    /// <summary>
    /// インスタンスを初期化します。
    /// </summary>
    public AggregateError() : this(SR.Error_DefaultAggregateErrorMessage) { }

    /// <summary>
    /// メッセージを指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    public AggregateError(string message) : this(message, Enumerable.Empty<IError>())
    {
    }

    /// <summary>
    /// 纏める複数のエラーを指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="errors">纏める複数のエラー</param>
    public AggregateError(params IError[] errors) : this(SR.Error_DefaultAggregateErrorMessage, errors) { }

    /// <summary>
    /// 纏める複数のエラーを指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="errors">纏める複数のエラー</param>
    public AggregateError(IEnumerable<IError> errors) : this(SR.Error_DefaultAggregateErrorMessage, errors) { }

    /// <summary>
    /// メッセージと纏める複数のエラーを指定してインスタンスを初期化します。
    /// </summary>
    /// <param name="message">メッセージ</param>
    /// <param name="errors">纏める複数のエラー</param>
    public AggregateError(string message, IEnumerable<IError> errors) : base(message)
    {
        if (errors is null) throw new ArgumentNullException(nameof(errors));
        var errorList = errors.ToList();
        if (errorList.Any(err => err is null))
        {
            throw new ArgumentException(SR.Exception_AggregateErrorIncludeNullInErrors);
        }
        Errors = new ReadOnlyCollection<IError>(errorList);
    }

    /// <summary>
    /// 纏めた複数のエラーを取得します。
    /// </summary>
    public ReadOnlyCollection<IError> Errors { get; }
}

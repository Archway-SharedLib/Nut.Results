// ReSharper disable CheckNamespace

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// �w�肵�� <see cref="Result"/> �Ɏw�肵�� <see cref="IError"/> ���܂܂�邩�ǂ������`�F�b�N���܂��B
    /// </summary>
    /// <param name="source"><see cref="IError"/> �������Ă��� <see cref="Result"/></param>
    /// <param name="error">�`�F�b�N���� <see cref="IError"/></param>
    /// <returns>�܂܂�Ă���ꍇ�� true �A�����łȂ��ꍇ�� false</returns>
    public static bool ContainsError(this in Result source, IError error)
        => ContainsError(source, error, null);

    /// <summary>
    /// �w�肵�� <see cref="Result"/> �Ɏw�肵�� <see cref="IError"/> ���܂܂�邩�ǂ������`�F�b�N���܂��B
    /// </summary>
    /// <param name="source"><see cref="IError"/> �������Ă��� <see cref="Result"/></param>
    /// <param name="error">�`�F�b�N���� <see cref="IError"/></param>
    /// <param name="comparer">��r���鏈��</param>
    /// <returns>�܂܂�Ă���ꍇ�� true �A�����łȂ��ꍇ�� false</returns>
    public static bool ContainsError(this in Result source, IError error, IEqualityComparer<IError>? comparer)
    {
        if (source.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(source._errorValue, error);
    }

    /// <summary>
    /// �w�肵�� <see cref="Task{Result}"/> �Ɏw�肵�� <see cref="IError"/> ���܂܂�邩�ǂ������`�F�b�N���܂��B
    /// </summary>
    /// <param name="source"><see cref="IError"/> �������Ă��� <see cref="Task{Result}"/></param>
    /// <param name="error">�`�F�b�N���� <see cref="IError"/></param>
    /// <returns>�܂܂�Ă���ꍇ�� true �A�����łȂ��ꍇ�� false</returns>
    public static Task<bool> ContainsError(this Task<Result> source, IError error)
        => ContainsError(source, error, null);

    /// <summary>
    /// �w�肵�� <see cref="Task{Result}"/> �Ɏw�肵�� <see cref="IError"/> ���܂܂�邩�ǂ������`�F�b�N���܂��B
    /// </summary>
    /// <param name="source"><see cref="IError"/> �������Ă��� <see cref="Task{Result}"/></param>
    /// <param name="error">�`�F�b�N���� <see cref="IError"/></param>
    /// <param name="comparer">��r���鏈��</param>
    /// <returns>�܂܂�Ă���ꍇ�� true �A�����łȂ��ꍇ�� false</returns>
    public static async Task<bool> ContainsError(this Task<Result> source, IError error, IEqualityComparer<IError>? comparer)
    {
        if (source is null) throw new ArgumentNullException(nameof(source));
        var s = await source.ConfigureAwait(false);
        if (s.IsOk) return false;
        comparer ??= EqualityComparer<IError>.Default;
        return comparer.Equals(s._errorValue, error);
    }
}

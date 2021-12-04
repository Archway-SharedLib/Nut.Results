using System;
using System.Threading.Tasks;
using SR = Nut.Results.Resources.Strings;



namespace Nut.Results;

public static partial class ResultUnsafeExtensions
{
    //this is unsafe method.

    /// <summary>
    /// 失敗の値を取得します。
    /// </summary>
    /// <param name="source">もととなる結果</param>
    /// <returns>失敗の値</returns>
    /// <exception cref="InvalidOperationException">結果が成功だった場合に発生します。</exception>
    public static async ValueTask<IError> GetError(this ValueTask<Result> source)
    {
        var result = await source.ConfigureAwait(false);
        if (result.IsOk) throw new InvalidOperationException(SR.Exception_ResultIsNotErrorBeforeCheck);
        return result._errorValue;
    }
}

using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// ダミーのパラメーターのためのインスタンス化できないクラスです。
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class DummyParam
    {
        private DummyParam() { }
    }
}

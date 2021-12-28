using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results;

public static partial class ResultExtensions
{
    /// <summary>
    /// ダミーのパラメーターのためのインスタンス化できないクラスです。
    /// </summary>
    public sealed class DummyParam
    {
        private DummyParam() { }
    }
}

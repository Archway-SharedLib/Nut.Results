using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results.Resources
{
    internal static class Strings { }
}

namespace System
{
    internal static partial class SR
    {
        private static global::System.Resources.ResourceManager? s_resourceManager = null;
        internal static global::System.Resources.ResourceManager ResourceManager => s_resourceManager ??= new global::System.Resources.ResourceManager(typeof(Nut.Results.Resources.Strings));

        internal static string Exception_ParameterIsNotResultT => GetResourceString(nameof(Exception_ParameterIsNotResultT), @"Parameter is not Result<> type.");

        internal static string Exception_InvalidReturnValue => GetResourceString(nameof(Exception_InvalidReturnValue), @"Invalid return value.");

        internal static string Exception_TypeParameterNeedResultType => GetResourceString(nameof(Exception_TypeParameterNeedResultType), @"Generic type parameter is not Result or Result<> type.");

        internal static string Exception_CannotSetNullToReturnValue => GetResourceString(nameof(Exception_CannotSetNullToReturnValue), @"Cannot set null to return value.");

        internal static string Exception_ResultIsNotErrorBeforeCheck => GetResourceString(nameof(Exception_ResultIsNotErrorBeforeCheck), @"Result is not error. You must check before.");

        internal static string Exception_ResultIsNotOkBeforeCheck => GetResourceString(nameof(Exception_ResultIsNotOkBeforeCheck), @"Result is not ok. You must check before.");

    }
}

// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using System.CodeDom.Compiler;
using System.Diagnostics.CodeAnalysis;
using System.Resources;

namespace System
{
    [ExcludeFromCodeCoverage]
    internal partial class SR
    {
        internal static string GetResourceString(string resourceKey, string? defaultString = null)
        {
            string? resourceString = null;
            try
            {
                resourceString = ResourceManager.GetString(resourceKey);
            }
            catch (MissingManifestResourceException) { }

            if (defaultString != null && resourceKey.Equals(resourceString))
            {
                return defaultString;
            }

            return resourceString!; // only null if missing resources
        }

        internal static string Format(string resourceFormat, object? p1)
        {
            return string.Format(resourceFormat, p1);
        }

        internal static string Format(string resourceFormat, object? p1, object? p2)
        {
            return string.Format(resourceFormat, p1, p2);
        }

        internal static string Format(string resourceFormat, object? p1, object? p2, object? p3)
        {
            return string.Format(resourceFormat, p1, p2, p3);
        }

        internal static string Format(string resourceFormat, params object?[]? args)
        {
            if (args != null)
            {
                return string.Format(resourceFormat, args);
            }

            return resourceFormat;
        }

        internal static string Format(IFormatProvider? provider, string resourceFormat, object? p1)
        {
            return string.Format(provider, resourceFormat, p1);
        }

        internal static string Format(IFormatProvider? provider, string resourceFormat, object? p1, object? p2)
        {
            return string.Format(provider, resourceFormat, p1, p2);
        }

        internal static string Format(IFormatProvider? provider, string resourceFormat, object? p1, object? p2, object? p3)
        {
            return string.Format(provider, resourceFormat, p1, p2, p3);
        }

        internal static string Format(IFormatProvider? provider, string resourceFormat, params object?[]? args)
        {
            if (args != null)
            {
                return string.Format(provider, resourceFormat, args);
            }
            return resourceFormat;
        }
    }
}

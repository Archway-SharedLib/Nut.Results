using System;
using System.Globalization;
using System.Threading.Tasks;
using FluentAssertions.Specialized;

namespace Nut.Results.Test;

public static class TestHelper
{
    public static IDisposable SetEnglishCulture()
    {
        return new CultureSwitcher(CultureInfo.GetCultureInfo("en"));
    }

    private class CultureSwitcher : IDisposable
    {
        private readonly CultureInfo _sourceCulture;
        public CultureSwitcher(CultureInfo targetCulture)
        {
            _sourceCulture = CultureInfo.CurrentCulture;
            CultureInfo.CurrentCulture = targetCulture;
            CultureInfo.CurrentUICulture = targetCulture;
        }

        public void Dispose()
        {
            CultureInfo.CurrentCulture = _sourceCulture;
            CultureInfo.CurrentUICulture = _sourceCulture;
        }
    }
}


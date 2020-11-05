using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Nut.Results.Test
{
    public static class TestHelper
    {
        public static IDisposable SetEnglishCulture()
        {
            return new CultureSwitcher(CultureInfo.GetCultureInfo("en"));
        }

        private class CultureSwitcher : IDisposable
        {
            private CultureInfo sourceCulture = null;
            public CultureSwitcher(CultureInfo targetCulture)
            {
                sourceCulture = CultureInfo.CurrentCulture;
                CultureInfo.CurrentCulture = targetCulture;
                CultureInfo.CurrentUICulture = targetCulture;
            }

            public void Dispose()
            {
                CultureInfo.CurrentCulture = sourceCulture;
                CultureInfo.CurrentUICulture = sourceCulture;
            }
        }

    }
}

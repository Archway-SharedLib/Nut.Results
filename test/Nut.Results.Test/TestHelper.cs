using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Threading.Tasks;

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

        public static Task<Result> AsTask(this Result source) => Task.FromResult(source);

        public static Task<Result<T>> AsTask<T>(this Result<T> source) => Task.FromResult(source);
    }
}

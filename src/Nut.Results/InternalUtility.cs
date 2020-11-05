using System;
using System.Collections.Generic;
using System.Text;

namespace Nut.Results
{
    internal static class InternalUtility
    {
        public static T CheckReturnValueNotNull<T>(T returnValue)
        {
            if (returnValue is null) RaizeReturnValueNotNull();
            return returnValue;
        }

        public static void RaizeReturnValueNotNull()
            => throw new InvalidReturnValueException(SR.Exception_CannotSetNullToReturnValue);
    }
}

using SR = Nut.Results.Resources.Strings;

namespace Nut.Results.Internals;

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

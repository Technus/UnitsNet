using System;
using System.Runtime.CompilerServices;

namespace UnitsNet;

internal static class ExceptionHelper
{
    internal static ArgumentException CreateArgumentException<TQuantity>(object obj, string argumentName)
        where TQuantity : IQuantity
        => new($"The given object is of type {obj.GetType()}. The expected type is {typeof(TQuantity)}.", argumentName);

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static ArgumentOutOfRangeException CreateArgumentOutOfRangeExceptionForNegativeTolerance(string argumentName)
        => new(argumentName, "The tolerance must be greater than or equal to 0.");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static InvalidOperationException CreateInvalidOperationOnEmptyCollectionException()
        => new("Sequence contains no elements.");

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    internal static InvalidCastException CreateInvalidCastException<TSource, TOther>()
        => CreateInvalidCastException<TSource>(typeof(TOther));

    internal static InvalidCastException CreateInvalidCastException<TSource>(Type targetType)
        => new($"Converting {typeof(TSource)} to {targetType} is not supported.");
}

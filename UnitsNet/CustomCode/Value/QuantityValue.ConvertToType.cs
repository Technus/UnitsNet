// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System.Runtime.CompilerServices;

namespace UnitsNet;

public partial struct QuantityValue
{
    #region Implementation of IConvertible

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    TypeCode IConvertible.GetTypeCode()
        => TypeCode.Object;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    bool IConvertible.ToBoolean(IFormatProvider? provider)
        => throw ExceptionHelper.CreateInvalidCastException<QuantityValue, char>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    byte IConvertible.ToByte(IFormatProvider? provider)
        => (byte)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    char IConvertible.ToChar(IFormatProvider? provider)
        => throw ExceptionHelper.CreateInvalidCastException<QuantityValue, char>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    DateTime IConvertible.ToDateTime(IFormatProvider? provider)
        => throw ExceptionHelper.CreateInvalidCastException<QuantityValue, DateTime>();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    decimal IConvertible.ToDecimal(IFormatProvider? provider)
        => ToDecimal();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    double IConvertible.ToDouble(IFormatProvider? provider)
        => ToDouble();

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    short IConvertible.ToInt16(IFormatProvider? provider)
        => (short)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    int IConvertible.ToInt32(IFormatProvider? provider)
        => (int)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    long IConvertible.ToInt64(IFormatProvider? provider)
        => (long)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    sbyte IConvertible.ToSByte(IFormatProvider? provider)
        => (sbyte)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    float IConvertible.ToSingle(IFormatProvider? provider)
        => (float)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    ushort IConvertible.ToUInt16(IFormatProvider? provider)
        => (ushort)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    uint IConvertible.ToUInt32(IFormatProvider? provider)
        => (uint)this;

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    ulong IConvertible.ToUInt64(IFormatProvider? provider)
        => (ulong)this;

    object IConvertible.ToType(Type conversionType, IFormatProvider? provider) => conversionType switch
    {
        null => throw new ArgumentNullException(nameof(conversionType)),
        _ when conversionType == typeof(string) => ToString(provider),
        _ when conversionType == typeof(double) => ToDouble(),
        _ when conversionType == typeof(decimal) => ToDecimal(),
        _ when conversionType == typeof(float) => (float)this,
        _ when conversionType == typeof(long) => (long)this,
        _ when conversionType == typeof(ulong) => (ulong)this,
        _ when conversionType == typeof(int) => (int)this,
        _ when conversionType == typeof(uint) => (uint)this,
        _ when conversionType == typeof(short) => (short)this,
        _ when conversionType == typeof(ushort) => (ushort)this,
        _ when conversionType == typeof(byte) => (byte)this,
        _ when conversionType == typeof(sbyte) => (sbyte)this,
        _ => throw ExceptionHelper.CreateInvalidCastException<QuantityValue>(conversionType),
    };

    #endregion
}

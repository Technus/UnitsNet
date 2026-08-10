using System.Runtime.CompilerServices;
#if USE_UNITSNET
using UnitsNet;
#endif

namespace UnitConverter.Console.Units;

internal static class UnitsExtensions
{
    extension(int d)
    {
#if USE_UNITSNET
        [Obsolete("Try adapting code to use static factory of base unit ex.: 3.MetersToLength()")]
        public QuantityValue Q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => d;
        }
#else
        [Obsolete("Try adapting code to use static factory of base unit ex.: 3.MetersToLength()")]
        public int Q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => d;
        }
#endif
        public int D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => d;
        }
    }

    extension(double d)
    {
#if USE_UNITSNET
        [Obsolete("Try adapting code to use static factory of base unit ex.: 3.MetersToLength()")]
        public QuantityValue Q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => d;
        }
#else
        [Obsolete("Try adapting code to use static factory of base unit ex.: 3.MetersToLength()")]
        public double Q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => d;
        }
#endif
        public double D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => d;
        }
    }

#if REF_UNITSNET
    extension<TSelf, TUnit>(IQuantity<TSelf, TUnit> quantity)
        where TSelf : IQuantity<TSelf, TUnit>
        where TUnit : struct, Enum
    {
#if USE_UNITSNET
        public TSelf Q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => (TSelf)quantity;
        }
#else
        public double Q
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => quantity.AsBaseValue();
        }
#endif
        [Obsolete("Try adapting code to use Q")]
        public double D
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => quantity.AsBaseValue();
        }
    }
#endif
}

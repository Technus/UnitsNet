using System.Globalization;
using System.Runtime.CompilerServices;

namespace UnitsNet;

public partial struct QuantityValue
{
    internal readonly struct QuantityValueDebugView(QuantityValue value)
    {
        public BigInteger A
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => value.Numerator;
        }
        public BigInteger B
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => value.Denominator;
        }
        public bool IsReduced => A.IsZero || B.IsZero || BigInteger.GreatestCommonDivisor(A, B).IsOne;
#if NET
        public long NbBits => A.GetBitLength() + B.GetBitLength();
#else
        public long NbBits => (A.IsZero ? 0 : (int)BigInteger.Log(BigInteger.Abs(A), 2) + 1) +
                              (B.IsZero ? 0 : (int)(BigInteger.Log(B, 2) + 1));
#endif
        public StringFormatsView StringFormats
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(value);
        }
        public NumericFormatsView ValueFormats
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => new(value);
        }

        [DebuggerDisplay("{ShortFormat}")]
        internal readonly struct StringFormatsView(QuantityValue value)
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly QuantityValue _value = value;

            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly CultureInfo _currentCulture = CultureInfo.CurrentCulture;

            public string GeneralFormat => _value.ToString("G", _currentCulture);

            public string ShortFormat => _value.ToString("G6", _currentCulture);

            public string SimplifiedFraction
            {
                get
                {
                    (BigInteger numerator, BigInteger denominator) = Reduce(_value);
                    return $"{numerator}/{denominator}";
                }
            }
        }

        [DebuggerDisplay("{Double}")]
        internal readonly struct NumericFormatsView(QuantityValue value)
        {
            [DebuggerBrowsable(DebuggerBrowsableState.Never)]
            private readonly QuantityValue _value = value;

            public int Integer
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => (int)_value;
            }
            public long Long
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => (long)_value;
            }
            public decimal Decimal
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _value.ToDecimal();
            }
            public double Double
            {
                [MethodImpl(MethodImplOptions.AggressiveInlining)]
                get => _value.ToDouble();
            }
        }
    }
}

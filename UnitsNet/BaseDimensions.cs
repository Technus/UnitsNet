// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System.Runtime.CompilerServices;
using System.Text;

namespace UnitsNet
{
    /// <summary>
    ///     Represents the base dimensions of a quantity.
    /// </summary>
    public sealed class BaseDimensions : IEquatable<BaseDimensions>
    {
        /// <summary>
        /// Represents a dimensionless (unitless) quantity.
        /// </summary>
        public static BaseDimensions Dimensionless { get; } = new();

        /// <summary>Creates an instance of <see cref="BaseDimensions"/>.</summary>
        public BaseDimensions(int length = default, int mass = default, int time = default, int current = default, int temperature = default, int amount = default, int luminousIntensity = default)
        {
            Length = length;
            Mass = mass;
            Time = time;
            Current = current;
            Temperature = temperature;
            Amount = amount;
            LuminousIntensity = luminousIntensity;
        }

        /// <summary>
        /// Checks if the dimensions represent a base quantity.
        /// </summary>
        /// <returns>True if the dimensions represent a base quantity, otherwise false.</returns>
        public bool IsBaseQuantity()
        {
            var ones = 0;

            if (Length is 1)
                ones++;
            else if (Length is not 0)
                return false;

            if (Mass is 1)
                ones++;
            else if (Mass is not 0)
                return false;

            if (Time is 1)
                ones++;
            else if (Time is not 0)
                return false;

            if (Current is 1)
                ones++;
            else if (Current is not 0)
                return false;

            if (Temperature is 1)
                ones++;
            else if (Temperature is not 0)
                return false;

            if (Amount is 1)
                ones++;
            else if (Amount is not 0)
                return false;

            if (LuminousIntensity is 1)
                ones++;
            else if (LuminousIntensity is not 0)
                return false;

            return ones is 1;
        }

        /// <summary>
        /// Checks if the dimensions represent a derived quantity.
        /// </summary>
        /// <returns>True if the dimensions represent a derived quantity, otherwise false.</returns>
        public bool IsDerivedQuantity()
        {
            var ones = 0;

            if (Length is 1)
                ones++;
            else if (Length is not 0)
                return true;

            if (Mass is 1)
                ones++;
            else if (Mass is not 0)
                return true;

            if (Time is 1)
                ones++;
            else if (Time is not 0)
                return true;

            if (Current is 1)
                ones++;
            else if (Current is not 0)
                return true;

            if (Temperature is 1)
                ones++;
            else if (Temperature is not 0)
                return true;

            if (Amount is 1)
                ones++;
            else if (Amount is not 0)
                return true;

            if (LuminousIntensity is 1)
                ones++;
            else if (LuminousIntensity is not 0)
                return true;

            return ones is > 1;
        }

        /// <summary>
        /// Checks if this base dimensions object represents a dimensionless quantity.
        /// </summary>
        /// <returns>True if this object represents a dimensionless quantity, otherwise false.</returns>
        public bool IsDimensionless() => Length == 0 &&
                                         Mass == 0 &&
                                         Time == 0 &&
                                         Current == 0 &&
                                         Temperature == 0 &&
                                         Amount == 0 &&
                                         LuminousIntensity == 0;

        /// <inheritdoc />
        public bool Equals(BaseDimensions? other)
            => other is not null && (ReferenceEquals(this, other) || EqualsCore(other));

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is BaseDimensions other && (ReferenceEquals(this, other) || EqualsCore(other));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool EqualsCore(BaseDimensions other) =>
            Length == other.Length &&
            Mass == other.Mass &&
            Time == other.Time &&
            Current == other.Current &&
            Temperature == other.Temperature &&
            Amount == other.Amount &&
            LuminousIntensity == other.LuminousIntensity;

        /// <inheritdoc />
        public override int GetHashCode()
        {
#if NET
            return HashCode.Combine(Length, Mass, Time, Current, Temperature, Amount, LuminousIntensity);
#else
            return new { Length, Mass, Time, Current, Temperature, Amount, LuminousIntensity }.GetHashCode();
#endif
        }

        /// <summary>
        /// Get resulting dimensions after multiplying two dimensions, by performing addition of each dimension.
        /// </summary>
        /// <param name="right">Other dimensions.</param>
        /// <returns>Resulting dimensions.</returns>
        public BaseDimensions Multiply(BaseDimensions right)
        {
            if(right is null)
                throw new ArgumentNullException(nameof(right));

            return new BaseDimensions(
                Length + right.Length,
                Mass + right.Mass,
                Time + right.Time,
                Current + right.Current,
                Temperature + right.Temperature,
                Amount + right.Amount,
                LuminousIntensity + right.LuminousIntensity);
        }

        /// <summary>
        /// Get resulting dimensions after dividing two dimensions, by performing subtraction of each dimension.
        /// </summary>
        /// <param name="right">Other dimensions.</param>
        /// <returns>Resulting dimensions.</returns>
        public BaseDimensions Divide(BaseDimensions right)
        {
            if(right is null)
                throw new ArgumentNullException(nameof(right));

            return new BaseDimensions(
                Length - right.Length,
                Mass - right.Mass,
                Time - right.Time,
                Current - right.Current,
                Temperature - right.Temperature,
                Amount - right.Amount,
                LuminousIntensity - right.LuminousIntensity);
        }

        /// <summary>
        /// Check if two dimensions are equal.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>True if equal.</returns>

        public static bool operator ==(BaseDimensions? left, BaseDimensions? right)
            => left?.Equals(right!) ?? right is null;

        /// <summary>
        /// Check if two dimensions are unequal.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>True if not equal.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BaseDimensions? left, BaseDimensions? right)
            => !(left == right);

        /// <summary>
        /// Multiply two dimensions.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>Resulting dimensions.</returns>
        public static BaseDimensions operator *(BaseDimensions? left, BaseDimensions? right)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            if (right is null) throw new ArgumentNullException(nameof(right));

            return left.Multiply(right);
        }

        /// <summary>
        /// Divide two dimensions.
        /// </summary>
        /// <param name="left">Left.</param>
        /// <param name="right">Right.</param>
        /// <returns>Resulting dimensions.</returns>
        public static BaseDimensions operator /(BaseDimensions? left, BaseDimensions? right)
        {
            if (left is null) throw new ArgumentNullException(nameof(left));
            if (right is null) throw new ArgumentNullException(nameof(right));

            return left.Divide(right);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            var sb = new StringBuilder();

            AppendDimensionString(sb, "Length", Length);
            AppendDimensionString(sb, "Mass", Mass);
            AppendDimensionString(sb, "Time", Time);
            AppendDimensionString(sb, "Current", Current);
            AppendDimensionString(sb, "Temperature", Temperature);
            AppendDimensionString(sb, "Amount", Amount);
            AppendDimensionString(sb, "LuminousIntensity", LuminousIntensity);

            return sb.ToString();
        }

        private static void AppendDimensionString(StringBuilder sb, string name, int value)
        {
            switch (value)
            {
                case 0:
                    return;
                case 1:
                    sb.AppendFormat("[{0}]", name);
                    break;
                default:
                    sb.AppendFormat("[{0}^{1}]", name, value);
                    break;
            }
        }
#if NET
        /// <summary>
        /// Gets the length dimensions (L).
        /// </summary>
        public int Length { get; init; }

        /// <summary>
        /// Gets the mass dimensions (M).
        /// </summary>
        public int Mass { get; init; }

        /// <summary>
        /// Gets the time dimensions (T).
        /// </summary>
        public int Time { get; init; }

        /// <summary>
        /// Gets the electric current dimensions (I).
        /// </summary>
        public int Current { get; init; }

        /// <summary>
        /// Gets the temperature dimensions (Θ).
        /// </summary>
        public int Temperature { get; init; }

        /// <summary>
        /// Gets the amount of substance dimensions (N).
        /// </summary>
        public int Amount { get; init; }

        /// <summary>
        /// Gets the luminous intensity dimensions (J).
        /// </summary>
        public int LuminousIntensity { get; init; }
#else
        /// <summary>
        /// Gets the length dimensions (L).
        /// </summary>
        public int Length { get; }

        /// <summary>
        /// Gets the mass dimensions (M).
        /// </summary>
        public int Mass{ get; }

        /// <summary>
        /// Gets the time dimensions (T).
        /// </summary>
        public int Time{ get; }

        /// <summary>
        /// Gets the electric current dimensions (I).
        /// </summary>
        public int Current{ get; }

        /// <summary>
        /// Gets the temperature dimensions (Θ).
        /// </summary>
        public int Temperature{ get; }

        /// <summary>
        /// Gets the amount of substance dimensions (N).
        /// </summary>
        public int Amount{ get; }

        /// <summary>
        /// Gets the luminous intensity dimensions (J).
        /// </summary>
        public int LuminousIntensity{ get; }
#endif
    }
}

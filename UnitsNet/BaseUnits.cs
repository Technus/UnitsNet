// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using UnitsNet.Units;

namespace UnitsNet
{
    /// <inheritdoc />
    /// <summary>
    ///     Represents the base units for a quantity. All quantities, both base and derived, can be
    ///     represented by a combination of these seven base units.
    /// </summary>
    public sealed class BaseUnits: IEquatable<BaseUnits>
    {
        /// <summary>
        /// Represents BaseUnits that have not been defined.
        /// </summary>
        public static BaseUnits Undefined { get; } = new BaseUnits();

        /// <summary>
        /// Creates an instance of if the base units class that represents the base units for a quantity.
        /// All quantities, both base and derived, can be represented by a combination of these seven base units.
        /// </summary>
        /// <param name="length">The length unit (L).</param>
        /// <param name="mass">The mass unit (M).</param>
        /// <param name="time">The time unit (T).</param>
        /// <param name="current">The electric current unit (I).</param>
        /// <param name="temperature">The temperature unit (Θ).</param>
        /// <param name="amount">The amount of substance unit (N).</param>
        /// <param name="luminousIntensity">The luminous intensity unit (J).</param>
        public BaseUnits(
            LengthUnit? length = null,
            MassUnit? mass = null,
            DurationUnit? time = null,
            ElectricCurrentUnit? current = null,
            TemperatureUnit? temperature = null,
            AmountOfSubstanceUnit? amount = null,
            LuminousIntensityUnit? luminousIntensity = null)
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
        /// Checks if all of the base units are equal to another instance's.
        /// </summary>
        /// <param name="other">The other instance to check if equal to.</param>
        /// <returns>True if equal, otherwise false.</returns>
        public bool Equals(BaseUnits? other)
            => other is not null && (ReferenceEquals(this, other) || EqualsCore(other));

        /// <inheritdoc />
        public override bool Equals(object? obj)
            => obj is BaseUnits other && (ReferenceEquals(this,other) || EqualsCore(other));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool EqualsCore(BaseUnits other) => Length == other.Length &&
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
            return new {Length, Mass, Time, Current, Temperature, Amount, LuminousIntensity}.GetHashCode();
#endif
        }

        /// <summary>
        /// Checks if the base units are a subset of another. Undefined base units are ignored.
        /// If all base united are undefined (equal to <see cref="Undefined"/>),
        /// IsSubsetOf will return true only if other is also equal to <see cref="Undefined"/>.
        /// </summary>
        /// <param name="other">The other <see cref="BaseUnits"/> to compare to.</param>
        /// <returns>True if the base units are a subset of other, otherwise false.</returns>
        public bool IsSubsetOf(BaseUnits other)
        {
            if (other == null) throw new ArgumentNullException(nameof(other));

            if(ReferenceEquals(this, other))
                return true;

            // If all base units are undefined, can only be a subset of another where all base units are undefined.
            if (IsFullyUndefined())
                return other.IsFullyUndefined();

            return (Length == null || Length == other.Length) &&
                (Mass == null || Mass == other.Mass) &&
                (Time == null || Time == other.Time) &&
                (Current == null || Current == other.Current) &&
                (Temperature == null || Temperature == other.Temperature) &&
                (Amount == null || Amount == other.Amount) &&
                (LuminousIntensity == null || LuminousIntensity == other.LuminousIntensity);
        }

        /// <summary>
        /// Checks if this instance is equal to another.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>True if equal, otherwise false.</returns>
        /// <seealso cref="Equals(BaseUnits)"/>
        public static bool operator ==(BaseUnits? left, BaseUnits? right)
            => left?.Equals(right!) ?? right is null;

        /// <summary>
        /// Checks if this instance is not equal to another.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>True if not equal, otherwise false.</returns>
        /// <seealso cref="Equals(BaseUnits)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(BaseUnits? left, BaseUnits? right)
            => !(left == right);

        /// <inheritdoc />
        public override string ToString()
        {
            if (IsFullyUndefined())
                return "Undefined";

            var sb = new StringBuilder();

            if (Length is not null)
                sb.AppendFormat("L={0}, ", Length);
            if (Mass is not null)
                sb.AppendFormat("M={0}, ", Mass);
            if (Time is not null)
                sb.AppendFormat("T={0}, ", Time);
            if (Current is not null)
                sb.AppendFormat("I={0}, ", Current);
            if (Temperature is not null)
                sb.AppendFormat("Θ={0}, ", Temperature);
            if (Amount is not null)
                sb.AppendFormat("N={0}, ", Amount);
            if (LuminousIntensity is not null)
                sb.AppendFormat("J={0}, ", LuminousIntensity);

            if (sb.Length >= 2)
                sb.Length -= 2;

            return sb.ToString();
        }
#if NET
        /// <summary>
        /// Gets the length unit (L).
        /// </summary>
        public LengthUnit? Length { get; init; }

        /// <summary>
        /// Gets the mass unit (M).
        /// </summary>
        public MassUnit? Mass { get; init; }

        /// <summary>
        /// Gets the time unit (T).
        /// </summary>
        public DurationUnit? Time { get; init; }

        /// <summary>
        /// Gets the electric current unit (I).
        /// </summary>
        public ElectricCurrentUnit? Current { get; init; }

        /// <summary>
        /// Gets the temperature unit (Θ).
        /// </summary>
        public TemperatureUnit? Temperature { get; init; }

        /// <summary>
        /// Gets the amount of substance unit (N).
        /// </summary>
        public AmountOfSubstanceUnit? Amount { get; init; }

        /// <summary>
        /// Gets the luminous intensity unit (J).
        /// </summary>
        public LuminousIntensityUnit? LuminousIntensity { get; init; }
#else
        /// <summary>
        /// Gets the length unit (L).
        /// </summary>
        public LengthUnit? Length { get; }

        /// <summary>
        /// Gets the mass unit (M).
        /// </summary>
        public MassUnit? Mass { get; }

        /// <summary>
        /// Gets the time unit (T).
        /// </summary>
        public DurationUnit? Time { get; }

        /// <summary>
        /// Gets the electric current unit (I).
        /// </summary>
        public ElectricCurrentUnit? Current { get; }

        /// <summary>
        /// Gets the temperature unit (Θ).
        /// </summary>
        public TemperatureUnit? Temperature { get; }

        /// <summary>
        /// Gets the amount of substance unit (N).
        /// </summary>
        public AmountOfSubstanceUnit? Amount { get; }

        /// <summary>
        /// Gets the luminous intensity unit (J).
        /// </summary>
        public LuminousIntensityUnit? LuminousIntensity { get; }
#endif

        /// <summary>
        ///     Gets a value indicating whether all base units are defined.
        /// </summary>
        /// <remarks>
        ///     This property returns <c>true</c> if all seven base units 
        ///     (Length, Mass, Time, Current, Temperature, Amount, and LuminousIntensity) 
        ///     are non-null; otherwise, it returns <c>false</c>.
        /// </remarks>
        public bool IsFullyDefined => Length is not null &&
                                      Mass is not null &&
                                      Time is not null &&
                                      Current is not null &&
                                      Temperature is not null &&
                                      Amount is not null &&
                                      LuminousIntensity is not null;

        /// <summary>
        ///     Gets a value indicating whether all base units are undefined.
        /// </summary>
        /// <remarks>
        ///     This property returns <c>true</c> if all seven base units 
        ///     (Length, Mass, Time, Current, Temperature, Amount, and LuminousIntensity) 
        ///     are null; otherwise, it returns <c>false</c>.
        /// </remarks>
        public bool IsFullyUndefined() => ReferenceEquals(this, Undefined) ||
            ( Length is null &&
              Mass is null &&
              Time is null &&
              Current is null &&
              Temperature is null &&
              Amount is null &&
              LuminousIntensity is null);
    }
}

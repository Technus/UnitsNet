// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System;
using System.Runtime.CompilerServices;
using UnitsNet.Units;

namespace UnitsNet
{
    /// <summary>
    ///     A unit system defined by a combination of base units.
    ///     This is typically used to define the "working units" for consistently creating and presenting quantities in the selected base units,
    ///     such as <see cref="SI"/> to use SI base units such as meters, kilograms and seconds.
    /// </summary>
    public sealed class UnitSystem : IEquatable<UnitSystem>
    {
        private static readonly BaseUnits SIBaseUnits = new(LengthUnit.Meter, MassUnit.Kilogram, DurationUnit.Second,
            ElectricCurrentUnit.Ampere, TemperatureUnit.Kelvin, AmountOfSubstanceUnit.Mole, LuminousIntensityUnit.Candela);

        /// <summary>
        /// Gets the SI unit system.
        /// </summary>
        public static UnitSystem SI { get; } = new UnitSystem(SIBaseUnits);

        /// <summary>
        ///     The base units of this unit system.
        /// </summary>
        public BaseUnits BaseUnits { get; }

        /// <summary>
        /// Creates an instance of a unit system with the specified base units.
        /// </summary>
        /// <param name="baseUnits">One or more base units that define the unit system.</param>
        public UnitSystem(BaseUnits baseUnits)
        {
            if (baseUnits is null) throw new ArgumentNullException(nameof(baseUnits));
            if (baseUnits == BaseUnits.Undefined) throw new ArgumentOutOfRangeException(nameof(baseUnits), baseUnits, "A unit system must define at least one base unit.");

            BaseUnits = baseUnits;
        }

        /// <inheritdoc />
        public override bool Equals(object? other)
            => other is UnitSystem otherUnitSystem && EqualsCore(otherUnitSystem);

        /// <inheritdoc />
        public bool Equals(UnitSystem? other)
            => other is not null && EqualsCore(other);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal bool EqualsCore(UnitSystem other) => BaseUnits.EqualsCore(other.BaseUnits);

        /// <summary>
        /// Checks if this instance is equal to another.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>True if equal, otherwise false.</returns>
        /// <seealso cref="Equals(UnitSystem)"/>
        public static bool operator ==(UnitSystem? left, UnitSystem? right)
            => left?.Equals(right) ?? right is null;

        /// <summary>
        /// Checks if this instance is equal to another.
        /// </summary>
        /// <param name="left">The left instance.</param>
        /// <param name="right">The right instance.</param>
        /// <returns>True if equal, otherwise false.</returns>
        /// <seealso cref="Equals(UnitSystem)"/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool operator !=(UnitSystem? left, UnitSystem? right)
            => !(left == right);

        /// <inheritdoc />
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public override int GetHashCode()
            => BaseUnits.GetHashCode();
    }
}

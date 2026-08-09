// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System.Runtime.CompilerServices;

namespace UnitsNet
{
    public partial struct MassConcentration
    {
        /// <summary>
        ///     Get <see cref="Molarity" /> from this <see cref="MassConcentration" /> using the known component <see cref="MolarMass" />.
        /// </summary>
        /// <param name="molecularWeight"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Molarity ToMolarity(MolarMass molecularWeight)
            => this / molecularWeight;

        /// <summary>
        ///  Get <see cref="VolumeConcentration" /> from this <see cref="MassConcentration" /> using the known component <see cref="Density" />.
        /// </summary>
        /// <param name="componentDensity"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public VolumeConcentration ToVolumeConcentration(Density componentDensity)
            => this / componentDensity;

        #region Static Methods

        /// <summary>
        ///     Get <see cref="MassConcentration" /> from <see cref="Molarity" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MassConcentration FromMolarity(Molarity molarity, MolarMass mass)
            => molarity * mass;

        /// <summary>
        ///     Get <see cref="MassConcentration" /> from <see cref="VolumeConcentration" /> and component <see cref="Density" />.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static MassConcentration FromVolumeConcentration(VolumeConcentration volumeConcentration, Density componentDensity)
            => volumeConcentration * componentDensity;

        #endregion
    }
}

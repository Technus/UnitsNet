using System.Runtime.CompilerServices;
using UnitsNet.Units;

namespace UnitsNet
{
    public partial struct Molarity
    {
        /// <summary>
        ///     Get a <see cref="MassConcentration"/> from this <see cref="Molarity"/>.
        /// </summary>
        /// <param name="molecularWeight"></param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MassConcentration ToMassConcentration(MolarMass molecularWeight)
            => this * molecularWeight;

        /// <summary>
        ///     Get a <see cref="MassConcentration"/> from this <see cref="Molarity"/>.
        /// </summary>
        /// <param name="componentDensity"></param>
        /// <param name="componentMass"></param>
        public VolumeConcentration ToVolumeConcentration(Density componentDensity, MolarMass componentMass)
            => this * componentMass / componentDensity;

        #region Static Methods

        /// <summary>
        ///  Get <see cref="Molarity"/> from <see cref="VolumeConcentration"/> and known component <see cref="Density"/> and <see cref="MolarMass"/>.
        /// </summary>
        /// <param name="volumeConcentration"></param>
        /// <param name="componentDensity"></param>
        /// <param name="componentMass"></param>
        /// <returns></returns>
        public static Molarity FromVolumeConcentration(VolumeConcentration volumeConcentration, Density componentDensity, MolarMass componentMass)
            => volumeConcentration * componentDensity / componentMass;

        #endregion
    }
}

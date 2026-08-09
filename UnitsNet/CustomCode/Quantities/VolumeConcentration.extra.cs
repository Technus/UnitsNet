using System.Runtime.CompilerServices;
using UnitsNet.Units;

namespace UnitsNet
{
    public partial struct VolumeConcentration
    {
        /// <summary>
        /// Get <see cref="MassConcentration" /> from this <see cref="VolumeConcentration" /> and component <see cref="Density" /> .
        /// </summary>
        /// <param name="componentDensity"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public MassConcentration ToMassConcentration(Density componentDensity)
            => this * componentDensity;

        /// <summary>
        /// Get <see cref="Molarity" /> from this <see cref="VolumeConcentration" /> and component <see cref="Density"/> and <see cref="MolarMass"/> .
        /// </summary>
        /// <param name="componentDensity"></param>
        /// <param name="compontMolarMass"></param>
        /// <returns></returns>
        public Molarity ToMolarity(Density componentDensity, MolarMass compontMolarMass)
            => this * componentDensity / compontMolarMass;

        #region Static Methods

        /// <summary>
        ///     Get <see cref="VolumeConcentration" /> from a component <see cref="Volume" /> and total mixture <see cref="Volume" /> .
        /// </summary>
        public static VolumeConcentration FromVolumes(Volume componentVolume, Volume mixtureMass)
            => new(componentVolume / mixtureMass, VolumeConcentrationUnit.DecimalFraction);

        /// <summary>
        ///     Get a <see cref="VolumeConcentration"/> from <see cref="Molarity" /> and a component <see cref="Density" /> and <see cref="MolarMass" />.
        /// </summary>
        /// <param name="molarity"></param>
        /// <param name="componentDensity"></param>
        /// <param name="componentMolarMass"></param>
        public static VolumeConcentration FromMolarity(Molarity molarity, Density componentDensity, MolarMass componentMolarMass)
            => molarity * componentMolarMass / componentDensity;

        #endregion
    }
}

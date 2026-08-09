using System.Runtime.CompilerServices;
using UnitsNet.Units;

namespace UnitsNet
{
    public partial struct MassFraction
    {
        /// <summary>
        /// Get the <see cref="Mass"/> of the component by multiplying the <see cref="Mass"/> of the mixture and this <see cref="MassFraction"/>.
        /// </summary>
        /// <param name="totalMass">The total mass of the mixture</param>
        /// <returns>The actual mass of the component involved in this mixture</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Mass GetComponentMass(Mass totalMass)
            => totalMass * this;

        /// <summary>
        /// Get the total <see cref="Mass"/> of the mixture by dividing the <see cref="Mass"/> of the component by this <see cref="MassFraction"/>
        /// </summary>
        /// <param name="componentMass">The actual mass of the component involved in this mixture</param>
        /// <returns>The total mass of the mixture</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Mass GetTotalMass(Mass componentMass)
            => componentMass / this;

        #region Static Methods

        /// <summary>
        ///     Get <see cref="MassFraction" /> from a component <see cref="Mass" /> and total mixture <see cref="Mass" /> .
        /// </summary>
        public static MassFraction FromMasses(Mass componentMass, Mass mixtureMass)
            => new(componentMass / mixtureMass, MassFractionUnit.DecimalFraction);

        #endregion
    }
}

#define CHEEKY

global using _Area = UnitsNet.Area;
global using _Length = UnitsNet.Length;

#if CHEEKY
global using Area = UnitsNet.Area;
global using Length = UnitsNet.Length;
#else
global using Area = double;
global using Length = double
#endif

using System;
using System.Collections.Generic;
using System.Text;
using UnitsNet;

namespace UnitConverter.Console;

public static class CheekyExtensions
{
    extension(double d)
    {
        public double D => d;
        public double N => d;
    }

    extension<TSelf, TUnit>(IQuantity<TSelf,TUnit> quantity)
        where TSelf : IQuantity<TSelf, TUnit>
        where TUnit : struct, Enum
    {
#if CHEEKY
        public TSelf N => (TSelf)quantity;
#else
        public double N => quantity.D;
#endif
        public double D => quantity.AsBaseValue();
    }
}

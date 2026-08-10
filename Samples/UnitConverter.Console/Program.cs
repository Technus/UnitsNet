using UnitsNet;
using static System.Console;

internal class Program
{
    public static double fe => 3.KilometersPerLiterToFuelEfficiency().D;
    public static double mf => 3.GramsPerSecondToMassFlow().D;
    public static double tcr => 3.DegreesCelsiusPerSecondToTemperatureChangeRate().D;
    public static double ti => 3.SquareMeterKelvinsPerKilowattToThermalInsulance().D;
    public static double tt => 3.KilowattsPerSquareMeterKelvinToThermalTransmittance().D;

    public static Quantity _fe => 3.KilometersPerLiterToFuelEfficiency().Value;
    public static Quantity _mf => 3.GramsPerSecondToMassFlow().Value;
    public static Quantity _tcr => 3.DegreesCelsiusPerSecondToTemperatureChangeRate().Value;
    public static Quantity _ti => 3.SquareMeterKelvinsPerKilowattToThermalInsulance().Value;
    public static Quantity _tt => 3.KilowattsPerSquareMeterKelvinToThermalTransmittance().Value;

    public static double ex => MassFlow.FromGramsPerSecond(3).D * Duration.FromSeconds(3).D;
    public static double e => 3.GramsPerSecondToMassFlow().D * 3.SecondsToDuration().D;
    public static Quantity _e => (3.GramsPerSecondToMassFlow() * 3.SecondsToDuration()).Value;

    public static double ffmf => 3.GramsPerSecondToMassFlow().D * 3.SecondsToDuration().D;
    public static Mass _ffmf => (3.GramsPerSecondToMassFlow() * 3.SecondsToDuration());

    private static void Main(string[] args)
    {
        var w = 4.MetersToLength();
        var h = 5.MetersToLength();
        var a = w * h;
        var area = A(w, h);
        WriteLine(area);

        var s = 3.KilogramsToMass();

        static Area A(Length l, Length w) => w * l;
    }
}

using UnitsNet;
using static System.Console;

var w = 4.MetersToLength();
var h = 5.MetersToLength();
var a = w * h;
var area = A(w, h);
WriteLine(area);

var s = 3.KilogramsToMass();

var fe = 3.KilometersPerLiterToFuelEfficiency();
var mf = 3.GramsPerSecondToMassFlow();
var tcr = 3.DegreesCelsiusPerSecondToTemperatureChangeRate();
var ti = 3.SquareMeterKelvinsPerKilowattToThermalInsulance();
var tt = 3.KilowattsPerSquareMeterKelvinToThermalTransmittance();

static Area A(Length l, Length w) => w * l;

using UnitsNet;
using UnitConverter.Console;

using static System.Console;

var w = Length.FromCentimeters(4).N;
var h = Length.FromMeters(5).N;
var a = w * h;
var area = a.D;
WriteLine(area);

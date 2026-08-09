using UnitsNet;
using UnitConverter.Console;

using static System.Console;
using UnitsNet.Units;
var w = _Length.FromCentimeters(4).N;
var h = _Length.FromMeters(5).N;
var a = w * h;
var area = A(w, h);
WriteLine(area);

static Area A(Length l, Length w) => w * l;

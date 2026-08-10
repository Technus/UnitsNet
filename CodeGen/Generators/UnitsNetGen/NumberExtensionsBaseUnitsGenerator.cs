using System.Collections.Generic;
using System.Linq;
using CodeGen.JsonTypes;

namespace CodeGen.Generators.UnitsNetGen
{
    internal class NumberExtensionsBaseUnitsGenerator(Quantity[] quantity) : GeneratorBase
    {
        private readonly Quantity[] _quantities = quantity;

        public string Generate()
        {
            Writer.WL(GeneratedFileHeader);
            Writer.WL($@"

using System.Runtime.CompilerServices;

#nullable enable

namespace UnitsNet;

/// <summary>
///     Dynamically parse or construct quantities when types are only known at runtime.
/// </summary>
public static class BaseUnitsExtensions
{{
#if NET
    extension(double d)
    {{
{WriteFromHelpers("double")}
    }}

    extension(int d)
    {{
{WriteFromHelpers("int")}
    }}
#endif
}}");
            return Writer.ToString();
        }

        string WriteFromHelpers(string type) => string.Join('\n', _quantities.Select(x => $@"
        /// <summary>
        ///     Convert to base unit quantity of {x.BaseUnit}.
        /// </summary>
        [Obsolete(""Yields unpredictable results with non bare SI based units"")]
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public {type} {x.Units.First(u => u.SingularName == x.BaseUnit).PluralName}ToBase{x.Name}()
            => d;

        /// <summary>
        ///     Convert to base unit quantity of {x.SiBaseUnit}.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public {type} {x.Units.First(u => u.SingularName == x.SiBaseUnit).PluralName}ToSiBase{x.Name}()
            => d;"));
    }
}

using System.Collections.Generic;
using System.Linq;
using CodeGen.JsonTypes;

namespace CodeGen.Generators.UnitsNetGen
{
    internal class StaticQuantityGenerator(Quantity[] quantities) : GeneratorBase
    {
        private readonly Quantity[] _quantities = quantities;

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
public partial class Quantity
{{
#if NET
    extension(double d)
    {{
{WriteFromHelpers()}
    }}

    extension(int d)
    {{
{WriteFromHelpers()}
    }}
#endif

    /// <summary>
    ///     Serves as a repository for predefined quantity conversion mappings, facilitating the automatic generation and retrieval of unit conversions in the UnitsNet library.
    /// </summary>
    internal static class DefaultProvider
    {{
        /// <summary>
        ///     All QuantityInfo instances that are present in UnitsNet by default.
        /// </summary>
        internal static readonly IReadOnlyList<QuantityInfo> Quantities = new QuantityInfo[]
        {{");
            foreach (var quantity in _quantities)
                Writer.WL($@"
            {quantity.Name}.Info,");
            Writer.WL($@"
        }};

        /// <summary>
        ///     All implicit quantity conversions that exist by default.
        /// </summary>
        internal static readonly IReadOnlyList<QuantityConversionMapping> Conversions = new QuantityConversionMapping[]
        {{");
            foreach (var quantityRelation in _quantities.SelectMany(quantity => quantity.Relations.Where(x => x.Operator == "inverse")).Distinct(new CumulativeRelationshipEqualityComparer()).OrderBy(relation => relation.LeftQuantity.Name))
                Writer.WL($@"
            new (typeof({quantityRelation.LeftQuantity.Name}), typeof({quantityRelation.RightQuantity.Name})),");
            Writer.WL($@"
        }};
    }}
}}");
            return Writer.ToString();
        }

        string WriteFromHelpers() => string.Join('\n', _quantities.Select(x => $@"
        /// <summary>
        ///     Convert to base unit quantity of {x.BaseUnit}.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public {x.Name} {x.Units.First(u => u.SingularName == x.BaseUnit).PluralName}To{x.Name}()
            => new(d, {x.Name}Unit.{x.BaseUnit});"));

    }

    internal class CumulativeRelationshipEqualityComparer: IEqualityComparer<QuantityRelation>{
        public bool Equals(QuantityRelation? x, QuantityRelation? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return
                x.ResultQuantity == y.ResultQuantity && (
                    (x.LeftQuantity.Equals(y.LeftQuantity) && x.RightQuantity.Equals(y.RightQuantity))
                    || (x.LeftQuantity.Equals(y.RightQuantity) && x.RightQuantity.Equals(y.LeftQuantity)));
        }

        public int GetHashCode(QuantityRelation obj)
        {
            return obj.LeftQuantity.GetHashCode() ^ obj.RightQuantity.GetHashCode();
        }
    }
}

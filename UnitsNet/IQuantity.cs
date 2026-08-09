// Licensed under MIT No Attribution, see LICENSE file at the root.
// Copyright 2013 Andreas Gullberg Larsen (andreas.larsen84@gmail.com). Maintained at https://github.com/angularsen/UnitsNet.

using System.Runtime.CompilerServices;

namespace UnitsNet
{
    /// <summary>
    ///     Represents a quantity.
    /// </summary>
    public interface IQuantity : IFormattable
    {
        /// <summary>
        ///     Information about the quantity type, such as unit values and names.
        /// </summary>
        /// <remarks>
        ///     Kept for back-compat with netstandard2.0. On .NET 5+, prefer the static <c>TSelf.Info</c>
        ///     property or the <c>GetQuantityInfo()</c> extension method on <see cref="QuantityExtensions"/>.
        /// </remarks>
#if NET
        [Obsolete("Kept for back-compat with netstandard2.0. On .NET 5+, use the static TSelf.Info property or the GetQuantityInfo() extension method.")]
#endif
        QuantityInfo QuantityInfo { get; }

        /// <summary>
        ///     The unit this quantity was constructed with -or- BaseUnit if default ctor was used.
        /// </summary>
        Enum Unit { get; }

        /// <summary>
        ///     The value this quantity was constructed with. See also <see cref="Unit"/>.
        /// </summary>
        QuantityValue Value { get; }

        /// <summary>
        ///     Gets the unique key for the unit type and its corresponding value.
        /// </summary>
        /// <remarks>
        ///     This property is particularly useful when using an enum-based unit in a hash-based collection,
        ///     as it avoids the boxing that would normally occur when casting the enum to <see cref="Enum" />.
        /// </remarks>
        UnitKey UnitKey { get; }
    }

    /// <summary>
    ///     A stronger typed interface where the unit enum type is known, to avoid passing in the
    ///     wrong unit enum type and not having to cast from <see cref="Enum"/>.
    /// </summary>
    /// <example>
    ///     IQuantity{LengthUnit} length;
    ///     QuantityValue centimeters = length.As(LengthUnit.Centimeter); // Type safety on enum type
    /// </example>
    /// <typeparam name="TUnitType">The unit type of the quantity.</typeparam>
    public interface IQuantity<TUnitType> : IQuantity
        where TUnitType : struct, Enum
    {
        /// <inheritdoc cref="IQuantity.Unit"/>
        new TUnitType Unit { get; }

        /// <inheritdoc cref="IQuantity.QuantityInfo"/>
#if NET
        [Obsolete("Kept for back-compat with netstandard2.0. On .NET 5+, use the static TSelf.Info property or the GetQuantityInfo() extension method.")]
#endif
        new QuantityInfo<TUnitType> QuantityInfo { get; }

#if NET

        #region Implementation of IQuantity

#pragma warning disable CS0618 // Type or member is obsolete
        QuantityInfo IQuantity.QuantityInfo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => QuantityInfo;
        }
#pragma warning restore CS0618

        Enum IQuantity.Unit
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => Unit;
        }

        #endregion

#endif
    }

    /// <inheritdoc cref="IQuantity" />
    /// <remarks>
    ///     This is a specialization of <see cref="IQuantity" /> that is used (internally) for constraining certain
    ///     methods, without having to include the unit type as additional generic parameter.
    /// </remarks>
    /// <typeparam name="TQuantity"></typeparam>
    public interface IQuantityOfType<out TQuantity> : IQuantity
        where TQuantity : IQuantity
    {
#if NET
        /// <summary>
        ///     The static <see cref="QuantityInfo"/> for this quantity type.
        /// </summary>
        /// <remarks>
        ///     Implemented by every quantity as a public static <c>Info</c> property. Prefer this and the
        ///     <see cref="QuantityExtensions.GetQuantityInfo(IQuantity)"/> extension method over the
        ///     obsolete instance <see cref="IQuantity.QuantityInfo"/> property.
        /// </remarks>
        public static abstract QuantityInfo Info { get; }

        /// <summary>
        ///     Creates an instance of the quantity from a specified value and unit.
        /// </summary>
        /// <param name="value">The numerical value of the quantity.</param>
        /// <param name="unit">The unit of the quantity.</param>
        /// <returns>An instance of the quantity with the specified value and unit.</returns>
        public static abstract TQuantity Create(QuantityValue value, UnitKey unit);
#else
        /// <inheritdoc cref="IQuantity.QuantityInfo"/>
        new IQuantityInstanceInfo<TQuantity> QuantityInfo { get; }
#endif
    }

    /// <summary>
    ///     An <see cref="IQuantity{TUnitType}"/> that supports generic equality comparison with tolerance.
    /// </summary>
    /// <typeparam name="TSelf">The type itself, for the CRT pattern.</typeparam>
    /// <typeparam name="TUnitType">The underlying unit enum type.</typeparam>
    public interface IQuantity<TSelf, TUnitType> : IQuantityOfType<TSelf>, IQuantity<TUnitType>
        where TSelf : IQuantity<TSelf, TUnitType>
        where TUnitType : struct, Enum
    {
        /// <inheritdoc cref="IQuantity.QuantityInfo"/>
#if NET
        [Obsolete("Kept for back-compat with netstandard2.0. On .NET 5+, use the static TSelf.Info property or the GetQuantityInfo() extension method.")]
#endif
        new QuantityInfo<TSelf, TUnitType> QuantityInfo { get; }

#if !NET

        /// <summary>
        /// Get the quantity as quantity of the base unit
        /// </summary>
        /// <remarks>Usually equal to: new(this.As(BaseUnit), BaseUnit) or From(this.As(BaseUnit), BaseUnit)</remarks>
        TSelf AsBaseQuantity();

        /// <summary>
        /// Get the value as <see cref="QuantityValue"/> of the base unit
        /// </summary>
        /// <remarks>Usually equal to: this.As(BaseUnit)</remarks>
        QuantityValue AsBaseValue();
#else
        /// <summary>
        /// Get the quantity as quantity of the base unit
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual TSelf AsBaseQuantity()
            => TSelf.From(AsBaseValue(), TSelf.BaseUnit);

        /// <summary>
        /// Get the value as <see cref="QuantityValue"/> of the base unit
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public virtual QuantityValue AsBaseValue()
            => UnitConverter.Default.ConvertValue(Value, Unit, TSelf.BaseUnit);

        /// <summary>
        /// Gets the base dimensions
        /// </summary>
        public static virtual BaseDimensions BaseDimensions
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => TSelf.Info.BaseDimensions;
        }

        /// <summary>
        /// Get the base unit
        /// </summary>
        public static virtual TUnitType BaseUnit
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => TSelf.Info.BaseUnitInfo.Value;
        }

        /// <summary>
        /// Gets defined units
        /// </summary>
        public static virtual IReadOnlyCollection<TUnitType> Units
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => TSelf.Info.Units;
        }

        /// <summary>
        /// Gets the abbreviation of unit
        /// </summary>
        /// <param name="unit"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual string GetAbbreviation(TUnitType unit)
            => TSelf.GetAbbreviation(unit, null);

        /// <summary>
        /// Gets the abbreviation of unit
        /// </summary>
        /// <param name="unit"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual string GetAbbreviation(TUnitType unit, IFormatProvider? provider)
            => UnitsNetSetup.Default.UnitAbbreviations.GetDefaultAbbreviation(unit, provider);

        /// <summary>
        /// Parse a string with one or two quantities of the format "&lt;quantity&gt; &lt;unit&gt;".
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual TSelf Parse(string str)
            => TSelf.Parse(str, null);

        /// <summary>
        /// Parse a string with one or two quantities of the format "&lt;quantity&gt; &lt;unit&gt;".
        /// </summary>
        /// <param name="str"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual TSelf Parse(string str, IFormatProvider? provider)
            => QuantityParser.Default.Parse<TSelf, TUnitType>(str, provider, TSelf.From);

        /// <summary>
        /// Try to parse a string with one or two quantities of the format "&lt;quantity&gt; &lt;unit&gt;".
        /// </summary>
        /// <param name="str"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual bool TryParse([NotNullWhen(true)] string? str, out TSelf? result)
            => TSelf.TryParse(str, null, out result);

        /// <summary>
        /// Try to parse a string with one or two quantities of the format "&lt;quantity&gt; &lt;unit&gt;".
        /// </summary>
        /// <param name="str"></param>
        /// <param name="provider"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual bool TryParse([NotNullWhen(true)] string? str, IFormatProvider? provider, out TSelf? result)
            => QuantityParser.Default.TryParse<TSelf, TUnitType>(str, provider, TSelf.From, out result);

        /// <summary>
        /// Parse a unit string.
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual TUnitType ParseUnit(string str)
            => TSelf.ParseUnit(str, null);

        /// <summary>
        /// Parse a unit string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="provider"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual TUnitType ParseUnit(string str, IFormatProvider? provider)
            => UnitParser.Default.Parse(str, TSelf.Info.UnitInfos, provider).Value;

        /// <summary>
        /// Parse a unit string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual bool TryParseUnit([NotNullWhen(true)] string? str, out TUnitType unit)
            => TSelf.TryParseUnit(str, null, out unit);

        /// <summary>
        /// Parse a unit string.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="provider"></param>
        /// <param name="unit"></param>
        /// <returns></returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static virtual bool TryParseUnit([NotNullWhen(true)] string? str, IFormatProvider? provider, out TUnitType unit)
            => UnitParser.Default.TryParse(str, TSelf.Info, provider, out unit);

        /// <inheritdoc cref="IQuantityOfType{TQuantity}.Info"/>
        public new static abstract QuantityInfo<TSelf, TUnitType> Info { get; }

        /// <summary>
        ///     Creates an instance of the quantity from a specified value and unit.
        /// </summary>
        /// <param name="value">The numerical value of the quantity.</param>
        /// <param name="unit">The unit of the quantity.</param>
        /// <returns>An instance of the quantity with the specified value and unit.</returns>
        public static abstract TSelf From(QuantityValue value, TUnitType unit);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        static TSelf IQuantityOfType<TSelf>.Create(QuantityValue value, UnitKey unit)
            => TSelf.From(value, unit.ToUnit<TUnitType>());

        static QuantityInfo IQuantityOfType<TSelf>.Info
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => TSelf.Info;
        }

#pragma warning disable CS0618 // Type or member is obsolete
        QuantityInfo<TUnitType> IQuantity<TUnitType>.QuantityInfo
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            get => QuantityInfo;
        }
#pragma warning restore CS0618

#endif

    }
}

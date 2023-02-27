using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.9 Complex type value
///
///     propertyValueList = "{" *propertySlot "}"
///
///     propertySlot      = propertyName "=" propertyValue ";"
///
///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
///
///     propertyName      = IDENTIFIER
///
/// </remarks>
public sealed record PropertyValueListAst : AstNode
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.PropertySlots = new();
        }

        public List<PropertySlotAst> PropertySlots
        {
            get;
            set;
        }

        public PropertyValueListAst Build()
        {
            return new(
                this.PropertySlots
            );
        }

    }

    #endregion

    #region Constructors

    internal PropertyValueListAst()
        : this(Enumerable.Empty<PropertySlotAst>())
    {

    }

    internal PropertyValueListAst(
        IEnumerable<PropertySlotAst> propertySlots
    )
    {
        this.PropertySlots = new(
            (propertySlots ?? throw new ArgumentNullException(nameof(propertySlots))).ToList()
        );
        this.PropertyValues = new(
            (propertySlots ?? throw new ArgumentNullException(nameof(propertySlots)))
                .ToDictionary(
                    slot => slot.PropertyName.Name,
                    slot => slot.PropertyValue
                )
        );
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<PropertySlotAst> PropertySlots
    {
        get;
    }

    public Dictionary<string, PropertyValueAst> PropertyValues
    {
        get;
    }

    #endregion

}

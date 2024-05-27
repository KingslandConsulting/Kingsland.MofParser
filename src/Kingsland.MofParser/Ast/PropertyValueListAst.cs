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
            this.PropertySlots = [];
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
        : this([])
    {

    }

    internal PropertyValueListAst(
        IEnumerable<PropertySlotAst> propertySlots
    )
    {
        this.PropertySlots = (propertySlots ?? throw new ArgumentNullException(nameof(propertySlots)))
            .ToList().AsReadOnly();
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

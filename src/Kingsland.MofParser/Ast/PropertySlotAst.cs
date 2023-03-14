using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.9 Complex type value
///
///     propertySlot      = propertyName "=" propertyValue ";"
///
///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
///
///     propertyName      = IDENTIFIER
///
/// </remarks>
public sealed record PropertySlotAst : AstNode
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
        }

        public IdentifierToken? PropertyName
        {
            get;
            set;
        }

        public PropertyValueAst? PropertyValue
        {
            get;
            set;
        }

        public PropertySlotAst Build()
        {
            return new(
                this.PropertyName ?? throw new NullReferenceException(),
                this.PropertyValue ?? throw new NullReferenceException()
            );
        }

    }

    #endregion

    #region Constructors

    internal PropertySlotAst(
        IdentifierToken propertyName, PropertyValueAst propertyValue
    )
    {
        this.PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        this.PropertyValue = propertyValue ?? throw new ArgumentNullException(nameof(propertyValue));
    }

    #endregion

    #region Properties

    public IdentifierToken PropertyName
    {
        get;
    }

    public PropertyValueAst PropertyValue
    {
        get;
    }

    #endregion

}

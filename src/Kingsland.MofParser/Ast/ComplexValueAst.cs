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
///     complexValue = aliasIdentifier /
///                    ( VALUE OF
///                      ( structureName / className / associationName )
///                      propertyValueList )
///
/// </remarks>
public sealed record ComplexValueAst : ComplexTypeValueAst
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.PropertyValues = new(
                new List<PropertySlotAst>()
            );
        }

        public AliasIdentifierToken? Alias
        {
            get;
            set;
        }

        public IdentifierToken? Value
        {
            get;
            set;
        }

        public IdentifierToken? Of
        {
            get;
            set;
        }

        public IdentifierToken? TypeName
        {
            get;
            set;
        }

        public PropertyValueListAst PropertyValues
        {
            get;
            set;
        }

        public ComplexValueAst Build()
        {
            return (this.Alias == null)
                ? new(
                    this.Value ?? throw new InvalidOperationException(
                        $"{nameof(this.Value)} property must be set before calling {nameof(Build)}."
                    ),
                    this.Of ?? throw new InvalidOperationException(
                        $"{nameof(this.Of)} property must be set before calling {nameof(Build)}."
                    ),
                    this.TypeName ?? throw new InvalidOperationException(
                        $"{nameof(this.TypeName)} property must be set before calling {nameof(Build)}."
                    ),
                    this.PropertyValues
                )
                : new(
                    this.Alias
                );
        }

    }

    #endregion

    #region Constructors

    internal ComplexValueAst(
        AliasIdentifierToken alias
    )
    {
        this.Alias = alias ?? throw new ArgumentNullException(nameof(alias));
        this.Value = null;
        this.Of = null;
        this.TypeName = null;
        this.PropertyValues = new(
            Enumerable.Empty<PropertySlotAst>()
        );
    }

    public ComplexValueAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        PropertyValueListAst propertyValues
    )
    {
        this.Alias = null;
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
        this.Of = of ?? throw new ArgumentNullException(nameof(of));
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.PropertyValues = propertyValues ?? throw new ArgumentNullException(nameof(propertyValues));
    }

    #endregion

    #region Properties

    public bool IsAlias =>
        this.Alias is not null;

    public bool IsValue =>
        this.Value is not null;

    public AliasIdentifierToken? Alias
    {
        get;
    }

    public IdentifierToken? Value
    {
        get;
    }

    public IdentifierToken? Of
    {
        get;
    }

    public IdentifierToken? TypeName
    {
        get;
    }

    public PropertyValueListAst PropertyValues
    {
        get;
    }

    #endregion

}

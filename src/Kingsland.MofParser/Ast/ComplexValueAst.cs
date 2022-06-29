using Kingsland.MofParser.CodeGen;
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
            this.PropertyValues = new PropertyValueListAst();
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
                ? new ComplexValueAst(
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
                : new ComplexValueAst(
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
        this.Alias = alias;
        this.Value = null;
        this.Of = null;
        this.TypeName = null;
        this.PropertyValues = new PropertyValueListAst();
    }

    public ComplexValueAst(
        IdentifierToken value,
        IdentifierToken of,
        IdentifierToken typeName,
        PropertyValueListAst propertyValues
    )
    {
        this.Alias = null;
        this.Value = value;
        this.Of = of;
        this.TypeName = typeName;
        this.PropertyValues = propertyValues;
    }

    #endregion

    #region Properties

    public bool IsAlias
    {
        get
        {
            return !(this.Alias == null);
        }
    }

    public bool IsValue
    {
        get
        {
            return !(this.Value == null);
        }
    }

    public AliasIdentifierToken? Alias
    {
        get;
        private init;
    }

    public IdentifierToken? Value
    {
        get;
        private init;
    }

    public IdentifierToken? Of
    {
        get;
        private init;
    }

    public IdentifierToken? TypeName
    {
        get;
        private init;
    }

    public PropertyValueListAst PropertyValues
    {
        get;
        private init;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertComplexValueAst(this);
    }

    #endregion

}

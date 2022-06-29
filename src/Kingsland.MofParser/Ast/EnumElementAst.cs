using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.4 Enumeration declaration
///
///     integerEnumElement     = [ qualifierList ] enumLiteral "=" integerValue
///     stringEnumElement      = [ qualifierList ] enumLiteral [ "=" stringValue ]
///
///     enumLiteral            = IDENTIFIER
///
/// </remarks>
public sealed record EnumElementAst : IAstNode
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.QualifierList = new QualifierListAst();
        }

        public QualifierListAst QualifierList
        {
            get;
            set;
        }

        public IdentifierToken? EnumElementName
        {
            get;
            set;
        }

        public IEnumElementValueAst? EnumElementValue
        {
            get;
            set;
        }

        public EnumElementAst Build()
        {
            return new EnumElementAst(
                this.QualifierList,
                this.EnumElementName ?? throw new InvalidOperationException(
                    $"{nameof(this.EnumElementName)} property must be set before calling {nameof(Build)}."
                ),
                this.EnumElementValue
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumElementAst(
        QualifierListAst qualifierList,
        IdentifierToken enumElementName,
        IEnumElementValueAst? enumElementValue
    )
    {
        this.QualifierList = qualifierList;
        this.EnumElementName = enumElementName;
        this.EnumElementValue = enumElementValue;
    }

    #endregion

    #region Properties

    public QualifierListAst QualifierList
    {
        get;
        private init;
    }

    public IdentifierToken EnumElementName
    {
        get;
        private init;
    }

    public IEnumElementValueAst? EnumElementValue
    {
        get;
        private init;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertEnumElementAst(this);
    }

    #endregion

}

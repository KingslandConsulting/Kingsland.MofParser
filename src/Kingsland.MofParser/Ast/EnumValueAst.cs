using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.3 Enum type value
///
///     enumValue   = [ enumName "." ] enumLiteral
///
///     enumLiteral = IDENTIFIER
///
/// 7.5.4 Enumeration declaration
///
///     enumName    = elementName
///
/// </remarks>
public sealed record EnumValueAst : EnumTypeValueAst
{

    #region Builder

    public sealed class Builder
    {

        public IdentifierToken? EnumName
        {
            get;
            set;
        }

        public IdentifierToken? EnumLiteral
        {
            get;
            set;
        }

        public EnumValueAst Build()
        {
            return new EnumValueAst(
                this.EnumName,
                this.EnumLiteral ?? throw new InvalidOperationException(
                    $"{nameof(this.EnumLiteral)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal EnumValueAst(
        IdentifierToken? enumName,
        IdentifierToken enumLiteral
    )
    {
        this.EnumName = enumName;
        this.EnumLiteral = enumLiteral ?? throw new ArgumentNullException(nameof(enumLiteral));
    }

    #endregion

    #region Properties

    public IdentifierToken? EnumName
    {
        get;
    }

    public IdentifierToken EnumLiteral
    {
        get;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertEnumValueAst(this);
    }

    #endregion

}

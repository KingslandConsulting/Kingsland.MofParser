using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <returns></returns>
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
    public class EnumElementAst : IAstNode
    {

        #region Builder

        public sealed class Builder
        {

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public IdentifierToken EnumElementName
            {
                get;
                set;
            }

            public IEnumValueAst EnumElementValue
            {
                get;
                set;
            }

            public EnumElementAst Build()
            {
                return new EnumElementAst(
                    this.Qualifiers,
                    this.EnumElementName,
                    this.EnumElementValue
                );
            }

        }

        #endregion

        #region Constructors

        public EnumElementAst(QualifierListAst qualifiers, IdentifierToken enumElementName, IEnumValueAst enumElementValue)
        {
            this.Qualifiers = qualifiers ?? new QualifierListAst.Builder().Build();
            this.EnumElementName = enumElementName;
            this.EnumElementValue = enumElementValue;
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        public IdentifierToken EnumElementName
        {
            get;
            private set;
        }

        public IEnumValueAst EnumElementValue
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertEnumElementAst(this);
        }

        #endregion

    }

}

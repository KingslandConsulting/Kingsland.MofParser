using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Ast
{

    public abstract class PropertyValueAst : AstNode
    {

        #region Constructors

        internal PropertyValueAst()
        {
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
        ///
        ///     propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        ///
        /// 7.3.5
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///
        /// A.1
        ///
        ///     complexTypeValue = complexValue / complexValueArray
        ///
        /// A.19
        ///
        ///     referenceTypeValue  = referenceValue / referenceValueArray
        ///     referenceValueArray = "{" [ objectPathValue *( "," objectPathValue ) ] 1163 "}"
        ///
        /// A.7
        ///
        ///     enumTypeValue = enumValue / enumValueArray
        ///     enumDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
        ///
        /// </remarks>
        internal static PropertyValueAst Parse(Parser parser)
        {

            // primitiveTypeValue
            parser.Descend();
            var primitiveTypeValue = default(PrimitiveTypeValueAst);
            if (PrimitiveTypeValueAst.TryParse(parser, ref primitiveTypeValue, false))
            {
                parser.Commit();
                return primitiveTypeValue;
            }
            parser.Backtrack();

            // complexTypeValue
            parser.Descend();
            var complexTypeValue = default(ComplexTypeValueAst);
            if (ComplexTypeValueAst.TryParse(parser, ref complexTypeValue, false))
            {
                parser.Commit();
                return complexTypeValue;
            }
            parser.Backtrack();

            // referenceTypeValue
            parser.Descend();
            var referenceTypeValue = default(ReferenceTypeValueAst);
            if (ReferenceTypeValueAst.TryParse(parser, ref referenceTypeValue, false))
            {
                parser.Commit();
                return referenceTypeValue;
            }
            parser.Backtrack();

            // unexpected token
            throw new UnexpectedTokenException(parser.Peek());

        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}

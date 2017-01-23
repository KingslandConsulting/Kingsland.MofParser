using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class PropertyValueAst : AstNode
    {

        #region Constructors

        private PropertyValueAst()
        {
        }

        #endregion

        #region Properties

        public object Value
        {
            get;
            private set;
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
        ///     primitiveType = DT_Integer / DT_Real / DT_STRING / DT_DATETIME / DT_BOOLEAN / DT_OCTETSTRING
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
        internal static PropertyValueAst Parse(ParserStream stream)
        {
            var node = new PropertyValueAst();
            var peek = stream.Peek();
            // propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
            if (LiteralValueAst.IsLiteralValueToken(peek))
            {
                // primitiveTypeValue -> literalValue
                node.Value = PrimitiveTypeValueAst.Parse(stream);
            }
            else if (peek is BlockOpenToken)
            {
                // we need to read the subsequent token to work out whether
                // this is a complexValueArray, literalValueArray, referenceValueArray or enumValueArray
                stream.Read();
                peek = stream.Peek();
                if (LiteralValueAst.IsLiteralValueToken(peek))
                {
                    // literalValueArray
                    stream.Backtrack();
                    node.Value = LiteralValueArrayAst.Parse(stream);
                }
                else
                {
                    // complexValueType
                    stream.Backtrack();
                    node.Value = ComplexValueArrayAst.Parse(stream);
                }
            }
            else if (peek is AliasIdentifierToken)
            {
                // referenceTypeValue
                node.Value = ReferenceTypeValueAst.Parse(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
            // return the result
            return node;
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

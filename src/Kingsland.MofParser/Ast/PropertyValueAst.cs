using System;
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
        ///     complexTypeValue  = complexValue / complexValueArray
        ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
        ///     complexValue      = ( INSTANCE / VALUE ) OF
        ///                         ( structureName / className / associationName )
        ///                         [ alias ] propertyValueList ";"
        ///     propertyValueList = "{" *propertySlot "}"
        ///     propertySlot      = propertyName "=" propertyValue ";"
        ///     propertyValue     = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        ///     alias             = AS aliasIdentifier
        ///     INSTANCE          = "instance" ; keyword: case insensitive
        ///     VALUE             = "value"    ; keyword: case insensitive
        ///     AS                = "as"       ; keyword: case insensitive
        ///     OF                = "of"       ; keyword: case insensitive
        ///
        ///     propertyName      = IDENTIFIER
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
                // this is a complexValueArray or a literalValueArray
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
                node.Value = ReferenceTypeValue.Parse(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
            // return the result
            return node;
        }

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            return string.Format("!!!!!{0}!!!!!", this.GetType().Name);
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}

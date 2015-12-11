using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class ComplexValueArrayAst : ComplexTypeValueAst
    {

        private List<ComplexValueAst> _values;

        private ComplexValueArrayAst()
        {
        }

        public List<ComplexValueAst> Values
        {
            get
            {
                if (_values == null)
                {
                    _values = new List<ComplexValueAst>();
                }
                return _values;
            }
        }

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
        internal new static ComplexValueArrayAst Parse(ParserStream stream)
        {
            // complexValueArray =
            var node = new ComplexValueArrayAst();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ complexValue
            node.Values.Add(ComplexValueAst.Parse(stream));
            // *( "," complexValue) ]
            while (stream.Peek<CommaToken>() != null)
            {
                stream.Read<CommaToken>();
                node.Values.Add(ComplexValueAst.Parse(stream));
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node;
        }

    }

}

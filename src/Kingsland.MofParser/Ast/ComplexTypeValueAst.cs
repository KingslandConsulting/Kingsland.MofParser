using System;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class ComplexTypeValueAst : MofProductionAst
    {

        internal ComplexTypeValueAst()
        {
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
        ///                         [ alias ] propertyValueList
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
        internal new static ComplexTypeValueAst Parse(ParserStream stream)
        {
            var peek = stream.Peek();
            if (peek is BlockOpenToken)
            {
                // complexValueArray
                return ComplexValueArrayAst.Parse(stream);
            }
            else if (peek is IdentifierToken)
            {
                // complexValue
                return ComplexValueAst.Parse(stream);
            }
            else
            {
                throw new InvalidOperationException();
            }
        }

    }

}

using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class ComplexValueAst : ComplexTypeValueAst
    {

        private Dictionary<string, PropertyValueAst> _properties;

        private ComplexValueAst()
        {
        }

        public bool IsInstance 
        { 
            get; 
            private set;
        }

        public bool IsValue
        {
            get;
            private set;
        }

        public string TypeName
        {
            get;
            private set;
        }

        public string Alias
        {
            get;
            private set;
        }

        public Dictionary<string, PropertyValueAst> Properties
        {
            get
            {
                if (_properties == null)
                {
                    _properties = new Dictionary<string, PropertyValueAst>();
                }
                return _properties;
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
        internal new static ComplexValueAst Parse(ParserStream stream)
        {
            // complexValue =
            var node = new ComplexValueAst();
            // ( INSTANCE / VALUE )
            var keyword = stream.ReadKeyword();
            switch (keyword.Name)
            {
                case "instance":
                    node.IsInstance = true;
                    node.IsValue = false;
                    break;
                case "value":
                    node.IsInstance = false;
                    node.IsValue = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            // OF
            stream.ReadKeyword("of");
            // ( structureName / className / associationName )
            node.TypeName = stream.Read<IdentifierToken>().Name;
            // [ alias ]
            if (stream.PeekKeyword("as"))
            {
                stream.ReadKeyword("as");
                // BUGBUG - PowerShell DSC MOFs allow schema names in an alias identifier
                //node.Alias = NameValidator.ValidateAliasIdentifier("$" + stream.Read<AliasIdentifierToken>().Name);
                node.Alias = stream.Read<AliasIdentifierToken>().Name;
            }
            // propertyValueList
            stream.Read<BlockOpenToken>();
            while (!stream.Eof && (stream.Peek<BlockCloseToken>() == null))
            {
                // propertyName
                var propertyName = NameValidator.ValidateIdentifier(stream.Read<IdentifierToken>().Name);
                // "="
                stream.Read<EqualsOperatorToken>();
                // propertyValue
                var propertyValue = PropertyValueAst.Parse(stream);
                // ";"
                stream.Read<StatementEndToken>();
                node.Properties.Add(propertyName, propertyValue);
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // ";"
            stream.Read<StatementEndToken>();
            // return the result
            return node;
        }
    
    }

}

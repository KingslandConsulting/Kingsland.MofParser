using System;
using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    public sealed class ComplexValueAst : ComplexTypeValueAst
    {

        #region Fields

        private Dictionary<string, PropertyValueAst> _properties;

        #endregion

        #region Constructors

        private ComplexValueAst()
        {
        }

        #endregion

        #region Properties

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

        #endregion

        #region Parsing Properties

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
        ///
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
        internal new static ComplexValueAst Parse(Parser parser)
        {

            // complexValue =
            var node = new ComplexValueAst();

            // ( INSTANCE / VALUE )
            var keyword = parser.ReadIdentifier();
            switch (keyword.GetNormalizedName())
            {
                case Keywords.INSTANCE:
                    node.IsInstance = true;
                    node.IsValue = false;
                    break;
                case Keywords.VALUE:
                    node.IsInstance = false;
                    node.IsValue = true;
                    break;
                default:
                    throw new UnexpectedTokenException(keyword);
            }

            // OF
            parser.ReadIdentifier(Keywords.OF);

            // ( structureName / className / associationName )
            node.TypeName = parser.Read<IdentifierToken>().Name;
            if (!StringValidator.IsStructureName(node.TypeName) &&
                !StringValidator.IsClassName(node.TypeName) &&
                !StringValidator.IsAssociationName(node.TypeName))
            {
                throw new InvalidOperationException("Identifer is not a structureName, className or associationName");
            }

            // [ alias ]
            if (parser.PeekIdentifier(Keywords.AS))
            {
                parser.ReadIdentifier(Keywords.AS);
                var aliasName = parser.Read<AliasIdentifierToken>().Name;
                if (!StringValidator.IsIdentifier(aliasName))
                {
                    throw new InvalidOperationException("Value is not a valid aliasIdentifier");
                }
                node.Alias = aliasName;
            }

            // propertyValueList
            parser.Read<BlockOpenToken>();
            while (!parser.Eof && (parser.Peek<BlockCloseToken>() == null))
            {
                // propertyName
                var propertyName = parser.Read<IdentifierToken>().Name;
                if (!StringValidator.IsIdentifier(propertyName))
                {
                    throw new InvalidOperationException("Value is not a valid property name.");
                }
                // "="
                parser.Read<EqualsOperatorToken>();
                // propertyValue
                var propertyValue = PropertyValueAst.Parse(parser);
                // ";"
                parser.Read<StatementEndToken>();
                node.Properties.Add(propertyName, propertyValue);
            }

            // "}"
            parser.Read<BlockCloseToken>();

            // ";"
            parser.Read<StatementEndToken>();

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

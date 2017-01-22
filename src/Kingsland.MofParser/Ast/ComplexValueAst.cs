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
        ///     alias             = AS aliasIdentifier
        ///     INSTANCE          = "instance" ; keyword: case insensitive
        ///     VALUE             = "value"    ; keyword: case insensitive
        ///     AS                = "as"       ; keyword: case insensitive
        ///     OF                = "of"       ; keyword: case insensitive
        ///
        ///     propertyName      = IDENTIFIER
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref ComplexValueAst node, bool throwIfError = false)
        {

            // ( INSTANCE / VALUE )
            var objectType = default(IdentifierToken);
            if (!parser.TryRead(ref objectType))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }
            switch (objectType.GetNormalizedName())
            {
                case Keywords.INSTANCE:
                case Keywords.VALUE:
                    break;
                default:
                    return AstNode.HandleUnexpectedToken(objectType, throwIfError);
            }

            // OF
            var of = default(IdentifierToken);
            if (!parser.TryReadIdentifier(Keywords.OF, ref of))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // ( structureName / className / associationName )
            var typeName = default(IdentifierToken);
            if (!parser.TryRead(ref typeName))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }
            if (!StringValidator.IsStructureName(typeName.Name) &&
                !StringValidator.IsClassName(typeName.Name) &&
                !StringValidator.IsAssociationName(typeName.Name))
            {
                //throw new InvalidOperationException("Identifer is not a structureName, className or associationName");
                return AstNode.HandleUnexpectedToken(objectType, throwIfError);
            }

            // [ alias ]
            var alias = default(IdentifierToken);
            var aliasName = default(AliasIdentifierToken);
            if (parser.TryReadIdentifier(Keywords.AS, ref alias))
            {
                if (!parser.TryRead(ref aliasName))
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }
                if (!StringValidator.IsIdentifier(aliasName.Name))
                {
                    //throw new InvalidOperationException("Value is not a valid aliasIdentifier");
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }
            }

            // propertyValueList = 
            var properties = new Dictionary<string, PropertyValueAst>();

            // "{"
            var blockOpen = default(BlockOpenToken);
            if (!parser.TryRead(ref blockOpen))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // *propertySlot
            while (!parser.Eof && (parser.Peek<BlockCloseToken>() == null))
            {

                // propertyName
                var propertyName = parser.Read<IdentifierToken>().Name;
                if (!StringValidator.IsIdentifier(propertyName))
                {
                    throw new InvalidOperationException("Value is not a valid property name.");
                }

                // "="
                var equalsOperator = default(EqualsOperatorToken);
                if (!parser.TryRead(ref equalsOperator))
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }

                // propertyValue
                var propertyValue = PropertyValueAst.Parse(parser);

                // ";"
                var proeprtyStatementEnd = default(StatementEndToken);
                if (!parser.TryRead(ref proeprtyStatementEnd))
                {
                    return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
                }

                properties.Add(propertyName, propertyValue);

            }

            // "}"
            var blockClose = default(BlockCloseToken);
            if (!parser.TryRead(ref blockClose))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // ";"
            var statementEnd = default(StatementEndToken);
            if (!parser.TryRead(ref statementEnd))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // build the result
            var result = new ComplexValueAst();
            switch (objectType.GetNormalizedName())
            {
                case Keywords.INSTANCE:
                    result.IsInstance = true;
                    result.IsValue = false;
                    break;
                case Keywords.VALUE:
                    result.IsInstance = false;
                    result.IsValue = true;
                    break;
                default:
                    throw new InvalidOperationException();
            }
            result.TypeName = typeName.Name;
            result.Alias = (aliasName == null) ? null : aliasName.Name;
            foreach (var item in properties)
            {
                result.Properties.Add(item.Key, item.Value);
            }

            // return the result
            node = result;
            return true;

        }

        internal new static ComplexValueAst Parse(Parser parser)
        {
            var node = default(ComplexValueAst);
            ComplexValueAst.TryParse(parser, ref node, true);
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

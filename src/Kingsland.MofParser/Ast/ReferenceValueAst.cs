using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class ReferenceValueAst : ReferenceTypeValueAst
    {

        #region Constructors

        private ReferenceValueAst()
        {
        }

        #endregion

        #region Properties

        public string Name
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.19 Reference type value
        ///
        ///     referenceTypeValue  = referenceValue / referenceValueArray
        ///     referenceValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
        ///
        /// No whitespace is allowed between the elements of the rules in this ABNF section.
        ///
        ///     objectPathValue = [namespacePath ":"] instanceId
        ///     namespacePath   = [serverPath] namespaceName
        ///
        /// ; Note: The production rules for host and port are defined in IETF
        /// ; RFC 3986 (Uniform Resource Identifiers (URI): Generic Syntax).
        ///
        ///     serverPath       = (host / LOCALHOST) [ ":" port] "/"
        ///     LOCALHOST        = "localhost" ; Case insensitive
        ///     instanceId       = className "." instanceKeyValue
        ///     instanceKeyValue = keyValue *( "," keyValue )
        ///     keyValue         = propertyName "=" literalValue
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref ReferenceValueAst node, bool throwIfError = false)
        {

            // objectPathValue
            var objectPathValue = default(AliasIdentifierToken);
            if (!parser.TryRead(ref objectPathValue))
            {
                return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
            }

            // build the result
            var result = new ReferenceValueAst();
            result.Name = objectPathValue.Name;

            // return the result
            node = result;
            return true;

        }

        internal new static ReferenceValueAst Parse(Parser parser)
        {
            var node = default(ReferenceValueAst);
            ReferenceValueAst.TryParse(parser, ref node, true);
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

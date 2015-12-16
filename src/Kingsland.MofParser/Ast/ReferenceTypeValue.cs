using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class ReferenceTypeValue : AstNode
    {

        #region Constructors

        private ReferenceTypeValue()
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
        internal static ReferenceTypeValue Parse(ParserStream stream)
        {
            var node = new ReferenceTypeValue();
            // referenceValue = objectPathValue
            node.Name = stream.Read<AliasIdentifierToken>().Name;
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

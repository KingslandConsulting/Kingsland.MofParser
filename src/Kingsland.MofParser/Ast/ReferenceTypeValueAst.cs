using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.4 Reference type value
    ///
    /// Whitespace as defined in 5.2 is allowed between the elements of the rules in this ABNF section.
    ///
    ///     referenceTypeValue   = objectPathValue / objectPathValueArray
    ///     objectPathValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
    ///                            "}"
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     ; Note: objectPathValues are URLs and shall conform to RFC 3986 (Uniform
    ///     ; Resource Identifiers(URI): Generic Syntax) and to the following ABNF.
    ///
    ///     objectPathValue  = [namespacePath ":"] instanceId
    ///     namespacePath    = [serverPath] namespaceName
    ///
    ///     ; Note: The production rules for host and port are defined in IETF
    ///     ; RFC 3986 (Uniform Resource Identifiers (URI): Generic Syntax).
    ///
    ///     serverPath       = (host / LOCALHOST) [ ":" port] "/"
    ///     LOCALHOST        = "localhost" ; Case insensitive
    ///     instanceId       = className "." instanceKeyValue
    ///     instanceKeyValue = keyValue *( "," keyValue )
    ///     keyValue         = propertyName "=" literalValue
    ///
    /// </remarks>
    public sealed class ReferenceTypeValueAst : PropertyValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public string Name
            {
                get;
                set;
            }

            public ReferenceTypeValueAst Build()
            {
                return new ReferenceTypeValueAst(
                    this.Name
                );
            }

        }

        #endregion

        #region Constructors

        public ReferenceTypeValueAst(string name)
        {
            this.Name = name;
        }

        #endregion

        #region Properties

        public string Name
        {
            get;
            private set;
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

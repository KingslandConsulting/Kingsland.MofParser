using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///
    /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
    ///
    /// A.17.6 Boolean value
    ///
    ///     booleanValue = TRUE / FALSE
    ///
    ///     FALSE        = "false" ; keyword: case insensitive
    ///     TRUE         = "true"  ; keyword: case insensitive
    ///
    /// </remarks>
    public sealed class BooleanLiteralToken : Token
    {

        internal BooleanLiteralToken(SourceExtent extent, bool value)
            : base(extent)
        {
            this.Value = value;
        }

        public bool Value
        {
            get;
            private set;
        }

    }

}

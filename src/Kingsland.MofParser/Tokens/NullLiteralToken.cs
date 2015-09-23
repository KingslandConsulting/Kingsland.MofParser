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
    /// A.17.7 Null value
    ///
    ///     nullValue = NULL
    ///
    ///     NULL = "null" ; keyword: case insensitive
    ///                   ; second
    ///
    /// </remarks>
    public sealed class NullLiteralToken : Token
    {

        internal NullLiteralToken(SourceExtent extent)
            : base(extent)
        {
        }

    }

}

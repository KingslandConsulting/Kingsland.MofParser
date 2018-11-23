using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class WhitespaceToken : Token
    {

        #region Constructors

        internal WhitespaceToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}

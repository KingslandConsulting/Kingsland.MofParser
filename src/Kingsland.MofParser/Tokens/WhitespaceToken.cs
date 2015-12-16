using Kingsland.MofParser.Lexing;

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

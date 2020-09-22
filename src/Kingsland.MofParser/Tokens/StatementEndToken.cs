using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StatementEndToken : SyntaxToken
    {

        #region Constructors

        public StatementEndToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

    }

}

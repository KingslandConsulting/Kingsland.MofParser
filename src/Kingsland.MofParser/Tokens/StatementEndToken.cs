using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StatementEndToken : SyntaxToken
    {

        #region Constructors

        public StatementEndToken()
            : this(SourceExtent.Empty)
        {
        }

        public StatementEndToken(SourcePosition start, SourcePosition end, string text)
            : this(new SourceExtent(start, end, text))
        {
        }

        public StatementEndToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? ";";
        }

        #endregion

    }

}

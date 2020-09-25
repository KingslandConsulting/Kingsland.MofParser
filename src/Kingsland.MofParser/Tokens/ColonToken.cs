using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class ColonToken : SyntaxToken
    {

        #region Constructors

        public ColonToken()
            : this(SourceExtent.Empty)
        {
        }

        public ColonToken(SourcePosition start, SourcePosition end, string text)
            : this(new SourceExtent(start, end, text))
        {
        }

        public ColonToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? ":";
        }

        #endregion

    }

}

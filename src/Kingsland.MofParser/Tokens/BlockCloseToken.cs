using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BlockCloseToken : SyntaxToken
    {

        #region Constructors

        public BlockCloseToken()
            : this(SourceExtent.Empty)
        {
        }

        public BlockCloseToken(SourcePosition start, SourcePosition end, string text)
            : this(new SourceExtent(start, end, text))
        {

        }

        public BlockCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return (this.Extent != SourceExtent.Empty) ?
                this.Extent.Text :
                "}";
        }

        #endregion

    }

}

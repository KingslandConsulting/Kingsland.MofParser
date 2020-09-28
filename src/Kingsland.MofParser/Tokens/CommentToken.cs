using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class CommentToken : SyntaxToken
    {

        #region Constructors

        public CommentToken(string value)
             : this(SourceExtent.Empty, value)
        {
        }

        public CommentToken(SourcePosition start, SourcePosition end, string text)
             : this(new SourceExtent(start, end, text), text)
        {
        }

        public CommentToken(SourceExtent extent, string value)
            : base(extent)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public string Value
        {
            get;
            private set;
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return (this.Extent != SourceExtent.Empty) ?
                this.Extent.Text :
                this.Value;
        }

        #endregion

    }

}

using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class RealLiteralToken : SyntaxToken
    {

        #region Constructors

        public RealLiteralToken(double value)
            : this(SourceExtent.Empty, value)
        {
        }

        public RealLiteralToken(SourcePosition start, SourcePosition end, string text, double value)
            : this(new SourceExtent(start, end, text), value)
        {
        }

        public RealLiteralToken(SourceExtent extent, double value)
            : base(extent)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public double Value
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
                this.Value.ToString();
        }

        #endregion

    }

}

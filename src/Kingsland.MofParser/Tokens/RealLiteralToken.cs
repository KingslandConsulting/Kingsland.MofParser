using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class RealLiteralToken : SyntaxToken
    {

        #region Constructors

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

    }

}

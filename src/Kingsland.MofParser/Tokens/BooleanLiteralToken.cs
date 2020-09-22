using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : SyntaxToken
    {

        #region Constructors

        public BooleanLiteralToken(SourceExtent extent, bool value)
            : base(extent)
        {
            this.Value = value;
        }

        #endregion

        #region Properties

        public bool Value
        {
            get;
            private set;
        }

        #endregion

    }

}

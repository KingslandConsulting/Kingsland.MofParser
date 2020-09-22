using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : SyntaxToken
    {

        #region Constructors

        public IntegerLiteralToken(SourceExtent extent, IntegerKind kind, long value)
            : base(extent)
        {
            this.Kind = kind;
            this.Value = value;
        }

        #endregion

        #region Properties

        public IntegerKind Kind
        {
            get;
            private set;
        }

        public long Value
        {
            get;
            private set;
        }

        #endregion

    }

}

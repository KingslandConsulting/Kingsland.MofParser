using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class AliasIdentifierToken : SyntaxToken
    {

        #region Constructors

        public AliasIdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        #endregion

        #region Properties

        public string Name
        {
            get;
            private set;
        }

        #endregion

    }

}

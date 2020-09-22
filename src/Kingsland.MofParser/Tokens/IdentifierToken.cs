using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IdentifierToken : SyntaxToken
    {

        #region Constructors

        public IdentifierToken(SourceExtent extent, string name)
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

        #region Methods

        public string GetNormalizedName()
        {
            var name = this.Name;
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            return name.ToLowerInvariant();
        }

        #endregion

    }

}

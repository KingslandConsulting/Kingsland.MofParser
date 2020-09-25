using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class AliasIdentifierToken : SyntaxToken
    {

        public AliasIdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? $"${this.Name}";
        }

        #endregion

    }

}

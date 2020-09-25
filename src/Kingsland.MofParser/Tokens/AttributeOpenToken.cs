using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class AttributeOpenToken : SyntaxToken
    {

        public AttributeOpenToken(SourceExtent extent)
            : base(extent)
        {
        }

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? "[";
        }

        #endregion

    }

}

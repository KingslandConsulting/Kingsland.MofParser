using Kingsland.ParseFx.Text;

namespace Kingsland.ParseFx.Syntax
{

    public abstract class SyntaxToken
    {

        protected SyntaxToken(SourceExtent extent)
        {
            this.Extent = extent;
        }

        public SourceExtent Extent
        {
            get;
            private set;
        }

        public override string ToString()
        {
            return this.Extent.Text;
        }

    }

}

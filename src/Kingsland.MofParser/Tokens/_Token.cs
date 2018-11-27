using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public abstract class Token
    {

        internal Token(SourceExtent extent)
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

using Kingsland.ParseFx.Text;
using System;

namespace Kingsland.ParseFx.Syntax
{

    public abstract class SyntaxToken
    {

        protected SyntaxToken(SourceExtent extent)
        {
            if (extent == null)
            {
                throw new ArgumentNullException(nameof(extent));
            }
            this.Extent = extent;
        }

        public SourceExtent Extent
        {
            get;
            private set;
        }

        public string Text
        {
            get
            {
                return this.Extent.Text;
            }
        }

        public override string ToString()
        {
            return this.Text;
        }

    }

}

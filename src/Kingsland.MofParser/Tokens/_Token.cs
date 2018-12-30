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

        public bool IsEqualTo(Token obj)
        {
            return object.ReferenceEquals(obj, this) ||
                   ((obj != null) &&
                    (obj.GetType() == this.GetType()) &&
                    obj.Extent.IsEqualTo(this.Extent));
        }

        public override string ToString()
        {
            return this.Extent.Text;
        }

    }

}

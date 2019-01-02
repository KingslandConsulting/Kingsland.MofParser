using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class RealLiteralToken : Token
    {

        public RealLiteralToken(SourceExtent extent, double value)
            : base(extent)
        {
            this.Value = value;
        }

        public double Value
        {
            get;
            private set;
        }

        public static bool AreEqual(RealLiteralToken obj1, RealLiteralToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if ((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return obj1.Extent.IsEqualTo(obj2.Extent) &&
                       (obj1.Value == obj2.Value);
            }
        }

    }

}

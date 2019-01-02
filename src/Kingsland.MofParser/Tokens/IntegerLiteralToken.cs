using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : Token
    {

        public IntegerLiteralToken(SourceExtent extent, long value)
            : base(extent)
        {
            this.Value = value;
        }

        public long Value
        {
            get;
            private set;
        }

        public static bool AreEqual(IntegerLiteralToken obj1, IntegerLiteralToken obj2)
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

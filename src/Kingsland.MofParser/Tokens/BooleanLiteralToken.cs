using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class BooleanLiteralToken : Token
    {

        public BooleanLiteralToken(SourceExtent extent, bool value)
            : base(extent)
        {
            this.Value = value;
        }

        public bool Value
        {
            get;
            private set;
        }

        public static bool AreEqual(BooleanLiteralToken obj1, BooleanLiteralToken obj2)
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

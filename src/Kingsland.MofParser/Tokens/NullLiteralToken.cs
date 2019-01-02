using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class NullLiteralToken : Token
    {

        public NullLiteralToken(SourceExtent extent)
            : base(extent)
        {
        }

        public static bool AreEqual(NullLiteralToken obj1, NullLiteralToken obj2)
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
                return obj1.Extent.IsEqualTo(obj2.Extent);
            }
        }

    }

}

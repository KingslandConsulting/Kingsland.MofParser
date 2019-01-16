using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class ParenthesisCloseToken : Token
    {

        public ParenthesisCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

        public static bool AreEqual(ParenthesisCloseToken obj1, ParenthesisCloseToken obj2)
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

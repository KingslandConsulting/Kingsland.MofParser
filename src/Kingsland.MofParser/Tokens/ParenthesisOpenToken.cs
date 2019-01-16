using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class ParenthesisOpenToken : Token
    {

        public ParenthesisOpenToken(SourceExtent extent)
            : base(extent)
        {
        }

        public static bool AreEqual(ParenthesisOpenToken obj1, ParenthesisOpenToken obj2)
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

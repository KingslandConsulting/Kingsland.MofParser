using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class PragmaToken : Token
    {

        public PragmaToken(SourceExtent extent)
            : base(extent)
        {
        }

        public static bool AreEqual(PragmaToken obj1, PragmaToken obj2)
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

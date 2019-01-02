using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class AliasIdentifierToken : Token
    {

        public AliasIdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public static bool AreEqual(AliasIdentifierToken obj1, AliasIdentifierToken obj2)
        {
            if ((obj1 == null) && (obj2 == null))
            {
                return true;
            }
            else if((obj1 == null) || (obj2 == null))
            {
                return false;
            }
            else
            {
                return obj1.Extent.IsEqualTo(obj2.Extent) &&
                       (obj1.Name == obj2.Name);
            }
        }

    }

}

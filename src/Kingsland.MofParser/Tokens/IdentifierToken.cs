using Kingsland.MofParser.Source;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IdentifierToken : Token
    {

        public IdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        public string Name
        {
            get;
            private set;
        }

        public string GetNormalizedName()
        {
            var name = this.Name;
            if(string.IsNullOrEmpty(name))
            {
                return name;
            }
            return name.ToLowerInvariant();
        }


    }

}

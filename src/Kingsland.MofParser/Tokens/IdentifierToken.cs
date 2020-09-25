using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IdentifierToken : SyntaxToken
    {

        #region Constructors

        public IdentifierToken(string name)
            : this(SourceExtent.Empty, name)
        {
        }

        public IdentifierToken(SourcePosition start, SourcePosition end, string text)
            : this(new SourceExtent(start, end, text), text)
        {
        }

        public IdentifierToken(SourceExtent extent, string name)
            : base(extent)
        {
            this.Name = name;
        }

        #endregion

        #region Properties

        public string Name
        {
            get;
            private set;
        }

        #endregion

        #region SyntaxToken Interface

        public override string GetSourceString()
        {
            return this?.Extent.Text ?? this.Name;
        }

        #endregion

        #region Helpers

        public string GetNormalizedName()
        {
            var name = this.Name;
            if (string.IsNullOrEmpty(name))
            {
                return name;
            }
            return name.ToLowerInvariant();
        }

        #endregion

    }

}

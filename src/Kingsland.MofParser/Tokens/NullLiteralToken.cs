using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class NullLiteralToken : Token
    {

        public const string NullText = "null";

        internal NullLiteralToken(SourceExtent extent)
			: base(extent)
		{
		}

	}

}

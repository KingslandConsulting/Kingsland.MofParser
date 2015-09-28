using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class PragmaToken : Token
    {

        internal PragmaToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static PragmaToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            sourceChars.AddRange(stream.ReadString("#pragma"));
            var extent = new SourceExtent(sourceChars);
            return new PragmaToken(extent);
        }

    }

}

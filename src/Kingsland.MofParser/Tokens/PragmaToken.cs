using System.Collections.Generic;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;

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
            sourceChars.AddRange(stream.ReadString(Keywords.PRAGMA));
            var extent = new SourceExtent(sourceChars);
            return new PragmaToken(extent);
        }

    }

}

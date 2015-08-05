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
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            sourceChars.Add(stream.ReadChar('#'));
            sourceChars.AddRange(stream.ReadString("pragma"));
            extent = extent.WithText(sourceChars);
            return new PragmaToken(extent);
        }
    }
}

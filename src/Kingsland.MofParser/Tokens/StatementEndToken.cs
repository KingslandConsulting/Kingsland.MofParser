using Kingsland.MofParser.Lexing;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    public sealed class StatementEndToken : Token
    {

        internal StatementEndToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static StatementEndToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the character
            sourceChars.Add(stream.ReadChar(';'));
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new StatementEndToken(extent);
        }

    }

}

using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class OpenParenthesesToken : Token
    {

        internal OpenParenthesesToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static CloseParenthesesToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the character
            sourceChars.Add(stream.ReadChar('('));
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new CloseParenthesesToken(extent);
        }

    }

}

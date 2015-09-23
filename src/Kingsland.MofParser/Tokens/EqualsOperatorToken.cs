using Kingsland.MofParser.Lexing;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    public sealed class EqualsOperatorToken : Token
    {

        internal EqualsOperatorToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static EqualsOperatorToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the character
            sourceChars.Add(stream.ReadChar('='));
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new EqualsOperatorToken(extent);
        }

    }

}

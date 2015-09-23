using Kingsland.MofParser.Lexing;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    public sealed class CommaToken : Token
    {

        internal CommaToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static CommaToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the character
            sourceChars.Add(stream.ReadChar(','));
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new CommaToken(extent);
        }

    }

}

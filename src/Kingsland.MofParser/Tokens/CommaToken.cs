using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

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
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the character
            sourceChars.Add(stream.ReadChar(','));
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new CommaToken(extent);
        }

    }

}

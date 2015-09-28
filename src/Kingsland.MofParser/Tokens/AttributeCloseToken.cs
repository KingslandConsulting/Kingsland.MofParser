using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class AttributeCloseToken : Token
    {

        internal AttributeCloseToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static AttributeCloseToken Read(ILexerStream stream)
        {
            var sourceChars = new List<SourceChar>();
            // read the character
            sourceChars.Add(stream.ReadChar(']'));
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new AttributeCloseToken(extent);
        }

    }

}

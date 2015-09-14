using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class MultilineCommentToken : Token
    {

        internal MultilineCommentToken(SourceExtent extent)
            : base(extent)
        {
        }

        internal static MultilineCommentToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the opening sequence
            sourceChars.Add(stream.ReadChar('/'));
            sourceChars.Add(stream.ReadChar('*'));
            // read the comment text
            while (!stream.Eof && !stream.PeekChar('*'))
            {
                sourceChars.Add(stream.Read());
            };
            // read the closing  sequence
            sourceChars.Add(stream.ReadChar('*'));
            sourceChars.Add(stream.ReadChar('/'));
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new MultilineCommentToken(extent);
        }

    }

}

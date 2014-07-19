using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class WhitespaceToken : Token
    {

        internal WhitespaceToken(SourceExtent extent)
            : base(extent)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// 5.2 - Whitespace
        /// 
        /// Whitespace in a MOF file is any combination of the following characters:
        /// 
        ///     Space (U+0020),
        ///     Horizontal Tab (U+0009),
        ///     Carriage Return (U+000D) and
        ///     Line Feed (U+000A).
        /// 
        /// </remarks>
        internal static WhitespaceToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the first whitespace character
            sourceChars.Add(stream.ReadWhitespace());
            // read the remaining whitespace
            while (!stream.Eof && char.IsWhiteSpace(stream.Peek()))
            {
                sourceChars.Add(stream.Read());
            }
            // return the result
            extent = extent.WithText(sourceChars);
            return new WhitespaceToken(extent);
        }

    }

}

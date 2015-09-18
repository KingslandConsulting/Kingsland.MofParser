using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Tokens
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    ///
    /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
    ///
    /// 5.2 - Whitespace
    ///
    /// Whitespace in a MOF file is any combination of the following characters:
    ///
    ///     Space (U+0020),
    ///     Horizontal Tab (U+0009),
    ///     Carriage Return (U+000D) and
    ///     Line Feed (U+000A).
    ///
    /// The WS ABNF rule represents any one of these whitespace characters:
    ///
    ///     WS = U+0020 / U+0009 / U+000D / U+000A
    ///
    /// </remarks>
    public sealed class WhitespaceToken : Token
    {

        #region Constructors

        internal WhitespaceToken(SourceExtent extent)
            : base(extent)
        {
        }

        #endregion

        internal static WhitespaceToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the first whitespace character
            sourceChars.Add(stream.ReadWhitespace());
            // read the remaining whitespace
            while (!stream.Eof && StringValidator.IsWhitespace(stream.Peek()))
            {
                sourceChars.Add(stream.Read());
            }
            // return the result
            extent = extent.WithText(sourceChars).WithEndExtent(stream);
            return new WhitespaceToken(extent);
        }

    }

}

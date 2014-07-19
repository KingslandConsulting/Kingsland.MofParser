using System;
using System.Collections.Generic;
using Kingsland.MofParser.Lexing;

namespace Kingsland.MofParser.Tokens
{

    public sealed class IntegerLiteralToken : Token
    {

        internal IntegerLiteralToken(SourceExtent extent, int value)
            : base(extent)
        {
            this.Value = value;
        }

        public int Value
        {
            get;
            private set;
        }

        internal static IntegerLiteralToken Read(ILexerStream stream)
        {
            var extent = new SourceExtent(stream);
            var sourceChars = new List<char>();
            // read the first character
            sourceChars.Add(stream.ReadDigit());
            // read the remaining characters
            while (stream.PeekDigit())
            {
                sourceChars.Add(stream.ReadDigit());
            }
            // return the result
            extent = extent.WithText(sourceChars);
            return new IntegerLiteralToken(extent, int.Parse(extent.Text));
        }

    }

}

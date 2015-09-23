using Kingsland.MofParser.Lexing;
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
    /// A.17.1 Integer value
    ///
    /// No whitespace is allowed between the elements of the rules in this ABNF section.
    ///
    ///     integerValue         = binaryValue / octalValue / hexValue / decimalValue
    ///
    ///     binaryValue          = [ "+" / "-" ] 1*binaryDigit ( "b" / "B" )
    ///     binaryDigit          = "0" / "1"
    ///
    ///     octalValue           = [ "+" / "-" ] unsignedOctalValue
    ///     unsignedOctalValue   = "0" 1*octalDigit
    ///     octalDigit           = "0" / "1" / "2" / "3" / "4" / "5" / "6" / "7"
    ///
    ///     hexValue             = [ "+" / "-" ] ( "0x" / "0X" ) 1*hexDigit
    ///     hexDigit             = decimalDigit / "a" / "A" / "b" / "B" / "c" / "C" /
    ///                                           "d" / "D" / "e" / "E" / "f" / "F"
    ///
    ///     decimalValue         = [ "+" / "-" ] unsignedDecimalValue
    ///     unsignedDecimalValue = positiveDecimalDigit *decimalDigit
    ///     decimalDigit         = "0" / positiveDecimalDigit
    ///     positiveDecimalDigit = "1"..."9"
    ///
    /// </remarks>
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
            // BUGBUG - this method is woefully underimplemented!
            var sourceChars = new List<SourceChar>();
            // read the first character
            sourceChars.Add(stream.ReadDigit());
            // read the remaining characters
            while (!stream.Eof && stream.PeekDigit())
            {
                sourceChars.Add(stream.ReadDigit());
            }
            // return the result
            var extent = new SourceExtent(sourceChars);
            return new IntegerLiteralToken(extent, int.Parse(extent.Text));
        }

    }

}

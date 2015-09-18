using System.Linq;

namespace Kingsland.MofParser.Parsing
{

    public sealed class StringValidator
    {

        #region 5.2 Whiteaspace

        #region WS = U+0020 / U+0009 / U+000D / U+000A

        internal static readonly char[] WhitespaceChars = new[] { '\u0020', '\u0009', '\u000D', '\u000A' };

        public static bool IsWhitespace(char @char)
        {
            return StringValidator.WhitespaceChars.Contains(@char);
        }

        #endregion

        #endregion

        #region 5.3 Line termination

        internal static readonly char[] LineTerminatorChars = new[] { '\u000D', '\u000A' };

        public static bool IsLineTerminator(char @char)
        {
            return StringValidator.LineTerminatorChars.Contains(@char);
        }

        #endregion

        #region A.13 Names

        #region IDENTIFIER = firstIdentifierChar *( nextIdentifierChar )

        public static bool IsIdentifier(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   StringValidator.IsFirstIdentifierChar(value.First()) &&
                   value.Skip(1).All(StringValidator.IsNextIdentifierChar);
        }

        #endregion

        #region firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE

        public static bool IsFirstIdentifierChar(char value)
        {
            return StringValidator.IsUpperAlpha(value) ||
                   StringValidator.IsLowerAlpha(value) ||
                   StringValidator.IsUnderscore(value);
        }

        #endregion

        #region nextIdentifierChar = firstIdentifierChar / decimalDigit

        public static bool IsNextIdentifierChar(char value)
        {
            return StringValidator.IsFirstIdentifierChar(value) ||
                   StringValidator.IsDecimalDigit(value);
        }

        #endregion

        // elementName = localName / schemaQualifiedName

        // localName = IDENTIFIER

        #endregion

        #region A.17.2 Real value

        // No whitespace is allowed between the elements of the rules in this ABNF section.

        // realValue = ["+" / "-"] * decimalDigit "." 1*decimalDigit
        //             [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]

        #region decimalDigit = "0" / positiveDecimalDigit

        public static bool IsDecimalDigit(char value)
        {
            return (value == '0') || StringValidator.IsPositiveDecimalDigit(value);
        }

        #endregion

        #region positiveDecimalDigit = "1"..."9"

        public static bool IsPositiveDecimalDigit(char value)
        {
            return (value >= '1') && (value <= '9');
        }

        #endregion

        #endregion

        #region A.17.4 Special characters

        // The following special characters are used in other ABNF rules in this specification:

        #region BACKSLASH = U+005C ; \ 

        public static readonly char Backslash = '\u005C';

        public static bool IsBackslash(char @char)
        {
            return (@char == StringValidator.Backslash);
        }

        #endregion

        #region DOUBLEQUOTE = U+0022 ; "

        public static readonly char DoubleQuote = '\u0022';

        public static bool IsDoubleQuote(char @char)
        {
            return (@char == StringValidator.DoubleQuote);
        }

        #endregion

        #region SINGLEQUOTE = U+0027 ; '

        public static readonly char SingleQuote = '\u0027';

        public static bool IsSingleQuote(char @char)
        {
            return (@char == StringValidator.SingleQuote);
        }

        #endregion

        #region UPPERALPHA = U+0041...U+005A ; A ... Z

        public static bool IsUpperAlpha(char @char)
        {
            return (@char >= '\u0041') && (@char <= '\u005A');
        }

        #endregion

        #region LOWERALPHA = U+0061...U+007A ; a ... z

        public static bool IsLowerAlpha(char @char)
        {
            return (@char >= '\u0061') && (@char <= '\u007A');
        }

        #endregion

        #region UNDERSCORE = U+005F ; _

        public static readonly char Underscore = '\u005F';

        public static bool IsUnderscore(char @char)
        {
            return (@char == StringValidator.Underscore);
        }

        #endregion

        #endregion

    }

}

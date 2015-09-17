namespace Kingsland.MofParser.Parsing
{

    public sealed class StringValidator
    {
        
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

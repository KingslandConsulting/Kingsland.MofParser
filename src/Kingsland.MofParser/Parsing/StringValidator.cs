using System;
using System.Linq;

namespace Kingsland.MofParser.Parsing
{

    public sealed class StringValidator
    {

        #region 5.2 Whitespace

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

        #region 7.3.2 Structure declaration

        #region structureName = elementName

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// structureName = ( IDENTIFIER / schemaQualifiedName )
        /// </remarks>
        public static bool IsStructureName(string value)
        {
            return StringValidator.IsElementName(value);
        }

        #endregion

        #endregion

        #region 7.3.3 Class declaration

        #region className = elementName

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// className = schemaQualifiedName
        /// </remarks>
        public static bool IsClassName(string value)
        {
            return StringValidator.IsElementName(value);
        }

        #endregion

        #endregion

        #region 7.3.4 Association declaration

        #region associationName = elementName

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// associationName = schemaQualifiedName
        /// </remarks>
        public static bool IsAssociationName(string value)
        {
            return StringValidator.IsElementName(value);
        }

        #endregion

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

        #region elementName = localName / schemaQualifiedName

        public static bool IsElementName(string value)
        {
            return StringValidator.IsLocalName(value) ||
                   StringValidator.IsSchemaQualifiedName(value);
        }

        #endregion

        #region localName = IDENTIFIER

        public static bool IsLocalName(string value)
        {
            return StringValidator.IsIdentifier(value);
        }

        #endregion

        #endregion

        #region A.13.1 Schema-qualified name

        #region schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER

        public static bool IsSchemaQualifiedName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            var underscore = value.IndexOf(StringValidator.Underscore);
            if ((underscore < 1) || (underscore == value.Length - 1))
            {
                return false;
            }
            return StringValidator.IsSchemaName(value.Substring(0, underscore)) &&
                   StringValidator.IsIdentifier(value.Substring(underscore + 1));
        }

        #endregion

        #region schemaName = firstSchemaChar *( nextSchemaChar )

        public static bool IsSchemaName(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   StringValidator.IsFirstSchemaChar(value[0]) &&
                   value.Skip(1).All(StringValidator.IsNextSchemaChar);
        }

        #endregion

        #region firstSchemaChar = UPPERALPHA / LOWERALPHA

        public static bool IsFirstSchemaChar(char value)
        {
            return StringValidator.IsUpperAlpha(value) ||
                   StringValidator.IsLowerAlpha(value);
        }

        #endregion

        #region nextSchemaChar = firstSchemaChar / decimalDigit

        public static bool IsNextSchemaChar(char value)
        {
            return StringValidator.IsFirstSchemaChar(value) ||
                   StringValidator.IsDecimalDigit(value);
        }

        #endregion

        #endregion

        #region A.13.2 Alias identifier

        #region aliasIdentifier = "$" IDENTIFIER

        public static bool IsAliasIdentifier(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   (value.First() == '$') &&
                   StringValidator.IsIdentifier(value.Substring(1));
        }

        #endregion

        #endregion

        #region A.17.1 Integer value

        #region decimalValue = [ "+" / "-" ] unsignedDecimalValue

        public static bool IsDecimalValue(string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }
            var chars = value.AsEnumerable();
            if (new[] { '+', '-' }.Contains(chars.First()))
            {
                chars = chars.Skip(1);
            }
            return StringValidator.IsPositiveDecimalDigit(chars.First()) &&
                   chars.Skip(1).All(StringValidator.IsDecimalDigit);
        }

        #endregion

        #region  unsignedDecimalValue = positiveDecimalDigit *decimalDigit

        public static bool IsUnsignedDecimalValue(string value)
        {
            if (string.IsNullOrEmpty(value)) { return false; }
            return StringValidator.IsPositiveDecimalDigit(value.First()) &&
                   value.Skip(1).All(StringValidator.IsDecimalDigit);
        }

        #endregion

        #endregion

        #region A.17.2 Real value

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

        #region A.17.6 Boolean value

        #region FALSE = "false" ; keyword: case insensitive

        public static bool IsFalse(string value)
        {
            return string.Equals(value, Keywords.FALSE, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #region TRUE = "true" ; keyword: case insensitive

        public static bool IsTrue(string value)
        {
            return string.Equals(value, Keywords.TRUE, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #endregion

        #region A.17.7 Null value

        #region NULL = "null" ; keyword: case insensitive

        public static bool IsNull(string value)
        {
            return string.Equals(value, Keywords.NULL, StringComparison.OrdinalIgnoreCase);
        }

        #endregion

        #endregion

        public static bool IsSpecialName(string name)
        {
            return name.StartsWith("__") && StringValidator.IsIdentifier(name.Substring(2));
        }

    }

}

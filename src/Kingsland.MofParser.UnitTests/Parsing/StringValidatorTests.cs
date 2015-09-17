using Kingsland.MofParser.Parsing;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Parsing
{

    public static partial class StringValidatorTests
    {

        #region A.13 Names

        #region IDENTIFIER = firstIdentifierChar *( nextIdentifierChar )

        [TestFixture]
        public static class IsIdentifierTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("0identifier", false)]
            [TestCase("1identifier", false)]
            [TestCase("5identifier", false)]
            [TestCase("9identifier", false)]
            [TestCase("\\", false)]
            [TestCase("\\identifier", false)]
            [TestCase(":", false)]
            [TestCase(":identifier", false)]
            [TestCase("identi-fier", false)]
            [TestCase("_", true)]
            [TestCase("__", true)]
            [TestCase("__0identifier", true)]
            [TestCase("__identifier", true)]
            [TestCase("identifier", true)]
            [TestCase("identifier0", true)]
            [TestCase("_identifier0", true)]
            [TestCase("__identifier0", true)]
            [TestCase("__Identifier0", true)]
            [TestCase("__IDENTIFIER0", true)]
            public static void IsIdentifierTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsIdentifier(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE

        public static class IsFirstIdentifierTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', true)]
            [TestCase('M', true)]
            [TestCase('Z', true)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', true)]
            [TestCase('`', false)]
            [TestCase('a', true)]
            [TestCase('m', true)]
            [TestCase('z', true)]
            [TestCase('{', false)]
            public static void IsFirstIdentifierCharTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsFirstIdentifierChar(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region nextIdentifierChar = firstIdentifierChar / decimalDigit

        public static class IsNextIdentifierTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', true)]
            [TestCase('5', true)]
            [TestCase('9', true)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', true)]
            [TestCase('M', true)]
            [TestCase('Z', true)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', true)]
            [TestCase('`', false)]
            [TestCase('a', true)]
            [TestCase('m', true)]
            [TestCase('z', true)]
            [TestCase('{', false)]
            public static void IsNextIdentifierTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsNextIdentifierChar(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        // elementName = localName / schemaQualifiedName

        // localName = IDENTIFIER

        #endregion

        #region A.17.2 Real value

        #region decimalDigit = "0" / positiveDecimalDigit

        [TestFixture]
        public static class IsDecimalDigitTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', true)]
            [TestCase('5', true)]
            [TestCase('9', true)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void IsDecimalDigitTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsDecimalDigit(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region positiveDecimalDigit = "1"..."9"

        [TestFixture]
        public static class IsPositiveDecimalDigitTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', true)]
            [TestCase('9', true)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void IsPositiveDecimalDigitTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsPositiveDecimalDigit(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region A.17.4 Special Characters

        #region BACKSLASH = U+005C ; \

        [TestFixture]
        public static class IsBackslashMethodTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', true)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void  IsBackslashTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsBackslash(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region DOUBLEQUOTE = U+0022 ; "

        [TestFixture]
        public static class IsDoubleQuoteTests
        {

            [TestCase('\"', true)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void IsDoubleQuoteTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsDoubleQuote(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region SINGLEQUOTE = U+0027 ; '

        [TestFixture]
        public static class IsSingleQuoteTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', true)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void IsSingleQuoteTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsSingleQuote(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region UPPERALPHA = U+0041...U+005A ; A ... Z

        [TestFixture]
        public static class IsUpperAlphaTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', true)]
            [TestCase('M', true)]
            [TestCase('Z', true)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void IsUpperAlphaTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsUpperAlpha(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region LOWERALPHA = U+0061...U+007A ; a ... z

        [TestFixture]
        public static class IsLowerAlphaTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', true)]
            [TestCase('m', true)]
            [TestCase('z', true)]
            [TestCase('{', false)]
            public static void IsLowerAlphaTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsLowerAlpha(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region UNDERSCORE = U+005F ; _

        [TestFixture]
        public static class IsUnderscoreTests
        {

            [TestCase('\"', false)]
            [TestCase('\'', false)]
            [TestCase('/', false)]
            [TestCase('0', false)]
            [TestCase('5', false)]
            [TestCase('9', false)]
            [TestCase(':', false)]
            [TestCase('@', false)]
            [TestCase('A', false)]
            [TestCase('M', false)]
            [TestCase('Z', false)]
            [TestCase('[', false)]
            [TestCase('\\', false)]
            [TestCase(']', false)]
            [TestCase('_', true)]
            [TestCase('`', false)]
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            public static void IsUnderscoreTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsUnderscore(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

    }

}

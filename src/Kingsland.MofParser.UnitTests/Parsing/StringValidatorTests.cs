using Kingsland.MofParser.Parsing;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Parsing
{

    public static partial class StringValidatorTests
    {

        #region A.17.4 Special Characters

        // The following special characters are used in other ABNF rules in this specification:

        #region BACKSLASH = U+005C ; \

        [TestFixture]
        public class IsBackslashMethodTests
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
        public class IsDoubleQuoteTests
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
        public class IsSingleQuoteTests
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
        public class IsUpperAlphaTests
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
        public class IsLowerAlphaTests
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
        public class IsUnderscoreTests
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

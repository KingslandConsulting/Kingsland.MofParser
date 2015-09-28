using Kingsland.MofParser.Parsing;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Parsing
{

    public static partial class StringValidatorTests
    {

        #region 5.2 Whitespace

        #region WS = U+0020 / U+0009 / U+000D / U+000A

        [TestFixture]
        public static class IsWhitespaceTests
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
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            [TestCase(' ', true)]
            [TestCase('\t', true)]
            [TestCase('\r', true)]
            [TestCase('\n', true)]
            public static void IsWhitespaceTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsWhitespace(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region 5.3 Line termination

        [TestFixture]
        public static class IsLineTerminatorTests
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
            [TestCase('a', false)]
            [TestCase('m', false)]
            [TestCase('z', false)]
            [TestCase('{', false)]
            [TestCase(' ', false)]
            [TestCase('\t', false)]
            [TestCase('\r', true)]
            [TestCase('\n', true)]
            public static void IsLineTerminatorTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsLineTerminator(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region 7.3.2 Structure declaration

        #region structureName = elementName

        [TestFixture]
        public static class IsStructureNameTests
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
            [TestCase("MSFT_0identifier", true)]
            [TestCase("MSFT_1identifier", true)]
            [TestCase("MSFT_5identifier", true)]
            [TestCase("MSFT_9identifier", true)]
            [TestCase("MSFT_\\", false)]
            [TestCase("MSFT_\\identifier", false)]
            [TestCase("MSFT_:", false)]
            [TestCase("MSFT_:identifier", false)]
            [TestCase("MSFT_identi-fier", false)]
            [TestCase("MSFT__", true)]
            [TestCase("MSFT___", true)]
            [TestCase("MSFT___0identifier", true)]
            [TestCase("MSFT___identifier", true)]
            [TestCase("MSFT_identifier", true)]
            [TestCase("MSFT_identifier0", true)]
            [TestCase("MSFT__identifier0", true)]
            [TestCase("MSFT___identifier0", true)]
            [TestCase("MSFT___Identifier0", true)]
            [TestCase("MSFT___IDENTIFIER0", true)]
            public static void IsStructureNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsStructureName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region 7.3.3 Class declaration

        #region className = elementName

        [TestFixture]
        public static class IsClassNameTests
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
            [TestCase("MSFT_0identifier", true)]
            [TestCase("MSFT_1identifier", true)]
            [TestCase("MSFT_5identifier", true)]
            [TestCase("MSFT_9identifier", true)]
            [TestCase("MSFT_\\", false)]
            [TestCase("MSFT_\\identifier", false)]
            [TestCase("MSFT_:", false)]
            [TestCase("MSFT_:identifier", false)]
            [TestCase("MSFT_identi-fier", false)]
            [TestCase("MSFT__", true)]
            [TestCase("MSFT___", true)]
            [TestCase("MSFT___0identifier", true)]
            [TestCase("MSFT___identifier", true)]
            [TestCase("MSFT_identifier", true)]
            [TestCase("MSFT_identifier0", true)]
            [TestCase("MSFT__identifier0", true)]
            [TestCase("MSFT___identifier0", true)]
            [TestCase("MSFT___Identifier0", true)]
            [TestCase("MSFT___IDENTIFIER0", true)]
            public static void IsClassNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsClassName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region 7.3.4 Association declaration

        #region associationName = elementName

        [TestFixture]
        public static class IsAssociationNameTests
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
            [TestCase("MSFT_0identifier", true)]
            [TestCase("MSFT_1identifier", true)]
            [TestCase("MSFT_5identifier", true)]
            [TestCase("MSFT_9identifier", true)]
            [TestCase("MSFT_\\", false)]
            [TestCase("MSFT_\\identifier", false)]
            [TestCase("MSFT_:", false)]
            [TestCase("MSFT_:identifier", false)]
            [TestCase("MSFT_identi-fier", false)]
            [TestCase("MSFT__", true)]
            [TestCase("MSFT___", true)]
            [TestCase("MSFT___0identifier", true)]
            [TestCase("MSFT___identifier", true)]
            [TestCase("MSFT_identifier", true)]
            [TestCase("MSFT_identifier0", true)]
            [TestCase("MSFT__identifier0", true)]
            [TestCase("MSFT___identifier0", true)]
            [TestCase("MSFT___Identifier0", true)]
            [TestCase("MSFT___IDENTIFIER0", true)]
            public static void IsAssociationNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsAssociationName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

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
            [TestCase("Identifier", true)]
            [TestCase("IDENTIFIER", true)]
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

        public static class IsFirstIdentifierCharTests
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

        public static class IsNextIdentifierCharTests
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
            public static void IsNextIdentifierCharTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsNextIdentifierChar(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region elementName = localName / schemaQualifiedName

        public static class IsElementNameTests
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
            [TestCase("MSFT_0identifier", true)]
            [TestCase("MSFT_1identifier", true)]
            [TestCase("MSFT_5identifier", true)]
            [TestCase("MSFT_9identifier", true)]
            [TestCase("MSFT_\\", false)]
            [TestCase("MSFT_\\identifier", false)]
            [TestCase("MSFT_:", false)]
            [TestCase("MSFT_:identifier", false)]
            [TestCase("MSFT_identi-fier", false)]
            [TestCase("MSFT__", true)]
            [TestCase("MSFT___", true)]
            [TestCase("MSFT___0identifier", true)]
            [TestCase("MSFT___identifier", true)]
            [TestCase("MSFT_identifier", true)]
            [TestCase("MSFT_identifier0", true)]
            [TestCase("MSFT__identifier0", true)]
            [TestCase("MSFT___identifier0", true)]
            [TestCase("MSFT___Identifier0", true)]
            [TestCase("MSFT___IDENTIFIER0", true)]
            public static void IsElementNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsElementName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region localName = IDENTIFIER

        public static class IsLocalNameTests
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
            [TestCase("Identifier", true)]
            [TestCase("IDENTIFIER", true)]
            [TestCase("identifier0", true)]
            [TestCase("_identifier0", true)]
            [TestCase("__identifier0", true)]
            [TestCase("__Identifier0", true)]
            [TestCase("__IDENTIFIER0", true)]
            public static void IsLocalNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsLocalName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region A.13.1 Schema-qualified name

        #region schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER

        [TestFixture]
        public static class IsSchemaQualifiedNameTests
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
            [TestCase("_", false)]
            [TestCase("__", false)]
            [TestCase("__0identifier", false)]
            [TestCase("__identifier", false)]
            [TestCase("identifier", false)]
            [TestCase("identifier0", false)]
            [TestCase("_identifier0", false)]
            [TestCase("__identifier0", false)]
            [TestCase("__Identifier0", false)]
            [TestCase("__IDENTIFIER0", false)]
            [TestCase("MSFT_0identifier", false)]
            [TestCase("MSFT_1identifier", false)]
            [TestCase("MSFT_5identifier", false)]
            [TestCase("MSFT_9identifier", false)]
            [TestCase("MSFT_\\", false)]
            [TestCase("MSFT_\\identifier", false)]
            [TestCase("MSFT_:", false)]
            [TestCase("MSFT_:identifier", false)]
            [TestCase("MSFT_identi-fier", false)]
            [TestCase("MSFT__", true)]
            [TestCase("MSFT___", true)]
            [TestCase("MSFT___0identifier", true)]
            [TestCase("MSFT___identifier", true)]
            [TestCase("MSFT_identifier", true)]
            [TestCase("MSFT_identifier0", true)]
            [TestCase("MSFT__identifier0", true)]
            [TestCase("MSFT___identifier0", true)]
            [TestCase("MSFT___Identifier0", true)]
            [TestCase("MSFT___IDENTIFIER0", true)]
            public static void IsSchemaQualifiedNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsSchemaQualifiedName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region schemaName = firstSchemaChar *( nextSchemaChar )

        public static class IsSchemaNameTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("0schemaname", false)]
            [TestCase("1schemaname", false)]
            [TestCase("5schemaname", false)]
            [TestCase("9schemaname", false)]
            [TestCase("\\", false)]
            [TestCase("\\schemaname", false)]
            [TestCase(":", false)]
            [TestCase(":schemaname", false)]
            [TestCase("schema-name", false)]
            [TestCase("_", false)]
            [TestCase("__", false)]
            [TestCase("__0schemaname", false)]
            [TestCase("__schemaname", false)]
            [TestCase("schemaname", true)]
            [TestCase("schemaname0", true)]
            [TestCase("SchemaName", true)]
            [TestCase("SCHEMANAME", true)]
            [TestCase("_schemaname0", false)]
            [TestCase("__schemaname0", false)]
            [TestCase("__SchemaName0", false)]
            [TestCase("__SCHEMANAME0", false)]
            public static void IsSchemaNameTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsSchemaName(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region firstSchemaChar = UPPERALPHA / LOWERALPHA

        public static class IsFirstSchemaCharTests
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
            [TestCase('a', true)]
            [TestCase('m', true)]
            [TestCase('z', true)]
            [TestCase('{', false)]
            public static void IsFirstSchemaCharTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsFirstSchemaChar(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region nextSchemaChar = firstSchemaChar / decimalDigit

        public static class IsNextSchemaCharTests
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
            [TestCase('_', false)]
            [TestCase('`', false)]
            [TestCase('a', true)]
            [TestCase('m', true)]
            [TestCase('z', true)]
            [TestCase('{', false)]
            public static void IsNextSchemaCharTest(char value, bool expectedResult)
            {
                var result = StringValidator.IsNextSchemaChar(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region A.13.2 Alias identifier

        #region aliasIdentifier = "$" IDENTIFIER

        [TestFixture]
        public static class IsAliasIdentifierTests
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
            [TestCase("_", false)]
            [TestCase("__", false)]
            [TestCase("__0identifier", false)]
            [TestCase("__identifier", false)]
            [TestCase("identifier", false)]
            [TestCase("Identifier", false)]
            [TestCase("IDENTIFIER", false)]
            [TestCase("identifier0", false)]
            [TestCase("_identifier0", false)]
            [TestCase("__identifier0", false)]
            [TestCase("__Identifier0", false)]
            [TestCase("__IDENTIFIER0", false)]
            [TestCase("$", false)]
            [TestCase("$0identifier", false)]
            [TestCase("$1identifier", false)]
            [TestCase("$5identifier", false)]
            [TestCase("$9identifier", false)]
            [TestCase("$\\", false)]
            [TestCase("$\\identifier", false)]
            [TestCase("$:", false)]
            [TestCase("$:identifier", false)]
            [TestCase("$identi-fier", false)]
            [TestCase("$_", true)]
            [TestCase("$__", true)]
            [TestCase("$__0identifier", true)]
            [TestCase("$__identifier", true)]
            [TestCase("$identifier", true)]
            [TestCase("$Identifier", true)]
            [TestCase("$IDENTIFIER", true)]
            [TestCase("$identifier0", true)]
            [TestCase("$_identifier0", true)]
            [TestCase("$__identifier0", true)]
            [TestCase("$__Identifier0", true)]
            [TestCase("$__IDENTIFIER0", true)]
            public static void IsAliasIdentifierTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsAliasIdentifier(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region A.17.1 Integer value

        #region decimalValue = [ "+" / "-" ] unsignedDecimalValue

        [TestFixture]
        public static class IsDecimalValueTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("0", false)]
            [TestCase("5", true)]
            [TestCase("9", true)]
            [TestCase("+0", false)]
            [TestCase("+5", true)]
            [TestCase("+9", true)]
            [TestCase("-0", false)]
            [TestCase("-5", true)]
            [TestCase("-9", true)]
            [TestCase("12345", true)]
            [TestCase("67890", true)]
            [TestCase("-12345", true)]
            [TestCase("-67890", true)]
            [TestCase("+12345", true)]
            [TestCase("+67890", true)]
            [TestCase("012345", false)]
            [TestCase("067890", false)]
            [TestCase("+012345", false)]
            [TestCase("+067890", false)]
            [TestCase("-012345", false)]
            [TestCase("-067890", false)]
            [TestCase("A", false)]
            [TestCase("M", false)]
            [TestCase("Z", false)]
            [TestCase("+A", false)]
            [TestCase("+M", false)]
            [TestCase("+Z", false)]
            [TestCase("-A", false)]
            [TestCase("-M", false)]
            [TestCase("-Z", false)]
            public static void IsDecimalValueTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsDecimalValue(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region  unsignedDecimalValue = positiveDecimalDigit *decimalDigit

        public static class IsUnsignedDecimalValueTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("0", false)]
            [TestCase("5", true)]
            [TestCase("9", true)]
            [TestCase("+0", false)]
            [TestCase("+5", false)]
            [TestCase("+9", false)]
            [TestCase("-0", false)]
            [TestCase("-5", false)]
            [TestCase("-9", false)]
            [TestCase("12345", true)]
            [TestCase("67890", true)]
            [TestCase("-12345", false)]
            [TestCase("-67890", false)]
            [TestCase("+12345", false)]
            [TestCase("+67890", false)]
            [TestCase("012345", false)]
            [TestCase("067890", false)]
            [TestCase("+012345", false)]
            [TestCase("+067890", false)]
            [TestCase("-012345", false)]
            [TestCase("-067890", false)]
            [TestCase("A", false)]
            [TestCase("M", false)]
            [TestCase("Z", false)]
            [TestCase("+A", false)]
            [TestCase("+M", false)]
            [TestCase("+Z", false)]
            [TestCase("-A", false)]
            [TestCase("-M", false)]
            [TestCase("-Z", false)]
            public static void IsUnsignedDecimalValueTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsUnsignedDecimalValue(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

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
        public static class IsBackslashTests
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
            public static void IsBackslashTest(char value, bool expectedResult)
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

        #region A.17.6 Boolean value

        #region FALSE = "false" ; keyword: case insensitive

        [TestFixture]
        public static class IsFalseTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("false", true)]
            [TestCase("False", true)]
            [TestCase("FALSE", true)]
            [TestCase("true", false)]
            [TestCase("True", false)]
            [TestCase("TRUE", false)]
            [TestCase("null", false)]
            [TestCase("Null", false)]
            [TestCase("NULL", false)]
            public static void IsFalseTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsFalse(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #region TRUE = "true" ; keyword: case insensitive

        [TestFixture]
        public static class IsTrueTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("false", false)]
            [TestCase("False", false)]
            [TestCase("FALSE", false)]
            [TestCase("true", true)]
            [TestCase("True", true)]
            [TestCase("TRUE", true)]
            [TestCase("null", false)]
            [TestCase("Null", false)]
            [TestCase("NULL", false)]
            public static void IsTrueTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsTrue(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

        #region A.17.7 Null value

        #region NULL = "null" ; keyword: case insensitive

        [TestFixture]
        public static class IsNullTests
        {

            [TestCase(null, false)]
            [TestCase("", false)]
            [TestCase("false", false)]
            [TestCase("False", false)]
            [TestCase("FALSE", false)]
            [TestCase("true", false)]
            [TestCase("True", false)]
            [TestCase("TRUE", false)]
            [TestCase("null", true)]
            [TestCase("Null", true)]
            [TestCase("NULL", true)]
            public static void IsNullTest(string value, bool expectedResult)
            {
                var result = StringValidator.IsNull(value);
                Assert.AreEqual(expectedResult, result);
            }

        }

        #endregion

        #endregion

    }

}

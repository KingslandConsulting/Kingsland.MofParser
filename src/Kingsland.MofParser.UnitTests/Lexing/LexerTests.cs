using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Helpers;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Kingsland.MofParser.UnitTests.Lexing
{

    [TestFixture]
    public static partial class LexerTests
    {

        [TestFixture]
        public static class EmptyFileTests
        {

            [Test]
            public static void ShouldReadEmptyFile()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From(string.Empty)
                );
                var expectedTokens = new List<SyntaxToken>();
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class MiscTests
        {

            [Test]
            public static void MissingWhitespaceTest()
            {
                var actualTokens = Lexer.Lex(
                    SourceReader.From("12345myIdentifier")
                );
                var expectedTokens = new List<SyntaxToken> {
                    new IntegerLiteralToken(
                        new SourceExtent
                        (
                            new SourcePosition(0, 1, 1),
                            new SourcePosition(4, 1, 5),
                            "12345"
                        ),
                        IntegerKind.DecimalValue, 12345
                    ),
                    new IdentifierToken(
                        new SourceExtent
                        (
                            new SourcePosition(5, 1, 6),
                            new SourcePosition(16, 1, 17),
                            "myIdentifier"
                        ),
                        "myIdentifier"
                    )
                };
                LexerAssert.AreEqual(expectedTokens, actualTokens);
            }

        }

        [TestFixture]
        public static class LexMethodTestCases
        {
            //[Test, TestCaseSource(typeof(LexMethodTestCases), "GetTestCases")]
            public static void LexMethodTestsFromDisk(string mofFilename)
            {
                LexerTests.LexMethodTest(mofFilename);
            }
            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("cim_schema_2.51.0Final-MOFs");
                }
            }
        }

        [TestFixture]
        public static class LexCimSpec
        {
            //[Test, TestCaseSource(typeof(LexCimSpec), "GetTestCases")]
            public static void LexMethodTestsFromDisk(string mofFilename)
            {
                LexerTests.LexMethodTest(mofFilename);
            }
            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("Lexer\\TestCases");
                }
            }
        }

        private static void LexMethodTest(string mofFilename)
        {
            var mofText = File.ReadAllText(mofFilename);
            var reader = SourceReader.From(mofText);
            var actualTokens = Lexer.Lex(reader);
            var actualText = TestUtils.ConvertToJson(actualTokens);
            var expectedFilename = Path.Combine(Path.GetDirectoryName(mofFilename),
                                                Path.GetFileNameWithoutExtension(mofFilename) + ".json");
            if (!File.Exists(expectedFilename))
            {
                File.WriteAllText(expectedFilename, actualText);
            }
            var expectedText = File.ReadAllText(expectedFilename);
            Assert.AreEqual(expectedText, actualText);
        }

    }

}
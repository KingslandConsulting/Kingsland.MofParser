using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections;
using System.IO;

namespace Kingsland.MofParser.UnitTests.Lexer
{

    public static class ParserTests
    {

        [TestFixture]
        public static class ParserMethodTokenTests
        {

            [Test, TestCaseSource(typeof(ParseMethodTestCases), "TestCases")]
            //[Test, TestCaseSource(typeof(ParseMethodTestCasesWmiWin81), "TestCases")]
            //[Test, TestCaseSource(typeof(ParseMethodTestCasesWmiWinXp), "TestCases")]
            public static void ParseMethodTestsFromDisk(string mofFilename)
            {
                //Console.WriteLine(mofFilename);
                var mofText = File.ReadAllText(mofFilename);
                var tokens = Lexing.Lexer.Lex(new StringLexerStream(mofText));
                var parser = new Parser(tokens);
                var ast = parser.Parse();
                var actualText = TestUtils.ConvertToJson(ast);
                var expectedFilename = Path.Combine(Path.GetDirectoryName(mofFilename),
                                                    Path.GetFileNameWithoutExtension(mofFilename) + ".json");
                if (!File.Exists(expectedFilename))
                {
                    File.WriteAllText(expectedFilename, actualText);
                }
                var expectedText = File.ReadAllText(expectedFilename);
                Assert.AreEqual(expectedText, actualText);
            }

            private static class ParseMethodTestCases
            {
                public static IEnumerable TestCases
                {
                    get
                    {
                        return TestUtils.GetMofTestCase("..\\..\\Parsing\\TestCases");
                    }
                }
            }

            private static class ParseMethodTestCasesWmiWin81
            {
                public static IEnumerable TestCases
                {
                    get
                    {
                        return TestUtils.GetMofTestCase("..\\..\\Parsing\\WMI\\Win81");
                    }
                }
            }

            private static class ParseMethodTestCasesWmiWinXp
            {
                public static IEnumerable TestCases
                {
                    get
                    {
                        return TestUtils.GetMofTestCase("..\\..\\Parsing\\WMI\\WinXp");
                    }
                }
            }

        }

    }

}

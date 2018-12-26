using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace Kingsland.MofParser.UnitTests.Lexer
{

    public static class ParserTests
    {

        [TestFixture]
        public static class ParseMethodTestCases
        {

            [Test, TestCaseSource(typeof(ParseMethodTestCases), "GetTestCases")]
            //[Test, TestCaseSource(typeof(ParseMethodTestCasesWmiWin81), "TestCases")]
            //[Test, TestCaseSource(typeof(ParseMethodTestCasesWmiWinXp), "TestCases")]
            //[Test, TestCaseSource(typeof(ParseMethodGolfExamples), "ParseMethodGolfExamples")]
            public static void ParseMethodTestsFromDisk(string mofFilename)
            {
                //Console.WriteLine(mofFilename);
                var mofText = File.ReadAllText(mofFilename);
                var reader = SourceReader.From(mofText);
                var tokens = Lexing.Lexer.Lex(reader);
                var ast = Parser.Parse(tokens);
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

            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("Parsing\\TestCases");
                }
            }

        }

        //private static class ParseMethodTestCasesWmiWin81
        //{
        //    public static IEnumerable TestCases
        //    {
        //        get
        //        {
        //            return TestUtils.GetMofTestCase("Parsing\\WMI\\Win81");
        //        }
        //    }
        //}

        //private static class ParseMethodTestCasesWmiWinXp
        //{
        //    public static IEnumerable TestCases
        //    {
        //        get
        //        {
        //            return TestUtils.GetMofTestCase("Parsing\\WMI\\WinXp");
        //        }
        //    }
        //}

        [TestFixture]
        public static class ParseMethodGolfExamples
        {

            //[Test, TestCaseSource(typeof(ParseMethodGolfExamples), "GetTestCases")]
            public static void ParseMethodTestsFromDisk(string mofFilename)
            {
                //Console.WriteLine(mofFilename);
                var mofText = File.ReadAllText(mofFilename);
                var reader = SourceReader.From(mofText);
                var tokens = Lexing.Lexer.Lex(reader);
                var ast = Parser.Parse(tokens);
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

            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("Parsing\\DSP0221_3.0.1");
                }
            }

        }

    }

}

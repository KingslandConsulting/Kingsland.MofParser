using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region Roundtrip Test Cases

        //[TestFixture]
        //public static class ConvertToMofMethodTestCasesWmiWinXp
        //{
        //    [Test, TestCaseSource(typeof(ConvertToMofMethodTestCasesWmiWinXp), "GetTestCases")]
        //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
        //    {
        //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
        //    }
        //    public static IEnumerable<TestCaseData> GetTestCases
        //    {
        //        get
        //        {
        //            return TestUtils.GetMofTestCase("Parsing\\WMI\\WinXp");
        //        }
        //    }
        //}

        //[TestFixture]
        //public static class ConvertToMofMethodGolfExamples
        //{
        //    //[Test, TestCaseSource(typeof(ConvertToMofMethodGolfExamples), "GetTestCases")]
        //    public static void ConvertToMofMethodTestsFromDisk(string mofFilename)
        //    {
        //        ConvertToMofTests.MofGeneratorRoundtripTest(mofFilename);
        //    }
        //    public static IEnumerable<TestCaseData> GetTestCases
        //    {
        //        get
        //        {
        //            return TestUtils.GetMofTestCase("Parsing\\DSP0221_3.0.1");
        //        }
        //    }
        //}

        private static void AssertRoundtrip(string sourceText, ParserQuirks parserQuirks = ParserQuirks.None)
        {
            // check the lexer tokens roundtrips ok
            var tokens = Lexer.Lex(SourceReader.From(sourceText));
            var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
            Assert.AreEqual(sourceText, tokensMof);
            // check the parser ast roundtrips ok
            var astNodes = Parser.Parse(tokens, parserQuirks);
            var astMof = AstMofGenerator.ConvertToMof(astNodes);
            Assert.AreEqual(sourceText, astMof);
        }

        private static void AssertRoundtripException(string sourceText, string expectedMessage)
        {
            var tokens = Lexer.Lex(SourceReader.From(sourceText));
            var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
            var ex = Assert.Throws<UnexpectedTokenException>(
                () => {
                    var astNodes = Parser.Parse(tokens);
                }
            );
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        #endregion

    }

}
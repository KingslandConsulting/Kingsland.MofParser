using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
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

        private static void AssertRoundtrip(string sourceMof, ParserQuirks parserQuirks = ParserQuirks.None)
        {
            // check the lexer tokens roundtrips ok
            var tokens = Lexer.Lex(SourceReader.From(sourceMof));
            var tokensMof = TokenMofGenerator.ConvertToMof(tokens);
            Assert.AreEqual(sourceMof, tokensMof);
            // check the parser ast roundtrips ok
            var astNodes = Parser.Parse(tokens, parserQuirks);
            var astMof = AstMofGenerator.ConvertToMof(astNodes);
            Assert.AreEqual(sourceMof, astMof);
        }

        #endregion

    }

}
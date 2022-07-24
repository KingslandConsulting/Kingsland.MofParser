using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.UnitTests.Helpers;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Parsing;

public static partial class ParserTests
{

    //[TestFixture]
    //private static class ParseMethodTestCasesWmiWin81
    //{
    //    [Test, TestCaseSource(typeof(LexMethodTestCases), "GetTestCases")]
    //    public static IEnumerable<TestCaseData> TestCases
    //    {
    //        get
    //        {
    //            return TestUtils.GetMofTestCase("Parsing\\WMI\\Win81");
    //        }
    //    }
    //}

    public static class ParseMethodTestCasesWmiWinXp
    {
        [Test, TestCaseSource(typeof(ParseMethodTestCasesWmiWinXp), "GetTestCases")]
        public static void ParseMethodTestsFromDisk(string mofFilename)
        {
            ParserTests.ParseMethodTest(mofFilename);
        }
        public static IEnumerable<TestCaseData> GetTestCases =>
            TestUtils.GetMofTestCase(
                Path.Combine("Parsing", "WMI", "WinXp")
            );
    }

    [TestFixture]
    public static class ParseMethodGolfExamples
    {
        [Test, TestCaseSource(typeof(ParseMethodGolfExamples), "GetTestCases")]
        public static void ParseMethodTestsFromDisk(string mofFilename)
        {
            ParserTests.ParseMethodTest(mofFilename);
        }
        public static IEnumerable<TestCaseData> GetTestCases =>
            TestUtils.GetMofTestCase(
                Path.Combine("Parsing", "DSP0221_3.0.1")
            );
    }

    private static void ParseMethodTest(string mofFilename)
    {
        var mofText = File.ReadAllText(mofFilename);
        var reader = SourceReader.From(mofText);
        var tokens = Lexer.Lex(reader);
        var ast = Parser.Parse(
            tokens,
            ParserQuirks.AllowMofV2Qualifiers |
            ParserQuirks.AllowEmptyQualifierValueArrays
        );
        var actualText = TestUtils.ConvertToJson(ast);
        var expectedFilename = Path.Combine(
            Path.GetDirectoryName(mofFilename)
                ?? throw new NullReferenceException(),
            Path.GetFileNameWithoutExtension(mofFilename) + ".json"
        );
        if (!File.Exists(expectedFilename))
        {
            File.WriteAllText(expectedFilename, actualText);
        }
        var expectedText = File.ReadAllText(expectedFilename);
        Assert.AreEqual(expectedText, actualText);
    }

}

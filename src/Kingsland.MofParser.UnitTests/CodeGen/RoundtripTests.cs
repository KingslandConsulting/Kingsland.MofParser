﻿using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Lexing;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using NUnit.Framework;
using System.Collections.Generic;

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

        private static void AssertRoundtrip(string sourceText, List<SyntaxToken> expectedTokens = null, ParserQuirks parserQuirks = ParserQuirks.None)
        {
            // check the lexer tokens roundtrip ok
            var actualTokens = Lexer.Lex(SourceReader.From(sourceText));
            if (expectedTokens != null)
            {
                LexerAssert.AreEqual(expectedTokens, actualTokens, true);
                var expectedText = TokenSerializer.ToMofText(expectedTokens);
                Assert.AreEqual(sourceText, expectedText);
            }
            var actualText = TokenSerializer.ToMofText(actualTokens);
            Assert.AreEqual(sourceText, actualText);
            // check the parser ast roundtrips ok
            var astNodes = Parser.Parse(actualTokens, parserQuirks);
            var astMof = AstMofGenerator.ConvertToMof(astNodes);
            Assert.AreEqual(sourceText, astMof);
        }

        private static void AssertRoundtripException(string sourceText, string expectedMessage, ParserQuirks parserQuirks = ParserQuirks.None)
        {
            var tokens = Lexer.Lex(SourceReader.From(sourceText));
            var tokensMof = TokenSerializer.ToMofText(tokens);
            var ex = Assert.Throws<UnexpectedTokenException>(
                () => {
                    var astNodes = Parser.Parse(tokens, parserQuirks);
                }
            );
            Assert.AreEqual(expectedMessage, ex.Message);
        }

        #endregion

    }

}
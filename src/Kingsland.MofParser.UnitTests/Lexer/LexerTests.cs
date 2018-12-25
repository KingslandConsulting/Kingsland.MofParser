using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Kingsland.MofParser.UnitTests.Lexer
{

    public static class LexerTests
    {

        [TestFixture]
        public static class LexMethodTestCases
        {

            [Test, TestCaseSource(typeof(LexMethodTokenTests), "GetTestCases")]
            public static void LexMethodTestsFromDisk(string mofFilename)
            {
                var mofText = File.ReadAllText(mofFilename);
                var reader = SourceReader.From(mofText);
                var tokens = Lexing.Lexer.Lex(reader);
                var actualText = TestUtils.ConvertToJson(tokens);
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
                    return TestUtils.GetMofTestCase("Lexer\\TestCases");
                }
            }

        }

    }

}

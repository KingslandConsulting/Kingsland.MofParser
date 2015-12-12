using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Tokens;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml;

namespace Kingsland.MofParser.UnitTests
{

    public static class LexerTests
    {

        [TestFixture]
        public static class LexMethodTokenTests
        {

            [Test, TestCaseSource(typeof(LexerTestCases), "TestCases")]
            public static void LexerTestsFromDisk(string mofFilename)
            {
                var mofText = File.ReadAllText(mofFilename);
                var tokens = Lexer.Lex(new StringLexerStream(mofText));
                var actualText = LexMethodTokenTests.ConvertToJson(tokens);
                var expectedFilename = Path.Combine(Path.GetDirectoryName(mofFilename),
                                                    Path.GetFileNameWithoutExtension(mofFilename) + ".json");
                if (!File.Exists(expectedFilename))
                {
                    File.WriteAllText(expectedFilename, actualText);
                }
                var expectedText = File.ReadAllText(expectedFilename);
                Assert.AreEqual(expectedText, actualText);
            }

            private static string ConvertToJson(List<Token> tokens)
            {
                var formatting = Newtonsoft.Json.Formatting.Indented;
                var settings = new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.All
                };
                var json = JsonConvert.SerializeObject(tokens, formatting , settings);
                return json;
            }

            private static string ConvertToXml(List<Token> tokens)
            {
                // serialize the result so we can compare it
                var settings = new XmlWriterSettings();
                settings.IndentChars = "  ";
                settings.Indent = true;
                using (var sw = new StringWriter())
                {
                    using (XmlWriter xw = XmlWriter.Create(sw, settings))
                    {
                        var serializer = new DataContractSerializer(typeof(List<Token>));
                        serializer.WriteObject(xw, tokens);
                        xw.Flush();
                        sw.Flush();
                        return sw.ToString();
                    }
                }
            }

            public static class LexerTestCases
            {
                public static IEnumerable TestCases
                {
                    get
                    {
                        var path = "D:\\Michaels Documents\\Repositories\\GitHub\\mikeclayton\\MofParser\\src\\Kingsland.MofParser.UnitTests\\Lexer\\Tokens";
                        var mofFilenames = Directory.GetFiles(path, "*.mof", SearchOption.AllDirectories);
                        foreach (var mofFilename in mofFilenames)
                        {
                            var testName = mofFilename.Substring(path.Length + 1)
                                                      .Replace("\\", ".");
                            //Path.GetFileName(mofFilename)
                            yield return new TestCaseData(mofFilename).SetName(testName);
                        }
                    }
                }
            }

        }

        [TestFixture]
        public static class LexMethodSampleFiles
        {

            [TestCase("WinXpProSp3CIMV2.mof")]
            [TestCase("WinXpProSp3Microsoft.mof")]
            [TestCase("WinXpProSp3WMI.mof")]
            public static void LexerTestsFromDiskWinXp(string mofFilename)
            {
                var filename = Path.Combine("D:\\Michaels Documents\\Repositories\\GitHub\\mikeclayton\\MofParser\\src\\Kingsland.MofParser.UnitTests\\Lexer\\WMI\\WinXp", mofFilename);
                var mofText = File.ReadAllText(filename);
                var lexer = new Lexer(new StringLexerStream(mofText));
                var tokens = lexer.AllTokens().ToList();
            }

            [Test, TestCaseSource(typeof(LexerTestsFromDiskTestCasesWin81), "TestCases")]
            public static void LexerTestsFromDiskWin81(string mofFilename)
            {
                var filename = Path.Combine("D:\\Michaels Documents\\Repositories\\GitHub\\mikeclayton\\MofParser\\src\\Kingsland.MofParser.UnitTests\\Lexer\\WMI\\WinXp", mofFilename);
                var mofText = File.ReadAllText(filename);
                var lexer = new Lexer(new StringLexerStream(mofText));
                var tokens = lexer.AllTokens().ToList();
                var ast = MofParser.Parsing.Parser.Parse(tokens);
            }

            public static class LexerTestsFromDiskTestCasesWin81
            {
                public static IEnumerable TestCases
                {
                    get
                    {
                        var path = "D:\\Michaels Documents\\Repositories\\GitHub\\mikeclayton\\MofParser\\src\\Kingsland.MofParser.UnitTests\\Lexer\\WMI\\Win81";
                        var mofFilenames = Directory.GetFiles(path, "*.mof", SearchOption.AllDirectories);
                        foreach (var mofFilename in mofFilenames)
                        {
                            var testName = mofFilename.Substring(path.Length + 1)
                                                      .Replace("\\", ".");
                            //Path.GetFileName(mofFilename)
                            yield return new TestCaseData(mofFilename).SetName(testName);
                        }
                    }
                }
            }

        }

    }

}

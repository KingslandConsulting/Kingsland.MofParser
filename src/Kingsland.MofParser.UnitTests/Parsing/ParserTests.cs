using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Kingsland.MofParser.UnitTests.Lexer
{

    public static class ParserTests
    {

        [TestFixture]
        public static class PropertyValueTests
        {

            [Test]
            public static void ParsePropetyValueLiteralString()
            {
                var tokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias0000006E\r\n" +
                        "{\r\n" +
                        "    ServerURL = \"https://URL\";\r\n" +
                        "};"
                    )
                );
                var actualAst = Parser.Parse(tokens);
                var expectedAst = new MofSpecificationAst(
                    new ReadOnlyCollection<MofProductionAst>(
                        new List<MofProductionAst>
                        {
                            new InstanceValueDeclarationAst(
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new AliasIdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(22, 1, 23),
                                        new SourcePosition(35, 1, 36),
                                        "$Alias0000006E"
                                    ),
                                    "Alias0000006E"
                                ),
                                new PropertyValueListAst(
                                    new ReadOnlyDictionary<string, PropertyValueAst>(
                                        new Dictionary<string, PropertyValueAst> {
                                            { "ServerURL", new StringValueAst("https://URL") }
                                        }
                                    )
                                )
                            )
                        }
                    )
                );
                var actualJson = TestUtils.ConvertToJson(actualAst);
                var expectedJson = TestUtils.ConvertToJson(expectedAst);
                Assert.AreEqual(expectedJson, actualJson);
            }

            [Test]
            public static void ParsePropetyValueWithReferenceTypeValue()
            {
                var tokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    Reference = $Alias0000006E;\r\n" +
                        "};"
                    )
                );
                var actualAst = Parser.Parse(tokens);
                var expectedAst = new MofSpecificationAst(
                    new ReadOnlyCollection<MofProductionAst>(
                        new List<MofProductionAst>
                        {
                            new InstanceValueDeclarationAst(
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new AliasIdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(22, 1, 23),
                                        new SourcePosition(35, 1, 36),
                                        "$Alias00000070"
                                    ),
                                    "Alias00000070"
                                ),
                                new PropertyValueListAst(
                                    new ReadOnlyDictionary<string, PropertyValueAst>(
                                        new Dictionary<string, PropertyValueAst> {
                                            { "Reference", new ReferenceTypeValueAst("Alias0000006E") }
                                        }
                                    )
                                )
                            )
                        }
                    )
                );
                var actualJson = TestUtils.ConvertToJson(actualAst);
                var expectedJson = TestUtils.ConvertToJson(expectedAst);
                Assert.AreEqual(expectedJson, actualJson);
            }

            [Test]
            public static void ParsePropetyValueEmptyArray()
            {
                var tokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    Reference = {};\r\n" +
                        "};"
                    )
                );
                var actualAst = Parser.Parse(tokens);
                var expectedAst = new MofSpecificationAst(
                    new ReadOnlyCollection<MofProductionAst>(
                        new List<MofProductionAst>
                        {
                            new InstanceValueDeclarationAst(
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new AliasIdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(22, 1, 23),
                                        new SourcePosition(35, 1, 36),
                                        "$Alias00000070"
                                    ),
                                    "Alias00000070"
                                ),
                                new PropertyValueListAst(
                                    new ReadOnlyDictionary<string, PropertyValueAst>(
                                        new Dictionary<string, PropertyValueAst> {
                                            { "Reference", new ComplexValueArrayAst(
                                                new ReadOnlyCollection<ComplexValueAst>(
                                                    new List<ComplexValueAst>()
                                                ))
                                            }
                                        }
                                    )
                                )
                            )
                        }
                    )
                );
                var actualJson = TestUtils.ConvertToJson(actualAst);
                var expectedJson = TestUtils.ConvertToJson(expectedAst);
                Assert.AreEqual(expectedJson, actualJson);
            }

            [Test]
            public static void ParsePropetyValueArrayWithSingleItem()
            {
                var tokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    Reference = {$Alias0000006E};\r\n" +
                        "};"
                    )
                );
                var actualAst = Parser.Parse(tokens);
                var expectedAst = new MofSpecificationAst(
                    new ReadOnlyCollection<MofProductionAst>(
                        new List<MofProductionAst>
                        {
                            new InstanceValueDeclarationAst(
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new AliasIdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(22, 1, 23),
                                        new SourcePosition(35, 1, 36),
                                        "$Alias00000070"
                                    ),
                                    "Alias00000070"
                                ),
                                new PropertyValueListAst(
                                    new ReadOnlyDictionary<string, PropertyValueAst>(
                                        new Dictionary<string, PropertyValueAst> {
                                            { "Reference", new ComplexValueArrayAst(
                                                new ReadOnlyCollection<ComplexValueAst>(
                                                    new List<ComplexValueAst> {
                                                        new ComplexValueAst(
                                                            true, false,
                                                            new AliasIdentifierToken(
                                                                new SourceExtent(
                                                                    new SourcePosition(58, 3, 18),
                                                                    new SourcePosition(71, 3, 31),
                                                                    "$Alias0000006E"
                                                                ),
                                                                "Alias0000006E"
                                                            ),
                                                            new IdentifierToken(
                                                                new SourceExtent(
                                                                    new SourcePosition(12, 1, 13),
                                                                    new SourcePosition(17, 1, 18),
                                                                    "myType"
                                                                ),
                                                                "myType"
                                                            ),
                                                            new PropertyValueListAst(
                                                                new ReadOnlyDictionary<string, PropertyValueAst>(
                                                                    new Dictionary<string, PropertyValueAst>()
                                                                )
                                                            )
                                                        )
                                                    }
                                                )
                                            )}
                                        }
                                    )
                                )
                            )
                        }
                    )
                );
                var actualJson = TestUtils.ConvertToJson(actualAst);
                var expectedJson = TestUtils.ConvertToJson(expectedAst);
                Assert.AreEqual(expectedJson, actualJson);
            }

            [Test]
            public static void ParsePropetyValueArrayWithMultipleItems()
            {
                var tokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    ServerURLs = { \"https://URL1\", \"https://URL2\" };\r\n" +
                        "};"
                    )
                );
                var actualAst = Parser.Parse(tokens);
                var expectedAst = new MofSpecificationAst(
                    new ReadOnlyCollection<MofProductionAst>(
                        new List<MofProductionAst>
                        {
                            new InstanceValueDeclarationAst(
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new AliasIdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(22, 1, 23),
                                        new SourcePosition(35, 1, 36),
                                        "$Alias00000070"
                                    ),
                                    "Alias00000070"
                                ),
                                new PropertyValueListAst(
                                    new ReadOnlyDictionary<string, PropertyValueAst>(
                                        new Dictionary<string, PropertyValueAst> {
                                            { "ServerURLs", new LiteralValueArrayAst(
                                                new ReadOnlyCollection<LiteralValueAst>(
                                                    new List<LiteralValueAst> {
                                                        new StringValueAst("https://URL1"),
                                                        new StringValueAst("https://URL2")
                                                    }
                                                )
                                            )}
                                        }
                                    )
                                )
                            )
                        }
                    )
                );
                var actualJson = TestUtils.ConvertToJson(actualAst);
                var expectedJson = TestUtils.ConvertToJson(expectedAst);
                Assert.AreEqual(expectedJson, actualJson);
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

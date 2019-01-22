using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace Kingsland.MofParser.UnitTests.Parsing
{

    public static class ParserTests
    {

        [TestFixture]
        public static class PropertyValueTests
        {

            [Test]
            public static void ParsePropetyValueWithLiteralString()
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
                                        new SourcePosition(0, 1, 1),
                                        new SourcePosition(7, 1, 8),
                                        "instance"
                                    ),
                                    "instance"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(9, 1, 10),
                                        new SourcePosition(10, 1, 11),
                                        "of"
                                    ),
                                    "of"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(19, 1, 20),
                                        new SourcePosition(20, 1, 21),
                                        "as"
                                    ),
                                    "as"
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
                                            { "ServerURL", new StringValueAst.Builder {
                                                StringLiteralValues = new List<StringLiteralToken> {
                                                    new StringLiteralToken(
                                                        new SourceExtent(
                                                            new SourcePosition(57, 3, 17),
                                                            new SourcePosition(69, 3, 29),
                                                            "\"https://URL\""
                                                        ),
                                                        "https://URL"
                                                    )
                                                },
                                                Value = "https://URL"
                                            }.Build() }
                                        }
                                    )
                                ),
                                new StatementEndToken(
                                    new SourceExtent(
                                        new SourcePosition(74, 4, 2),
                                        new SourcePosition(74, 4, 2),
                                        ";"
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
            public static void ParsePropetyValueWithAliasIdentifier()
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
                                        new SourcePosition(0, 1, 1),
                                        new SourcePosition(7, 1, 8),
                                        "instance"
                                    ),
                                    "instance"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(9, 1, 10),
                                        new SourcePosition(10, 1, 11),
                                        "of"
                                    ),
                                    "of"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(19, 1, 20),
                                        new SourcePosition(20, 1, 21),
                                        "as"
                                    ),
                                    "as"
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
                                            { "Reference", new ComplexValueAst(
                                                new AliasIdentifierToken(
                                                    new SourceExtent(
                                                        new SourcePosition(57, 3, 17),
                                                        new SourcePosition(70, 3, 30),
                                                        "$Alias0000006E"
                                                    ),
                                                    "Alias0000006E"
                                                )
                                            )}
                                        }
                                    )
                                ),
                                new StatementEndToken(
                                    new SourceExtent(
                                        new SourcePosition(75, 4, 2),
                                        new SourcePosition(75, 4, 2),
                                        ";"
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
            public static void ParsePropetyValueWithEmptyArray()
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
                                        new SourcePosition(0, 1, 1),
                                        new SourcePosition(7, 1, 8),
                                        "instance"
                                    ),
                                    "instance"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(9, 1, 10),
                                        new SourcePosition(10, 1, 11),
                                        "of"
                                    ),
                                    "of"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(19, 1, 20),
                                        new SourcePosition(20, 1, 21),
                                        "as"
                                    ),
                                    "as"
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
                                            { "Reference", new LiteralValueArrayAst(null) }
                                        }
                                    )
                                ),
                                new StatementEndToken(
                                    new SourceExtent(
                                        new SourcePosition(63, 4, 2),
                                        new SourcePosition(63, 4, 2),
                                        ";"
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
            public static void ParsePropetyValueArrayWithAliasIdentifier()
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
                                        new SourcePosition(0, 1, 1),
                                        new SourcePosition(7, 1, 8),
                                        "instance"
                                    ),
                                    "instance"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(9, 1, 10),
                                        new SourcePosition(10, 1, 11),
                                        "of"
                                    ),
                                    "of"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(19, 1, 20),
                                        new SourcePosition(20, 1, 21),
                                        "as"
                                    ),
                                    "as"
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
                                                            new AliasIdentifierToken(
                                                                new SourceExtent(
                                                                    new SourcePosition(58, 3, 18),
                                                                    new SourcePosition(71, 3, 31),
                                                                    "$Alias0000006E"
                                                                ),
                                                                "Alias0000006E"
                                                            )
                                                        )
                                                    }
                                                )
                                            )}
                                        }
                                    )
                                ),
                                new StatementEndToken(
                                    new SourceExtent(
                                        new SourcePosition(77, 4, 2),
                                        new SourcePosition(77, 4, 2),
                                        ";"
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
            public static void ParsePropetyValueArrayWithLiteralStrings()
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
                                        new SourcePosition(0, 1, 1),
                                        new SourcePosition(7, 1, 8),
                                        "instance"
                                    ),
                                    "instance"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(9, 1, 10),
                                        new SourcePosition(10, 1, 11),
                                        "of"
                                    ),
                                    "of"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(19, 1, 20),
                                        new SourcePosition(20, 1, 21),
                                        "as"
                                    ),
                                    "as"
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
                                                        new StringValueAst.Builder {
                                                            StringLiteralValues = new List<StringLiteralToken> {
                                                                new StringLiteralToken(
                                                                    new SourceExtent(
                                                                        new SourcePosition(60, 3, 20),
                                                                        new SourcePosition(73, 3, 33),
                                                                        "\"https://URL1\""
                                                                    ),
                                                                    "https://URL1"
                                                                )
                                                            },
                                                            Value = "https://URL1"
                                                        }.Build(),
                                                        new StringValueAst.Builder {
                                                            StringLiteralValues = new List<StringLiteralToken> {
                                                                new StringLiteralToken(
                                                                    new SourceExtent(
                                                                        new SourcePosition(76, 3, 36),
                                                                        new SourcePosition(89, 3, 49),
                                                                        "\"https://URL2\""
                                                                    ),
                                                                    "https://URL2"
                                                                )
                                                            },
                                                            Value = "https://URL2"
                                                        }.Build()
                                                    }
                                                )
                                            )}
                                        }
                                    )
                                ),
                                new StatementEndToken(
                                    new SourceExtent(
                                        new SourcePosition(96, 4, 2),
                                        new SourcePosition(96, 4, 2),
                                        ";"
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
            public static void ParsePropetyValueArrayWithNumericLiteralValues()
            {
                var tokens = Lexing.Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    MyBinaryValue = 0101010b;\r\n" +
                        "    MyOctalValue = 0444444;\r\n" +
                        "    MyHexValue = 0xABC123;\r\n" +
                        "    MyDecimalValue = 12345;\r\n" +
                        "    MyRealValue = 123.45;\r\n" +
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
                                        new SourcePosition(0, 1, 1),
                                        new SourcePosition(7, 1, 8),
                                        "instance"
                                    ),
                                    "instance"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(9, 1, 10),
                                        new SourcePosition(10, 1, 11),
                                        "of"
                                    ),
                                    "of"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(12, 1, 13),
                                        new SourcePosition(17, 1, 18),
                                        "myType"
                                    ),
                                    "myType"
                                ),
                                new IdentifierToken(
                                    new SourceExtent(
                                        new SourcePosition(19, 1, 20),
                                        new SourcePosition(20, 1, 21),
                                        "as"
                                    ),
                                    "as"
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
                                            { "MyBinaryValue", new IntegerValueAst(
                                                new IntegerLiteralToken(
                                                    new SourceExtent(
                                                        new SourcePosition(61, 3, 21),
                                                        new SourcePosition(68, 3, 28),
                                                        "0101010b"
                                                    ),
                                                    IntegerKind.BinaryValue, 0b101010
                                                )
                                            )},
                                            { "MyOctalValue", new IntegerValueAst(
                                                new IntegerLiteralToken(
                                                    new SourceExtent(
                                                        new SourcePosition(91, 4, 20),
                                                        new SourcePosition(97, 4, 26),
                                                        "0444444"
                                                    ),
                                                    IntegerKind.OctalValue, Convert.ToInt32("444444", 8)
                                                )
                                            )},
                                            { "MyHexValue", new IntegerValueAst(
                                                new IntegerLiteralToken(
                                                    new SourceExtent(
                                                        new SourcePosition(118, 5, 18),
                                                        new SourcePosition(125, 5, 25),
                                                        "0xABC123"
                                                    ),
                                                    IntegerKind.HexValue, 0xABC123
                                                )
                                            )},
                                            { "MyDecimalValue", new IntegerValueAst(
                                                new IntegerLiteralToken(
                                                    new SourceExtent(
                                                        new SourcePosition(150, 6, 22),
                                                        new SourcePosition(154, 6, 26),
                                                        "12345"
                                                    ),
                                                    IntegerKind.DecimalValue, 12345
                                                )
                                            )},
                                            { "MyRealValue", new RealValueAst(
                                                new RealLiteralToken(
                                                    new SourceExtent(
                                                        new SourcePosition(150, 6, 22),
                                                        new SourcePosition(154, 6, 26),
                                                        "123.45"
                                                    ),
                                                    123.45
                                                )
                                            )}
                                        }
                                    )
                                ),
                                new StatementEndToken(
                                    new SourceExtent(
                                        new SourcePosition(186, 8, 2),
                                        new SourcePosition(186, 8, 2),
                                        ";"
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
            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("Parsing\\WMI\\WinXp");
                }
            }
        }

        [TestFixture]
        public static class ParseMethodGolfExamples
        {
            [Test, TestCaseSource(typeof(ParseMethodGolfExamples), "GetTestCases")]
            public static void ParseMethodTestsFromDisk(string mofFilename)
            {
                ParserTests.ParseMethodTest(mofFilename);
            }
            public static IEnumerable<TestCaseData> GetTestCases
            {
                get
                {
                    return TestUtils.GetMofTestCase("Parsing\\DSP0221_3.0.1");
                }
            }
        }

        private static void ParseMethodTest(string mofFilename)
        {
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

    }

}

using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Models;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Helpers;
using Kingsland.ParseFx.Text;
using NUnit.Framework;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.UnitTests.Parsing;

public static partial class ParserTests
{

    #region 7.5.9 Complex type value

    public static class PropertyValueTests
    {

        [Test]
        public static void ParsePropertyValueWithLiteralString()
        {
            var tokens = TestUtils.RemoveExtents(
                Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias0000006E\r\n" +
                        "{\r\n" +
                        "    ServerURL = \"https://URL\";\r\n" +
                        "};"
                    )
                )
            );
            var actualAst = Parser.Parse(tokens);
            var expectedAst = new MofSpecificationAst(
                new List<MofProductionAst> {
                    new InstanceValueDeclarationAst(
                        new IdentifierToken("instance"),
                        new IdentifierToken("of"),
                        new IdentifierToken("myType"),
                        new IdentifierToken("as"),
                        new AliasIdentifierToken("Alias0000006E"),
                        new PropertyValueListAst(
                            new List<PropertySlotAst> {
                                new PropertySlotAst(
                                    new IdentifierToken("ServerURL"),
                                    new StringValueAst.Builder {
                                        StringLiteralValues = new List<StringLiteralToken> {
                                            new StringLiteralToken("https://URL")
                                        },
                                        Value = "https://URL"
                                    }.Build()
                                )
                            }
                        ),
                        new StatementEndToken()
                    )
                }
            );
            var actualJson = TestUtils.ConvertToJson(actualAst);
            var expectedJson = TestUtils.ConvertToJson(expectedAst);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            var actualModule = ModelConverter.ConvertMofSpecificationAst(actualAst);
            var expectedModule = new Module.Builder
            {
                Instances = new List<Instance> {
                    new Instance.Builder {
                        TypeName = "myType",
                        Alias = "Alias0000006E",
                        Properties = new List<Property> {
                            new Property("ServerURL", "https://URL")
                        }
                    }.Build()
                }
            }.Build();
            ModelAssert.AreEqual(expectedModule, actualModule);
        }

        [Test]
        public static void ParsePropertyValueWithAliasIdentifier()
        {
            var tokens = TestUtils.RemoveExtents(
                Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    Reference = $Alias0000006E;\r\n" +
                        "};"
                    )
                )
            );
            var actualAst = Parser.Parse(tokens);
            var expectedAst = new MofSpecificationAst(
                new ReadOnlyCollection<MofProductionAst>(
                    new List<MofProductionAst> {
                        new InstanceValueDeclarationAst(
                            new IdentifierToken("instance"),
                            new IdentifierToken("of"),
                            new IdentifierToken("myType"),
                            new IdentifierToken("as"),
                            new AliasIdentifierToken("Alias00000070"),
                            new PropertyValueListAst(
                                new List<PropertySlotAst> {
                                    new PropertySlotAst(
                                        new IdentifierToken("Reference"),
                                        new ComplexValueAst(
                                            new AliasIdentifierToken("Alias0000006E")
                                        )
                                    )
                                }
                            ),
                            new StatementEndToken()
                        )
                    }
                )
            );
            var actualJson = TestUtils.ConvertToJson(actualAst);
            var expectedJson = TestUtils.ConvertToJson(expectedAst);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            var actualModule = ModelConverter.ConvertMofSpecificationAst(actualAst);
            var expectedModule = new Module.Builder
            {
                Instances = new List<Instance> {
                    new Instance.Builder {
                        TypeName = "myType",
                        Alias = "Alias00000070",
                        Properties = new List<Property> {
                            new Property("Reference", "Alias0000006E")
                        }
                    }.Build()
                }
            }.Build();
            ModelAssert.AreEqual(expectedModule, actualModule);
        }

        [Test]
        public static void ParsePropertyValueWithEmptyArray()
        {
            var tokens = TestUtils.RemoveExtents(
                Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    Reference = {};\r\n" +
                        "};"
                    )
                )
            );
            var actualAst = Parser.Parse(tokens);
            var expectedAst = new MofSpecificationAst(
                new ReadOnlyCollection<MofProductionAst>(
                    new List<MofProductionAst> {
                        new InstanceValueDeclarationAst(
                            new IdentifierToken("instance"),
                            new IdentifierToken("of"),
                            new IdentifierToken("myType"),
                            new IdentifierToken("as"),
                            new AliasIdentifierToken("Alias00000070"),
                            new PropertyValueListAst(
                                new List<PropertySlotAst> {
                                    new PropertySlotAst(
                                       new IdentifierToken("Reference"),
                                       new LiteralValueArrayAst()
                                    )
                                }
                            ),
                            new StatementEndToken()
                        )
                    }
                )
            );
            var actualJson = TestUtils.ConvertToJson(actualAst);
            var expectedJson = TestUtils.ConvertToJson(expectedAst);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            var actualModule = ModelConverter.ConvertMofSpecificationAst(actualAst);
            var expectedModule = new Module.Builder {
                Instances = new List<Instance> {
                    new Instance.Builder {
                        TypeName = "myType",
                        Alias = "Alias00000070",
                        Properties = new List<Property> {
                            new Property("Reference", new List<object>())
                        }
                    }.Build()
                }
            }.Build();
            ModelAssert.AreEqual(expectedModule, actualModule);
        }

        [Test]
        public static void ParsePropertyValueArrayWithAliasIdentifier()
        {
            var tokens = TestUtils.RemoveExtents(
                Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    Reference = {$Alias0000006E};\r\n" +
                        "};"
                    )
                )
            );
            var actualAst = Parser.Parse(tokens);
            var expectedAst = new MofSpecificationAst(
                new List<MofProductionAst> {
                    new InstanceValueDeclarationAst(
                        new IdentifierToken("instance"),
                        new IdentifierToken("of"),
                        new IdentifierToken("myType"),
                        new IdentifierToken("as"),
                        new AliasIdentifierToken("Alias00000070"),
                        new PropertyValueListAst(
                            new List<PropertySlotAst> {
                                new PropertySlotAst(
                                    new("Reference"),
                                    new ComplexValueArrayAst(
                                        new List<ComplexValueAst> {
                                            new ComplexValueAst(
                                                new AliasIdentifierToken(
                                                    "Alias0000006E"
                                                )
                                            )
                                        }
                                    )
                                )
                            }
                        ),
                        new StatementEndToken()
                    )
                }
            );
            var actualJson = TestUtils.ConvertToJson(actualAst);
            var expectedJson = TestUtils.ConvertToJson(expectedAst);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            var actualModule = ModelConverter.ConvertMofSpecificationAst(actualAst);
            var expectedModule = new Module.Builder
            {
                Instances = new List<Instance> {
                    new Instance.Builder
                    {
                        TypeName = "myType",
                        Alias = "Alias00000070",
                        Properties = new List<Property> {
                            new Property("Reference", new List<object> {
                                "Alias0000006E"
                            })
                        }
                    }.Build()
                }
            }.Build();
            ModelAssert.AreEqual(expectedModule, actualModule);
        }

        [Test]
        public static void ParsePropertyValueArrayWithLiteralStrings()
        {
            var tokens = TestUtils.RemoveExtents(
                Lexer.Lex(
                    SourceReader.From(
                        "instance of myType as $Alias00000070\r\n" +
                        "{\r\n" +
                        "    ServerURLs = { \"https://URL1\", \"https://URL2\" };\r\n" +
                        "};"
                    )
                )
            );
            var actualAst = Parser.Parse(tokens);
            var expectedAst = new MofSpecificationAst(
                new ReadOnlyCollection<MofProductionAst>(
                    new List<MofProductionAst> {
                        new InstanceValueDeclarationAst(
                            new IdentifierToken("instance"),
                            new IdentifierToken("of"),
                            new IdentifierToken("myType"),
                            new IdentifierToken("as"),
                            new AliasIdentifierToken("Alias00000070"),
                            new PropertyValueListAst(
                                new List<PropertySlotAst> {
                                    new PropertySlotAst(
                                        new IdentifierToken("ServerURLs"),
                                        new LiteralValueArrayAst(
                                            new List<LiteralValueAst> {
                                                new StringValueAst.Builder {
                                                    StringLiteralValues = new List<StringLiteralToken> {
                                                        new StringLiteralToken("https://URL1")
                                                    },
                                                    Value = "https://URL1"
                                                }.Build(),
                                                new StringValueAst.Builder {
                                                    StringLiteralValues = new List<StringLiteralToken> {
                                                        new StringLiteralToken("https://URL2")
                                                    },
                                                    Value = "https://URL2"
                                                }.Build()
                                            }
                                        )
                                    )
                                }
                            ),
                            new StatementEndToken()
                        )
                    }
                )
            );
            var actualJson = TestUtils.ConvertToJson(actualAst);
            var expectedJson = TestUtils.ConvertToJson(expectedAst);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            var actualModule = ModelConverter.ConvertMofSpecificationAst(actualAst);
            var expectedModule = new Module.Builder {
                Instances = new List<Instance> {
                    new Instance.Builder {
                        TypeName = "myType",
                        Alias = "Alias00000070",
                        Properties = new List<Property> {
                            new Property("ServerURLs", new List<object> {
                                "https://URL1", "https://URL2"
                            })
                        }
                    }.Build()
                }
            }.Build();
            ModelAssert.AreEqual(expectedModule, actualModule);
        }

        [Test]
        public static void ParsePropertyValueArrayWithNumericLiteralValues()
        {
            var tokens = TestUtils.RemoveExtents(
                Lexer.Lex(
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
                )
            );
            var actualAst = Parser.Parse(tokens);
            var expectedAst = new MofSpecificationAst(
                new ReadOnlyCollection<MofProductionAst>(
                    new List<MofProductionAst> {
                        new InstanceValueDeclarationAst(
                            new IdentifierToken("instance"),
                            new IdentifierToken("of"),
                            new IdentifierToken("myType"),
                            new IdentifierToken("as"),
                            new AliasIdentifierToken("Alias00000070"),
                            new PropertyValueListAst(
                                new List<PropertySlotAst> {
                                    new PropertySlotAst(
                                        new IdentifierToken("MyBinaryValue"),
                                        new IntegerValueAst(
                                            new IntegerLiteralToken(
                                                IntegerKind.BinaryValue, 0b101010
                                            )
                                        )
                                    ),
                                    new(
                                        new("MyOctalValue"),
                                        new IntegerValueAst(
                                            new IntegerLiteralToken(
                                                IntegerKind.OctalValue, Convert.ToInt32("444444", 8)
                                            )
                                        )
                                    ),
                                    new(
                                        new("MyHexValue"),
                                        new IntegerValueAst(
                                            new IntegerLiteralToken(
                                                IntegerKind.HexValue, 0xABC123
                                            )
                                        )
                                    ),
                                    new(
                                        new("MyDecimalValue"),
                                        new IntegerValueAst(
                                            new IntegerLiteralToken(
                                                IntegerKind.DecimalValue, 12345
                                            )
                                        )
                                    ),
                                    new(
                                        new("MyRealValue"),
                                        new RealValueAst(
                                            new RealLiteralToken(
                                                123.45
                                            )
                                        )
                                    )
                                }
                            ),
                            new StatementEndToken()
                        )
                    }
                )
            );
            var actualJson = TestUtils.ConvertToJson(actualAst);
            var expectedJson = TestUtils.ConvertToJson(expectedAst);
            Assert.That(actualJson, Is.EqualTo(expectedJson));
            var actualModule = ModelConverter.ConvertMofSpecificationAst(actualAst);
            var expectedModule = new Module.Builder
            {
                Instances = new List<Instance> {
                    new Instance.Builder {
                        TypeName = "myType",
                        Alias = "Alias00000070",
                        Properties = new List<Property> {
                            new Property("MyBinaryValue", 42),
                            new Property("MyOctalValue", 149796),
                            new Property("MyHexValue", 11256099),
                            new Property("MyDecimalValue", 12345),
                            new Property("MyRealValue", 123.45)
                        }
                    }.Build()
                }
            }.Build();
            ModelAssert.AreEqual(expectedModule, actualModule);
        }

    }

    #endregion

}

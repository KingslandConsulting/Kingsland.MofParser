using Kingsland.MofParser.Models;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.UnitTests.Helpers;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Model;

[TestFixture]
public static class ModelConverter
{

    [TestFixture]
    public static class ConvertMofSpecificationAstTests
    {

        [Test]
        public static void InstanceWithLiteralValuePropertiesShouldConvert()
        {
            const string sourceText = @"
                instance of MyClass as $MyAlias
                {
                    IntegerProperty = 100;
                    RealProperty    = 10.5;
                    BooleanProperty = true;
                    NullProperty    = null;
                    StringProperty  = ""MyStringValue"";
                };
            ";
            var actual = Parser.ParseText(sourceText);
            var expected = new Module(
                instances:
                [
                    new(
                        typeName: "MyClass",
                        alias: "MyAlias",
                        properties: new List<Property>
                        {
                            new("IntegerProperty", 100),
                            new("RealProperty", 10.5),
                            new("BooleanProperty", true),
                            new("NullProperty", null),
                            new("StringProperty", "MyStringValue"),
                        }
                    )
                ]
            );
            ModelAssert.AreEqual(expected, actual);
        }

        [Test]
        public static void InstanceWithLiteralValueArrayPropertiesShouldConvert()
        {
            const string sourceText = @"
                instance of MyClass as $MyAlias
                {
                    EmptyProperty   = { };
                    IntegerProperty = { 100, 200, 300 };
                    RealProperty    = { 10.5, 20.75, 30.99 };
                    BooleanProperty = { true, false, true, false };
                    NullProperty    = { null, null, null };
                    StringProperty  = { ""MyStringValue"", ""MyOtherStringValue"" };
                };
            ";
            var actual = Parser.ParseText(sourceText);
            var expected = new Module(
                instances:
                [
                    new(
                        typeName: "MyClass",
                        alias: "MyAlias",
                        properties:
                        [
                            new("EmptyProperty", (int[])[]),
                            new("IntegerProperty", (int[])[100, 200, 300]),
                            new("RealProperty", (double[])[10.5, 20.75, 30.99]),
                            new("BooleanProperty", (bool[])[true, false, true, false]),
                            new("NullProperty", (object?[])[null, null, null]),
                            new("StringProperty", (string[])["MyStringValue", "MyOtherStringValue"]),
                        ]
                    )
                ]
            );
            ModelAssert.AreEqual(expected, actual);
        }

    }

}

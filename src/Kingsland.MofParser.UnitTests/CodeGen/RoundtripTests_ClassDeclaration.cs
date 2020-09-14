using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.2 Class declaration

        public static class ClassDeclarationTests
        {

            [Test]
            public static void EmptyClassDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ClassDeclarationWithSuperclassShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base : GOLF_Superclass\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ClassDeclarationWithClassFeaturesShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tstring InstanceID;\r\n" +
                    "\tstring Caption = Null;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ClassDeclarationsWithQualifierListShouldRoundtrip()
            {
                var sourceText =
                    "[Abstract, OCL{\"-- the key property cannot be NULL\", \"inv: InstanceId.size() = 10\"}]\r\n" +
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\t[Description(\"an instance of a class that derives from the GOLF_Base class. \"), Key] string InstanceID;\r\n" +
                    "\t[Description(\"A short textual description (one- line string) of the\"), MaxLen(64)] string Caption = Null;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            //[Test]
            //public static void ClassDeclarationsAstWithNumericPropertiesShouldRoundtrip()
            //{
            //    RoundtripTests.AssertRoundtrip(
            //        "instance of myType as $Alias00000070\r\n" +
            //        "{\r\n" +
            //        "\tMyBinaryValue = 0101010b;\r\n" +
            //        "\tMyOctalValue = 0444444;\r\n" +
            //        "\tMyHexValue = 0xABC123;\r\n" +
            //        "\tMyDecimalValue = 12345;\r\n" +
            //        "\tMyRealValue = 123.45;\r\n" +
            //        "};"
            //    );
            //}

        }

        #endregion

    }

}
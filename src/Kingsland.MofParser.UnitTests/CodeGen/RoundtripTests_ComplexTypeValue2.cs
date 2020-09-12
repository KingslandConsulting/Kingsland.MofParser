using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.2 Complex type value

        public static class InstanceValueDeclarationTests
        {

            [Test]
            public static void InstanceValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void InstanceValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};"
                );
            }

            [Test]
            public static void InstanceValueDeclarationWithAliasShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

        }

        public static class StructureValueDeclarationTests
        {

            [Test]
            public static void StructureValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StructureValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};"
                );
            }

        }

        //[Test]
        //public static void InstanceValueDeclarationShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "};"
        //    );
        //}

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

        //[Test(Description = "https://github.com/mikeclayton/MofParser/issues/26"),
        // Ignore("https://github.com/mikeclayton/MofParser/issues/26")]
        //public static void InstanceValueDeclarationsWithInstanceValuePropertyShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tLastPaymentDate = instance of GOLF_Date\r\n" +
        //        "\t{\r\n" +
        //        "\tYear = 2011;\r\n" +
        //        "\tMonth = July;\r\n" +
        //        "\tDay = 31;\r\n" +
        //        "\t};\r\n" +
        //        "}";
        //    );
        //}

        //[Test]
        //public static void InstanceValueDeclarationWithStructureValueDeclarationPropertyShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "instance of GOLF_ClubMember\r\n" +
        //        "{\r\n" +
        //        "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
        //        "\tMemberAddress = value of GOLF_Address\r\n" +
        //        "\t{\r\n" +
        //        "\t\tState = \"IL\";\r\n" +
        //        "\t\tCity = \"Oak Park\";\r\n" +
        //        "\t\tStreet = \"Oak Park Av.\";\r\n" +
        //        "\t\tStreetNo = \"1177\";\r\n" +
        //        "\t\tApartmentNo = \"3B\";\r\n" +
        //        "\t};\r\n" +
        //        "};";
        //    );
        //}

        //[Test]
        //public static void StructureValueDeclarationShouldRoundtrip()
        //{
        //    RoundtripTests.AssertRoundtrip(
        //        "value of GOLF_PhoneNumber as $JohnDoesPhoneNo\r\n" +
        //        "{\r\n" +
        //        "\tAreaCode = {\"9\", \"0\", \"7\"};\r\n" +
        //        "\tNumber = {\"7\", \"4\", \"7\", \"4\", \"8\", \"8\", \"4\"};\r\n" +
        //        "};";
        //    );
        //}

        #endregion

    }

}
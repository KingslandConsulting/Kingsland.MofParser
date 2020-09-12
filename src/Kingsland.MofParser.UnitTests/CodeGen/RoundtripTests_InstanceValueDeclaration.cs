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
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void InstanceValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void InstanceValueDeclarationWithAliasShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
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

        }

        #endregion

    }

}
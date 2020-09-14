using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.2 Complex type value

        public static class StructureValueDeclarationTests
        {

            [Test]
            public static void StructureValueDeclarationWithNoPropertiesShouldRoundtrip()
            {
                var sourceText =
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void StructureValueDeclarationWithChildPropertiesShouldRoundtrip()
            {
                var sourceText =
                    "value of GOLF_ClubMember as $MyAliasIdentifier\r\n" +
                    "{\r\n" +
                    "\tFirstName = \"John\";\r\n" +
                    "\tLastName = \"Doe\";\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

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

        }

        #endregion

    }

}
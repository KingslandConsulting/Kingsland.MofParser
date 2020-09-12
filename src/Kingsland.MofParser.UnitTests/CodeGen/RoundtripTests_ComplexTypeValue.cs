using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.9 Complex type value

        public static class ComplexTypeValueTests
        {

            [Test]
            public static void ComplexTypeValueWithComplexValuePropertyShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ComplexTypeValueWithComplexValueArrayPropertyShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};"
                );
            }

        }

        public static class ComplexValueTests
        {

            [Test]
            public static void ComplexValuePropertyShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ComplexValuePropertyWithValueOfShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = value of GOLF_Date\r\n" +
                    "\t{\r\n" +
                    "\t\tMonth = July;\r\n" +
                    "\t};\r\n" +
                    "};"
                );
            }

        }

        public static class ComplexValueArrayTests
        {

            [Test]
            public static void ComplexValueArrayWithOneItemShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void ComplexValueArrayWithMultipleItemsShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}
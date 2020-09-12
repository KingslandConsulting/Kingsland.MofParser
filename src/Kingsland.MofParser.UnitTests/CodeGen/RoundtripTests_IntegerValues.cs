using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1.1 Integer values

        public static class IntegerValueTests
        {

            [Test]
            public static void IntegerValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 100;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void PositiveIntegerValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +100;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void NegativeIntegerValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -100;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void IntegerValuePropertiesInOtherBasesShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of myType as $Alias00000070\r\n" +
                    "{\r\n" +
                    "\tMyBinaryValue1 = 0101010b;\r\n" +
                    "\tMyBinaryValue2 = +0101010b;\r\n" +
                    "\tMyBinaryValue3 = -0101010b;\r\n" +
                    "\tMyOctalValue1 = 000444444;\r\n" +
                    "\tMyOctalValue2 = +000444444;\r\n" +
                    "\tMyOctalValue3 = -000444444;\r\n" +
                    "\tMyHexValue1 = 0x00ABC123;\r\n" +
                    "\tMyHexValue2 = +0x00ABC123;\r\n" +
                    "\tMyHexValue3 = -0x00ABC123;\r\n" +
                    "\tMyDecimalValue1 = 12345;\r\n" +
                    "\tMyDecimalValue2 = +12345;\r\n" +
                    "\tMyDecimalValue3 = -12345;\r\n" +
                    "\tMyRealValue1 = 00123.45;\r\n" +
                    "\tMyRealValue2 = +00123.45;\r\n" +
                    "\tMyRealValue3 = -123.45;\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}
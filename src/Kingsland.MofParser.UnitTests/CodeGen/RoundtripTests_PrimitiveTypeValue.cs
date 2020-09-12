using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1 Primitive type value

        public static class LiteralValueArrayTests
        {

            [Test]
            public static void LiteralValueArrayWithOneItemShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1};\r\n" +
                    "};"
                );
            }

            [Test]
            public static void LiteralValueArrayWithMultipleItemsShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {1, 2};\r\n" +
                    "};"
                );
            }

        }

        public static class LiteralValueTests
        {

            [Test]
            public static void IntegerLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 1;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void RealLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 0.5;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void BooleanLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = true;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void NullLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = null;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void StringLiteralValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = \"aaa\";\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}
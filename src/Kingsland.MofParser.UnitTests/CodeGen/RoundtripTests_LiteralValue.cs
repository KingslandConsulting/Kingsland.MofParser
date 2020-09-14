using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1 Primitive type value

        public static class LiteralValueTests
        {

            [Test]
            public static void IntegerLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 1;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void RealLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = 0.5;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void BooleanLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = true;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void NullLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = null;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void StringLiteralValueShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = \"aaa\";\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
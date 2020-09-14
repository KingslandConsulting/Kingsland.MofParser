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

        #endregion

    }

}
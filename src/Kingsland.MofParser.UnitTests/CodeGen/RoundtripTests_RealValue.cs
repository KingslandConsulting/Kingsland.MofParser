using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1.1 Real values

        public static class RealValueTests
        {

            [Test]
            public static void RealValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.5;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void PositiveRealValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = +0.5;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void NegativeRealValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = -0.5;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoFractionShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 5.0;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithTrailingZerosShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = 0.50;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/xx")]
            public static void RealValueWithNoIntegerPartShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = .5;\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}
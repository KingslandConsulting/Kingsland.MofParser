using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.9 Complex type value

        public static class ComplexValueArrayTests
        {

            [Test]
            public static void ComplexValueArrayWithOneItemShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ComplexValueArrayWithMultipleItemsShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier, $MyOtherAliasIdentifier};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
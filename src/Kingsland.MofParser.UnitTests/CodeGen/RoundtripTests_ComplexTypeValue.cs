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
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = $MyAliasIdentifier;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ComplexTypeValueWithComplexValueArrayPropertyShouldRoundtrip()
            {
                var sourceText =
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tLastPaymentDate = {$MyAliasIdentifier};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
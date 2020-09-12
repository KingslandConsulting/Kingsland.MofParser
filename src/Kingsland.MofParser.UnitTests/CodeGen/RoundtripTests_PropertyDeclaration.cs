using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.5 Property declaration

        public static class PropertyDeclarationTests
        {

            [Test]
            public static void PropertyDeclarationShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity;\r\n" +
                    "};"
                );
            }

            [Test]
            public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity[];\r\n" +
                    "};"
                );
            }

            [Test]
            public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity = 0;\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
            public static void PropertyDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tuint8 SeverityUint8;\r\n" +
                    "\tuint16 SeverityUint16;\r\n" +
                    "\tuint32 SeverityUint32;\r\n" +
                    "\tuint64 SeverityUint64;\r\n" +
                    "\tsint8 SeveritySint8;\r\n" +
                    "\tsint16 SeveritySint16;\r\n" +
                    "\tsint32 SeveritySint32;\r\n" +
                    "\tsint64 SeveritySint64;\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}
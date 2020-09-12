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
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity[];\r\n" +
                    "};";
                 RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
            {
                var sourceText =
                    "class GOLF_Base\r\n" +
                    "{\r\n" +
                    "\tInteger Severity = 0;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
            public static void PropertyDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
            {
                var sourceText =
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
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
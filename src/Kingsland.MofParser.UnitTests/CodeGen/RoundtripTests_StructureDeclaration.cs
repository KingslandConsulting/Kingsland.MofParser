using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.1 Structure declaration

        public static class StructureDeclarationTests
        {

            [Test]
            public static void EmptyStructureDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void StructureDeclarationWithSuperstructureShouldRoundtrip()
            {
                var sourceText =
                    "structure Sponsor : GOLF_MySupestructure\r\n" +
                    "{\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void StructureDeclarationWithStructureFeaturesShouldRoundtrip()
            {
                var sourceText =
                    "structure Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "\tGOLF_Date ContractSignedDate;\r\n" +
                    "\treal32 ContractAmount;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
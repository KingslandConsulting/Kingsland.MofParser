using Kingsland.MofParser.Tokens;
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
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

            [Test]
            public static void StructureDeclarationWithSuperstructureShouldRoundtrip()
            {
                var sourceText =
                    "structure Sponsor : GOLF_MySupestructure\r\n" +
                    "{\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor : GOLF_MySupestructure
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_MySupestructure")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
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
                var expectedTokens = new TokenBuilder()
                    // structure Sponsor : GOLF_MySupestructure
                    .IdentifierToken("structure")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Sponsor")
                    .WhitespaceToken("\r\n")
                    // {
                    .BlockOpenToken()
                    .WhitespaceToken("\r\n\t")
                    // string Name;
                    .IdentifierToken("string")
                    .WhitespaceToken(" ")
                    .IdentifierToken("Name")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // GOLF_Date ContractSignedDate;
                    .IdentifierToken("GOLF_Date")
                    .WhitespaceToken(" ")
                    .IdentifierToken("ContractSignedDate")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n\t")
                    // real32 ContractAmount;
                    .IdentifierToken("real32")
                    .WhitespaceToken(" ")
                    .IdentifierToken("ContractAmount")
                    .StatementEndToken()
                    .WhitespaceToken("\r\n")
                    // };
                    .BlockCloseToken()
                    .StatementEndToken()
                    .ToList();
                RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
            }

        }

        #endregion

    }

}
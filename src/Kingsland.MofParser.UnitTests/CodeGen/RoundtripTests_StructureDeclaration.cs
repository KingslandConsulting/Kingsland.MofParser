using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.1 Structure declaration

    public static class StructureDeclarationTests
    {

        [Test]
        public static void EmptyStructureDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                    structure Sponsor
                    {
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // structure Sponsor
                .IdentifierToken("structure")
                .WhitespaceToken(" ")
                .IdentifierToken("Sponsor")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void StructureDeclarationWithSuperstructureShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                    structure Sponsor : GOLF_MySupestructure
                    {
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // structure Sponsor : GOLF_MySupestructure
                .IdentifierToken("structure")
                .WhitespaceToken(" ")
                .IdentifierToken("Sponsor")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_MySupestructure")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void StructureDeclarationWithStructureFeaturesShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                    structure Sponsor
                    {
                        string Name;
                        GOLF_Date ContractSignedDate;
                        real32 ContractAmount;
                    };
                ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // structure Sponsor : GOLF_MySupestructure
                .IdentifierToken("structure")
                .WhitespaceToken(" ")
                .IdentifierToken("Sponsor")
                .WhitespaceToken($"{newline}")
                // {
                .BlockOpenToken()
                .WhitespaceToken($"{newline}{indent}")
                // string Name;
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("Name")
                .StatementEndToken()
                .WhitespaceToken($"{newline}{indent}")
                // GOLF_Date ContractSignedDate;
                .IdentifierToken("GOLF_Date")
                .WhitespaceToken(" ")
                .IdentifierToken("ContractSignedDate")
                .StatementEndToken()
                .WhitespaceToken($"{newline}{indent}")
                // real32 ContractAmount;
                .IdentifierToken("real32")
                .WhitespaceToken(" ")
                .IdentifierToken("ContractAmount")
                .StatementEndToken()
                .WhitespaceToken($"{newline}")
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

    }

    #endregion

}

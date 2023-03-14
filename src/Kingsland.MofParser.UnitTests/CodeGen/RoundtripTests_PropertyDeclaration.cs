using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.5.5 Property declaration

    public static class PropertyDeclarationTests
    {

        [Test]
        public static void PropertyDeclarationShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer Severity;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // class GOLF_Base
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Base")
               .WhitespaceToken(newline)
               // {
               .BlockOpenToken()
               .WhitespaceToken(newline + indent)
               //     Integer Severity;
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("Severity")
               .StatementEndToken()
               .WhitespaceToken(newline)
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void PropertyDeclarationWithArrayTypeShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer Severity[];
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // class GOLF_Base
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Base")
               .WhitespaceToken(newline)
               // {
               .BlockOpenToken()
               .WhitespaceToken(newline + indent)
               //     Integer Severity[];
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("Severity")
               .AttributeOpenToken()
               .AttributeCloseToken()
               .StatementEndToken()
               .WhitespaceToken(newline)
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void PropertyDeclarationWithDefaultValueShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer Severity = 0;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class GOLF_Base
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     Integer Severity = 0;
                .IdentifierToken("Integer")
                .WhitespaceToken(" ")
                .IdentifierToken("Severity")
                .WhitespaceToken(" ")
                .EqualsOperatorToken()
                .WhitespaceToken(" ")
                .IntegerLiteralToken(IntegerKind.DecimalValue, 0)
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test]
        public static void PropertyDeclarationWithRefTypeShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    Integer REF Severity = {1, 2, 3};
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
               // class GOLF_Base
               .IdentifierToken("class")
               .WhitespaceToken(" ")
               .IdentifierToken("GOLF_Base")
               .WhitespaceToken(newline)
               // {
               .BlockOpenToken()
               .WhitespaceToken(newline + indent)
               //     Integer REF Severity = {1, 2, 3};
               .IdentifierToken("Integer")
               .WhitespaceToken(" ")
               .IdentifierToken("REF")
               .WhitespaceToken(" ")
               .IdentifierToken("Severity")
               .WhitespaceToken(" ")
               .EqualsOperatorToken()
               .WhitespaceToken(" ")
               .BlockOpenToken()
               .IntegerLiteralToken(IntegerKind.DecimalValue, 1)
               .CommaToken()
               .WhitespaceToken(" ")
               .IntegerLiteralToken(IntegerKind.DecimalValue, 2)
               .CommaToken()
               .WhitespaceToken(" ")
               .IntegerLiteralToken(IntegerKind.DecimalValue, 3)
               .BlockCloseToken()
               .StatementEndToken()
               .WhitespaceToken(newline)
               // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

        [Test(Description = "https://github.com/mikeclayton/MofParser/issues/28")]
        public static void PropertyDeclarationWithDeprecatedMof300IntegerReturnTypesAndQuirksDisabledShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                class GOLF_Base
                {
                    uint8 SeverityUint8;
                    uint16 SeverityUint16;
                    uint32 SeverityUint32;
                    uint64 SeverityUint64;
                    sint8 SeveritySint8;
                    sint16 SeveritySint16;
                    sint32 SeveritySint32;
                    sint64 SeveritySint64;
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // class GOLF_Base
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("GOLF_Base")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     uint8 SeverityUint8;
                .IdentifierToken("uint8")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint8")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     uint16 SeverityUint16;
                .IdentifierToken("uint16")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint16")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     uint32 SeverityUint32;
                .IdentifierToken("uint32")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint32")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     uint64 SeverityUint64;
                .IdentifierToken("uint64")
                .WhitespaceToken(" ")
                .IdentifierToken("SeverityUint64")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint8 SeveritySint8;
                .IdentifierToken("sint8")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint8")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint16 SeveritySint16;
                .IdentifierToken("sint16")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint16")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint32 SeveritySint32;
                .IdentifierToken("sint32")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint32")
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     sint64 SeveritySint64;
                .IdentifierToken("sint64")
                .WhitespaceToken(" ")
                .IdentifierToken("SeveritySint64")
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
               .BlockCloseToken()
               .StatementEndToken()
               .ToList();
            RoundtripTests.AssertRoundtrip(sourceText, expectedTokens);
        }

    }

    #endregion

}

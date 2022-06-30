using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.UnitTests.Extensions;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen;

public static partial class RoundtripTests
{

    #region 7.4.1 QualifierList

    public static class QualifierValueTests
    {

        [Test]
        public static void QualifierWithMofV2FlavorsAndQuirksEnabledShouldRoundtrip()
        {
            var newline = Environment.NewLine;
            var indent = "    ";
            var sourceText = @"
                [Locale(1033): ToInstance, UUID(""{BE46D060-7A7C-11d2-BC85-00104B2CF71C}""): ToInstance]
                class Win32_PrivilegesStatus : __ExtendedStatus
                {
                    [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesNotHeld[];
                    [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesRequired[];
                };
            ".TrimIndent(newline).TrimString(newline);
            var expectedTokens = new TokenBuilder()
                // [Locale(1033): ToInstance, UUID(""{BE46D060-7A7C-11d2-BC85-00104B2CF71C}""): ToInstance]
                .AttributeOpenToken()
                .IdentifierToken("Locale")
                .ParenthesisOpenToken()
                .IntegerLiteralToken(IntegerKind.DecimalValue, 1033)
                .ParenthesisCloseToken()
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("ToInstance")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("UUID")
                .ParenthesisOpenToken()
                .StringLiteralToken("{BE46D060-7A7C-11d2-BC85-00104B2CF71C}")
                .ParenthesisCloseToken()
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("ToInstance")
                .AttributeCloseToken()
                .WhitespaceToken(newline)
                // class Win32_PrivilegesStatus : __ExtendedStatus
                .IdentifierToken("class")
                .WhitespaceToken(" ")
                .IdentifierToken("Win32_PrivilegesStatus")
                .WhitespaceToken(" ")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("__ExtendedStatus")
                .WhitespaceToken(newline)
                // {
                .BlockOpenToken()
                .WhitespaceToken(newline + indent)
                //     [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesNotHeld[];
                .AttributeOpenToken()
                .IdentifierToken("read")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("ToSubClass")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("MappingStrings")
                .BlockOpenToken()
                .StringLiteralToken("Win32API|AccessControl|Windows NT Privileges")
                .BlockCloseToken()
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("ToSubClass")
                .AttributeCloseToken()
                .WhitespaceToken(" ")
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("PrivilegesNotHeld")
                .AttributeOpenToken()
                .AttributeCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline + indent)
                //     [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesRequired[];
                .AttributeOpenToken()
                .IdentifierToken("read")
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("ToSubClass")
                .CommaToken()
                .WhitespaceToken(" ")
                .IdentifierToken("MappingStrings")
                .BlockOpenToken()
                .StringLiteralToken("Win32API|AccessControl|Windows NT Privileges")
                .BlockCloseToken()
                .ColonToken()
                .WhitespaceToken(" ")
                .IdentifierToken("ToSubClass")
                .AttributeCloseToken()
                .WhitespaceToken(" ")
                .IdentifierToken("string")
                .WhitespaceToken(" ")
                .IdentifierToken("PrivilegesRequired")
                .AttributeOpenToken()
                .AttributeCloseToken()
                .StatementEndToken()
                .WhitespaceToken(newline)
                // };
                .BlockCloseToken()
                .StatementEndToken()
                .ToList();
            RoundtripTests.AssertRoundtrip(
                sourceText,
                expectedTokens,
                null,
                null,
                ParserQuirks.AllowMofV2Qualifiers
            );
        }

        [Test]
        public static void QualifierWithMofV2FlavorsAndQuirksDisabledShouldThrow()
        {
            var newline = Environment.NewLine;
            var sourceText = @"
                [Locale(1033): ToInstance, UUID(""{BE46D060-7A7C-11d2-BC85-00104B2CF71C}""): ToInstance]
                class Win32_PrivilegesStatus : __ExtendedStatus
                {
                   [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesNotHeld[];
                   [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesRequired[];
                };
            ".TrimIndent(newline).TrimString(newline);
            var errorline = 1;
            var expectedMessage = @$"
                Unexpected token found at Position {13 + (errorline - 1) * newline.Length}, Line Number {errorline}, Column Number 14.
                Token Type: 'ColonToken'
                Token Text: ':'
            ".TrimIndent(newline).TrimString(newline);
            RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
        }

    }

    #endregion

}

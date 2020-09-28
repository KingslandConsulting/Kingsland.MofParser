using Kingsland.MofParser.Tokens;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.4 Qualifiers

        public static class QualifierTests
        {

            [Test]
            public static void QualifierShouldRoundtrip()
            {
                var sourceText =
                    "[Description(\"Instances of this class represent golf clubs. A golf club is \" \"an organization that provides member services to golf players \" \"both amateur and professional.\")]\r\n" +
                    "class GOLF_Club : GOLF_Base\r\n" +
                    "{\r\n" +
                    "};";
                var expectedTokens = new TokenBuilder()
                    // [Description("Instances of this class represent golf clubs. A golf club is " "an organization that provides member services to golf players " "both amateur and professional.")]
                    .AttributeOpenToken()
                    .IdentifierToken("Description")
                    .ParenthesisOpenToken()
                    .StringLiteralToken("Instances of this class represent golf clubs. A golf club is ")
                    .WhitespaceToken(" ")
                    .StringLiteralToken("an organization that provides member services to golf players ")
                    .WhitespaceToken(" ")
                    .StringLiteralToken("both amateur and professional.")
                    .ParenthesisCloseToken()
                    .AttributeCloseToken()
                    .WhitespaceToken("\r\n")
                    // class GOLF_Club : GOLF_Base\r\n
                    .IdentifierToken("class")
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Club")
                    .WhitespaceToken(" ")
                    .ColonToken()
                    .WhitespaceToken(" ")
                    .IdentifierToken("GOLF_Base")
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

        }

        #endregion

    }

}
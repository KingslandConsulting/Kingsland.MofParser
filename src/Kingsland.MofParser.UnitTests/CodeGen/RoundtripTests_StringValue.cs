using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Parsing;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Text;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.6.1.3 String values

        public static class StringValueTests
        {

            [Test]
            public static void SingleStringValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\";\r\n" +
                    "};"
                );
            }

            [Test]
            public static void MultistringValueShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance\" \"of\" \"John\" \"Doe\";\r\n" +
                    "};"
                );
            }

            [Test(Description = "https://github.com/mikeclayton/MofParser/issues/20")]
            public static void StringValueWithSingleQuoteShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "instance of GOLF_ClubMember\r\n" +
                    "{\r\n" +
                    "\tCaption = \"Instance of John Doe\\\'s GOLF_ClubMember object\";\r\n" +
                    "};"
                );
            }

        }

        #endregion

    }

}
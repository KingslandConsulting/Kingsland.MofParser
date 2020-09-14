using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.3 Compiler directives

        public static class CompilerDirectiveTests
        {

            [Test]
            public static void CompilerDirectiveShouldRoundtrip()
            {
                var sourceText =
                    "#pragma include (\"GlobalStructs/GOLF_Address.mof\")";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void CompilerDirectiveWithMultipleSingleStringsShouldRoundtrip()
            {
                var sourceText =
                    "#pragma include (\"GlobalStructs\" \"/\" \"GOLF_Address.mof\")";
                 RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
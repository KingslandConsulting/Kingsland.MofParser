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
                RoundtripTests.AssertRoundtrip(
                    "#pragma include (\"GlobalStructs/GOLF_Address.mof\")"
                );
            }

            [Test]
            public static void CompilerDirectiveWithMultipleSingleStringsShouldRoundtrip()
            {
                RoundtripTests.AssertRoundtrip(
                    "#pragma include (\"GlobalStructs\" \"/\" \"GOLF_Address.mof\")"
                );
            }

        }

        #endregion

    }

}
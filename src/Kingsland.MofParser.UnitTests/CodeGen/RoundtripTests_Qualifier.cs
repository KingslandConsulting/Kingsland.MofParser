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
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
using Kingsland.MofParser.Parsing;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.4.1 QualifierList

        public static class QualifierValueTests
        {

            [Test]
            public static void QualifierWithMofV2FlavorsAndQuirksEnabledShouldRoundtrip()
            {
                var sourceText =
                    "[Locale(1033): ToInstance, UUID(\"{BE46D060-7A7C-11d2-BC85-00104B2CF71C}\"): ToInstance]\r\n" +
                    "class Win32_PrivilegesStatus : __ExtendedStatus\r\n" +
                    "{\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(
                    sourceText,
                    null
                    ParserQuirks.AllowMofV2Qualifiers
                );
            }

            [Test]
            public static void QualifierWithMofV2FlavorsAndQuirksDisabledShouldThrow()
            {
                var sourceText =
                    "[Locale(1033): ToInstance, UUID(\"{BE46D060-7A7C-11d2-BC85-00104B2CF71C}\"): ToInstance]\r\n" +
                    "class Win32_PrivilegesStatus : __ExtendedStatus\r\n" +
                    "{\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];\r\n" +
                    "\t[read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];\r\n" +
                    "};";
                var expectedMessage =
                    "Unexpected token found at Position 13, Line Number 1, Column Number 14.\r\n" +
                    "Token Type: 'ColonToken'\r\n" +
                    "Token Text: ':'";
                RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
            }

        }

        #endregion

    }

}
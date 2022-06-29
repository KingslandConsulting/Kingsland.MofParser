using Kingsland.MofParser.Parsing;
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
            var sourceText = @"
                    [Locale(1033): ToInstance, UUID(""{BE46D060-7A7C-11d2-BC85-00104B2CF71C}""): ToInstance]
                    class Win32_PrivilegesStatus : __ExtendedStatus
                    {
                        [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesNotHeld[];
                        [read: ToSubClass, MappingStrings{""Win32API|AccessControl|Windows NT Privileges""}: ToSubClass] string PrivilegesRequired[];
                    };
                ".TrimIndent(newline).TrimString(newline);
            RoundtripTests.AssertRoundtrip(
                sourceText,
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

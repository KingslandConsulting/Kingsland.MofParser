﻿using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{

    public static partial class RoundtripTests
    {

        #region 7.5.2 Class declaration

        public static class ClassFeatureTests
        {

            [Test]
            public static void ClassFeatureWithQualifiersShouldRoundtrip()
            {
                var sourceText =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\t[Description(\"Monthly salary in $US\")] string Name;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void InvalidClassFeatureShouldThrow()
            {
                var sourceText =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\t100\r\n" +
                    "};";
                var expectedMessage =
                    "Unexpected token found at Position 19, Line Number 3, Column Number 2.\r\n" +
                    "Token Type: 'IntegerLiteralToken'\r\n" +
                    "Token Text: '100'";
                RoundtripTests.AssertRoundtripException(sourceText, expectedMessage);
            }

            [Test]
            public static void ClassFeatureWithStructureDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tstructure Nested\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ClassFeatureWithEnumerationDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tenumeration MonthsEnum : Integer\r\n" +
                    "\t{\r\n" +
                    "\t};\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

            [Test]
            public static void ClassFeatureWithPropertyDeclarationShouldRoundtrip()
            {
                var sourceText =
                    "class Sponsor\r\n" +
                    "{\r\n" +
                    "\tstring Name;\r\n" +
                    "};";
                RoundtripTests.AssertRoundtrip(sourceText);
            }

        }

        #endregion

    }

}
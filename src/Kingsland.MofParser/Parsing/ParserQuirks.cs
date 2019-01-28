﻿using System;

namespace Kingsland.MofParser.Parsing
{

    [Flags]
    public enum ParserQuirks
    {

        None = 0,

        /// <summary>
        /// Allows deprecated qualifier "flavors" to be used in MOF files.
        /// </summary>
        /// <remarks>
        ///
        /// See https://github.com/mikeclayton/MofParser/issues/49
        ///
        /// A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
        /// MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
        ///
        /// In MOF V2, the ColonToken separates qualifier "values" and "flavors", e.g:
        ///
        ///     [Locale(1033): ToInstance, UUID("{BE46D060-7A7C-11d2-BC85-00104B2CF71C}"): ToInstance]
        ///     class Win32_PrivilegesStatus : __ExtendedStatus
        ///     {
        ///         [read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];
        ///         [read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];
        ///     }
        ///
        /// but this is no longer valid in MOF V3.
        ///
        /// If this quirk is enabled we'll read qualifier flavors anyway for compatibility witn MOF V2 mof files.
        ///
        /// </remarks>
        AllowMofV2Qualifiers = 1,

        /// <summary>
        /// Allows deprecated qualifier "flavors" to be used in MOF files.
        /// </summary>
        /// <remarks>
        ///
        /// See https://github.com/mikeclayton/MofParser/issues/25
        ///
        /// Enum value arrays are defined in the MOF 3.0.1 spec as:
        ///
        ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
        ///
        /// but should *probably* be
        ///
        ///     enumValueArray = "{" [ enumValue *( "," enumValue ) ] "}"
        ///
        /// because this is invalid otherwise:
        ///
        ///     instance of MY_Class
        ///     {
        ///         Months = { MonthEnum.January, MonthEnum.April, MonthEnum.June, MonthEnum.September };
        ///     };
        ///
        /// If this quirk is enabled we'll assume enum array entries are enumValues instead of enumNames.
        ///
        /// </remarks>
        EnumValueArrayContainsEnumValuesNotEnumNames = 2

    }

}

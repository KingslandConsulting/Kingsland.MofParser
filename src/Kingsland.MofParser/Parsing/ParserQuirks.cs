using System;

namespace Kingsland.MofParser.Parsing
{

    [Flags]
    public enum ParserQuirks
    {

        None = 0,

        /// <summary>
        /// Allows deprecated MOF V2 qualifier "flavors" to be used in MOF files.
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
        AllowMofV2Qualifiers = 0x0001,

        /// <summary>
        /// Allows fully qualified enum namedto be iused in enum array values, e.g. "{ MonthEnum.January, MonthEnum.April}"
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
        EnumValueArrayContainsEnumValuesNotEnumNames = 0x0002,

        /// <summary>
        /// Allows use of deprecated integer types - int8, int16, uint8, uint16, etc
        /// </summary>
        /// <remarks>
        ///
        /// See https://github.com/mikeclayton/MofParser/issues/28
        ///
        /// The MOF Spec 3.0.1 deprecated a number of integer datatypes defined in MOF SPec 3.0.0:
        ///
        ///     // MOF Spec 3.0.0
        ///     DT_Integer         = DT_UnsignedInteger / DT_SignedInteger
        ///     DT_UnsignedInteger = "uint8" / "uint16" / "uint32"/ "uint64"
        ///     DT_SignedInteger   = "sint8" / "sint16" / "sint32" / "sint64"
        ///
        /// became
        ///
        ///     // MOF Spec 3.0.1
        ///     DT_Integer         = "integer"
        ///
        /// which means 'uint32' is valid as a base enum type in MOF 3.0.0 but not MOF 3.0.1
        ///
        ///     enum MyEnum : uint32
        ///     {
        ///     };
        ///
        /// If this quirk is enabled we'll allow deprectated integer types in enumeration declarations.
        ///
        /// </remarks>
        AllowDeprecatedMof300IntegerTypesAsEnumerationDeclarationsBase = 0x0004,

        /// <summary>
        /// Allows use of empty qualifier value arrays - e.g. "[ValueMap{}]".
        /// </summary>
        /// <remarks>
        ///
        /// See https://github.com/mikeclayton/MofParser/issues/51
        ///
        /// The MOF 3.0.1 spec doesn't allow for empty qualifier value arrays like
        /// "{}" in this property from the "MSMCAEvent_InvalidError" class in WinXpProSp3WMI.mof:
        ///
        ///	    [WmiDataId(3), ValueMap{}] uint32 Type;
        ///
        /// It's presumably allowed in earlier versions of the MOF spec, or maybe the
        /// System.Management.ManagementBaseObject.GetFormat method returns invalid MOF
        /// text for some classes, but we'll provide an option to allow or disallow empty
        /// arrays here...
        ///
        /// If this quirk is enabled we'll allow empty qualifier value arrays.
        ///
        /// </remarks>
        AllowEmptyQualifierValueArrays = 0x0008

    }

}

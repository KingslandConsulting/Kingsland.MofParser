namespace Kingsland.MofParser.Parsing
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// ANNEX B
    /// </remarks>
    public static class Keywords
    {

        // A.1 Value definitions
        public const string INSTANCE = "instance";
        public const string VALUE = "value";
        public const string AS = "as";
        public const string OF = "of";

        // A.3 Compiler directive
        public const string PRAGMA = "#pragma";
        public const string INCLUDE = "include";

        // A.4 Structure declaration
        public const string STRUCTURE = "structure";

        // A.5 Class declaration
        public const string CLASS = "class";
        public const string VOID = "void";

        // A.6 Association declaration
        public const string ASSOCIATION = "association";

        // A.7 Enumeration declaration
        public const string ENUMERATION = "enumeration";

        // A.8 Qualifier type declaration
        public const string SCOPE = "scope";
        public const string ANY = "any";
        public const string POLICY = "policy";
        public const string ENABLEOVERRIDE = "enableoverride";
        public const string DISABLEOVERRIDE = "disableoverride";
        public const string RESTRICTED = "restricted";
        public const string ENUMERATIONVALUE = "enumerationvalue";
        public const string PROPERTY = "property";
        public const string REFPROPERTY = "reference";
        public const string METHOD = "method";
        public const string PARAMETER = "parameter";
        public const string QUALIFIERTYPE = "qualifiertype";

        public const string FLAVOR = "flavor";
        public const string QUALIFIER = "qualifier";

        // A.15 Primitive data types
        public const string DT_UINT8 = "uint8";
        public const string DT_UINT16 = "uint16";
        public const string DT_UINT32 = "uint32";
        public const string DT_UINT64 = "uint64";
        public const string DT_SINT8 = "sint8";
        public const string DT_SINT16 = "sint16";
        public const string DT_SINT32 = "sint32";
        public const string DT_SINT64 = "sint64";
        public const string DT_REAL32 = "real32";
        public const string DT_REAL64 = "real64";
        public const string DT_STRING = "string";
        public const string DT_DATETIME = "datetime";
        public const string DT_BOOLEAN = "boolean";
        public const string DT_OCTECTSTRING = "octetstring";

        // A.16 Reference data type
        public const string REF = "ref";

        // A.17.6 Boolean value
        public const string FALSE = "false";
        public const string TRUE = "true";

        // A.17.7 Null value
        public const string NULL = "null";

    }

}

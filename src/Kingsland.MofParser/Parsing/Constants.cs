namespace Kingsland.MofParser.Parsing
{

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// </remarks>
    public static class Constants
    {

        // 7.3 Compiler directives
        public const string PRAGMA = "#pragma";
        public const string INCLUDE = "include";

        // 7.4 Qualifiers
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

        // 7.5.1 Structure declaration
        public const string STRUCTURE = "structure";

        // 7.5.2 Class declaration
        public const string CLASS = "class";
        public const string VOID = "void";

        // 7.5.3 Association declaration
        public const string ASSOCIATION = "association";

        // 7.5.4 Enumeration declaration
        public const string ENUMERATION = "enumeration";

        // 7.5.8 Primitive type declarations
        public const string DT_INTEGER = "integer";
        public const string DT_REAL32 = "real32";
        public const string DT_REAL64 = "real64";
        public const string DT_STRING = "string";
        public const string DT_DATETIME = "datetime";
        public const string DT_BOOLEAN = "boolean";
        public const string DT_OCTECTSTRING = "octetstring";

        // deprecated in MOF 3.0.1, but defined here to support parser quirks
        public const string DT_UINT8 = "uint8";
        public const string DT_UINT16 = "uint16";
        public const string DT_UINT32 = "uint32";
        public const string DT_UINT64 = "uint64";
        public const string DT_SINT8 = "sint8";
        public const string DT_SINT16 = "sint16";
        public const string DT_SINT32 = "sint32";
        public const string DT_SINT64 = "sint64";

        // 7.5.9 Complex type value
        public const string INSTANCE = "instance";
        public const string VALUE = "value";
        public const string AS = "as";
        public const string OF = "of";

        // 7.5.10 Reference type declaration
        public const string REF = "ref";

        // 7.6.1.3 String values

        public const char BACKSPACE_ESC = 'b';
        public const char TAB_ESC = 't';
        public const char LINEFEED_ESC = 'n';
        public const char FORMFEED_ESC = 'f';
        public const char CARRIAGERETURN_ESC = 'r';

        public const char BACKSLASH = '\x005C'; // \
        public const char DOUBLEQUOTE = '\x0022'; // "
        public const char SINGLEQUOTE = '\x0027'; // '
        //public const char UPPERALPHA = "\x0041...\x005A"; // A ... Z
        //public const char LOWERALPHA = "\x0061...\x007A"; // a ... z
        public const char UNDERSCORE = '\x005F'; //_


        // 7.6.1.5 Boolean value
        public const string FALSE = "false";
        public const string TRUE = "true";

        // 7.6.1.6 Null value
        public const string NULL = "null";

    }

}

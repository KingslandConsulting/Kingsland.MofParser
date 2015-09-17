using System;
using System.Linq;

namespace Kingsland.MofParser.Parsing
{

    /// <summary>
    /// http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
    /// </summary>
    internal static class NameValidator
    {

        #region Section 6.2.2 - Structure declaration

        #region structureName = ( IDENTIFIER / schemaQualifiedName )

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// structureName = ( IDENTIFIER / schemaQualifiedName ) 
        /// </remarks>
        public static bool IsStructureName(string value)
        {
            return StringValidator.IsIdentifier(value) ||
                   NameValidator.IsSchemaQualifiedName(value);
        }

        public static string ValidateStructureName(string value)
        {
            if (!NameValidator.IsStructureName(value))
            {
                throw new ArgumentException("Value is not a valid structureName", "value");
            }
            return value;
        }

        #endregion

        #endregion

        #region Section 6.2.3 - Class declaration

        #region className = schemaQualifiedName

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// className = schemaQualifiedName
        /// </remarks>
        public static bool IsClassName(string value)
        {
            return NameValidator.IsSchemaQualifiedName(value);
        }

        public static string ValidateClassName(string value)
        {
            if (!NameValidator.IsClassName(value))
            {
                throw new ArgumentException("Value is not a valid className", "value");
            }
            return value;
        }

        #endregion

        #endregion

        #region Section 6.2.4 - Association declaration

        #region className = schemaQualifiedName

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// associationName = schemaQualifiedName
        /// </remarks>
        public static bool IsAssociationName(string value)
        {
            return NameValidator.IsSchemaQualifiedName(value);
        }

        public static string ValidateAssociationName(string value)
        {
            if (!NameValidator.IsAssociationName(value))
            {
                throw new ArgumentException("Value is not a valid associationName", "value");
            }
            return value;
        }

        #endregion

        #endregion

        #region Section A.15 - Names

        #region schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER
        /// </remarks>
        public static bool IsSchemaQualifiedName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                return false;
            }
            var underscore = value.IndexOf("_", StringComparison.InvariantCulture);
            if (underscore == -1)
            {
                return false;
            }
            return NameValidator.IsSchemaName(value.Substring(0, underscore)) &&
                   StringValidator.IsIdentifier(value.Substring(underscore + 1));
        }

        public static string ValidateSchemaQualifiedName(string value)
        {
            if (!NameValidator.IsSchemaQualifiedName(value))
            {
                throw new ArgumentException("Value is not a valid schemaQualifiedName", "value");
            }
            return value;
        }

        #endregion

        #region schemaName = firstSchemaChar *( nextSchemaChar )

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// schemaName = firstSchemaChar *( nextSchemaChar ) 
        /// </remarks>
        public static bool IsSchemaName(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   NameValidator.IsFirstSchemaChar(value[0]) &&
                   value.Skip(1).All(NameValidator.IsNextSchemaChar);
        }

        public static string ValidateSchemaName(string value)
        {
            if (!NameValidator.IsSchemaName(value))
            {
                throw new ArgumentException("Value is not a valid schemaName", "value");
            }
            return value;
        }

        #endregion

        #region firstSchemaChar = UPPERALPHA / LOWERALPHA

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// firstSchemaChar = UPPERALPHA / LOWERALPHA 
        /// </remarks>
        public static bool IsFirstSchemaChar(char value)
        {
            return char.IsLetter(value);
        }

        public static char ValidateFirstSchemaChar(char value)
        {
            if (!NameValidator.IsFirstSchemaChar(value))
            {
                throw new ArgumentException("Value is not a valid firstSchemaChar", "value");
            }
            return value;
        }

        #endregion

        #region nextSchemaChar = firstSchemaChar / decimalDigit

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// nextSchemaChar = firstSchemaChar / decimalDigit
        /// </remarks>
        public static bool IsNextSchemaChar(char value)
        {
            return char.IsLetter(value) || char.IsDigit(value);
        }

        public static char ValidateNextSchemaChar(char value)
        {
            if (!NameValidator.IsFirstSchemaChar(value))
            {
                throw new ArgumentException("Value is not a valid firstSchemaChar", "value");
            }
            return value;
        }

        #endregion

        #region aliasIdentifier = "$" IDENTIFIER

        /// <summary>
        /// </summary>
        /// <param name="value"></param>
        /// <remarks>
        /// aliasIdentifier = "$" IDENTIFIER 
        /// </remarks>
        public static bool IsAliasIdentifier(string value)
        {
            return !string.IsNullOrEmpty(value) &&
                   (value[0] == '$') &&
                   StringValidator.IsIdentifier(value.Substring(1));
        }

        public static string ValidateAliasIdentifier(string value)
        {
            if (!NameValidator.IsAliasIdentifier(value))
            {
                throw new ArgumentException("Value is not a valid aliasIdentifier", "value");
            }
            return value;
        }

        #endregion

        #endregion

    }

}

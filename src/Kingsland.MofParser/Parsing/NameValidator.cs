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
                   StringValidator.IsSchemaQualifiedName(value);
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
            return StringValidator.IsSchemaQualifiedName(value);
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
            return StringValidator.IsSchemaQualifiedName(value);
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

    }

}

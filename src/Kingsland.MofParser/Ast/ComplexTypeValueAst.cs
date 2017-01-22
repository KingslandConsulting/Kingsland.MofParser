﻿using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class ComplexTypeValueAst : PropertyValueAst
    {

        #region Constructors

        internal ComplexTypeValueAst()
        {
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.14 Complex type value
        ///
        ///     complexTypeValue  = complexValue / complexValueArray
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref ComplexTypeValueAst node, bool throwIfError = false)
        {

            // complexValue
            parser.Descend();
            var complexValue = default(ComplexValueAst);
            if (ComplexValueAst.TryParse(parser, ref complexValue, false))
            {
                parser.Commit();
                node = complexValue;
                return true;
            }
            parser.Backtrack();

            // complexValueArray
            parser.Descend();
            var complexValueArray = default(ComplexValueArrayAst);
            if (ComplexValueArrayAst.TryParse(parser, ref complexValueArray, false))
            {
                parser.Commit();
                node = complexValueArray;
                return true;
            }
            parser.Backtrack();

            // unexpected token
            return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);

        }

        #endregion

    }

}

﻿using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class MofSpecificationAst : AstNode
    {

        #region Fields

        private List<MofProductionAst> _productions;

        #endregion

        #region Constructors

        private MofSpecificationAst()
        {
        }

        #endregion

        #region Properties

        public List<MofProductionAst> Productions
        {
            get
            {
                if (_productions == null)
                {
                    _productions = new List<MofProductionAst>();
                }
                return _productions;
            }
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// Section A.2 - MOF specification
        ///
        ///     mofSpecification = *mofProduction
        ///
        /// </remarks>
        internal static MofSpecificationAst Parse(Parser parser)
        {
            var state = parser.CurrentState;
            var specification = new MofSpecificationAst();
            while (!state.Eof)
            {
                var production = MofProductionAst.Parse(parser);
                specification.Productions.Add(production);
            }
            return specification;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}

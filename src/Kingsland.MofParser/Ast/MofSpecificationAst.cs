using System.Collections.Generic;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Ast
{

    public sealed class MofSpecificationAst : AstNode
    {

        private List<MofProductionAst> _productions;

        private MofSpecificationAst()
        {
        }

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

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// Section A.2 - MOF specification
        ///
        ///     mofSpecification = *mofProduction
        ///
        /// </remarks>
        internal static MofSpecificationAst Parse(ParserStream stream)
        {
            var specification = new MofSpecificationAst();
            while (!stream.Eof)
            {
                var production = MofProductionAst.Parse(stream);
                specification.Productions.Add(production);
            }
            return specification;
        }

    }

}

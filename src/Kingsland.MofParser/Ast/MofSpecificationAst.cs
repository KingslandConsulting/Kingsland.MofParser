using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using System.Text;

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

        #endregion


        #region AstNode Members

        public override string GetMofSource()
        {
            var source = new StringBuilder();
            for(var i = 0; i < this.Productions.Count; i++)
            {
                if (i > 0)
                {
                    source.AppendLine();
                }
                source.Append(this.Productions[i].GetMofSource());
            }
            return source.ToString();
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}

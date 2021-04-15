using Kingsland.MofParser.CodeGen;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.6.1 Primitive type value
    ///
    ///     literalValueArray = "{" [ literalValue *( "," literalValue ) ] "}"
    ///
    /// </remarks>
    public sealed record LiteralValueArrayAst : PrimitiveTypeValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Values = new List<LiteralValueAst>();
            }

            public List<LiteralValueAst> Values
            {
                get;
                set;
            }

            public LiteralValueArrayAst Build()
            {
                return new LiteralValueArrayAst(
                    this.Values
                );
            }

        }

        #endregion

        #region Constructors

        internal LiteralValueArrayAst(
            IEnumerable<LiteralValueAst> values
        )
        {
            this.Values = new ReadOnlyCollection<LiteralValueAst>(
                values?.ToList() ?? new List<LiteralValueAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<LiteralValueAst> Values
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertLiteralValueArrayAst(this);
        }

        #endregion

    }

}

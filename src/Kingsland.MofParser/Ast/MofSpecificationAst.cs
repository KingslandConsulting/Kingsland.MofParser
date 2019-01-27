using Kingsland.MofParser.CodeGen;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.2 MOF specification
    ///
    ///     mofSpecification = *mofProduction
    ///
    /// </remarks>
    public sealed class MofSpecificationAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Productions = new List<MofProductionAst>();
            }

            public List<MofProductionAst> Productions
            {
                get;
            }

            public MofSpecificationAst Build()
            {
                return new MofSpecificationAst(
                    new ReadOnlyCollection<MofProductionAst>(
                        this.Productions ?? new List<MofProductionAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public MofSpecificationAst(ReadOnlyCollection<MofProductionAst> productions)
        {
            this.Productions = productions ?? new ReadOnlyCollection<MofProductionAst>(
                new List<MofProductionAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<MofProductionAst> Productions
        {
            get;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertMofSpecificationAst(this);
        }

        #endregion

    }

}

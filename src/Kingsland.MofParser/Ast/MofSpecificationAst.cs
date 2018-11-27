using Kingsland.MofParser.CodeGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class MofSpecificationAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

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

        private MofSpecificationAst(ReadOnlyCollection<MofProductionAst> productions)
        {
            this.Productions = productions ?? throw new ArgumentNullException(nameof(productions));
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
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}

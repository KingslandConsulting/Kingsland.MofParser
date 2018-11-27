using Kingsland.MofParser.CodeGen;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class LiteralValueArrayAst : PrimitiveTypeValueAst
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
                    new ReadOnlyCollection<LiteralValueAst>(
                        this.Values ?? new List<LiteralValueAst>()
                    )
                );
            }


        }

        #endregion

        #region Constructors

        private LiteralValueArrayAst(ReadOnlyCollection<LiteralValueAst> values)
        {
            this.Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<LiteralValueAst> Values
        {
            get;
            private set;
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

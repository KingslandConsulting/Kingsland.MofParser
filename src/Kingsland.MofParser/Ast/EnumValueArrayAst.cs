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
    /// 7.6.3 Enum type value
    ///
    ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
    ///
    /// </remarks>
    public sealed record EnumValueArrayAst : EnumTypeValueAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Values = new List<EnumValueAst>();
            }

            public List<EnumValueAst> Values
            {
                get;
                private set;
            }

            public EnumValueArrayAst Build()
            {
                return new EnumValueArrayAst(
                    this.Values
                );
            }

        }

        #endregion

        #region Constructors

        internal EnumValueArrayAst()
            : this(new List<EnumValueAst>())
        {
        }

        internal EnumValueArrayAst(
            IEnumerable<EnumValueAst> values
        )
        {
            this.Values = new ReadOnlyCollection<EnumValueAst>(
                values.ToList()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<EnumValueAst> Values
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertEnumValueArrayAst(this);
        }

        #endregion

    }

}

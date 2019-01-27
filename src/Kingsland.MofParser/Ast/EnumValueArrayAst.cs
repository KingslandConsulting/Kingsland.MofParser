using Kingsland.MofParser.CodeGen;
using System;
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
    /// 7.6.3 Enum type value
    ///
    ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
    ///
    /// </remarks>
    public sealed class EnumValueArrayAst : EnumTypeValueAst
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
                    new ReadOnlyCollection<EnumValueAst>(
                        this.Values ?? new List<EnumValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public EnumValueArrayAst(
            ReadOnlyCollection<EnumValueAst> values
        )
        {
            this.Values = values ?? throw new ArgumentNullException(nameof(values));
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<EnumValueAst> Values
        {
            get;
            private set;
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

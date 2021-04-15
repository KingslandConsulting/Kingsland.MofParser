using Kingsland.MofParser.CodeGen;
using Kingsland.ParseFx.Parsing;
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
    /// 7.4.1 QualifierList
    ///
    ///     qualifierList = "[" qualifierValue *( "," qualifierValue ) "]"
    ///
    /// </remarks>
    public sealed record QualifierListAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.QualifierValues = new List<QualifierValueAst>();
            }

            public List<QualifierValueAst> QualifierValues
            {
                get;
                set;
            }

            public QualifierListAst Build()
            {
                return new QualifierListAst(
                    this.QualifierValues
                );
            }

        }

        #endregion

        #region Constructors

        internal QualifierListAst()
            : this(default(IEnumerable<QualifierValueAst>))
        {
        }

        internal QualifierListAst(
            IEnumerable<QualifierValueAst> qualifierValues
        )
        {
            this.QualifierValues = new ReadOnlyCollection<QualifierValueAst>(
                qualifierValues?.ToList() ?? new List<QualifierValueAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<QualifierValueAst> QualifierValues
        {
            get;
            private init;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return AstMofGenerator.ConvertQualifierListAst(this);
        }

        #endregion

    }

}

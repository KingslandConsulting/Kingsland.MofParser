using Kingsland.MofParser.CodeGen;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <param name="stream"></param>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.4.1 QualifierList
    ///
    ///     qualifierList = "[" qualifierValue *( "," qualifierValue ) "]"
    ///
    /// </remarks>
    public sealed class QualifierListAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Qualifiers = new List<QualifierValueAst>();
            }

            public List<QualifierValueAst> Qualifiers
            {
                get;
                set;
            }

            public QualifierListAst Build()
            {
                return new QualifierListAst(
                    new ReadOnlyCollection<QualifierValueAst>(
                        this.Qualifiers ?? new List<QualifierValueAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public QualifierListAst(ReadOnlyCollection<QualifierValueAst> qualifiers)
        {
            this.Qualifiers = qualifiers ?? new ReadOnlyCollection<QualifierValueAst>(
                new List<QualifierValueAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<QualifierValueAst> Qualifiers
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertQualifierListAst(this);
        }

        #endregion

    }

}

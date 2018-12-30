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
    ///     qualifierList                 = "[" qualifierValue *( "," qualifierValue ) "]"
    ///
    ///     qualifierValue                = qualifierName [ qualifierValueInitializer /
    ///                                     qualiferValueArrayInitializer ]
    ///
    ///     qualifierValueInitializer     = "(" literalValue ")"
    ///
    ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
    ///
    /// </remarks>
    public sealed class QualifierListAst : AstNode
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Qualifiers = new List<QualifierTypeDeclarationAst>();
            }

            public List<QualifierTypeDeclarationAst> Qualifiers
            {
                get;
                set;
            }

            public QualifierListAst Build()
            {
                return new QualifierListAst(
                    new ReadOnlyCollection<QualifierTypeDeclarationAst>(
                        this.Qualifiers ?? new List<QualifierTypeDeclarationAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public QualifierListAst(ReadOnlyCollection<QualifierTypeDeclarationAst> qualifiers)
        {
            this.Qualifiers = qualifiers ?? new ReadOnlyCollection<QualifierTypeDeclarationAst>(
                new List<QualifierTypeDeclarationAst>()
            );
        }

        #endregion

        #region Properties

        public ReadOnlyCollection<QualifierTypeDeclarationAst> Qualifiers
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

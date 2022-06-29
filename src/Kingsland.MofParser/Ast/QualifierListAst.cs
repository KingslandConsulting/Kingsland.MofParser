using Kingsland.MofParser.CodeGen;
using Kingsland.ParseFx.Parsing;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

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
        : this(new List<QualifierValueAst>())
    {
    }

    internal QualifierListAst(
        IEnumerable<QualifierValueAst> qualifierValues
    )
    {
        this.QualifierValues = new ReadOnlyCollection<QualifierValueAst>(
            (qualifierValues ?? throw new ArgumentNullException(nameof(qualifierValues)))
                .ToList()
        );
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<QualifierValueAst> QualifierValues
    {
        get;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertQualifierListAst(this);
    }

    #endregion

}

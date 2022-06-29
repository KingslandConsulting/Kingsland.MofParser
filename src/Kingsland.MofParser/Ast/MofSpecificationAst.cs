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
/// 7.2 MOF specification
///
///     mofSpecification = *mofProduction
///
/// </remarks>
public sealed record MofSpecificationAst : AstNode
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
            set;
        }

        public MofSpecificationAst Build()
        {
            return new MofSpecificationAst(
                this.Productions
            );
        }

    }

    #endregion

    #region Constructors

    internal MofSpecificationAst()
        : this(new List<MofProductionAst>())
    {
    }

    internal MofSpecificationAst(
        IEnumerable<MofProductionAst> productions
    )
    {
        this.Productions = new ReadOnlyCollection<MofProductionAst>(
            (productions ?? throw new ArgumentNullException(nameof(productions)))
                .ToList()
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

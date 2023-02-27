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
            this.Productions = new();
        }

        public List<MofProductionAst> Productions
        {
            get;
            set;
        }

        public MofSpecificationAst Build()
        {
            return new(
                this.Productions
            );
        }

    }

    #endregion

    #region Constructors

    internal MofSpecificationAst()
        : this(Enumerable.Empty<MofProductionAst>())
    {
    }

    internal MofSpecificationAst(
        IEnumerable<MofProductionAst> productions
    )
    {
        this.Productions = new(
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

}

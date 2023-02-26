using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.6.1 Primitive type value
///
///     literalValueArray = "{" [ literalValue *( "," literalValue ) ] "}"
///
/// </remarks>
public sealed record LiteralValueArrayAst : PrimitiveTypeValueAst
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
                this.Values
            );
        }

    }

    #endregion

    #region Constructors

    internal LiteralValueArrayAst()
        : this(new List<LiteralValueAst>())
    {
    }

    internal LiteralValueArrayAst(
        IEnumerable<LiteralValueAst> values
    )
    {
        this.Values = new ReadOnlyCollection<LiteralValueAst>(
            (values ?? throw new ArgumentNullException(nameof(values)))
                .ToList()
        );
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<LiteralValueAst> Values
    {
        get;
    }

    #endregion

}

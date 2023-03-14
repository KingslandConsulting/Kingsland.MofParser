using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System.Globalization;

namespace Kingsland.MofParser.Tokens;

public sealed record RealLiteralToken : SyntaxToken
{

    #region Constructors

    public RealLiteralToken(double value)
        : this(null, value)
    {
    }

    public RealLiteralToken(SourcePosition? start, SourcePosition? end, string text, double value)
        : this(new SourceExtent(start, end, text), value)
    {
    }

    public RealLiteralToken(SourceExtent? extent, double value)
        : base(extent)
    {
        this.Value = value;
    }

    #endregion

    #region Properties

    public double Value
    {
        get;
    }

    #endregion

    #region SyntaxToken Interface

    public override string GetSourceString()
    {
        return this.Text
            ?? this.Value.ToString(CultureInfo.InvariantCulture);
    }

    #endregion

}

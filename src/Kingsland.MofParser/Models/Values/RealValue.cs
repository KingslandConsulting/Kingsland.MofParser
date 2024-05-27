using System.Globalization;

namespace Kingsland.MofParser.Models.Values;

public sealed class RealValue : LiteralValue
{

    public RealValue(double value)
    {
        this.Value = value;
    }

    public double Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value.ToString(CultureInfo.InvariantCulture);
    }

}

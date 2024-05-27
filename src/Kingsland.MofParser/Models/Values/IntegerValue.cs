using System.Globalization;

namespace Kingsland.MofParser.Models.Values;

public sealed class IntegerValue : LiteralValue
{

    public IntegerValue(long value)
    {
        this.Value = value;
    }

    public long Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value.ToString(CultureInfo.InvariantCulture);
    }

}

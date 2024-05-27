using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Models.Values;

public sealed class BooleanValue : LiteralValue
{

    public BooleanValue(bool value)
    {
        this.Value = value;
    }

    public bool Value
    {
        get;
    }

    public override string ToString()
    {
        return this.Value
            ? Constants.TRUE
            : Constants.FALSE;
    }

}

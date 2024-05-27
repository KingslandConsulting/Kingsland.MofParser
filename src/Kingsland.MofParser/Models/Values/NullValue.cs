using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Models.Values;

public sealed class NullValue : LiteralValue
{

    public static readonly NullValue Null = new();

    private NullValue()
    {
    }

    public override string ToString()
    {
        return Constants.NULL;
    }

}

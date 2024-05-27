using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Models.Values;

public sealed class StringValue : LiteralValue
{

    public StringValue(string value)
    {
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Value
    {
        get;
    }

    public override string ToString()
    {
        return $"\"{StringLiteralToken.EscapeString(this.Value)}\"";
    }

}

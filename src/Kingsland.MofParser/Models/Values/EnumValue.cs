namespace Kingsland.MofParser.Models.Values;

public sealed class EnumValue : EnumTypeValue
{

    public EnumValue(string name)
        : this(null, name)
    {
    }

    public EnumValue(string? type, string name)
    {
        this.Type = type;
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string? Type
    {
        get;
    }

    public string Name
    {
        get;
    }

}

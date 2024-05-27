using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Types;

public sealed record Property
{

    internal Property(string name, PropertyValue value)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Value = value ?? throw new ArgumentNullException(nameof(value));
    }

    public string Name
    {
        get;
    }

    public PropertyValue Value
    {
        get;
    }

    public override string ToString()
    {
        return $"{this.Name} = {this.Value}";
    }

}

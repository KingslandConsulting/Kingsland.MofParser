namespace Kingsland.MofParser.Models.Values;

public sealed class ComplexValueAlias : ComplexValueBase
{

    public ComplexValueAlias(string name)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
    }

    public string Name
    {
        get;
    }

    public override string ToString()
    {
        return $"${this.Name}";
    }

}

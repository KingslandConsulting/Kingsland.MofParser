namespace Kingsland.MofParser.Models.Types;

public sealed class Enumeration
{

    public Enumeration(string name, Type underlyingType)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.UnderlyingType =
            new Type[] { typeof(string), typeof(int) }
                .Contains(underlyingType ?? throw new ArgumentNullException(nameof(underlyingType)))
            ? underlyingType
            : throw new ArgumentException(
                "underlying type must be string or int",
                nameof(underlyingType)
            );
    }

    public string Name
    {
        get;
    }

    public Type UnderlyingType
    {
        get;
    }

}

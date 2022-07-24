namespace Kingsland.MofParser.Models;

public sealed class Enumeration
{

    #region Constructors

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

    #endregion

    #region Properties

    public string Name
    {
        get;
    }

    public Type UnderlyingType
    {
        get;
    }

    #endregion

}

namespace Kingsland.MofParser.Models.Types;

public sealed record Class
{

    internal Class(string className, string superClass)
    {
        this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
        this.SuperClass = superClass ?? throw new ArgumentNullException(nameof(superClass));
    }

    public string ClassName
    {
        get;
    }

    public string SuperClass
    {
        get;
    }

}

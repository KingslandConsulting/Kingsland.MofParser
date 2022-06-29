namespace Kingsland.MofParser.Model;

public sealed record Class
{

    #region Builder

    public sealed class Builder
    {

        public string? ClassName
        {
            get;
            set;
        }

        public string? SuperClass
        {
            get;
            set;
        }

        public Class Build()
        {
            return new Class(
                this.ClassName ?? throw new InvalidOperationException(
                    $"{nameof(this.ClassName)} property must be set before calling {nameof(Build)}."
                ),
                this.SuperClass ?? throw new InvalidOperationException(
                    $"{nameof(this.SuperClass)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal Class(string className, string superClass)
    {
        this.ClassName = className;
        this.SuperClass = superClass;
    }

    #endregion

    #region Properties

    public string ClassName
    {
        get;
        private init;
    }

    public string SuperClass
    {
        get;
        private init;
    }

    #endregion

}

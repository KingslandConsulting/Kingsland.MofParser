using Kingsland.MofParser.Parsing;
using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models;

public sealed record Instance
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Properties = [];
        }

        public string? TypeName
        {
            get;
            set;
        }

        public string? Alias
        {
            get;
            set;
        }

        public List<Property> Properties
        {
            get;
            set;
        }

        public Instance Build()
        {
            return new Instance(
                this.TypeName ?? throw new InvalidOperationException(
                    $"{nameof(this.TypeName)} property must be set before calling {nameof(Build)}."
                ),
                this.Alias,
                this.Properties
            );
        }

    }

    #endregion

    #region Constructors

    internal Instance(string typeName, string? alias, IEnumerable<Property> properties)
    {
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.Alias = alias;
        this.Properties = new ReadOnlyCollection<Property>(
            (properties ?? throw new ArgumentNullException(nameof(properties)))
                .ToList()
        );
    }

    #endregion

    #region Properties

    public string TypeName
    {
        get;
    }

    public string? Alias
    {
        get;
    }

    public ReadOnlyCollection<Property> Properties
    {
        get;
    }

    #endregion

    #region Methods

    //public T GetValue<T>(string name)
    //{
    //    return (T)this.Properties.Single(p => p.Name == name).Value;
    //}

    //public bool TryGetValue<T>(string name, out T result)
    //{
    //    var property = this.Properties.SingleOrDefault(p => p.Name == name);
    //    if (property == null)
    //    {
    //        result = default;
    //        return false;
    //    }
    //    var value = property.Value;
    //    if (value is T typed)
    //    {
    //        result = typed;
    //        return true;
    //    }
    //    result = default;
    //    return false;
    //}

    #endregion

    #region Object Interface

    public override string ToString()
    {
        var result = new StringBuilder();
        result.Append($"{Constants.INSTANCE} {Constants.OF} {this.TypeName}");
        if (!string.IsNullOrEmpty(this.Alias))
        {
            result.Append($" {Constants.AS} ${this.Alias}");
        }
        return result.ToString();
    }

    #endregion

}

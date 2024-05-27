using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Parsing;
using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

public sealed class ComplexValueObject : ComplexValueBase
{

    internal ComplexValueObject(string typeName, IEnumerable<Property> properties)
    {
        this.TypeName = typeName ?? throw new ArgumentNullException(nameof(typeName));
        this.Properties = (properties ?? throw new ArgumentNullException(nameof(properties)))
            .ToList().AsReadOnly();
    }

    public string TypeName
    {
        get;
    }

    public ReadOnlyCollection<Property> Properties
    {
        get;
    }

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

    public override string ToString()
    {
        var result = new StringBuilder();
        result.Append($"{Constants.VALUE} {Constants.OF} {this.TypeName}");
        return result.ToString();
    }

}

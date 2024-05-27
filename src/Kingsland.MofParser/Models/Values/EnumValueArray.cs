using System.Collections.ObjectModel;
using System.Text;

namespace Kingsland.MofParser.Models.Values;

public sealed class EnumValueArray : EnumTypeValue
{

    public EnumValueArray(params EnumValue[] values)
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    public EnumValueArray(IEnumerable<EnumValue> values)
    {
        this.Values = (values ?? throw new ArgumentNullException(nameof(values)))
            .ToList().AsReadOnly();
    }

    public ReadOnlyCollection<EnumValue> Values
    {
        get;
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.Append("{");
        for (var i = 0; i < this.Values.Count; i++)
        {
            if (i > 0)
            {
                sb.Append(", ");
            }
            sb.Append(this.Values[i]);
        }
        sb.Append("}");
        return sb.ToString();
    }

}

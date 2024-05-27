namespace Kingsland.MofParser.Models.Values;

public abstract class PropertyValue
{

    internal PropertyValue()
    {
    }

    public static implicit operator PropertyValue(int value) => new IntegerValue(value);
    public static implicit operator PropertyValue(double value) => new RealValue(value);
    public static implicit operator PropertyValue(bool value) => new BooleanValue(value);
    public static implicit operator PropertyValue(string value) => new StringValue(value);

}

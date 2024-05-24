using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Text;

namespace Kingsland.MofParser.Models;

public sealed record Property
{

    #region Builder

    public sealed class Builder
    {

        public string? Name
        {
            get;
            set;
        }

        public object? Value
        {
            get;
            set;
        }

        public Property Build()
        {
            return new Property(
                this.Name ?? throw new InvalidOperationException(
                    $"{nameof(this.Name)} property must be set before calling {nameof(Build)}."
                ),
                this.Value
            );
        }

    }

    #endregion

    #region Constructors

    internal Property(string name, object? value)
    {
        this.Name = name ?? throw new ArgumentNullException(nameof(name));
        this.Value = value;
    }

    #endregion

    #region Properties

    public string Name
    {
        get;
    }

    public object? Value
    {
        get;
    }

    #endregion

    #region Object Interface

    public override string ToString()
    {
        var result = new StringBuilder();
        result.Append($"{this.Name} = ");
        result.Append(
            this.Value switch
            {
                null => Constants.NULL,
                true => Constants.TRUE,
                false => Constants.FALSE,
                string s => $"\"{StringLiteralToken.EscapeString(s)}\"",
                _ => $"!!!{this.Value.GetType().FullName}!!!"
            }
        );
        return result.ToString();
    }

    #endregion

}

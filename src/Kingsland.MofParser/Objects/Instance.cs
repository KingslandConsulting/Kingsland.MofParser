using Kingsland.MofParser.Ast;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Objects;

public sealed record Instance
{

    #region Builder

    public sealed class Builder
    {

        public Builder()
        {
            this.Properties = new Dictionary<string, object?>();
        }

        public string? ClassName
        {
            get;
            set;
        }

        public string? Alias
        {
            get;
            set;
        }

        public Dictionary<string, object?> Properties
        {
            get;
            set;
        }

        public Instance Build()
        {
            return new Instance(
                this.ClassName ?? throw new InvalidOperationException(
                    $"{nameof(this.ClassName)} property must be set before calling {nameof(Build)}."
                ),
                this.Alias,
                this.Properties
            );
        }

    }

    #endregion

    #region Constructors

    internal Instance(
        string className,
        string? alias,
        IDictionary<string, object?> properties
    )
    {
        this.ClassName = className ?? throw new ArgumentNullException(nameof(className));
        this.Alias = alias;
        this.Properties = new ReadOnlyDictionary<string, object?>(
            (properties ?? throw new ArgumentNullException(nameof(properties)))
                .ToDictionary(
                    kvp => kvp.Key, kvp => kvp.Value
                )
        );
    }

    #endregion

    #region Properties

    public string ClassName
    {
        get;
    }

    public string? Alias
    {
        get;
   }

    public ReadOnlyDictionary<string, object?> Properties
    {
        get;
    }

    #endregion

    #region Methods

    public static Instance FromAstNode(InstanceValueDeclarationAst node)
    {
        if (node == null)
        {
            throw new ArgumentNullException(nameof(node));
        }
        var instance = new Instance.Builder
        {
            ClassName = node.TypeName.Name,
            Alias = node.Alias?.Name
        };
        foreach (var property in node.PropertyValues.PropertyValues)
        {
            var propertyValue = property.Value;
            switch (propertyValue)
            {
                case ComplexValueArrayAst complexValueArray:
                    var complexValues = complexValueArray.Values
                        .Select(Instance.GetComplexValue)
                        .ToArray();
                    instance.Properties.Add(property.Key, complexValues);
                    break;
                case LiteralValueArrayAst literalValueArray:
                    var literalValues = literalValueArray.Values
                        .Select(Instance.GetLiteralValue)
                        .ToArray();
                    instance.Properties.Add(property.Key, literalValues);
                    break;
                case LiteralValueAst literalValue:
                    instance.Properties.Add(property.Key, Instance.GetLiteralValue(literalValue));
                    break;
                default:
                    throw new NotImplementedException($"Unhandled property value type '{propertyValue.GetType().FullName}'");
            }
        }
        return instance.Build();
    }

    private static object GetComplexValue(ComplexValueAst node)
    {
        return node.IsAlias
            ? node.Alias ?? throw new NullReferenceException()
            : throw new NotImplementedException(
                $"Unhandled value-type complex values."
            );
    }

    private static object? GetLiteralValue(LiteralValueAst node)
    {
        return node switch
        {
            BooleanValueAst booleanValue => booleanValue.Value,
            IntegerValueAst integerValue => integerValue.Value,
            StringValueAst stringValue => stringValue.Value,
            NullValueAst _ => null,
            _ => throw new NotImplementedException(
                $"Unhandled literal value type '{node.GetType().FullName}'"
            ),
        };
    }

    #endregion

}

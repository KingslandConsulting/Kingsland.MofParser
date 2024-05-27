using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.5.9 Complex type value

    public static PropertyValue ConvertComplexTypeValueAst(ComplexTypeValueAst node)
    {
        return node switch
        {
            ComplexValueArrayAst n => ModelConverter.ConvertComplexValueArrayAst(n),
            ComplexValueAst n => ModelConverter.ConvertComplexValueAst(n),
            _ => throw new NotImplementedException(),
        };
    }

    public static ComplexValueArray ConvertComplexValueArrayAst(ComplexValueArrayAst node)
    {
        return new(
            node.Values.Select(ModelConverter.ConvertComplexValueAst)
        );
    }

    public static ComplexValueBase ConvertComplexValueAst(ComplexValueAst node)
    {
        if (node.IsAlias)
        {
            return new ComplexValueAlias(
                (node.Alias ?? throw new InvalidOperationException()).Name
            );
        }
        else
        {
            return new ComplexValueObject(
                (node.TypeName ?? throw new InvalidOperationException()).Name,
                ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)
            );
        }
    }

    public static IEnumerable<Property> ConvertPropertyValueListAst(PropertyValueListAst node)
    {
        return node.PropertyValues
            .Select(
                kvp => new Property(
                    name: kvp.Key,
                    value: ModelConverter.ConvertPropertyValueAst(kvp.Value)
                )
            );
    }

    public static PropertyValue ConvertPropertyValueAst(PropertyValueAst node)
    {
        return node switch
        {
            PrimitiveTypeValueAst n => ModelConverter.ConvertPrimitiveTypeValueAst(n),
            ComplexTypeValueAst n => ModelConverter.ConvertComplexTypeValueAst(n),
            //ReferenceTypeValueAst n => ModelConverter.FromReferenceTypeValueAst(n),
            EnumTypeValueAst n => ModelConverter.ConvertEnumTypeValueAst(n),
            _ => throw new NotImplementedException()
        };
    }

    #endregion

}

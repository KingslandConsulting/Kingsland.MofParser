using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.3 Compiler directives

    public static void ConvertCompilerDirectiveAst(CompilerDirectiveAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.4 Qualifiers

    public static void ConvertQualifierTypeDeclarationAst(QualifierTypeDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.4.1 QualifierList

    public static void ConvertQualifierListAst(QualifierListAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertQualifierValueAst(QualifierValueAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertIQualifierInitializerAst(IQualifierInitializerAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertQualifierValueInitializerAst(QualifierValueInitializerAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertQualifierValueArrayInitializerAst(QualifierValueArrayInitializerAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.1 Structure declaration

    public static void ConvertStructureDeclarationAst(StructureDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertStructureFeatureAst(IStructureFeatureAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.2 Class declaration

    public static void ConvertClassDeclarationAst(ClassDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertClassFeatureAst(IClassFeatureAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.3 Association declaration

    public static void ConvertAssociationDeclarationAst(AssociationDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.4 Enumeration declaration

    public static Enumeration ConvertEnumerationDeclarationAst(EnumerationDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertEnumElementAst(EnumElementAst node)
    {
        throw new NotImplementedException();
    }

    public static void ConvertIEnumElementValueAst(IEnumElementValueAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.5 Property declaration

    public static void ConvertPropertyDeclarationAst(PropertyDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.6 Method declaration

    public static void ConvertMethodDeclarationAst(MethodDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.5.7 Parameter declaration

    public static void ConvertParameterDeclarationAst(ParameterDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.6.1 Primitive type value

    public static PrimitiveTypeValue ConvertPrimitiveTypeValueAst(PrimitiveTypeValueAst node)
    {
        return node switch
        {
            LiteralValueAst n => ModelConverter.ConvertLiteralValueAst(n),
            LiteralValueArrayAst n => ModelConverter.ConvertLiteralValueArrayAst(n),
            _ => throw new NotImplementedException(),
        };
    }

    public static LiteralValue ConvertLiteralValueAst(LiteralValueAst node)
    {
        return node switch
        {
            IntegerValueAst n => ModelConverter.ConvertIntegerValueAst(n),
            RealValueAst n => ModelConverter.ConvertRealValueAst(n),
            BooleanValueAst n => ModelConverter.ConvertBooleanValueAst(n),
            NullValueAst n => ModelConverter.ConvertNullValueAst(n),
            StringValueAst n => ModelConverter.ConvertStringValueAst(n),
            _ => throw new NotImplementedException(),
        };
    }

    public static LiteralValueArray ConvertLiteralValueArrayAst(LiteralValueArrayAst node)
    {
        return new(
            node.Values.Select(ModelConverter.ConvertLiteralValueAst)
        );
    }

    #endregion

    #region 7.6.1.1 Integer value

    public static IntegerValue ConvertIntegerValueAst(IntegerValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.2 Real value

    public static RealValue ConvertRealValueAst(RealValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.3 String values

    public static StringValue ConvertStringValueAst(StringValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.5 Boolean value

    public static BooleanValue ConvertBooleanValueAst(BooleanValueAst node)
    {
        return new(node.Value);
    }

    #endregion

    #region 7.6.1.6 Null value

    public static NullValue ConvertNullValueAst(NullValueAst node)
    {
        return NullValue.Null;
    }

    #endregion

    #region 7.6.2 Complex type value

    public static Instance ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node)
    {
        return new Instance(
            typeName: node.TypeName.Name,
            alias: node.Alias?.Name,
            properties: [.. ModelConverter.ConvertPropertyValueListAst(node.PropertyValues)]
        );
    }

    public static void ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node)
    {
        throw new NotImplementedException();
    }

    #endregion

    #region 7.6.3 Enum type value

    public static EnumTypeValue ConvertEnumTypeValueAst(EnumTypeValueAst node)
    {
        return node switch
        {
            EnumValueAst n => ModelConverter.ConvertEnumValueAst(n),
            EnumValueArrayAst n => ModelConverter.ConvertEnumValueArrayAst(n),
            _ => throw new NotImplementedException()
        };
    }

    public static EnumValue ConvertEnumValueAst(EnumValueAst node)
    {
        return new(
            node.EnumName?.Name,
            node.EnumLiteral.Name
        );
    }

    public static EnumValueArray ConvertEnumValueArrayAst(EnumValueArrayAst node)
    {
        return new(
            node.Values.Select(ModelConverter.ConvertEnumValueAst)
        );
    }

    #endregion

}

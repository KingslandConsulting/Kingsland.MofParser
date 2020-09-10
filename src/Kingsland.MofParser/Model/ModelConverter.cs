using Kingsland.MofParser.Ast;
using System;
using System.Collections.ObjectModel;
using System.Linq;

namespace Kingsland.MofParser.Model
{

    internal static class ModelConverter
    {

        #region 7.2 MOF specification

        public static Module ConvertMofSpecificationAst(MofSpecificationAst node)
        {
            return new Module.Builder
            {
                Instances = node.Productions
                    .OfType<InstanceValueDeclarationAst>()
                    .Select(ModelConverter.ConvertInstanceValueDeclarationAst)
                    .ToList()
            }.Build();
        }

        #endregion

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
            //    return new Class.Builder
            //    {
            //        ClassName = node.ClassName.Name,
            //        SuperClass = node.SuperClass.Name
            //    }.Build();
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

        public static void ConvertEnumerationDeclarationAst(EnumerationDeclarationAst node)
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

        #region 7.5.9 Complex type value

        public static object ConvertComplexTypeValueAst(ComplexTypeValueAst node)
        {
            return node switch
            {
                ComplexValueArrayAst n => ModelConverter.ConvertComplexValueArrayAst(n),
                ComplexValueAst n => ModelConverter.ConvertComplexValueAst(n),
                _ => throw new NotImplementedException(),
            };
        }

        public static ReadOnlyCollection<object> ConvertComplexValueArrayAst(ComplexValueArrayAst node)
        {
            return node.Values
                .Select(ModelConverter.ConvertComplexValueAst)
                .ToList()
                .AsReadOnly();
        }

        public static object ConvertComplexValueAst(ComplexValueAst node)
        {
            if (node.IsAlias)
            {
                return node.Alias.Name;
            };
            return node switch
            {
                _ => throw new NotImplementedException()
            };
        }

        public static ReadOnlyCollection<Property> ConvertPropertyValueListAst(PropertyValueListAst node)
        {
            return node.PropertyValues
                .Select(
                    kvp => new Property.Builder
                    {
                        Name = kvp.Key,
                        Value = ModelConverter.ConvertPropertyValueAst(kvp.Value)
                    }.Build()
                ).ToList()
                .AsReadOnly();
        }

        public static object ConvertPropertyValueAst(PropertyValueAst node)
        {
            return node switch
            {
                PrimitiveTypeValueAst n => ModelConverter.ConvertPrimitiveTypeValueAst(n),
                ComplexTypeValueAst n => ModelConverter.ConvertComplexTypeValueAst(n),
                //ReferenceTypeValueAst n => ModelConverter.FromReferenceTypeValueAst(n),
                //EnumTypeValueAst n => ModelConverter.FromEnumTypeValueAst(n),
                _ => throw new NotImplementedException()
            };
        }

        #endregion

        #region 7.6.1 Primitive type value

        public static object ConvertPrimitiveTypeValueAst(PrimitiveTypeValueAst node)
        {
            return node switch
            {
                LiteralValueAst n => ModelConverter.ConvertLiteralValueAst(n),
                LiteralValueArrayAst n => ModelConverter.ConvertLiteralValueArrayAst(n),
                _ => throw new NotImplementedException(),
            };
        }

        public static object ConvertLiteralValueAst(LiteralValueAst node)
        {
            return node switch
            {
                IntegerValueAst n => ModelConverter.ConvertIntegerValueAst(n),
                RealValueAst n => ModelConverter.ConvertRealValueAst(n),
                //BooleanValueAst n => ModelConverter.ConvertBooleanValueAst(n),
                //NullValueAst n => ModelConverter.ConvertNullValueAst(n),
                StringValueAst n => ModelConverter.ConvertStringValueAst(n),
                _ => throw new NotImplementedException(),
            };
        }

        public static ReadOnlyCollection<object> ConvertLiteralValueArrayAst(LiteralValueArrayAst node)
        {
            return node.Values
                .Select(ModelConverter.ConvertLiteralValueAst)
                .ToList()
                .AsReadOnly();
        }

        #endregion

        #region 7.6.1.1 Integer value

        public static long ConvertIntegerValueAst(IntegerValueAst node)
        {
            return node.Value;
        }

        #endregion

        #region 7.6.1.2 Real value

        public static double ConvertRealValueAst(RealValueAst node)
        {
            return node.Value;
        }

        #endregion

        #region 7.6.1.3 String values

        public static string ConvertStringValueAst(StringValueAst node)
        {
            return node.Value;
        }

        #endregion

        #region 7.6.1.5 Boolean value

        public static void ConvertBooleanValueAst(BooleanValueAst node)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 7.6.1.6 Null value

        public static void ConvertNullValueAst(NullValueAst node)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 7.6.2 Complex type value

        public static Instance ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node)
        {
            return new Instance.Builder
            {
                TypeName = node.TypeName.Name,
                Alias = node.Alias?.Name,
                Properties = ModelConverter.ConvertPropertyValueListAst(node.PropertyValues).ToList()
            }.Build();
        }

        public static void ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region 7.6.3 Enum type value

        public static void ConvertEnumTypeValueAst(EnumTypeValueAst node)
        {
            throw new NotImplementedException();
        }

        public static void ConvertEnumValueAst(EnumValueAst node)
        {
            throw new NotImplementedException();
        }

        public static void ConvertEnumValueArrayAst(EnumValueArrayAst node)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}

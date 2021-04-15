using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using System;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.CodeGen
{

    public sealed class AstMofGenerator
    {

        #region Dispatcher

        public static string ConvertToMof(AstNode node, MofQuirks quirks = MofQuirks.None)
        {
            return node switch
            {
                null =>
                    string.Empty,
                MofSpecificationAst ast =>
                    // 7.2 MOF specification
                    AstMofGenerator.ConvertMofSpecificationAst(ast, quirks),
                CompilerDirectiveAst ast =>
                    // 7.3 Compiler directives
                    AstMofGenerator.ConvertCompilerDirectiveAst(ast, quirks),
                QualifierTypeDeclarationAst ast =>
                    // 7.4 Qualifiers
                    AstMofGenerator.ConvertQualifierTypeDeclarationAst(ast, quirks),
                QualifierListAst ast =>
                    // 7.4.1 QualifierList
                    AstMofGenerator.ConvertQualifierListAst(ast, quirks),
                ClassDeclarationAst ast =>
                    // 7.5.2 Class declaration
                    AstMofGenerator.ConvertClassDeclarationAst(ast, quirks),
                PropertyDeclarationAst ast =>
                    // 7.5.5 Property declaration
                    AstMofGenerator.ConvertPropertyDeclarationAst(ast, quirks),
                MethodDeclarationAst ast =>
                    // 7.5.6 Method declaration
                    AstMofGenerator.ConvertMethodDeclarationAst(ast, quirks),
                ParameterDeclarationAst ast =>
                    // 7.5.7 Parameter declaration
                    AstMofGenerator.ConvertParameterDeclarationAst(ast, quirks),
                ComplexValueArrayAst ast =>
                    // 7.5.9 Complex type value
                    AstMofGenerator.ConvertComplexValueArrayAst(ast, quirks),
                ComplexValueAst ast =>
                    // 7.5.9 Complex type value
                    AstMofGenerator.ConvertComplexValueAst(ast, quirks),
                PropertyValueListAst ast =>
                    // 7.5.9 Complex type value
                    AstMofGenerator.ConvertPropertyValueListAst(ast, quirks),
                LiteralValueArrayAst ast =>
                    // 7.6.1 Primitive type value
                    AstMofGenerator.ConvertLiteralValueArrayAst(ast, quirks),
                IntegerValueAst ast =>
                    // 7.6.1.1 Integer value
                    AstMofGenerator.ConvertIntegerValueAst(ast, quirks),
                RealValueAst ast =>
                    // 7.6.1.2 Real value
                    AstMofGenerator.ConvertRealValueAst(ast, quirks),
                StringValueAst ast =>
                    // 7.6.1.3 String values
                    AstMofGenerator.ConvertStringValueAst(ast, quirks),
                BooleanValueAst ast =>
                    // 7.6.1.5 Boolean value
                    AstMofGenerator.ConvertBooleanValueAst(ast, quirks),
                NullValueAst ast =>
                    // 7.6.1.6 Null value
                    AstMofGenerator.ConvertNullValueAst(ast, quirks),
                InstanceValueDeclarationAst ast =>
                    // 7.6.2 Complex type value
                    AstMofGenerator.ConvertInstanceValueDeclarationAst(ast, quirks),
                StructureValueDeclarationAst ast =>
                    // 7.6.2 Complex type value
                    AstMofGenerator.ConvertStructureValueDeclarationAst(ast, quirks),
                EnumValueAst ast =>
                    // 7.6.3 Enum type value
                    AstMofGenerator.ConvertEnumValueAst(ast, quirks),
                EnumValueArrayAst ast =>
                    // 7.6.3 Enum type value
                    AstMofGenerator.ConvertEnumValueArrayAst(ast, quirks),
                EnumTypeValueAst ast =>
                    // 7.6.3 Enum type value
                    AstMofGenerator.ConvertEnumTypeValueAst(ast, quirks),
                PropertyValueAst ast =>
                    // 7.5.9 Complex type value
                    AstMofGenerator.ConvertPropertyValueAst(ast, quirks),
                _ =>
                    // unknown
                    throw new InvalidOperationException(node.GetType().FullName)
            };
        }

        #endregion

        #region 7.2 MOF specification

        public static string ConvertMofSpecificationAst(MofSpecificationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            for (var i = 0; i < node.Productions.Count; i++)
            {
                if (i > 0)
                {
                    source.AppendLine();
                }
                source.Append(AstMofGenerator.ConvertMofProductionAst(node.Productions[i], quirks));
            }
            return source.ToString();
        }

        public static string ConvertMofProductionAst(MofProductionAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node switch
            {
                CompilerDirectiveAst ast => AstMofGenerator.ConvertCompilerDirectiveAst(ast, quirks),
                StructureDeclarationAst ast => AstMofGenerator.ConvertStructureDeclarationAst(ast, quirks),
                ClassDeclarationAst ast => AstMofGenerator.ConvertClassDeclarationAst(ast, quirks),
                AssociationDeclarationAst ast => AstMofGenerator.ConvertAssociationDeclarationAst(ast, quirks),
                EnumerationDeclarationAst ast => AstMofGenerator.ConvertEnumerationDeclarationAst(ast, quirks),
                InstanceValueDeclarationAst ast => AstMofGenerator.ConvertInstanceValueDeclarationAst(ast, quirks),
                StructureValueDeclarationAst ast => AstMofGenerator.ConvertStructureValueDeclarationAst(ast, quirks),
                QualifierTypeDeclarationAst ast => AstMofGenerator.ConvertQualifierTypeDeclarationAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        #endregion

        #region 7.3 Compiler directives

        public static string ConvertCompilerDirectiveAst(CompilerDirectiveAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append(node.PragmaKeyword.Extent.Text);
            source.Append(' ');
            source.Append(node.PragmaName.Name);
            source.Append(' ');
            source.Append('(');
            source.Append(AstMofGenerator.ConvertStringValueAst(node.PragmaParameter, quirks));
            source.Append(')');
            return source.ToString();
        }

        #endregion

        #region 7.4 Qualifiers

        public static string ConvertQualifierTypeDeclarationAst(QualifierTypeDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (!string.IsNullOrEmpty(node.QualifierName.Name))
            {
                source.Append(node.QualifierName.Name);
            }
            if (node.Flavors.Any())
            {
                source.Append(": ");
                source.Append(string.Join(" ", node.Flavors.ToArray()));
            }
            return source.ToString();
        }

        #endregion

        #region 7.4.1 QualifierList

        public static string ConvertQualifierListAst(QualifierListAst node, MofQuirks quirks = MofQuirks.None)
        {

            // [Abstract, OCL]

            var omitSpacesQuirkEnabled = (quirks & MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations) == MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations;

            var source = new StringBuilder();
            var lastQualifierName = default(string);

            source.Append('[');
            for (var i = 0; i < node.QualifierValues.Count; i++)
            {
                var thisQualifierValue = node.QualifierValues[i];
                var thisQualifierName = thisQualifierValue.QualifierName.GetNormalizedName();
                if (i > 0)
                {
                    source.Append(',');
                    if (!omitSpacesQuirkEnabled || (lastQualifierName != "in") || (thisQualifierName != "out"))
                    {
                        source.Append(' ');
                    }
                }
                source.Append(AstMofGenerator.ConvertQualifierValueAst(thisQualifierValue, quirks));
                lastQualifierName = thisQualifierName;
            }
            source.Append(']');

            return source.ToString();

        }

        public static string ConvertQualifierValueAst(QualifierValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append(node.QualifierName.Extent.Text);
            if (node.Initializer != null)
            {
                source.Append(AstMofGenerator.ConvertIQualifierInitializerAst(node.Initializer, quirks));
            }
            ///
            /// 7.4 Qualifiers
            ///
            /// NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
            /// MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
            ///
            /// These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
            ///
            if (node.Flavors.Any())
            {
                source.Append(": ");
                source.Append(
                    string.Join(
                        " ",
                        node.Flavors.Select(f => f.Name)
                    )
                );
            }
            return source.ToString();
        }

        public static string ConvertIQualifierInitializerAst(IQualifierInitializerAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node switch
            {
                QualifierValueInitializerAst ast =>
                    AstMofGenerator.ConvertQualifierValueInitializerAst(ast, quirks),
                QualifierValueArrayInitializerAst ast =>
                    AstMofGenerator.ConvertQualifierValueArrayInitializerAst(ast, quirks),
                _ =>
                    throw new NotImplementedException()
            };
        }

        public static string ConvertQualifierValueInitializerAst(QualifierValueInitializerAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            // e.g.
            // Description("an instance of a class that derives from the GOLF_Base class. ")
            source.Append('(');
            source.Append(AstMofGenerator.ConvertLiteralValueAst(node.Value, quirks));
            source.Append(')');
            return source.ToString();
        }

        public static string ConvertQualifierValueArrayInitializerAst(QualifierValueArrayInitializerAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            // e.g.
            // OCL{"-- the key property cannot be NULL", "inv: InstanceId.size() = 10"}
            source.Append('{');
            var index = 0;
            foreach (var value in node.Values)
            {
                if (index > 0)
                {
                    source.Append(", ");
                }
                source.Append(AstMofGenerator.ConvertLiteralValueAst(value, quirks));
                index++;
            }
            source.Append('}');
            return source.ToString();
        }

        #endregion

        #region 7.5.1 Structure declaration

        public static string ConvertStructureDeclarationAst(StructureDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.AppendLine(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(indent);
            }
            source.Append($"{Constants.STRUCTURE} {node.StructureName.Name}");
            if (node.SuperStructure != null)
            {
                source.Append($" : {node.SuperStructure.Name}");
            }
            source.AppendLine();
            source.Append(indent);
            source.AppendLine("{");
            foreach (var structureFeature in node.StructureFeatures)
            {
                source.Append(indent);
                source.Append('\t');
                source.AppendLine(AstMofGenerator.ConvertStructureFeatureAst(structureFeature, quirks, indent + '\t'));
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertStructureFeatureAst(IStructureFeatureAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            return node switch
            {
                StructureDeclarationAst ast => AstMofGenerator.ConvertStructureDeclarationAst(ast, quirks, indent),
                EnumerationDeclarationAst ast => AstMofGenerator.ConvertEnumerationDeclarationAst(ast, quirks, indent),
                PropertyDeclarationAst ast => AstMofGenerator.ConvertPropertyDeclarationAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        #endregion

        #region 7.5.2 Class declaration

        public static string ConvertClassDeclarationAst(ClassDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.AppendLine(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(indent);
            }
            source.Append($"{Constants.CLASS} {node.ClassName.Name}");
            if (node.SuperClass != null)
            {
                source.Append($" : {node.SuperClass.Name}");
            }
            source.AppendLine();
            source.Append(indent);
            source.AppendLine("{");
            foreach (var classFeature in node.ClassFeatures)
            {
                source.Append(indent);
                source.Append('\t');
                source.Append(AstMofGenerator.ConvertClassFeatureAst(classFeature, quirks, indent + '\t'));
				source.AppendLine();
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertClassFeatureAst(IClassFeatureAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            return node switch
            {
                IStructureFeatureAst ast => AstMofGenerator.ConvertStructureFeatureAst(ast, quirks, indent),
                MethodDeclarationAst ast => AstMofGenerator.ConvertMethodDeclarationAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        #endregion

        #region 7.5.3 Association declaration

        public static string ConvertAssociationDeclarationAst(AssociationDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.AppendLine(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(indent);
            }
            source.Append($"{Constants.ASSOCIATION} {node.AssociationName.Name}");
            if (node.SuperAssociation != null)
            {
                source.Append($" : {node.SuperAssociation.Name}");
            }
            source.AppendLine();
            source.Append(indent);
            source.AppendLine("{");
            foreach (var classFeature in node.ClassFeatures)
            {
                source.Append(indent);
                source.Append('\t');
                source.AppendLine(AstMofGenerator.ConvertClassFeatureAst(classFeature, quirks, indent + '\t'));
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        #endregion

        #region 7.5.4 Enumeration declaration

        public static string ConvertEnumerationDeclarationAst(EnumerationDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.AppendLine(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(indent);
            }
            source.Append($"{Constants.ENUMERATION} {node.EnumName.Name}");
            source.Append($" : {node.EnumType.Name}");
            source.AppendLine();
            source.Append(indent);
            source.AppendLine("{");
            for (var i = 0; i < node.EnumElements.Count; i++)
            {
                source.Append(indent);
                source.Append('\t');
                source.Append(AstMofGenerator.ConvertEnumElementAst(node.EnumElements[i], quirks));
                if (i < (node.EnumElements.Count - 1))
                {
                    source.Append(',');
                }
                source.AppendLine();
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertEnumElementAst(EnumElementAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(' ');
            }
            source.Append(node.EnumElementName.Name);
            if (node.EnumElementValue != null)
            {
                source.Append(" = ");
                source.Append(AstMofGenerator.ConvertIEnumElementValueAst(node.EnumElementValue, quirks));
            }
            return source.ToString();
        }

        public static string ConvertIEnumElementValueAst(IEnumElementValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node switch
            {
                IntegerValueAst ast => AstMofGenerator.ConvertIntegerValueAst(ast, quirks),
                StringValueAst ast => AstMofGenerator.ConvertStringValueAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        #endregion

        #region 7.5.5 Property declaration

        public static string ConvertPropertyDeclarationAst(PropertyDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(' ');
            }
            source.Append(node.ReturnType.Name);
            source.Append(' ');
            if (node.ReturnTypeIsRef)
            {
                var returnTypeRef = node.ReturnTypeRef ?? throw new NullReferenceException();
                source.Append(returnTypeRef.Name);
                source.Append(' ');
            }
            source.Append(node.PropertyName.Name);
            if (node.ReturnTypeIsArray)
            {
                source.Append("[]");
            }
            if (node.Initializer != null)
            {
                source.Append(" = ");
                source.Append(AstMofGenerator.ConvertPropertyValueAst(node.Initializer, quirks));
            }
            source.Append(';');
            return source.ToString();
        }

        #endregion

        #region 7.5.6 Method declaration

        public static string ConvertMethodDeclarationAst(MethodDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {

            var prefixQuirkEnabled = (quirks & MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations) == MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations;

            var source = new StringBuilder();

            if (node.QualifierList.QualifierValues.Any())
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
            }

            if (prefixQuirkEnabled || node.QualifierList.QualifierValues.Any())
            {
                source.Append(' ');
            }

            source.Append(node.ReturnType.Name);
            if (node.ReturnTypeIsArray)
            {
                source.Append("[]");
            }

            source.Append(' ');
            source.Append(node.Name.Name);

            if (node.Parameters.Count == 0)
            {
                source.Append("();");
            }
            else
            {
                var values = node.Parameters.Select(p => AstMofGenerator.ConvertParameterDeclarationAst(p, quirks)).ToArray();
                source.Append('(');
                source.Append(string.Join(", ", values));
                source.Append(");");
            }

            return source.ToString();

        }

        #endregion

        #region 7.5.7 Parameter declaration

        public static string ConvertParameterDeclarationAst(ParameterDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Any())
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(' ');
            }
            source.Append(node.ParameterType.Name);
            source.Append(' ');
            if (node.ParameterIsRef)
            {
                var parameterRef = node.ParameterRef ?? throw new NullReferenceException();
                source.Append(parameterRef.Name);
                source.Append(' ');
            }
            source.Append(node.ParameterName.Name);
            if (node.ParameterIsArray)
            {
                source.Append("[]");
            }
            if (node.DefaultValue != null)
            {
                source.Append(" = ");
                source.Append(AstMofGenerator.ConvertPropertyValueAst(node.DefaultValue, quirks));
            }
            return source.ToString();
        }

        #endregion

        #region 7.5.9 Complex type value

        public static string ConvertComplexTypeValueAst(ComplexTypeValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            return node switch
            {
                ComplexValueArrayAst ast => AstMofGenerator.ConvertComplexValueArrayAst(ast, quirks, indent),
                ComplexValueAst ast => AstMofGenerator.ConvertComplexValueAst(ast, quirks, indent),
                _ => throw new NotImplementedException(),
            };
        }

        public static string ConvertComplexValueArrayAst(ComplexValueArrayAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            source.Append('{');
            source.Append(
                string.Join(
                    ", ",
                    node.Values
                        .Select(n => AstMofGenerator.ConvertComplexValueAst(n, quirks, indent))
                )
            );
            source.Append('}');
            return source.ToString();
        }

        public static string ConvertComplexValueAst(ComplexValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            if (node.IsAlias)
            {
                var alias = node.Alias ?? throw new NullReferenceException();
                return $"${alias.Name}";
            }
            else
            {
                var source = new StringBuilder();
                // value of GOLF_PhoneNumber
                var value = node.Value ?? throw new NullReferenceException();
                source.Append(value.Extent.Text);
                source.Append(' ');
                var of = node.Of ?? throw new NullReferenceException();
                source.Append(of.Extent.Text);
                source.Append(' ');
                var typeName = node.TypeName ?? throw new NullReferenceException();
                source.Append(typeName.Name);
                source.AppendLine();
                // {
                //     AreaCode = { "9", "0", "7" };
                //     Number = { "7", "4", "7", "4", "8", "8", "4" };
                // }
                source.Append(indent);
                source.Append(AstMofGenerator.ConvertPropertyValueListAst(node.PropertyValues, quirks, indent));
                return source.ToString();
            }
        }

        public static string ConvertPropertyValueListAst(PropertyValueListAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            // {
            var source = new StringBuilder();
            source.Append('{');
            source.AppendLine();
            //     Reference = TRUE;
            foreach (var propertyValue in node.PropertyValues)
            {
                source.Append(indent);
                source.Append('\t');
                source.Append(propertyValue.Key);
                source.Append(" = ");
                source.Append(AstMofGenerator.ConvertPropertyValueAst(propertyValue.Value, quirks, indent + "\t"));
                source.Append(';');
                source.AppendLine();
            }
            // }
            source.Append(indent);
            source.Append('}');
            return source.ToString();
        }

        public static string ConvertPropertyValueAst(PropertyValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            return node switch
            {
                PrimitiveTypeValueAst ast => AstMofGenerator.ConvertPrimitiveTypeValueAst(ast, quirks, indent),
                ComplexTypeValueAst ast => AstMofGenerator.ConvertComplexTypeValueAst(ast, quirks, indent),
                //ReferenceTypeValueAst ast =>
                EnumTypeValueAst ast => AstMofGenerator.ConvertEnumTypeValueAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        #endregion

        #region 7.6.1 Primitive type value

        public static string ConvertPrimitiveTypeValueAst(PrimitiveTypeValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            return node switch
            {
                LiteralValueAst ast => AstMofGenerator.ConvertLiteralValueAst(ast, quirks),
                LiteralValueArrayAst ast => AstMofGenerator.ConvertLiteralValueArrayAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        public static string ConvertLiteralValueAst(LiteralValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node switch
            {
                IntegerValueAst ast => AstMofGenerator.ConvertIntegerValueAst(ast, quirks),
                RealValueAst ast => AstMofGenerator.ConvertRealValueAst(ast, quirks),
                BooleanValueAst ast => AstMofGenerator.ConvertBooleanValueAst(ast, quirks),
                NullValueAst ast => AstMofGenerator.ConvertNullValueAst(ast, quirks),
                StringValueAst ast => AstMofGenerator.ConvertStringValueAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        public static string ConvertLiteralValueArrayAst(LiteralValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append('{');
            source.Append(
                string.Join(
                    ", ",
                    node.Values.Select(v => AstMofGenerator.ConvertLiteralValueAst(v, quirks))
                )
            );
            source.Append('}');
            return source.ToString();
        }

        #endregion

        #region 7.6.1.1 Integer value

        public static string ConvertIntegerValueAst(IntegerValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.IntegerLiteralToken.Extent.Text;
        }

        #endregion

        #region 7.6.1.2 Real value

        public static string ConvertRealValueAst(RealValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.RealLiteralToken.Extent.Text;
        }

        #endregion

        #region 7.6.1.3 String values

        public static string ConvertStringValueAst(StringValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Join(
                " ",
                node.StringLiteralValues
                    .Select(n => $"\"{StringLiteralToken.EscapeString(n.Value)}\"")
            );
        }

        #endregion

        #region 7.6.1.5 Boolean value

        public static string ConvertBooleanValueAst(BooleanValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Token.Extent.Text;
        }

        #endregion

        #region 7.6.1.6 Null value

        public static string ConvertNullValueAst(NullValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Token.Extent.Text;
        }

        #endregion

        #region 7.6.2 Complex type value

        public static string ConvertInstanceValueDeclarationAst(InstanceValueDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            // instance of myType as $Alias00000070
            // {
            //     Reference = TRUE;
            // };
            var source = new StringBuilder();
            // instance of myType as $Alias00000070
            source.Append(node.Instance.Extent.Text);
            source.Append(' ');
            source.Append(node.Of.Extent.Text);
            source.Append(' ');
            source.Append(node.TypeName.Name);
            if (node.Alias != null)
            {
                source.Append(' ');
                var @as = node.As ?? throw new NullReferenceException();
                source.Append(@as.Extent.Text);
                source.Append(' ');
                source.Append('$');
                source.Append(node.Alias.Name);
            }
            source.AppendLine();
            // {
            //     Reference = TRUE;
            // }
            source.Append(AstMofGenerator.ConvertPropertyValueListAst(node.PropertyValues, quirks, indent));
            // ;
            source.Append(node.StatementEnd.Extent.Text);
            return source.ToString();
        }

        public static string ConvertStructureValueDeclarationAst(StructureValueDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            // value of GOLF_PhoneNumber as $JohnDoesPhoneNo
            // {
            //     AreaCode = { "9", "0", "7" };
            //     Number = { "7", "4", "7", "4", "8", "8", "4" };
            // };
            var source = new StringBuilder();
            // value of GOLF_PhoneNumber as $JohnDoesPhoneNo
            source.Append(node.Value.Extent.Text);
            source.Append(' ');
            source.Append(node.Of.Extent.Text);
            source.Append(' ');
            source.Append(node.TypeName.Name);
            if (node.Alias != null)
            {
                source.Append(' ');
                var @as = node.As ?? throw new NullReferenceException();
                source.Append(@as.Extent.Text);
                source.Append(" $");
                source.Append(node.Alias.Name);
            }
            source.AppendLine();
            // {
            //     AreaCode = { "9", "0", "7" };
            //     Number = { "7", "4", "7", "4", "8", "8", "4" };
            // }
            source.Append(AstMofGenerator.ConvertPropertyValueListAst(node.PropertyValues, quirks));
            // ;
            source.Append(node.StatementEnd.Extent.Text);
            return source.ToString();
        }

        #endregion

        #region 7.6.3 Enum type value

        public static string ConvertEnumTypeValueAst(EnumTypeValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node switch
            {
                EnumValueAst ast => AstMofGenerator.ConvertEnumValueAst(ast, quirks),
                EnumValueArrayAst ast => AstMofGenerator.ConvertEnumValueArrayAst(ast, quirks),
                _ => throw new NotImplementedException(),
            };
        }

        public static string ConvertEnumValueAst(EnumValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.EnumName != null)
            {
                source.Append(node.EnumName.Name);
                source.Append('.');
            }
            source.Append(node.EnumLiteral.Name);
            return source.ToString();
        }

        public static string ConvertEnumValueArrayAst(EnumValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append('{');
            source.Append(
                string.Join(
                    ", ",
                    node.Values.Select(v => AstMofGenerator.ConvertEnumValueAst(v, quirks))
                )
            );
            source.Append('}');
            return source.ToString();
        }

        #endregion

    }

}

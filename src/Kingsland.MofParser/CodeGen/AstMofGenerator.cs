using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.CodeGen
{

    public sealed class AstMofGenerator
    {

        #region Dispatcher

        public static string ConvertToMof(AstNode node, MofQuirks quirks = MofQuirks.None)
        {
            if (node == null)
            {
                return null;
            }
            switch (node)
            {
                case MofSpecificationAst ast:
                    // 7.2 MOF specification
                    return AstMofGenerator.ConvertMofSpecificationAst(ast, quirks);
                case CompilerDirectiveAst ast:
                    // 7.3 Compiler directives
                    return AstMofGenerator.ConvertCompilerDirectiveAst(ast, quirks);
                case QualifierTypeDeclarationAst ast:
                    // 7.4 Qualifiers
                    return AstMofGenerator.ConvertQualifierTypeDeclarationAst(ast, quirks);
                case QualifierListAst ast:
                    // 7.4.1 QualifierList
                    return AstMofGenerator.ConvertQualifierListAst(ast, quirks);
                case ClassDeclarationAst ast:
                    // 7.5.2 Class declaration
                    return AstMofGenerator.ConvertClassDeclarationAst(ast, quirks);
                case PropertyDeclarationAst ast:
                    // 7.5.5 Property declaration
                    return AstMofGenerator.ConvertPropertyDeclarationAst(ast, quirks);
                case MethodDeclarationAst ast:
                    // 7.5.6 Method declaration
                    return AstMofGenerator.ConvertMethodDeclarationAst(ast, quirks);
                case ParameterDeclarationAst ast:
                    // 7.5.7 Parameter declaration
                    return AstMofGenerator.ConvertParameterDeclarationAst(ast, quirks);
                case ComplexValueArrayAst ast:
                    // 7.5.9 Complex type value
                    return AstMofGenerator.ConvertComplexValueArrayAst(ast, quirks);
                case ComplexValueAst ast:
                    // 7.5.9 Complex type value
                    return AstMofGenerator.ConvertComplexValueAst(ast, quirks);
                case PropertyValueListAst ast:
                    // 7.5.9 Complex type value
                    return AstMofGenerator.ConvertPropertyValueListAst(ast, quirks);
                case LiteralValueArrayAst ast:
                    // 7.6.1 Primitive type value
                    return AstMofGenerator.ConvertLiteralValueArrayAst(ast, quirks);
                case IntegerValueAst ast:
                    // 7.6.1.1 Integer value
                    return AstMofGenerator.ConvertIntegerValueAst(ast, quirks);
                case RealValueAst ast:
                    // 7.6.1.2 Real value
                    return AstMofGenerator.ConvertRealValueAst(ast, quirks);
                case StringValueAst ast:
                    // 7.6.1.3 String values
                    return AstMofGenerator.ConvertStringValueAst(ast, quirks);
                case BooleanValueAst ast:
                    // 7.6.1.5 Boolean value
                    return AstMofGenerator.ConvertBooleanValueAst(ast, quirks);
                case NullValueAst ast:
                    // 7.6.1.6 Null value
                    return AstMofGenerator.ConvertNullValueAst(ast, quirks);
                case InstanceValueDeclarationAst ast:
                    // 7.6.2 Complex type value
                    return AstMofGenerator.ConvertInstanceValueDeclarationAst(ast, quirks);
                case StructureValueDeclarationAst ast:
                    // 7.6.2 Complex type value
                    return AstMofGenerator.ConvertStructureValueDeclarationAst(ast, quirks);
                case EnumValueAst ast:
                    // 7.6.3 Enum type value
                    return AstMofGenerator.ConvertEnumValueAst(ast, quirks);
                case EnumValueArrayAst ast:
                    // 7.6.3 Enum type value
                    return AstMofGenerator.ConvertEnumValueArrayAst(ast, quirks);
                case EnumTypeValueAst ast:
                    // 7.6.3 Enum type value
                    return AstMofGenerator.ConvertEnumTypeValueAst(ast, quirks);
                case PropertyValueAst ast:
                    // 7.5.9 Complex type value
                    return AstMofGenerator.ConvertPropertyValueAst(ast, quirks);
                default:
                    // unknown
                    throw new InvalidOperationException(node.GetType().FullName);
            }

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
            switch (node)
            {
                case CompilerDirectiveAst ast:
                    return AstMofGenerator.ConvertCompilerDirectiveAst(ast, quirks);
                case StructureDeclarationAst ast:
                    return AstMofGenerator.ConvertStructureDeclarationAst(ast, quirks);
                case ClassDeclarationAst ast:
                    return AstMofGenerator.ConvertClassDeclarationAst(ast, quirks);
                case AssociationDeclarationAst ast:
                    return AstMofGenerator.ConvertAssociationDeclarationAst(ast, quirks);
                case EnumerationDeclarationAst ast:
                    return AstMofGenerator.ConvertEnumerationDeclarationAst(ast, quirks);
                case InstanceValueDeclarationAst ast:
                    return AstMofGenerator.ConvertInstanceValueDeclarationAst(ast, quirks);
                case StructureValueDeclarationAst ast:
                    return AstMofGenerator.ConvertStructureValueDeclarationAst(ast, quirks);
                case QualifierTypeDeclarationAst ast:
                    return AstMofGenerator.ConvertQualifierTypeDeclarationAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.3 Compiler directives

        public static string ConvertCompilerDirectiveAst(CompilerDirectiveAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append(node.PragmaKeyword.Extent.Text);
            source.Append(" ");
            source.Append(node.PragmaName);
            source.Append(" ");
            source.Append("(");
            source.Append(AstMofGenerator.ConvertStringValueAst(node.PragmaParameter));
            source.Append(")");
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
            if (node.Initializer != null)
            {
                if (node.Initializer is LiteralValueAst)
                {
                    source.Append("(");
                    source.Append(AstMofGenerator.ConvertPrimitiveTypeValueAst(node.Initializer, quirks));
                    source.Append(")");
                }
                else if (node.Initializer is LiteralValueArrayAst)
                {
                    source.Append(AstMofGenerator.ConvertPrimitiveTypeValueAst(node.Initializer, quirks));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            if (node.Flavors.Count > 0)
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

            source.Append("[");
            for (var i = 0; i < node.QualifierValues.Count; i++)
            {
                var thisQualifierValue = node.QualifierValues[i];
                var thisQualifierName = thisQualifierValue.QualifierName.GetNormalizedName();
                if (i > 0)
                {
                    source.Append(",");
                    if (!omitSpacesQuirkEnabled || (lastQualifierName != "in") || (thisQualifierName != "out"))
                    {
                        source.Append(" ");
                    }
                }
                source.Append(AstMofGenerator.ConvertQualifierValueAst(thisQualifierValue, quirks));
                lastQualifierName = thisQualifierName;
            }
            source.Append("]");

            return source.ToString();

        }

        public static string ConvertQualifierValueAst(QualifierValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            // Abstract
            // Description("an instance of a class that derives from the GOLF_Base class. ")
            // OCL{"-- the key property cannot be NULL", "inv: InstanceId.size() = 10"}
            var source = new StringBuilder();
            source.Append(node.QualifierName.Extent.Text);
            if (node.ValueInitializer != null)
            {
                source.Append("(");
                source.Append(AstMofGenerator.ConvertLiteralValueAst(node.ValueInitializer, quirks));
                source.Append(")");
            }
            else if (node.ValueArrayInitializer != null)
            {
                source.Append(AstMofGenerator.ConvertLiteralValueArrayAst(node.ValueArrayInitializer, quirks));
            }
            ///
            /// 7.4 Qualifiers
            ///
            /// NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
            /// MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
            ///
            /// These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
            ///
            if (node.Flavors.Count > 0)
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

        #endregion

        #region 7.5.1 Structure declaration

        public static string ConvertStructureDeclarationAst(StructureDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Count > 0)
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
                source.Append("\t");
                source.AppendLine(AstMofGenerator.ConvertStructureFeatureAst(structureFeature, quirks, indent + '\t'));
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertStructureFeatureAst(IStructureFeatureAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            switch (node)
            {
                case StructureDeclarationAst ast:
                    return AstMofGenerator.ConvertStructureDeclarationAst(ast, quirks, indent);
                case EnumerationDeclarationAst ast:
                    return AstMofGenerator.ConvertEnumerationDeclarationAst(ast, quirks, indent);
                case PropertyDeclarationAst ast:
                    return AstMofGenerator.ConvertPropertyDeclarationAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.5.2 Class declaration

        public static string ConvertClassDeclarationAst(ClassDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Count > 0)
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
                source.Append("\t");
                source.Append(AstMofGenerator.ConvertClassFeatureAst(classFeature, quirks, indent + '\t'));
				source.AppendLine();
            }
            source.Append(indent);
            source.Append("};");
            return source.ToString();
        }

        public static string ConvertClassFeatureAst(IClassFeatureAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            switch (node)
            {
                case IStructureFeatureAst ast:
                    return AstMofGenerator.ConvertStructureFeatureAst(ast, quirks, indent);
                case MethodDeclarationAst ast:
                    return AstMofGenerator.ConvertMethodDeclarationAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.5.3 Association declaration

        public static string ConvertAssociationDeclarationAst(AssociationDeclarationAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Count > 0)
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
                source.Append("\t");
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
            if (node.QualifierList.QualifierValues.Count > 0)
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
                source.Append("\t");
                source.Append(AstMofGenerator.ConvertEnumElementAst(node.EnumElements[i], quirks));
                if (i < (node.EnumElements.Count - 1))
                {
                    source.Append(",");
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
            if (node.QualifierList.QualifierValues.Count > 0)
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(" ");
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
            switch (node)
            {
                case IntegerValueAst ast:
                    return AstMofGenerator.ConvertIntegerValueAst(ast, quirks);
                case StringValueAst ast:
                    return AstMofGenerator.ConvertStringValueAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.5.5 Property declaration

        public static string ConvertPropertyDeclarationAst(PropertyDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.QualifierList.QualifierValues.Count > 0)
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(" ");
            }
            source.Append(node.ReturnType.Name);
            source.Append(" ");
            if (node.ReturnTypeIsRef)
            {
                source.Append(node.ReturnTypeRef.Name);
                source.Append(" ");
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
            source.Append(";");
            return source.ToString();
        }

        #endregion

        #region 7.5.6 Method declaration

        public static string ConvertMethodDeclarationAst(MethodDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {

            var prefixQuirkEnabled = (quirks & MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations) == MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations;

            var source = new StringBuilder();

            if (node.QualifierList.QualifierValues.Count > 0)
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
            }

            if (prefixQuirkEnabled || (node.QualifierList.QualifierValues.Count > 0))
            {
                source.Append(" ");
            }

            source.Append(node.ReturnType.Name);
            if (node.ReturnTypeIsArray)
            {
                source.Append("[]");
            }

            source.Append(" ");
            source.Append(node.Name.Name);

            if (node.Parameters.Count == 0)
            {
                source.Append("();");
            }
            else
            {
                var values = node.Parameters.Select(p => AstMofGenerator.ConvertParameterDeclarationAst(p, quirks)).ToArray();
                source.Append("(");
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
            if (node.QualifierList.QualifierValues.Count > 0)
            {
                source.Append(AstMofGenerator.ConvertQualifierListAst(node.QualifierList, quirks));
                source.Append(" ");
            }
            source.Append(node.ParameterType.Name);
            source.Append(" ");
            if (node.ParameterIsRef)
            {
                source.Append(node.ParameterRef.Name);
                source.Append(" ");
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
            switch (node)
            {
                case ComplexValueArrayAst ast:
                    return AstMofGenerator.ConvertComplexValueArrayAst(ast, quirks, indent);
                case ComplexValueAst ast:
                    return AstMofGenerator.ConvertComplexValueAst(ast, quirks, indent);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertComplexValueArrayAst(ComplexValueArrayAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            var source = new StringBuilder();
            source.Append("{");
            source.Append(
                string.Join(
                    ", ",
                    node.Values
                        .Select(n => AstMofGenerator.ConvertComplexValueAst(n, quirks, indent))
                )
            );
            source.Append("}");
            return source.ToString();
        }

        public static string ConvertComplexValueAst(ComplexValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            if (node.IsAlias)
            {
                return $"${node.Alias.Name}";
            }
            else
            {
                var source = new StringBuilder();
                // value of GOLF_PhoneNumber
                source.Append(node.Value.Extent.Text);
                source.Append(" ");
                source.Append(node.Of.Extent.Text);
                source.Append(" ");
                source.Append(node.TypeName.Name);
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
            source.Append("{");
            source.AppendLine();
            //     Reference = TRUE;
            foreach (var propertyValue in node.PropertyValues)
            {
                source.Append(indent);
                source.Append("\t");
                source.Append(propertyValue.Key);
                source.Append(" = ");
                source.Append(AstMofGenerator.ConvertPropertyValueAst(propertyValue.Value, quirks, indent + "\t"));
                source.Append(";");
                source.AppendLine();
            }
            // }
            source.Append(indent);
            source.Append("}");
            return source.ToString();
        }

        public static string ConvertPropertyValueAst(PropertyValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            switch (node)
            {
                case PrimitiveTypeValueAst ast:
                    return AstMofGenerator.ConvertPrimitiveTypeValueAst(ast, quirks, indent);
                case ComplexTypeValueAst ast:
                    return AstMofGenerator.ConvertComplexTypeValueAst(ast, quirks, indent);
                //case ReferenceTypeValueAst ast:
                case EnumTypeValueAst ast:
                    return AstMofGenerator.ConvertEnumTypeValueAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        #endregion

        #region 7.6.1 Primitive type value

        public static string ConvertPrimitiveTypeValueAst(PrimitiveTypeValueAst node, MofQuirks quirks = MofQuirks.None, string indent = "")
        {
            switch (node)
            {
                case LiteralValueAst ast:
                    return AstMofGenerator.ConvertLiteralValueAst(ast, quirks);
                case LiteralValueArrayAst ast:
                    return AstMofGenerator.ConvertLiteralValueArrayAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertLiteralValueAst(LiteralValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            switch (node)
            {
                case IntegerValueAst ast:
                    return AstMofGenerator.ConvertIntegerValueAst(ast, quirks);
                case RealValueAst ast:
                    return AstMofGenerator.ConvertRealValueAst(ast, quirks);
                case BooleanValueAst ast:
                    return AstMofGenerator.ConvertBooleanValueAst(ast, quirks);
                case NullValueAst ast:
                    return AstMofGenerator.ConvertNullValueAst(ast, quirks);
                case StringValueAst ast:
                    return AstMofGenerator.ConvertStringValueAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertLiteralValueArrayAst(LiteralValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append("{");
            source.Append(
                string.Join(
                    ", ",
                    node.Values.Select(v => AstMofGenerator.ConvertLiteralValueAst(v, quirks))
                )
            );
            source.Append("}");
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
                    .Select(n => $"\"{AstMofGenerator.EscapeString(n.Value)}\"")
            );
        }

        internal static string EscapeString(string value)
        {
            var escapeMap = new Dictionary<char, string>()
            {
                { '\\' , "\\\\" }, { '\"' , "\\\"" },  { '\'' , "\\\'" },
                { '\b', "\\b" }, { '\t', "\\t" },  { '\n', "\\n" }, { '\f', "\\f" }, { '\r', "\\r" }
            };
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var escaped = new StringBuilder();
            foreach (var @char in value.ToCharArray())
            {
                if (escapeMap.ContainsKey(@char))
                {
                    escaped.Append(escapeMap[@char]);
                }
                else if ((@char >= 32) && (@char <= 126))
                {
                    // printable characters ' ' - '~'
                    escaped.Append(@char);
                }
                else
                {
                    throw new InvalidOperationException(new string(new char[] { @char }));
                }
            }
            return escaped.ToString();
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
            source.Append(" ");
            source.Append(node.Of.Extent.Text);
            source.Append(" ");
            source.Append(node.TypeName.Name);
            if (node.Alias != null)
            {
                source.Append(" ");
                source.Append(node.As.Extent.Text);
                source.Append(" ");
                source.Append("$");
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
            source.Append(" ");
            source.Append(node.Of.Extent.Text);
            source.Append(" ");
            source.Append(node.TypeName.Name);
            if (node.Alias != null)
            {
                source.Append(" ");
                source.Append(node.As.Extent.Text);
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
            switch (node)
            {
                case EnumValueAst ast:
                    return AstMofGenerator.ConvertEnumValueAst(ast, quirks);
                case EnumValueArrayAst ast:
                    return AstMofGenerator.ConvertEnumValueArrayAst(ast, quirks);
                default:
                    throw new NotImplementedException();
            }
        }

        public static string ConvertEnumValueAst(EnumValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.EnumName != null)
            {
                source.Append(node.EnumName.Name);
                source.Append(".");
            }
            source.Append(node.EnumLiteral.Name);
            return source.ToString();
        }

        public static string ConvertEnumValueArrayAst(EnumValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            source.Append("{");
            source.Append(
                string.Join(
                    ", ",
                    node.Values.Select(v => AstMofGenerator.ConvertEnumValueAst(v, quirks))
                )
            );
            source.Append("}");
            return source.ToString();
        }

        #endregion

    }

}

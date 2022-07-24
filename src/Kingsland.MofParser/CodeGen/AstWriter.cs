using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.CodeGen;

public sealed class AstWriter
{

    #region Constructors

    private AstWriter(TextWriter output, MofWriterSettings settings)
    {
        this.TextWriter = output;
        this.Settings = settings;
    }

    #endregion

    #region Properties

    private TextWriter TextWriter
    {
        get;
        init;
    }

    private MofWriterSettings Settings
    {
        get;
        init;
    }

    #endregion

    #region Static Members

    public static AstWriter Create(Stream output)
    {
        return AstWriter.Create(output, new MofWriterSettings());
    }

    public static AstWriter Create(Stream output, MofWriterSettings settings)
    {
        return new AstWriter(
            new StreamWriter(output),
            settings
        );
    }

    public static AstWriter Create(TextWriter output)
    {
        return AstWriter.Create(output, new MofWriterSettings());
    }

    public static AstWriter Create(TextWriter output, MofWriterSettings settings)
    {
        return new AstWriter(output, settings);
    }

    #endregion

    #region Write Helpers

    private int IndentDepth
    {
        get;
        set;
    }

    private void Indent()
    {
        this.IndentDepth += 1;
    }

    private void Unindent()
    {
        if (this.IndentDepth == 0)
        {
            throw new InvalidOperationException();
        }
        this.IndentDepth -= 1;
    }

    private void WriteIndent()
    {
        for (var i = 0; i < this.IndentDepth; i++)
        {
            this.TextWriter.Write(this.Settings.IndentStep);
        }
    }

    private void WriteLine()
    {
        this.TextWriter.WriteLine();
    }

    private void Write(char value)
    {
        this.TextWriter.Write(value);
    }

    private void Write(string value)
    {
        this.TextWriter.Write(value);
    }

    private void WriteList<T>(IEnumerable<T> source, Action<T> writer, string separator)
    {
        var count = 0;
        foreach (var value in source)
        {
            if (count > 0)
            {
                this.Write(separator);
            }
            writer(value);
            count++;
        }
    }

    #endregion

    #region 7.2 MOF specification

    public void WriteMofSpecificationAst(MofSpecificationAst node)
    {
        for (var i = 0; i < node.Productions.Count; i++)
        {
            if (i > 0)
            {
                this.WriteLine();
            }
            this.WriteMofProductionAst(
                node.Productions[i]
            );
        }
    }

    public void WriteMofProductionAst(MofProductionAst node)
    {
        switch (node)
        {
            case CompilerDirectiveAst ast:
                this.WriteCompilerDirectiveAst(ast);
                break;
            case StructureDeclarationAst ast:
                this.WriteStructureDeclarationAst(ast);
                break;
            case ClassDeclarationAst ast:
                this.WriteClassDeclarationAst(ast);
                break;
            case AssociationDeclarationAst ast:
                this.WriteAssociationDeclarationAst(ast);
                break;
            case EnumerationDeclarationAst ast:
                this.WriteEnumerationDeclarationAst(ast);
                break;
            case InstanceValueDeclarationAst ast:
                this.WriteInstanceValueDeclarationAst(ast);
                break;
            case StructureValueDeclarationAst ast:
                this.WriteStructureValueDeclarationAst(ast);
                break;
            case QualifierTypeDeclarationAst ast:
                this.WriteQualifierTypeDeclarationAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

    #region 7.3 Compiler directives

    public void WriteCompilerDirectiveAst(CompilerDirectiveAst node)
    {
        this.Write(node.PragmaKeyword.Extent.Text);
        this.Write(' ');
        this.Write(node.PragmaName.Name);
        this.Write(' ');
        this.Write('(');
        this.Write(
            AstMofGenerator.ConvertStringValueAst(
                node.PragmaParameter
            )
        );
        this.Write(')');
    }

    #endregion

    #region 7.4 Qualifiers

    public void WriteQualifierTypeDeclarationAst(QualifierTypeDeclarationAst node)
    {
        if (!string.IsNullOrEmpty(node.QualifierName.Name))
        {
            this.Write(node.QualifierName.Name);
        }
        if (node.Flavors.Any())
        {
            this.Write(": ");
            this.Write(string.Join(" ", node.Flavors.ToArray()));
        }
    }

    #endregion

    #region 7.4.1 QualifierList

    public void WriteQualifierListAst(QualifierListAst node)
    {

        // [Abstract, OCL]

        var omitSpaceQuirkEnabled = this.Settings.Quirks.HasFlag(
            MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations
        );

        var prevQualifierValue = default(QualifierValueAst);

        this.Write('[');
        foreach (var thisQualifierValue in node.QualifierValues)
        {
            if (prevQualifierValue is not null)
            {
                this.Write(',');
                if (!omitSpaceQuirkEnabled || !prevQualifierValue.QualifierName!.IsKeyword("in") || !thisQualifierValue.QualifierName.IsKeyword("out"))
                {
                    this.Write(' ');
                }
            }
            this.WriteQualifierValueAst(
                thisQualifierValue
            );
            prevQualifierValue = thisQualifierValue;
        }
        this.Write(']');

    }

    public void WriteQualifierValueAst(QualifierValueAst node)
    {
        this.Write(node.QualifierName.Extent.Text);
        if (node.Initializer is not null)
        {
            this.WriteQualifierInitializerAst(
                node.Initializer
            );
        }
        //
        // 7.4 Qualifiers
        //
        // NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
        // MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
        //
        // These aren't part of the MOF 3.0.1 spec, but we'll include them anyway for backward compatibility.
        //
        if (node.Flavors.Any())
        {
            this.Write(": ");
            this.Write(
                string.Join(
                    " ",
                    node.Flavors.Select(f => f.Name)
                )
            );
        }
    }

    public void WriteQualifierInitializerAst(IQualifierInitializerAst node)
    {
        switch (node)
        {
            case QualifierValueInitializerAst ast:
                this.WriteQualifierValueInitializerAst(ast);
                break;
            case QualifierValueArrayInitializerAst ast:
                this.WriteQualifierValueArrayInitializerAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void WriteQualifierValueInitializerAst(QualifierValueInitializerAst node)
    {
        // e.g.
        // Description("an instance of a class that derives from the GOLF_Base class. ")
        this.Write('(');
        this.WriteLiteralValueAst(
            node.Value
        );
        this.Write(')');
    }

    public void WriteQualifierValueArrayInitializerAst(QualifierValueArrayInitializerAst node)
    {
        // e.g.
        // OCL{"-- the key property cannot be NULL", "inv: InstanceId.size() = 10"}
        this.Write('{');
        var index = 0;
        foreach (var value in node.Values)
        {
            if (index > 0)
            {
                this.Write(", ");
            }
            this.WriteLiteralValueAst(
                value
            );
            index++;
        }
        this.Write('}');
    }

    #endregion

    #region 7.5.1 Structure declaration

    public void WriteStructureDeclarationAst(StructureDeclarationAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }
        this.Write($"{Constants.STRUCTURE} {node.StructureName.Name}");
        if (node.SuperStructure is not null)
        {
            this.Write($" : {node.SuperStructure.Name}");
        }
        this.WriteLine();
        this.WriteIndent();
        this.Write("{");
        this.WriteLine();
        this.Indent();
        foreach (var structureFeature in node.StructureFeatures)
        {
            this.WriteIndent();
            this.WriteStructureFeatureAst(
                structureFeature
            );
            this.WriteLine();
        }
        this.Unindent();
        this.WriteIndent();
        this.Write("};");
    }

    public void WriteStructureFeatureAst(IStructureFeatureAst node)
    {
        switch (node)
        {
            case StructureDeclarationAst ast:
                this.WriteStructureDeclarationAst(ast);
                break;
            case EnumerationDeclarationAst ast:
                this.WriteEnumerationDeclarationAst(ast);
                break;
            case PropertyDeclarationAst ast:
                this.WritePropertyDeclarationAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

    #region 7.5.2 Class declaration

    public void WriteClassDeclarationAst(ClassDeclarationAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }
        this.Write($"{Constants.CLASS} {node.ClassName.Name}");
        if (node.SuperClass is not null)
        {
            this.Write($" : {node.SuperClass.Name}");
        }
        this.WriteLine();
        this.WriteIndent();
        this.Write("{");
        this.WriteLine();
        this.Indent();
        foreach (var classFeature in node.ClassFeatures)
        {
            this.WriteIndent();
            this.WriteClassFeatureAst(
                classFeature
            );
            this.WriteLine();
        }
        this.Unindent();
        this.WriteIndent();
        this.Write("};");
    }

    public void WriteClassFeatureAst(IClassFeatureAst node)
    {
        switch (node)
        {
            case IStructureFeatureAst ast:
                this.WriteStructureFeatureAst(ast);
                break;
            case MethodDeclarationAst ast:
                this.WriteMethodDeclarationAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

    #region 7.5.3 Association declaration

    public void WriteAssociationDeclarationAst(AssociationDeclarationAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }
        this.Write($"{Constants.ASSOCIATION} {node.AssociationName.Name}");
        if (node.SuperAssociation is not null)
        {
            this.Write($" : {node.SuperAssociation.Name}");
        }
        this.WriteLine();
        this.WriteIndent();
        this.Write("{");
        this.WriteLine();
        this.Indent();
        foreach (var classFeature in node.ClassFeatures)
        {
            this.WriteIndent();
            this.WriteClassFeatureAst(
                classFeature
            );
            this.WriteLine();
        }
        this.Unindent();
        this.WriteIndent();
        this.Write("};");
    }

    #endregion

    #region 7.5.4 Enumeration declaration

    public void WriteEnumerationDeclarationAst(EnumerationDeclarationAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
            this.WriteLine();
            this.WriteIndent();
        }
        this.Write($"{Constants.ENUMERATION} {node.EnumName.Name}");
        this.Write($" : {node.EnumType.Name}");
        this.WriteLine();
        this.WriteIndent();
        this.Write("{");
        this.WriteLine();
        this.Indent();
        for (var i = 0; i < node.EnumElements.Count; i++)
        {
            this.WriteIndent();
            this.WriteEnumElementAst(
                node.EnumElements[i]
            );
            if (i < (node.EnumElements.Count - 1))
            {
                this.Write(',');
            }
            this.WriteLine();
        }
        this.Unindent();
        this.WriteIndent();
        this.Write("};");
    }

    public void WriteEnumElementAst(EnumElementAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                    node.QualifierList
            );
            this.Write(' ');
        }
        this.Write(node.EnumElementName.Name);
        if (node.EnumElementValue is not null)
        {
            this.Write(" = ");
            this.WriteEnumElementValueAst(
                node.EnumElementValue
            );
        }
    }

    public void WriteEnumElementValueAst(IEnumElementValueAst node)
    {
        switch (node)
        {
            case IntegerValueAst ast:
                this.WriteIntegerValueAst(ast);
                break;
            case StringValueAst ast:
                this.WriteStringValueAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

    #region 7.5.5 Property declaration

    public void WritePropertyDeclarationAst(PropertyDeclarationAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
            this.Write(' ');
        }
        this.Write(node.ReturnType.Name);
        this.Write(' ');
        if (node.ReturnTypeIsRef)
        {
            var returnTypeRef = node.ReturnTypeRef
                ?? throw new NullReferenceException();
            this.Write(returnTypeRef.Name);
            this.Write(' ');
        }
        this.Write(node.PropertyName.Name);
        if (node.ReturnTypeIsArray)
        {
            this.Write("[]");
        }
        if (node.Initializer is not null)
        {
            this.Write(" = ");
            this.WritePropertyValueAst(
                node.Initializer
            );
        }
        this.Write(';');
    }

    #endregion

    #region 7.5.6 Method declaration

    public void WriteMethodDeclarationAst(MethodDeclarationAst node)
    {

        var prefixQuirkEnabled = this.Settings.Quirks.HasFlag(
            MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations
        );

        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
        }

        if (prefixQuirkEnabled || node.QualifierList.QualifierValues.Any())
        {
            this.Write(' ');
        }

        this.Write(node.ReturnType.Name);
        if (node.ReturnTypeIsArray)
        {
            this.Write("[]");
        }

        this.Write(' ');
        this.Write(node.Name.Name);

        if (node.Parameters.Count == 0)
        {
            this.Write("();");
        }
        else
        {
            this.Write('(');
            this.WriteList(
                node.Parameters,
                this.WriteParameterDeclarationAst,
                ", "
            );
            this.Write(");");
        }

    }

    #endregion

    #region 7.5.7 Parameter declaration

    public void WriteParameterDeclarationAst(ParameterDeclarationAst node)
    {
        if (node.QualifierList.QualifierValues.Any())
        {
            this.WriteQualifierListAst(
                node.QualifierList
            );
            this.Write(' ');
        }
        this.Write(node.ParameterType.Name);
        this.Write(' ');
        if (node.ParameterIsRef)
        {
            var parameterRef = node.ParameterRef
                ?? throw new NullReferenceException();
            this.Write(parameterRef.Name);
            this.Write(' ');
        }
        this.Write(node.ParameterName.Name);
        if (node.ParameterIsArray)
        {
            this.Write("[]");
        }
        if (node.DefaultValue is not null)
        {
            this.Write(" = ");
            this.WritePropertyValueAst(
                node.DefaultValue
            );
        }
    }

    #endregion

    #region 7.5.9 Complex type value

    public void WriteComplexTypeValueAst(ComplexTypeValueAst node)
    {
        switch (node)
        {
            case ComplexValueArrayAst ast:
                this.WriteComplexValueArrayAst(ast);
                break;
            case ComplexValueAst ast:
                this.WriteComplexValueAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void WriteComplexValueArrayAst(ComplexValueArrayAst node)
    {
        this.Write('{');
        this.WriteList(
            node.Values,
            this.WriteComplexValueAst,
            ", "
        );
        this.Write('}');
    }

    public void WriteComplexValueAst(ComplexValueAst node)
    {
        if (node.IsAlias)
        {
            var alias = node.Alias ?? throw new NullReferenceException();
            this.Write($"${alias.Name}");
        }
        else
        {
            // value of GOLF_PhoneNumber
            var value = node.Value ?? throw new NullReferenceException();
            this.Write(value.Extent.Text);
            this.Write(' ');
            var of = node.Of ?? throw new NullReferenceException();
            this.Write(of.Extent.Text);
            this.Write(' ');
            var typeName = node.TypeName ?? throw new NullReferenceException();
            this.Write(typeName.Name);
            this.WriteLine();
            // {
            //     AreaCode = { "9", "0", "7" };
            //     Number = { "7", "4", "7", "4", "8", "8", "4" };
            // }
            this.WriteIndent();
            this.WritePropertyValueListAst(
                node.PropertyValues
            );
        }
    }

    public void WritePropertyValueListAst(PropertyValueListAst node)
    {
        // {
        this.Write('{');
        this.WriteLine();
        //     Reference = TRUE;
        this.Indent();
        foreach (var propertyValue in node.PropertyValues)
        {
            this.WriteIndent();
            this.Write(propertyValue.Key);
            this.Write(" = ");
            this.WritePropertyValueAst(
                propertyValue.Value
            );
            this.Write(';');
            this.WriteLine();
        }
        this.Unindent();
        // }
        this.WriteIndent();
        this.Write('}');
    }

    public void WritePropertyValueAst(PropertyValueAst node)
    {
        switch (node)
        {
            case PrimitiveTypeValueAst ast:
                this.WritePrimitiveTypeValueAst(ast);
                break;
            case ComplexTypeValueAst ast:
                this.WriteComplexTypeValueAst(ast);
                break;
            //case ReferenceTypeValueAst ast:
            case EnumTypeValueAst ast:
                this.WriteEnumTypeValueAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    #endregion

    #region 7.6.1 Primitive type value

    public void WritePrimitiveTypeValueAst(PrimitiveTypeValueAst node)
    {
        switch (node)
        {
            case LiteralValueAst ast:
                this.WriteLiteralValueAst(ast);
                break;
            case LiteralValueArrayAst ast:
                this.WriteLiteralValueArrayAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void WriteLiteralValueAst(LiteralValueAst node)
    {
        switch (node)
        {
            case IntegerValueAst ast:
                this.WriteIntegerValueAst(ast);
                break;
            case RealValueAst ast:
                this.WriteRealValueAst(ast);
                break;
            case BooleanValueAst ast:
                this.WriteBooleanValueAst(ast);
                break;
            case NullValueAst ast:
                this.WriteNullValueAst(ast);
                break;
            case StringValueAst ast:
                this.WriteStringValueAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void WriteLiteralValueArrayAst(LiteralValueArrayAst node)
    {
        this.Write('{');
        this.WriteList(
            node.Values,
            this.WriteLiteralValueAst,
            ", "
        );
        this.Write('}');
    }

    #endregion

    #region 7.6.1.1 Integer value

    public void WriteIntegerValueAst(IntegerValueAst node)
    {
        this.Write(node.IntegerLiteralToken.Extent.Text);
    }

    #endregion

    #region 7.6.1.2 Real value

    public void WriteRealValueAst(RealValueAst node)
    {
        this.Write(node.RealLiteralToken.Extent.Text);
    }

    #endregion

    #region 7.6.1.3 String values

    public void WriteStringValueAst(StringValueAst node)
    {
        this.WriteList(
            node.StringLiteralValues,
            (value) => {
                this.Write($"\"{StringLiteralToken.EscapeString(value.Value)}\"");
            },
            " "
        );
    }

    #endregion

    #region 7.6.1.5 Boolean value

    public void WriteBooleanValueAst(BooleanValueAst node)
    {
        this.Write(node.Token.Extent.Text);
    }

    #endregion

    #region 7.6.1.6 Null value

    public void WriteNullValueAst(NullValueAst node)
    {
        this.Write(node.Token.Extent.Text);
    }

    #endregion

    #region 7.6.2 Complex type value

    public void WriteInstanceValueDeclarationAst(InstanceValueDeclarationAst node)
    {

        // instance of myType as $Alias00000070
        // {
        //     Reference = TRUE;
        // };

        // instance of myType as $Alias00000070
        this.Write(node.Instance.Extent.Text);
        this.Write(' ');
        this.Write(node.Of.Extent.Text);
        this.Write(' ');
        this.Write(node.TypeName.Name);
        if (node.Alias is not null)
        {
            this.Write(' ');
            var @as = node.As ?? throw new NullReferenceException();
            this.Write(@as.Extent.Text);
            this.Write(' ');
            this.Write('$');
            this.Write(node.Alias.Name);
        }
        this.WriteLine();

        // {
        //     Reference = TRUE;
        // }

        this.WritePropertyValueListAst(
            node.PropertyValues
        );

        // ;
        this.Write(node.StatementEnd.Extent.Text);

    }

    public void WriteStructureValueDeclarationAst(StructureValueDeclarationAst node)
    {

        // value of GOLF_PhoneNumber as $JohnDoesPhoneNo
        // {
        //     AreaCode = { "9", "0", "7" };
        //     Number = { "7", "4", "7", "4", "8", "8", "4" };
        // };

        // value of GOLF_PhoneNumber as $JohnDoesPhoneNo
        this.Write(node.Value.Extent.Text);
        this.Write(' ');
        this.Write(node.Of.Extent.Text);
        this.Write(' ');
        this.Write(node.TypeName.Name);
        if (node.Alias is not null)
        {
            this.Write(' ');
            var @as = node.As ?? throw new NullReferenceException();
            this.Write(@as.Extent.Text);
            this.Write(" $");
            this.Write(node.Alias.Name);
        }
        this.WriteLine();

        // {
        //     AreaCode = { "9", "0", "7" };
        //     Number = { "7", "4", "7", "4", "8", "8", "4" };
        // }
        this.WritePropertyValueListAst(
            node.PropertyValues
        );

        // ;
        this.Write(node.StatementEnd.Extent.Text);

    }

    #endregion

    #region 7.6.3 Enum type value

    public void WriteEnumTypeValueAst(EnumTypeValueAst node)
    {
        switch (node)
        {
            case EnumValueAst ast:
                this.WriteEnumValueAst(ast);
                break;
            case EnumValueArrayAst ast:
                this.WriteEnumValueArrayAst(ast);
                break;
            default:
                throw new NotImplementedException();
        }
    }

    public void WriteEnumValueAst(EnumValueAst node)
    {
        if (node.EnumName is not null)
        {
            this.Write(node.EnumName.Name);
            this.Write('.');
        }
        this.Write(node.EnumLiteral.Name);
    }

    public void WriteEnumValueArrayAst(EnumValueArrayAst node)
    {
        this.Write('{');
        var count = 0;
        foreach (var value in node.Values)
        {
            if (count > 0)
            {
                this.Write(", ");
            }
            this.WriteEnumValueAst(
                value
            );
            count++;
        }
        this.Write('}');
    }

    #endregion

}

using Kingsland.MofParser.Ast;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class AstAssert
{

    #region Node Comparison Methods

    public static void AreEqual(MofSpecificationAst? expected, MofSpecificationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.Productions.Count, Is.EqualTo(expected.Productions.Count));
        for (var i = 0; i < expected.Productions.Count; i++)
        {
            AstAssert.AreEqual(expected.Productions[i], actual.Productions[i], ignoreExtent);
        }

    }

    private static void AreEqual(MofProductionAst? expected, MofProductionAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual, Is.InstanceOf(expected.GetType()));
        switch (expected)
        {
            case CompilerDirectiveAst ast:
                AstAssert.AreEqual(ast, (CompilerDirectiveAst)actual, ignoreExtent);
                return;
            case StructureDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            case ClassDeclarationAst ast:
                AstAssert.AreEqual(ast, (ClassDeclarationAst)actual, ignoreExtent);
                return;
            case AssociationDeclarationAst ast:
                AstAssert.AreEqual(ast, (AssociationDeclarationAst)actual, ignoreExtent);
                return;
            case EnumerationDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            case InstanceValueDeclarationAst ast:
                AstAssert.AreEqual(ast, (InstanceValueDeclarationAst)actual, ignoreExtent);
                return;
            case StructureValueDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            case QualifierTypeDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    private static void AreEqual(CompilerDirectiveAst? expected, CompilerDirectiveAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        TokenAssert.AreEqual(expected.PragmaKeyword, actual.PragmaKeyword, ignoreExtent);
        TokenAssert.AreEqual(expected.PragmaName, actual.PragmaName, ignoreExtent);
        AstAssert.AreEqual(expected.PragmaParameter, actual.PragmaParameter, ignoreExtent);
    }

    private static void AreEqual(AssociationDeclarationAst? expected, AssociationDeclarationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.AssociationName, actual.AssociationName, ignoreExtent);
        TokenAssert.AreEqual(expected.SuperAssociation, actual.SuperAssociation, ignoreExtent);
        Assert.That(actual.ClassFeatures.Count, Is.EqualTo(expected.ClassFeatures.Count));
        for (var i = 0; i < expected.ClassFeatures.Count; i++)
        {
            AstAssert.AreEqual(expected.ClassFeatures[i], actual.ClassFeatures[i], ignoreExtent);
        }
    }

    private static void AreEqual(ClassDeclarationAst? expected, ClassDeclarationAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        TokenAssert.AreEqual(expected.ClassName, actual.ClassName, ignoreExtent);
        TokenAssert.AreEqual(expected.SuperClass, actual.SuperClass, ignoreExtent);
        Assert.That(actual.ClassFeatures.Count, Is.EqualTo(expected.ClassFeatures.Count));
        for (var i = 0; i < expected.ClassFeatures.Count; i++)
        {
            AstAssert.AreEqual(expected.ClassFeatures[i], actual.ClassFeatures[i], ignoreExtent);
        }
    }

    private static void AreEqual(QualifierListAst? expected, QualifierListAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.QualifierValues.Count, Is.EqualTo(expected.QualifierValues.Count));
        for (var i = 0; i < expected.QualifierValues.Count; i++)
        {
            AstAssert.AreEqual(expected.QualifierValues[i], actual.QualifierValues[i], ignoreExtent);
        }
    }

    private static void AreEqual(QualifierValueAst? expected, QualifierValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        TokenAssert.AreEqual(expected.QualifierName, actual.QualifierName, ignoreExtent);
        AstAssert.AreEqual(expected.Initializer, actual.Initializer, ignoreExtent);
        Assert.That(actual.Flavors.Count, Is.EqualTo(expected.Flavors.Count));
        for (var i = 0; i < expected.Flavors.Count; i++)
        {
            TokenAssert.AreEqual(expected.Flavors[i], actual.Flavors[i], ignoreExtent);
        }
    }

    private static void AreEqual(IQualifierInitializerAst? expected, IQualifierInitializerAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual, Is.InstanceOf(expected.GetType()));
        switch (expected)
        {
            case QualifierValueInitializerAst ast:
                AstAssert.AreEqual(ast, (QualifierValueInitializerAst)actual, ignoreExtent);
                return;
            case QualifierValueArrayInitializerAst ast:
                AstAssert.AreEqual(ast, (QualifierValueArrayInitializerAst)actual, ignoreExtent);
                return;
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    private static void AreEqual(QualifierValueInitializerAst? expected, QualifierValueInitializerAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        AstAssert.AreEqual(expected.Value, actual.Value, ignoreExtent);
    }

    private static void AreEqual(QualifierValueArrayInitializerAst? expected, QualifierValueArrayInitializerAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.Values, Is.EquivalentTo(expected.Values));
    }

    private static void AreEqual(IClassFeatureAst? expected, IClassFeatureAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual, Is.InstanceOf(expected.GetType()));
        switch (expected)
        {
            case StructureDeclarationAst ast:
                AstAssert.AreEqual(ast, (StructureDeclarationAst)actual, ignoreExtent);
                return;
            case EnumerationDeclarationAst ast:
                AstAssert.AreEqual(ast, (EnumerationDeclarationAst)actual, ignoreExtent);
                return;
            case PropertyDeclarationAst ast:
                AstAssert.AreEqual(ast, (PropertyDeclarationAst)actual, ignoreExtent);
                return;
            case MethodDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            //AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual, ignoreExtent);
            //return;
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    private static void AreEqual(StructureDeclarationAst? expected, StructureDeclarationAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.StructureName, actual.StructureName, ignoreExtent);
        TokenAssert.AreEqual(expected.SuperStructure, actual.SuperStructure, ignoreExtent);
        Assert.That(actual.StructureFeatures, Is.EquivalentTo(expected.StructureFeatures));
    }

    private static void AreEqual(EnumerationDeclarationAst? expected, EnumerationDeclarationAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.EnumName, actual.EnumName, ignoreExtent);
        TokenAssert.AreEqual(expected.EnumType, actual.EnumType, ignoreExtent);
        Assert.That(actual.EnumElements.Count, Is.EqualTo(expected.EnumElements.Count));
        for (var i = 0; i < expected.EnumElements.Count; i++)
        {
            AstAssert.AreEqual(expected.EnumElements[i], actual.EnumElements[i], ignoreExtent);
        }
    }

    private static void AreEqual(EnumElementAst? expected, EnumElementAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.EnumElementName, actual.EnumElementName, ignoreExtent);
        AstAssert.AreEqual(expected.EnumElementValue, actual.EnumElementValue, ignoreExtent);
    }

    private static void AreEqual(IEnumElementValueAst? expected, IEnumElementValueAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual, Is.InstanceOf(expected.GetType()));
        switch (expected) {
            case IntegerValueAst ast:
                AstAssert.AreEqual(ast, (IntegerValueAst)actual, ignoreExtent);
                return;
            case StringValueAst ast:
                AstAssert.AreEqual(ast, (StringValueAst)actual, ignoreExtent);
                return;
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    private static void AreEqual(PropertyDeclarationAst? expected, PropertyDeclarationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.ReturnType, actual.ReturnType, ignoreExtent);
        TokenAssert.AreEqual(expected.ReturnTypeRef, actual.ReturnTypeRef, ignoreExtent);
        TokenAssert.AreEqual(expected.PropertyName, actual.PropertyName, ignoreExtent);
        Assert.That(actual.ReturnTypeIsArray, Is.EqualTo(expected.ReturnTypeIsArray));
        AstAssert.AreEqual(expected.Initializer, actual.Initializer, ignoreExtent);
    }

    private static void AreEqual(InstanceValueDeclarationAst? expected, InstanceValueDeclarationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        TokenAssert.AreEqual(expected.Instance, actual.Instance, ignoreExtent);
        TokenAssert.AreEqual(expected.Of, actual.Of, ignoreExtent);
        TokenAssert.AreEqual(expected.TypeName, actual.TypeName, ignoreExtent);
        TokenAssert.AreEqual(expected.As, actual.As, ignoreExtent);
        TokenAssert.AreEqual(expected.Alias, actual.Alias, ignoreExtent);
        AstAssert.AreEqual(expected.PropertyValues, actual.PropertyValues, ignoreExtent);
        TokenAssert.AreEqual(expected.StatementEnd, actual.StatementEnd, ignoreExtent);
    }

    private static void AreEqual(PropertyValueListAst? expected, PropertyValueListAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.PropertyValues.Count, Is.EqualTo(expected.PropertyValues.Count));
        var keys = expected.PropertyValues.Keys;
        foreach (var key in keys)
        {
            Assert.That(actual.PropertyValues, Contains.Key(key));
            AstAssert.AreEqual(expected.PropertyValues[key], actual.PropertyValues[key], ignoreExtent);
        }
    }

    private static void AreEqual(PropertyValueAst? expected, PropertyValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual, Is.InstanceOf(expected.GetType()));
        switch (expected)
        {
            case ComplexTypeValueAst ast:
                AstAssert.AreEqual(ast, (ComplexTypeValueAst)actual, ignoreExtent);
                return;
            case BooleanValueAst ast:
                AstAssert.AreEqual(ast, (BooleanValueAst)actual, ignoreExtent);
                return;
            case IntegerValueAst ast:
                AstAssert.AreEqual(ast, (IntegerValueAst)actual, ignoreExtent);
                return;
            case StringValueAst ast:
                AstAssert.AreEqual(ast, (StringValueAst)actual, ignoreExtent);
                return;
            case NullValueAst ast:
                AstAssert.AreEqual(ast, (NullValueAst)actual, ignoreExtent);
                return;
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    private static void AreEqual(ComplexTypeValueAst? expected, ComplexTypeValueAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual, Is.InstanceOf(expected.GetType()));
        switch (expected) {
            case ComplexValueAst ast:
                AstAssert.AreEqual(ast, (ComplexValueAst)actual, ignoreExtent);
                return;
            case ComplexValueArrayAst ast:
                AstAssert.AreEqual(ast, (ComplexValueArrayAst)actual, ignoreExtent);
                return;
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    private static void AreEqual(ComplexValueAst? expected, ComplexValueAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        TokenAssert.AreEqual(expected.Alias, actual.Alias, ignoreExtent);
        TokenAssert.AreEqual(expected.Value, actual.Value, ignoreExtent);
        TokenAssert.AreEqual(expected.Of, actual.Of, ignoreExtent);
        TokenAssert.AreEqual(expected.TypeName, actual.TypeName, ignoreExtent);
        AstAssert.AreEqual(expected.PropertyValues, actual.PropertyValues, ignoreExtent);
    }

    private static void AreEqual(ComplexValueArrayAst? expected, ComplexValueArrayAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null)) {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.Values.Count, Is.EqualTo(expected.Values.Count));
        for (var i = 0; i < expected.Values.Count; i++)
        {
            AstAssert.AreEqual(expected.Values[i], actual.Values[i], ignoreExtent);
        }
    }

    private static void AreEqual(BooleanValueAst? expected, BooleanValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.Value, Is.EqualTo(expected.Value));
    }

    private static void AreEqual(IntegerValueAst? expected, IntegerValueAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.Kind, Is.EqualTo(expected.Kind));
        Assert.That(actual.Value, Is.EqualTo(expected.Value));
    }

    private static void AreEqual(StringValueAst? expected, StringValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        Assert.That(actual.StringLiteralValues.Count, Is.EqualTo(expected.StringLiteralValues.Count));
        for (var i = 0; i < expected.StringLiteralValues.Count; i++)
        {
            TokenAssert.AreEqual(expected.StringLiteralValues[i], actual.StringLiteralValues[i], ignoreExtent);
        }
    }

    private static void AreEqual(NullValueAst? expected, NullValueAst? actual, bool ignoreExtent) {
        if ((expected == null) || (actual == null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        TokenAssert.AreEqual(expected.Token, actual.Token, ignoreExtent);
    }

    #endregion

}

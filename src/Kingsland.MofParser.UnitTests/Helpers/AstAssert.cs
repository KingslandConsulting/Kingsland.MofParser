using Kingsland.MofParser.Ast;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class AstAssert
{

    #region Node Comparison Methods

    public static void AreEqual(MofSpecificationAst? expected, MofSpecificationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.AreEqual(expected.Productions.Count, actual.Productions.Count);
        for (var i = 0; i < expected.Productions.Count; i++)
        {
            AstAssert.AreEqual(expected.Productions[i], actual.Productions[i], ignoreExtent);
        }
    }

    public static void AreEqual(MofProductionAst? expected, MofProductionAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.IsInstanceOf(expected.GetType(), actual);
        switch (expected)
        {
            case CompilerDirectiveAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            case StructureDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            case ClassDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
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

    public static void AreEqual(AssociationDeclarationAst? expected, AssociationDeclarationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.AssociationName, actual.AssociationName, ignoreExtent);
        TokenAssert.AreEqual(expected.SuperAssociation, actual.SuperAssociation, ignoreExtent);
        Assert.AreEqual(expected.ClassFeatures.Count, actual.ClassFeatures.Count);
        for (var i = 0; i < expected.ClassFeatures.Count; i++)
        {
            AstAssert.AreEqual(expected.ClassFeatures[i], actual.ClassFeatures[i], ignoreExtent);
        }
    }

    public static void AreEqual(QualifierListAst? expected, QualifierListAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.AreEqual(expected.QualifierValues.Count, actual.QualifierValues.Count);
        for (var i = 0; i < expected.QualifierValues.Count; i++)
        {
            AstAssert.AreEqual(expected.QualifierValues[i], actual.QualifierValues[i], ignoreExtent);
        }
    }

    public static void AreEqual(QualifierValueAst? expected, QualifierValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
    }

    public static void AreEqual(IClassFeatureAst? expected, IClassFeatureAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.IsInstanceOf(expected.GetType(), actual);
        switch (expected)
        {
            case StructureDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                //AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual, ignoreExtent);
                //return;
            case EnumerationDeclarationAst:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            //AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual, ignoreExtent);
            //return;
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

    public static void AreEqual(PropertyDeclarationAst? expected, PropertyDeclarationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        AstAssert.AreEqual(expected.QualifierList, actual.QualifierList, ignoreExtent);
        TokenAssert.AreEqual(expected.ReturnType, actual.ReturnType, ignoreExtent);
        TokenAssert.AreEqual(expected.ReturnTypeRef, actual.ReturnTypeRef, ignoreExtent);
        TokenAssert.AreEqual(expected.PropertyName, actual.PropertyName, ignoreExtent);
        Assert.AreEqual(expected.ReturnTypeIsArray, actual.ReturnTypeIsArray);
        Assert.AreEqual(expected.Initializer, actual.Initializer);
    }

    public static void AreEqual(InstanceValueDeclarationAst? expected, InstanceValueDeclarationAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
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

    public static void AreEqual(PropertyValueListAst? expected, PropertyValueListAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.AreEqual(expected.PropertyValues.Count, actual.PropertyValues.Count);
        var keys = expected.PropertyValues.Keys;
        foreach (var key in keys)
        {
            Assert.Contains(key, actual.PropertyValues.Keys);
            AstAssert.AreEqual(expected.PropertyValues[key], actual.PropertyValues[key], ignoreExtent);
        }
    }

    public static void AreEqual(PropertyValueAst? expected, PropertyValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.IsInstanceOf(expected.GetType(), actual);
        switch (expected)
        {
            case BooleanValueAst:
                AstAssert.AreEqual((BooleanValueAst)expected, (BooleanValueAst)actual, ignoreExtent);
                return;
            case StringValueAst:
                AstAssert.AreEqual((StringValueAst)expected, (StringValueAst)actual, ignoreExtent);
                return;
            default:
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
        }
    }

    public static void AreEqual(BooleanValueAst? expected, BooleanValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.AreEqual(expected.Value, actual.Value);
    }

    public static void AreEqual(StringValueAst? expected, StringValueAst? actual, bool ignoreExtent)
    {
        if ((expected == null) && (actual == null))
        {
            return;
        }
        if ((expected == null) || (actual == null))
        {
            return;
        }
        Assert.Multiple(() =>
        {
            Assert.AreEqual(expected.StringLiteralValues.Count, actual.StringLiteralValues.Count, "expected and actual are different lengths");
            for (var i = 0; i < Math.Min(expected.StringLiteralValues.Count, actual.StringLiteralValues.Count); i++)
            {
                TokenAssert.AreEqual(expected.StringLiteralValues[i], actual.StringLiteralValues[i], ignoreExtent);
            }
        });
    }

    #endregion

}

using Kingsland.MofParser.Ast;
using Kingsland.MofParser.UnitTests.Tokens;
using NUnit.Framework;
using System;
using System.Linq;

namespace Kingsland.MofParser.UnitTests.Ast
{

    internal static class AstAssert
    {

        #region Node Comparison Methods

        public static void AreEqual(MofSpecificationAst? expected, MofSpecificationAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.AreEqual(expected.Productions.Count, actual.Productions.Count);
                for (var i = 0; i < expected.Productions.Count; i++)
                {
                    AstAssert.AreEqual(expected.Productions[i], actual.Productions[i]);
                }
            }
        }

        public static void AreEqual(MofProductionAst? expected, MofProductionAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.IsInstanceOf(expected.GetType(), actual);
                switch (expected)
                {
                    case CompilerDirectiveAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                    case StructureDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                    case ClassDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                    case AssociationDeclarationAst _:
                        AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual);
                        return;
                    case EnumerationDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                    case InstanceValueDeclarationAst _:
                        AstAssert.AreEqual((InstanceValueDeclarationAst)expected, (InstanceValueDeclarationAst)actual);
                        return;
                    case StructureValueDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                    case QualifierTypeDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                    default:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                }
            }
        }

        public static void AreEqual(AssociationDeclarationAst? expected, AssociationDeclarationAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                AstAssert.AreEqual(expected.QualifierList, actual.QualifierList);
                TokenAssert.AreEqual(expected.AssociationName, actual.AssociationName, ignoreExtent);
                TokenAssert.AreEqual(expected.SuperAssociation, actual.SuperAssociation, ignoreExtent);
                Assert.AreEqual(expected.ClassFeatures.Count, actual.ClassFeatures.Count);
                for (var i = 0; i < expected.ClassFeatures.Count; i++)
                {
                    AstAssert.AreEqual(expected.ClassFeatures[i], actual.ClassFeatures[i]);
                }
            }
        }

        public static void AreEqual(QualifierListAst? expected, QualifierListAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.AreEqual(expected.QualifierValues.Count, actual.QualifierValues.Count);
                for (var i = 0; i < expected.QualifierValues.Count; i++)
                {
                    AstAssert.AreEqual(expected.QualifierValues[i], actual.QualifierValues[i]);
                }
            }
        }

        public static void AreEqual(QualifierValueAst? expected, QualifierValueAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
            }
        }

        public static void AreEqual(IClassFeatureAst? expected, IClassFeatureAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.IsInstanceOf(expected.GetType(), actual);
                switch (expected)
                {
                    case StructureDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                        //AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual);
                        //return;
                    case EnumerationDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                        //AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual);
                        //return;
                    case PropertyDeclarationAst _:
                        AstAssert.AreEqual((PropertyDeclarationAst)expected, (PropertyDeclarationAst)actual);
                        return;
                    case MethodDeclarationAst _:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                        //AstAssert.AreEqual((AssociationDeclarationAst)expected, (AssociationDeclarationAst)actual);
                        //return;
                    default:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                }
            }
        }

        public static void AreEqual(PropertyDeclarationAst? expected, PropertyDeclarationAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                AstAssert.AreEqual(expected.QualifierList, actual.QualifierList);
                TokenAssert.AreEqual(expected.ReturnType, actual.ReturnType, ignoreExtent);
                TokenAssert.AreEqual(expected.ReturnTypeRef, actual.ReturnTypeRef, ignoreExtent);
                TokenAssert.AreEqual(expected.PropertyName, actual.PropertyName, ignoreExtent);
                Assert.AreEqual(expected.ReturnTypeIsArray, actual.ReturnTypeIsArray);
                Assert.AreEqual(expected.Initializer, actual.Initializer);
            }
        }

        public static void AreEqual(InstanceValueDeclarationAst? expected, InstanceValueDeclarationAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                TokenAssert.AreEqual(expected.Instance, actual.Instance);
                TokenAssert.AreEqual(expected.Of, actual.Of);
                TokenAssert.AreEqual(expected.TypeName, actual.TypeName);
                TokenAssert.AreEqual(expected.As, actual.As, ignoreExtent);
                TokenAssert.AreEqual(expected.Alias, actual.Alias, ignoreExtent);
                AstAssert.AreEqual(expected.PropertyValues, actual.PropertyValues);
                TokenAssert.AreEqual(expected.StatementEnd, actual.StatementEnd, ignoreExtent);
            }
        }

        public static void AreEqual(PropertyValueListAst? expected, PropertyValueListAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.AreEqual(expected.PropertyValues.Count, actual.PropertyValues.Count);
                var keys = expected.PropertyValues.Keys;
                foreach (var key in keys)
                {
                    Assert.Contains(key, actual.PropertyValues.Keys);
                    AstAssert.AreEqual(expected.PropertyValues[key], actual.PropertyValues[key]);
                }
            }
        }

        public static void AreEqual(PropertyValueAst? expected, PropertyValueAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.IsInstanceOf(expected.GetType(), actual);
                switch (expected)
                {
                    case BooleanValueAst _:
                        AstAssert.AreEqual((BooleanValueAst)expected, (BooleanValueAst)actual);
                        return;
                    default:
                        throw new NotImplementedException($"unhandled node type {expected.GetType().Name}");
                }
            }
        }

        public static void AreEqual(BooleanValueAst? expected, BooleanValueAst? actual, bool ignoreExtent = false)
        {
            if ((expected == null) && (actual == null))
            {
                return;
            }
            else if ((expected == null) || (actual == null))
            {
                return;
            }
            else
            {
                Assert.AreEqual(expected.Value, actual.Value);
            }
        }

        #endregion

    }

}

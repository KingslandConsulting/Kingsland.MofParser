using Kingsland.MofParser.Models.Types;
using Kingsland.MofParser.Models.Values;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class ModelAssert
{

    public static void AreDeepEqual(object? actual, object? expected)
    {
        // handle null values
        if ((actual is null) || (expected is null))
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        // make sure types match
        var expectedType = expected.GetType();
        Assert.That(actual, Is.TypeOf(expectedType));
        // handle value types
        if (expectedType.IsValueType)
        {
            Assert.That(actual, Is.EqualTo(expected));
            return;
        }
        // handle strings first, otherwise they get detected as IEnumerable
        if (expected is string expectedString)
        {
            Assert.That((string)actual, Is.EqualTo(expectedString));
            return;
        }
        // handle collections
        if (expected is System.Collections.IEnumerable expectedEnumerable)
        {
            var actualItems = ((System.Collections.IEnumerable)actual).Cast<object>().ToList();
            var expectedItems = expectedEnumerable.Cast<object>().ToList();
            Assert.That(actualItems.Count, Is.EqualTo(expectedItems.Count));
            for (var i = 0; i < expectedItems.Count; i++)
            {
                ModelAssert.AreDeepEqual(actualItems[i], expectedItems[i]);
            }
            return;
        }
        // compare properties
        var properties = expectedType.GetProperties();
        foreach (var property in properties)
        {
            var actualValue = property.GetValue(actual);
            var expectedValue = property.GetValue(expected);
            ModelAssert.AreDeepEqual(actualValue, expectedValue);
        }
    }

    //public static void AreEqual(Module actual, Module expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual.Instances.Count, Is.EqualTo(expected.Instances.Count));
    //    for (var i = 0; i < expected.Instances.Count; i++)
    //    {
    //        ModelAssert.AreEqual(actual.Instances[i], expected.Instances[i]);
    //    }
    //}

    //public static void AreEqual(Instance actual, Instance expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.TypeName)).EqualTo(expected.TypeName)
    //    );
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Alias)).EqualTo(expected.Alias)
    //    );
    //    Assert.That(actual.Properties.Count, Is.EqualTo(expected.Properties.Count));
    //    for (var i = 0; i < expected.Properties.Count; i++)
    //    {
    //        ModelAssert.AreEqual(actual.Properties[i], expected.Properties[i]);
    //    }
    //}

    //public static void AreEqual(Property actual, Property expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Name)).EqualTo(expected.Name)
    //    );
    //    ModelAssert.AreEqual(actual.Value, expected.Value);
    //}

    //private static void AreEqual(PropertyValue actual, PropertyValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    switch (expected)
    //    {
    //        case PrimitiveTypeValue exp:
    //            ModelAssert.AreEqual((PrimitiveTypeValue)actual, exp);
    //            break;
    //        case ComplexTypeValue exp:
    //            ModelAssert.AreEqual((ComplexTypeValue)actual, exp);
    //            break;
    //        case EnumTypeValue exp:
    //            ModelAssert.AreEqual((EnumTypeValue)actual, exp);
    //            break;
    //        default:
    //            throw new NotImplementedException();
    //    };
    //}

    //private static void AreEqual(PrimitiveTypeValue actual, PrimitiveTypeValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    switch (expected)
    //    {
    //        case LiteralValue exp:
    //            ModelAssert.AreEqual((LiteralValue)actual, exp);
    //            break;
    //        case LiteralValueArray exp:
    //            ModelAssert.AreEqual((LiteralValueArray)actual, exp);
    //            break;
    //        default:
    //            throw new NotImplementedException();
    //    }
    //}

    //private static void AreEqual(LiteralValue actual, LiteralValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    switch (expected)
    //    {
    //        case IntegerValue exp:
    //            ModelAssert.AreEqual((IntegerValue)actual, exp);
    //            break;
    //        case RealValue exp:
    //            ModelAssert.AreEqual((RealValue)actual, exp);
    //            break;
    //        case BooleanValue exp:
    //            ModelAssert.AreEqual((BooleanValue)actual, exp);
    //            break;
    //        case NullValue exp:
    //            ModelAssert.AreEqual((NullValue)actual, exp);
    //            break;
    //        case StringValue exp:
    //            ModelAssert.AreEqual((StringValue)actual, exp);
    //            break;
    //        default:
    //            throw new NotImplementedException();
    //    }
    //}

    //private static void AreEqual(IntegerValue actual, IntegerValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Value)).EqualTo(expected.Value)
    //    );
    //}

    //private static void AreEqual(RealValue actual, RealValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Value)).EqualTo(expected.Value)
    //    );
    //}

    //private static void AreEqual(BooleanValue actual, BooleanValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Value)).EqualTo(expected.Value)
    //    );
    //}

    //private static void AreEqual(NullValue actual, NullValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //}

    //private static void AreEqual(StringValue actual, StringValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Value)).EqualTo(expected.Value)
    //    );
    //}

    //private static void AreEqual(LiteralValueArray actual, LiteralValueArray expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual.Values.Count, Is.EqualTo(expected.Values.Count));
    //    for (var i = 0; i < expected.Values.Count; i++)
    //    {
    //        ModelAssert.AreEqual(actual.Values[i], expected.Values[i]);
    //    }
    //}

    //private static void AreEqual(ComplexTypeValue actual, ComplexTypeValue expected)
    //{
    //    throw new NotImplementedException();
    //}

    //private static void AreEqual(EnumTypeValue actual, EnumTypeValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual, Is.TypeOf(expected.GetType()));
    //    switch (expected)
    //    {
    //        case EnumValue exp:
    //            ModelAssert.AreEqual((EnumValue)actual, exp);
    //            break;
    //        case EnumValueArray exp:
    //            ModelAssert.AreEqual((EnumValueArray)actual, exp);
    //            break;
    //        default:
    //            throw new NotImplementedException();
    //    };
    //}

    //public static void AreEqual(EnumValue actual, EnumValue expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Type)).EqualTo(expected.Type)
    //    );
    //    Assert.That(actual,
    //        Has.Property(nameof(actual.Name)).EqualTo(expected.Name)
    //    );
    //}

    //public static void AreEqual(EnumValueArray actual, EnumValueArray expected)
    //{
    //    Assert.That(expected, Is.Not.Null);
    //    Assert.That(actual, Is.Not.Null);
    //    Assert.That(actual.Values.Count, Is.EqualTo(expected.Values.Count));
    //    for (var i = 0; i < expected.Values.Count; i++)
    //    {
    //        ModelAssert.AreEqual(actual.Values[i], expected.Values[i]);
    //    }
    //}

}

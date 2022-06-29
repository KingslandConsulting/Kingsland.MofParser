using Kingsland.MofParser.Model;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Model;

internal static class ModelAssert
{

    public static void AreEqual(Module expected, Module actual)
    {
        Assert.IsNotNull(expected);
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected.Instances.Count, actual.Instances.Count);
        foreach (var pair in expected.Instances
            .Zip(actual.Instances, (exp, act) => (exp, act)))
        {
            ModelAssert.AreEqual(pair.exp, pair.act);
        }
    }

    public static void AreEqual(Instance expected, Instance actual)
    {
        Assert.IsNotNull(expected);
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected.TypeName, actual.TypeName);
        Assert.AreEqual(expected.Alias, actual.Alias);
        Assert.AreEqual(expected.Properties.Count, actual.Properties.Count);
        foreach (var pair in expected.Properties
            .Zip(actual.Properties, (exp, act) => (exp, act)))
        {
            ModelAssert.AreEqual(pair.exp, pair.act);
        }
    }

    public static void AreEqual(Property expected, Property actual)
    {
        Assert.IsNotNull(expected);
        Assert.IsNotNull(actual);
        Assert.AreEqual(expected.Name, actual.Name);
        Assert.AreEqual(expected.Value, actual.Value);
    }

}

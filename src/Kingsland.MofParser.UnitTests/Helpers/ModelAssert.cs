using Kingsland.MofParser.Models;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal static class ModelAssert
{

    public static void AreEqual(Module expected, Module actual)
    {
        Assert.That(expected, Is.Not.Null);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Instances.Count, Is.EqualTo(expected.Instances.Count));
        for (var i = 0; i < expected.Instances.Count; i++)
        {
            ModelAssert.AreEqual(expected.Instances[i], actual.Instances[i]);
        }
    }

    public static void AreEqual(Instance expected, Instance actual)
    {
        Assert.That(expected, Is.Not.Null);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.TypeName, Is.EqualTo(expected.TypeName));
        Assert.That(actual.Alias, Is.EqualTo(expected.Alias));
        Assert.That(actual.Properties.Count, Is.EqualTo(expected.Properties.Count));
        for (var i = 0; i < expected.Properties.Count; i++)
        {
            ModelAssert.AreEqual(expected.Properties[i], actual.Properties[i]);
        }
    }

    public static void AreEqual(Property expected, Property actual)
    {
        Assert.That(expected, Is.Not.Null);
        Assert.That(actual, Is.Not.Null);
        Assert.That(actual.Name, Is.EqualTo(expected.Name));
        Assert.That(actual.Value, Is.EqualTo(expected.Value));
    }

}

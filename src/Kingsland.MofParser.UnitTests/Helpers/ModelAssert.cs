using Kingsland.MofParser.Model;
using NUnit.Framework;
using System.Linq;

namespace Kingsland.MofParser.UnitTests.Helpers
{

    internal static class ModelAssert
    {

        public static void AreEqual(Module obj1, Module obj2)
        {
            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);
            Assert.AreEqual(obj1.Instances.Count, obj2.Instances.Count);
            foreach(var pair in  obj1.Instances
                .Zip(obj2.Instances, (i1, i2) => (i1, i2)))
            {
                ModelAssert.AreEqual(pair.i1, pair.i2);
            }
        }

        public static void AreEqual(Instance obj1, Instance obj2)
        {
            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);
            Assert.AreEqual(obj1.TypeName, obj2.TypeName);
            Assert.AreEqual(obj1.Alias, obj2.Alias);
            Assert.AreEqual(obj1.Properties.Count, obj2.Properties.Count);
            foreach (var pair in obj1.Properties
                .Zip(obj2.Properties, (p1, p2) => (p1, p2)))
            {
                ModelAssert.AreEqual(pair.p1, pair.p2);
            }
        }

        public static void AreEqual(Property obj1, Property obj2)
        {
            Assert.IsNotNull(obj1);
            Assert.IsNotNull(obj2);
            Assert.AreEqual(obj1.Name, obj2.Name);
            Assert.AreEqual(obj1.Value, obj2.Value);
        }

    }

}

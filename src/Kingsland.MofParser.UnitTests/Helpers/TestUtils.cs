using Newtonsoft.Json;
using NUnit.Framework;
using System.Reflection;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal sealed class TestUtils
{

    internal static string ConvertToJson<T>(T value)
    {
        var formatting = Formatting.Indented;
        var settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };
        var json = JsonConvert.SerializeObject(value, formatting, settings);
        return json;
    }

    public static IEnumerable<TestCaseData> GetMofTestCase(string path)
    {
        var localPath = Assembly.GetExecutingAssembly().Location;
        var testCasePath = Path.Combine(
            Path.GetDirectoryName(localPath)
                ?? throw new NullReferenceException(),
            path
        );
        var testCaseFiles = Directory.GetFiles(testCasePath, "*.mof", SearchOption.AllDirectories);
        foreach (var testCaseFile in testCaseFiles)
        {
            var testName = testCaseFile[(testCasePath.Length + 1)..].Replace("\\", ".");
            yield return new TestCaseData(testCaseFile).SetName(testName);
        }
    }

}

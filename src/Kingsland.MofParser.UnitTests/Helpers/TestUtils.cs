using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Xml;

namespace Kingsland.MofParser.UnitTests.Helpers
{

    internal sealed class TestUtils
    {

        internal static string ConvertToJson<T>(T value)
        {
            var formatting = Newtonsoft.Json.Formatting.Indented;
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            };
            var json = JsonConvert.SerializeObject(value, formatting, settings);
            return json;
        }

        internal static string ConvertToXml<T>(T value)
        {
            // serialize the result so we can compare it
            var settings = new XmlWriterSettings
            {
                IndentChars = "  ",
                Indent = true
            };
            using (var sw = new StringWriter())
            {
                using (XmlWriter xw = XmlWriter.Create(sw, settings))
                {
                    var serializer = new DataContractSerializer(typeof(T));
                    serializer.WriteObject(xw, typeof(T));
                    xw.Flush();
                    sw.Flush();
                    return sw.ToString();
                }
            }
        }

        public static IEnumerable<TestCaseData> GetMofTestCase(string path)
        {
            var localPath = new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath;
            var testCasePath = Path.Combine(
                Path.GetDirectoryName(localPath),
                path
            );
            var testCaseFiles = Directory.GetFiles(testCasePath, "*.mof", SearchOption.AllDirectories);
            foreach (var testCaseFile in testCaseFiles)
            {
                var testName = testCaseFile.Substring(testCasePath.Length + 1).Replace("\\", ".");
                yield return new TestCaseData(testCaseFile).SetName(testName);
            }
        }

    }

}

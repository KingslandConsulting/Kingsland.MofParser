using Newtonsoft.Json;
using NUnit.Framework;
using System;
using System.Collections;
using System.IO;
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
            var settings = new XmlWriterSettings();
            settings.IndentChars = "  ";
            settings.Indent = true;
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

        public static IEnumerable GetMofTestCase(string path)
        {
            var mofFilenames = Directory.GetFiles(path, "*.mof", SearchOption.AllDirectories);
            foreach (var mofFilename in mofFilenames)
            {
                var testName = mofFilename.Substring(path.Length + 1)
                                            .Replace("\\", ".");
                //var filename =  Path.GetFileName(mofFilename);
                //Console.WriteLine(string.Format("'{0}'", mofFilename));
                yield return new TestCaseData(mofFilename).SetName(testName);
            }
        }

    }

}

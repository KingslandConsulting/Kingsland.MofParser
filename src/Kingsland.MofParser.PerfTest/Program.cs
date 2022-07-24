using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.PerfTest
{

    class Program
    {

        static void Main()
        {

            Console.WriteLine("press a key to start tests...");
            var key = Console.ReadKey();

            var sourceText = File.ReadAllText(".\\WinXpProSp3CIMV2.mof");

            // parse the mof file
            for (var i = 0; i < 5; i++)
            {
                Console.WriteLine($"i = {i}");
                var module = Parser.ParseText(
                    sourceText,
                    ParserQuirks.AllowMofV2Qualifiers |
                    ParserQuirks.AllowEmptyQualifierValueArrays
                );
            }

        }

    }

}

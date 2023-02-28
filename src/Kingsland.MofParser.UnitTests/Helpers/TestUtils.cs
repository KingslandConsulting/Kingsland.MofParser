using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Syntax;
using Newtonsoft.Json;
using NUnit.Framework;
using System.Reflection;

namespace Kingsland.MofParser.UnitTests.Helpers;

internal sealed class TestUtils
{

    internal static IEnumerable<SyntaxToken> RemoveExtents(IEnumerable<SyntaxToken> tokens)
    {
        if (tokens == null)
        {
            throw new ArgumentNullException(nameof(tokens));
        }
        foreach (var token in tokens)
        {
            yield return token switch
            {
                AliasIdentifierToken t => new AliasIdentifierToken(t.Name),
                AttributeCloseToken t => new AttributeCloseToken(),
                AttributeOpenToken t => new AttributeOpenToken(),
                BlockCloseToken t => new BlockCloseToken(),
                BlockOpenToken t => new BlockOpenToken(),
                BooleanLiteralToken t => new BooleanLiteralToken(t.Value),
                CommaToken t => new CommaToken(),
                ColonToken t => new ColonToken(),
                CommentToken t => new CommentToken(t.Value),
                DotOperatorToken t => new DotOperatorToken(),
                EqualsOperatorToken t => new EqualsOperatorToken(),
                IdentifierToken t => new IdentifierToken(t.Name),
                IntegerLiteralToken t => new IntegerLiteralToken(t.Kind, t.Value),
                NullLiteralToken t => new NullLiteralToken(),
                ParenthesisCloseToken t => new ParenthesisCloseToken(),
                ParenthesisOpenToken t => new ParenthesisOpenToken(),
                PragmaToken t => new PragmaToken(),
                RealLiteralToken t => new RealLiteralToken(t.Value),
                StatementEndToken t => new StatementEndToken(),
                StringLiteralToken t => new StringLiteralToken(t.Value),
                WhitespaceToken t => new WhitespaceToken(t.Value),
                _ => throw new NotImplementedException()
            };
        }
    }

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

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
        ArgumentNullException.ThrowIfNull(tokens);
        foreach (var token in tokens)
        {
            yield return token switch
            {
                AliasIdentifierToken t => new AliasIdentifierToken(t.Name),
                AttributeCloseToken => new AttributeCloseToken(),
                AttributeOpenToken => new AttributeOpenToken(),
                BlockCloseToken => new BlockCloseToken(),
                BlockOpenToken => new BlockOpenToken(),
                BooleanLiteralToken t => new BooleanLiteralToken(t.Value),
                CommaToken => new CommaToken(),
                ColonToken => new ColonToken(),
                CommentToken t => new CommentToken(t.Value),
                DotOperatorToken => new DotOperatorToken(),
                EqualsOperatorToken => new EqualsOperatorToken(),
                IdentifierToken t => new IdentifierToken(t.Name),
                IntegerLiteralToken t => new IntegerLiteralToken(t.Kind, t.Value),
                NullLiteralToken => new NullLiteralToken(),
                ParenthesisCloseToken => new ParenthesisCloseToken(),
                ParenthesisOpenToken => new ParenthesisOpenToken(),
                PragmaToken => new PragmaToken(),
                RealLiteralToken t => new RealLiteralToken(t.Value),
                StatementEndToken => new StatementEndToken(),
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

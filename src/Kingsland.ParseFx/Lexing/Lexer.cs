using Kingsland.ParseFx.Lexing.Matches;
using Kingsland.ParseFx.Syntax;
using Kingsland.ParseFx.Text;
using System.Collections.ObjectModel;
using static Kingsland.ParseFx.Lexing.Scanner;

namespace Kingsland.ParseFx.Lexing;

public sealed class Lexer
{

    #region Constructors

    public Lexer()
        : this(new List<Scanner>())
    {
    }

    public Lexer(IEnumerable<Scanner> scanners)
    {
        this.Scanners = (scanners == null) ?
            throw new ArgumentNullException(nameof(scanners)) :
            new ReadOnlyCollection<Scanner>(scanners.ToList()
        );
        this.ScannerCache = new Dictionary<char, Scanner>();
    }

    #endregion

    #region Properties

    public ReadOnlyCollection<Scanner> Scanners
    {
        get;
    }

    private Dictionary<char, Scanner> ScannerCache
    {
        get;
        set;
    }

    #endregion

    #region Lexing Methods

    public List<SyntaxToken> Lex(string sourceText)
    {
        var reader = SourceReader.From(sourceText);
        return this.ReadToEnd(reader).ToList();
    }

    public IEnumerable<SyntaxToken> ReadToEnd(SourceReader reader)
    {
        var thisReader = reader;
        while (!thisReader.Eof())
        {
            var result = this.ReadToken(thisReader);
            yield return result.Token;
            thisReader = result.NextReader;
        }
    }

    public ScannerResult ReadToken(SourceReader reader)
    {
        var peek = reader.Peek();
        // make sure the rule for the next character is in the rule cache
        if (!this.ScannerCache.ContainsKey(peek.Value))
        {
            this.ScannerCache.Add(
                peek.Value,
                this.Scanners.FirstOrDefault(r => r.Match.Matches(peek.Value))
                    ?? throw new UnexpectedCharacterException(peek)
            );
        }
        // apply the scanner for the next character
        return this.ScannerCache[peek.Value].Action.Invoke(reader);
    }

    #endregion

    #region Scanner Methods

    public Lexer AddScanner(char value, Func<SourceExtent, SyntaxToken> factoryMethod)
    {
        return this.AddScanner(
            value,
            (reader) => {
                (var sourceChar, var nextReader) = reader.Read(value);
                var extent = SourceExtent.From(sourceChar);
                return new ScannerResult(
                    factoryMethod(extent), nextReader
                );
            }
        );
    }

    public Lexer AddScanner(char[] values, ScannerAction action)
    {
        return this.AddScanner(new CharArrayMatch(values), action);
    }

    public Lexer AddScanner(char value, ScannerAction action)
    {
        return this.AddScanner(new CharMatch(value), action);
    }

    public Lexer AddScanner(char fromValue, char toValue, ScannerAction action)
    {
        return this.AddScanner(new RangeMatch(fromValue, toValue), action);
    }

    public Lexer AddScanner(string pattern, ScannerAction action)
    {
        return this.AddScanner(new RegexMatch(pattern), action);
    }

    public Lexer AddScanner(IMatch match, ScannerAction action)
    {
        var newScanners = this.Scanners.ToList();
        newScanners.Add(new Scanner(match, action));
        return new Lexer(newScanners);
    }

    #endregion

}

using Kingsland.ParseFx.Lexing;

namespace Kingsland.ParseFx.Text;

public sealed class SourceReader
{

    #region Constructors

    private SourceReader(SourceStream stream, int position)
    {
        this.Stream = stream ?? throw new ArgumentNullException(nameof(stream));
        this.Position = position;
    }

    #endregion

    #region Properties

    public SourceStream Stream
    {
        get;
    }

    public int Position
    {
        get;
    }

    #endregion

    #region Eof Methods

    public bool Eof()
    {
        return this.Stream.Eof(this.Position);
    }

    #endregion

    #region Peek Methods

    /// <summary>
    /// Reads the current character off of the input stream, but does not advance the current position.
    /// </summary>
    /// <returns></returns>
    public SourceChar Peek()
    {
        if (this.Eof())
        {
            throw new UnexpectedEndOfStreamException();
        }
        return this.Stream.Read(this.Position);
    }

    /// <summary>
    /// Returns true if the current character on the input stream matches the specified value.
    /// </summary>
    /// <returns></returns>
    public bool Peek(char value)
    {
        var peek = this.Peek();
        return (peek.Value == value);
    }

    /// <summary>
    /// Returns true if the current character on the input stream matches the specified predicate.
    /// </summary>
    /// <returns></returns>
    public bool Peek(Func<char, bool> predicate)
    {
        var peek = this.Peek();
        return predicate(peek.Value);
    }

    #endregion

    #region Read Methods

    private SourceReader? _next;

    public SourceReader Next()
    {
        this._next ??= this.Eof()
            ? throw new UnexpectedEndOfStreamException()
            : new(
                stream: this.Stream,
                position: this.Position + 1
            );
        return this._next;
    }

    /// <summary>
    /// Reads the current character off of the input stream and advances the current position.
    /// </summary>
    /// <returns></returns>
    public (SourceChar SourceChar, SourceReader NextReader) Read()
    {
        return (this.Peek(), this.Next());
    }

    /// <summary>
    /// Reads the current character off of the input stream and advances the current position.
    /// Throws an exception if the character does not match the specified value.
    /// </summary>
    /// <returns></returns>
    public (SourceChar SourceChar, SourceReader NextReader) Read(char value)
    {
        var peek = this.Peek();
        if (peek.Value != value)
        {
            throw new UnexpectedCharacterException(peek, value);
        }
        return (peek, this.Next());
    }

    /// <summary>
    /// Reads the current character off of the input stream and advances the current position.
    /// Throws an exception if the character does not match the specified predicate.
    /// </summary>
    /// <returns></returns>
    public (SourceChar SourceChar, SourceReader NextReader) Read(Func<char, bool> predicate)
    {
        var peek = this.Peek();
        if (!predicate(peek.Value))
        {
            throw new UnexpectedCharacterException(peek);
        }
        return (peek, this.Next());
    }

    /// <summary>
    /// Reads the current character off of the input stream and advances the current position.
    /// Throws an exception if the character does not match the specified predicate.
    /// </summary>
    /// <returns></returns>
    public (List<SourceChar> SourceChars, SourceReader NextReader) ReadWhile(Func<char, bool> predicate)
    {
        var thisReader = this;
        var sourceChars = new List<SourceChar>();
        while (!thisReader.Eof())
        {
            if (predicate(thisReader.Peek().Value))
            {
                (var sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
            }
            else
            {
                break;
            }
        }
        return (sourceChars, thisReader);
    }

    /// <summary>
    /// Reads a string off of the input stream and advances the current position beyond the end of the string.
    /// Throws an exception if the string does not match the specified value.
    /// </summary>
    /// <returns></returns>
    public (List<SourceChar> SourceChars, SourceReader NextReader) ReadString(string value, bool ignoreCase = false)
    {
        var thisReader = this;
        var sourceChars = new List<SourceChar>();
        foreach (var expectedChar in value)
        {
            var sourceChar = thisReader.Peek();
            if (sourceChar.Value == expectedChar)
            {
                // case sensitive match
                (sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
            }
            else if (ignoreCase &&
                string.Equals(new string(sourceChar.Value, 1), new string(expectedChar, 1), StringComparison.InvariantCultureIgnoreCase))
            {
                // case insensitive match
                (sourceChar, thisReader) = thisReader.Read();
                sourceChars.Add(sourceChar);
            }
            else
            {
                // not a match, so force an exception
                (sourceChar, thisReader) = thisReader.Read(expectedChar);
            }
        }
        return (sourceChars, thisReader);
    }

    #endregion

    #region Factory Methods

    public static SourceReader From(TextReader value)
    {
        return (value == null) ?
            throw new ArgumentNullException(nameof(value)) :
            new SourceReader(
                stream: SourceStream.From(value),
                position: 0
            );
    }

    public static SourceReader From(string value)
    {
        return (value == null) ?
            throw new ArgumentNullException(nameof(value)) :
            new SourceReader(
                stream: SourceStream.From(value),
                position: 0
            );
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return $"Position={this.Position}," +
               $"Eof={this.Eof()}," +
               $"Peek={(this.Eof() ? null : this.Peek())}";
    }

    #endregion

}

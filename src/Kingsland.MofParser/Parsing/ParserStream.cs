using System;
using System.Collections.Generic;
using Kingsland.MofParser.Tokens;
using System.Text;

namespace Kingsland.MofParser.Parsing
{

    internal sealed class ParserStream
    {

        #region Field

        private List<Token> source;
        private int position;

        #endregion

        #region Constructors

        public ParserStream(List<Token> source)
        {
            this.source = source ?? throw new ArgumentNullException(nameof(source));
            this.position = 0;
        }

        #endregion

        #region Properties

        private List<Token> Source
        {
            get
            {
                return this.source;
            }
        }

        public int Position
        {
            get
            {
                return this.position;
            }
        }

        public bool Eof
        {
            get
            {
                return (this.source.Count == 0) ||
                       (this.position >= this.source.Count);
            }
        }

        #endregion

        #region Peek Methods

        public Token Peek()
        {
            if ((this.source.Count == 0) ||
               (this.position >= this.source.Count))
            {
                throw new UnexpectedEndOfStreamException();
            }
            return this.source[this.position];
        }

        public T Peek<T>() where T : Token
        {
            if ((this.source.Count == 0) ||
               (this.position >= this.source.Count))
            {
                throw new UnexpectedEndOfStreamException();
            }
            return (this.Source[this.Position] as T);
        }

        #endregion

        #region TryPeek Methods

        public bool TryPeek<T>() where T : Token
        {

            if ((this.source.Count == 0) ||
               (this.position >= this.source.Count))
            {
                return false;
            }
            return (this.Source[this.Position] is T);
        }

        public bool TryPeek<T>(out T result) where T : Token
        {
            if ((this.source.Count == 0) ||
                (this.position >= this.source.Count))
            {
                throw new UnexpectedEndOfStreamException();
            }
            var peek = this.source[this.position] as T;
            if (peek == null)
            {
                result = null;
                return false;
            }
            result = peek;
            return true;
        }

        public bool TryPeek<T>(Func<T, bool> predicate, out T result) where T : Token
        {

            if ((this.source.Count == 0) ||
                (this.position >= this.source.Count))
            {
                throw new UnexpectedEndOfStreamException();
            }
            var peek = this.source[this.position] as T;
            if ((peek != null) && predicate(peek))
            {
                result = peek;
                return true;
            }
            result = null;
            return false;
        }

        #endregion

        #region PeekIdentifierToken Methods

        public bool TryPeekIdentifierToken(string name, out IdentifierToken result)
        {
            return this.TryPeek<IdentifierToken>(
                t => t.GetNormalizedName() == name,
                out result
            );
        }

        #endregion

        #region Read Methods

        public Token Read()
        {
            var value = this.Peek();
            this.position += 1;
            return value;
        }

        public T Read<T>() where T : Token
        {
            var token = this.Peek();
            if (token is T cast)
            {
                this.Read();
                return cast;
            }
            throw new UnexpectedTokenException(token);
        }

        #endregion

        #region TryRead Methods

        public bool TryRead<T>(out T result) where T : Token
        {
            if (this.TryPeek<T>(out result))
            {
                this.Read();
                return true;
            }
            return false;
        }

        #endregion

        #region ReadIdentifierToken Methods

        public IdentifierToken ReadIdentifierToken(string name)
        {
            var token = this.Read<IdentifierToken>();
            if (token.GetNormalizedName() != name)
            {
                throw new UnexpectedTokenException(token);
            }
            return token;
        }

        public IdentifierToken ReadIdentifierToken(Func<IdentifierToken, bool> predicate)
        {
            var token = this.Read<IdentifierToken>();
            return predicate(token) ? token : throw new UnexpectedTokenException(token);
        }

        public bool TryReadIdentifierToken(string name, out IdentifierToken result)
        {
            if (this.TryPeekIdentifierToken(name, out result))
            {
                this.Read();
                return true;
            }
            return false;
        }

        #endregion

        #region Backtrack Methods

        /// <summary>
        /// Moves the stream position back a token.
        /// </summary>
        public void Backtrack()
        {
            this.Backtrack(1);
        }

        /// <summary>
        /// Moves the stream position back the specified number of tokens.
        /// </summary>
        public void Backtrack(int count)
        {
            if (this.Position < count)
            {
                throw new InvalidOperationException();
            }
            this.position -= count;
        }

        #endregion

        #region Object Interface

        public override string ToString()
        {
            var result = new StringBuilder();
            var count = 0;
            for (var i = Math.Max(0, this.Position - 5); i < Math.Min(this.Source.Count, this.Position + 5); i++)
            {
                if (count > 0)
                {
                    result.Append(" ");
                    count += 1;
                }
                if (i == this.Position)
                {
                    result.Append(">>>");
                    count += 3;
                }
                result.Append(this.Source[i]);
            }
            return string.Format("Current = '{0}'", result.ToString());
        }

        #endregion

    }

}

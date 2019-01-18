using System;
using System.Collections.Generic;
using Kingsland.MofParser.Tokens;
using System.Text;

namespace Kingsland.MofParser.Parsing
{

    internal class ParserStream
    {

        #region Constructors

        public ParserStream(List<Token> source)
        {
            this.Source = source;
            this.Position = 0;
        }

        #endregion

        #region Properties

        private List<Token> Source
        {
            get;
            set;
        }

        public int Position
        {
            get;
            private set;
        }

        public bool Eof
        {
            get
            {
                return (this.Source == null) ||
                       (this.Source.Count == 0) ||
                       this.Position >= this.Source.Count;
            }
        }

        #endregion

        #region Peek Methods

        public Token Peek()
        {
            if (this.Eof)
            {
                throw new UnexpectedEndOfStreamException();
            }
            return this.Source[this.Position];
        }

        public Token Peek(Func<Token, bool> predicate)
        {
            var token = this.Peek();
            if ((token == null) || !predicate(token))
            {
                return null;
            }
            return token;
        }

        public T Peek<T>() where T : Token
        {
            var token = this.Peek();
            var cast = (token as T);
            return cast;
        }

        public T Peek<T>(Func<T, bool> predicate) where T : Token
        {
            var token = this.Peek<T>();
            if ((token == null) || !predicate(token))
            {
                return null;
            }
            return token;
        }

        public IdentifierToken PeekIdentifierToken(string name)
        {
            var token = this.Peek<IdentifierToken>();
            if ((token == null) || (token.GetNormalizedName() != name))
            {
                return null;
            }
            return token;
        }

        #endregion

        #region Read Methods

        public Token Read()
        {
            var value = this.Peek();
            this.Position += 1;
            return value;
        }

        public T Read<T>() where T : Token
        {
            var token = this.Read();
            var cast = (token as T);
            if (cast == null)
            {
                throw new UnexpectedTokenException(token);
            }
            return cast;
        }

        public IdentifierToken ReadIdentifierToken(string name)
        {
            var token = this.Read<IdentifierToken>();
            if (token.GetNormalizedName() != name)
            {
                throw new UnexpectedTokenException(token);
            }
            return token;
        }

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
            this.Position -= count;
        }

        #endregion

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
    }

}

using System;
using System.Collections.Generic;
using Kingsland.MofParser.Tokens;

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
                throw new InvalidOperationException("Unexpected end of file encountered.");
            }
            return this.Source[this.Position];
        }

        public T Peek<T>() where T : Token
        {
            var peek = this.Peek();
            return (peek as T);
        }

        public IdentifierToken PeekKeyword()
        {
            return this.Peek<IdentifierToken>();
        }

        public bool PeekKeyword(string name)
        {
            var token = this.Peek<IdentifierToken>();
            return (token != null) && (token.Name == name);
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
                throw new InvalidOperationException(
                    string.Format("Unexpected token type '{0}' encountered", token.GetType().Name));
            }
            return cast;
        }

        public IdentifierToken ReadKeyword()
        {
            var token = this.Read<IdentifierToken>();
            return token;
        }

        public IdentifierToken ReadKeyword(string name)
        {
            var token = this.Read<IdentifierToken>();
            if (token.Name != name)
            {
                throw new InvalidOperationException(
                    string.Format("Unexpected keyword '{0}' encountered", token.Name));
            }
            return token;
        }

        /// <summary>
        /// Moves the stream position back a token.
        /// </summary>
        public void Backtrack()
        {
            if (this.Position == 0)
            {
                throw new InvalidOperationException();
            }
            this.Position -= 1;
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
            string s = "";
            for (var i = Math.Max(0, Position - 5); i < Math.Min(Source.Count, Position + 5); i++)
            {
                if (s.Length > 0)
                    s += " ";

                if (i == Position)
                    s += ">>>";

                s += Source[i];
            }

            return string.Format("Current = '{0}'", s);
        }
    }

}

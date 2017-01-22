﻿using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Parsing
{

    internal sealed class ParserState : ICloneable
    {

        #region Constructors

        public ParserState(IEnumerable<Token> source)
        {
            this.Source = new ReadOnlyCollection<Token>(source.ToList());
            this.Position = 0;
        }

        #endregion

        #region Properties

        private ReadOnlyCollection<Token> Source
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

        public T Peek<T>() where T : Token
        {
            var peek = this.Peek();
            return (peek as T);
        }

        public IdentifierToken PeekIdentifier()
        {
            return this.Peek<IdentifierToken>();
        }

        public bool PeekIdentifier(string name)
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

        public bool TryRead<T>(ref T result) where T : Token
        {
            if (this.Peek<T>() == null)
            {
                return false;
            }
            result = this.Read<T>();
            return true;
        }

        public T Read<T>() where T : Token
        {
            var token = this.Read();
            var cast = token as T;
            if (cast == null)
            {
                throw new UnexpectedTokenException(token);
            }
            return cast;
        }

        public IdentifierToken ReadIdentifier()
        {
            var token = this.Read<IdentifierToken>();
            return token;
        }

        public bool TryReadIdentifier(string name, ref IdentifierToken result)
        {
            if (this.Peek<IdentifierToken>() == null)
            {
                return false;
            }
            var token = this.Read<IdentifierToken>();
            if (token.GetNormalizedName() != name)
            {
                return false;
            }
            result = token;
            return true;
        }

        public IdentifierToken ReadIdentifier(string name)
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

        #region ICloneable Interface

        public object Clone()
        {
            return this.MemberwiseClone();
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
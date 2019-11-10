using Kingsland.Lexing.Text;
using System;
using System.Collections.Generic;

namespace Kingsland.Lexing
{

    public abstract class Lexer
    {

        #region Constructors

        protected Lexer(SourceReader reader)
        {
            this.Reader = reader ??
                throw new ArgumentNullException(nameof(reader));
        }

        #endregion

        #region Properties

        public SourceReader Reader
        {
            get;
            private set;
        }

        public bool Eof
        {
            get
            {
                return this.Reader.Eof();
            }
        }

        #endregion

        #region Lexing Methods

        public IEnumerable<Token> ReadAllTokens()
        {
            var thisLexer = this;
            var nextToken = default(Token);
            while (!thisLexer.Eof)
            {
                (nextToken, thisLexer) = thisLexer.ReadToken();
                yield return nextToken;
            }
        }

        public abstract (Token Token, Lexer NextLexer) ReadToken();

        #endregion

    }

}
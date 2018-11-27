using Kingsland.MofParser.Source;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingsland.MofParser.Lexing
{

    public sealed class Lexer
    {

        #region Constructors

        public Lexer(SourceReader reader)
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

        public static List<Token> Lex(SourceReader reader)
        {
            var lexer = new Lexer(reader);
            var allTokens = lexer.AllTokens().ToList();
            return allTokens;
        }

        public IEnumerable<Token> AllTokens()
        {
            var thisLexer = this;
            var nextToken = default(Token);
            while (!thisLexer.Eof)
            {
                (nextToken, thisLexer) = LexerEngine.ReadToken(thisLexer);
                yield return nextToken;
            }
        }

        #endregion

    }

}
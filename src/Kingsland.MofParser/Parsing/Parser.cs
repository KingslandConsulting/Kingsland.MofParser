using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingsland.MofParser.Parsing
{

    public class Parser
    {

        #region Constructors

        public Parser(IEnumerable<Token> source)
        {
            // remove all comments and whitespace from the source
            var filtered = source.Where(lt => !(lt is CommentToken) &&
                                              !(lt is WhitespaceToken));
            // initialise state stack
            var initialState = new ParserState(filtered);
            this.States = new Stack<ParserState>();
            this.States.Push(initialState);
        }

        #endregion

        #region State Members

        private Stack<ParserState> States
        {
            get;
            set;
        }

        internal ParserState CurrentState
        {
            get
            {
                return this.States.Peek();
            }
        }

        internal ParserState Descend()
        {
            var newState = (ParserState)this.CurrentState.Clone();
            this.States.Push(newState);
            return newState;
        }

        internal void Commit()
        {
            if (this.States.Count <= 1)
            {
                throw new InvalidOperationException("Cannot commit initial state.");
            }
            var currentState = this.CurrentState;
            this.States.Pop();
            this.States.Pop();
            this.States.Push(currentState);
        }

        internal void Backtrack()
        {
            if (this.States.Count <= 1)
            {
                throw new InvalidOperationException("Cannot roll back past initial state.");
            }
            this.States.Pop();
        }

        #endregion

        #region CurrentState Members

        internal bool Eof
        {
            get
            {
                return this.CurrentState.Eof;
            }
        }

        internal Token Peek()
        {
            return this.CurrentState.Peek();
        }

        public T Peek<T>() where T : Token
        {
            return this.CurrentState.Peek<T>();
        }

        internal IdentifierToken PeekIdentifier()
        {
            return this.CurrentState.PeekIdentifier();
        }

        internal bool PeekIdentifier(string name)
        {
            return this.CurrentState.PeekIdentifier(name);
        }

        internal Token Read()
        {
            return this.Read();
        }

        internal bool TryRead<T>(ref T result) where T : Token
        {
            return this.CurrentState.TryRead<T>(ref result);
        }

        public T Read<T>() where T : Token
        {
            return this.CurrentState.Read<T>();
        }

        internal IdentifierToken ReadIdentifier()
        {
            return this.CurrentState.ReadIdentifier();
        }

        internal IdentifierToken ReadIdentifier(string name)
        {
            return this.CurrentState.ReadIdentifier(name);
        }

        #endregion

        #region Methods

        public AstNode Parse()
        {
            var program = MofSpecificationAst.Parse(this);
            return program;
        }

        #endregion

    }

}
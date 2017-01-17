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

        private ParserState Descend()
        {
            var newState = (ParserState)this.CurrentState.Clone();
            this.States.Push(newState);
            return newState;
        }

        private void Commit()
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

        private void Backtrack()
        {
            if (this.States.Count <= 1)
            {
                throw new InvalidOperationException("Cannot roll back past initial state.");
            }
            this.States.Pop();
        }

        #endregion

        #region Methods

        public AstNode Parse()
        {
            var program = MofSpecificationAst.Parse(this.CurrentState);
            return program;
        }

        #endregion

    }

}
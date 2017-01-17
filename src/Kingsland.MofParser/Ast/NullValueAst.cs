using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class NullValueAst : LiteralValueAst
    {

        #region Constructors

        private NullValueAst()
        {
        }

        #endregion

        #region Properties

        public NullLiteralToken Token
        {
            get;
            private set;
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        ///
        /// Section A.17.7 - Null value
        ///
        ///     nullValue = NULL
        ///     NULL = "null" ; keyword: case insensitive
        ///                   ; second
        ///
        /// </remarks>
        internal new static NullValueAst Parse(ParserState state)
        {
            var token = state.Read<NullLiteralToken>();
            return new NullValueAst()
            {
                Token = token
            };
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}

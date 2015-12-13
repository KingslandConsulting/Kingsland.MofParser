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
        /// <param name="stream"></param>
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
        internal new static NullValueAst Parse(ParserStream stream)
        {
            var token = stream.Read<NullLiteralToken>();
            return new NullValueAst()
            {
                Token = token
            };
        }

        #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            return this.Token.Extent.Text;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return this.GetMofSource();
        }

        #endregion

    }

}

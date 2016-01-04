using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class BooleanValueAst : LiteralValueAst
    {

        #region Constructors

        private BooleanValueAst()
        {
        }

        #endregion

        #region Properties

        public BooleanLiteralToken Token
        {
            get;
            private set;
        }

        public bool Value
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// A.17.6 Boolean value
        ///
        ///     booleanValue = TRUE / FALSE
        ///     FALSE        = "false" ; keyword: case insensitive
        ///     TRUE         = "true"  ; keyword: case insensitive
        ///
        /// </remarks>
        internal new static BooleanValueAst Parse(ParserStream stream)
        {
            var token = stream.Read<BooleanLiteralToken>();
            return new BooleanValueAst
            {
                Token = token,
                Value = token.Value
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

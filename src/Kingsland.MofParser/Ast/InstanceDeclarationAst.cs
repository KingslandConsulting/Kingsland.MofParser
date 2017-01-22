using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;

namespace Kingsland.MofParser.Ast
{

    public sealed class InstanceDeclarationAst : MofProductionAst
    {

        #region Constructors

        private InstanceDeclarationAst()
        {
        }

        #endregion

        #region Properties

        public ComplexValueAst Instance
        {
            get;
            internal set;
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        ///
        /// </remarks>
        internal static bool TryParse(Parser parser, ref InstanceDeclarationAst node, bool throwIfError = false)
        {

            // instance
            parser.Descend();
            var instance = default(ComplexValueAst);
            if (ComplexValueAst.TryParse(parser, ref instance, false) && instance.IsInstance)
            {
                parser.Commit();
                // build the result
                var result = new InstanceDeclarationAst
                {
                    Instance = instance
                };
                // return the result
                node = result;
                return true;
            }
            parser.Backtrack();

            return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);

        }

        internal new static BooleanValueAst Parse(Parser parser)
        {
            var node = default(BooleanValueAst);
            BooleanValueAst.TryParse(parser, ref node, true);
            return node;
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

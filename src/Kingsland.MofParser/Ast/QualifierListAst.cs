using System.Collections.Generic;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast
{
    public sealed class QualifierListAst : AstNode
    {

        #region Constructors

        private QualifierListAst()
        {
            this.Qualifiers = new List<QualifierValueAst>();
        }

        #endregion

        #region Properties

        public List<QualifierValueAst> Qualifiers
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
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.9 Qualifier list
        ///
        ///     qualifierList  = "[" qualifierValue *( "," qualifierValue ) "]" 
        ///     
        ///     qualifierValue = qualifierName [ qualifierValueInitializer / 
        ///                      qualiferValueArrayInitializer ] 
        ///                      
        ///     qualifierValueInitializer     = "(" literalValue ")" 
        ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
        ///     
        /// </remarks>
        internal static QualifierListAst Parse(Parser parser)
        {

            var ast = new QualifierListAst();

            // "["
            parser.Read<AttributeOpenToken>();

            // qualifierValue *( "," qualifierValue )
            while (!parser.Eof)
            {
                ast.Qualifiers.Add(QualifierValueAst.Parse(parser));
                if (parser.Peek<CommaToken>() == null)
                {
                    break;
                }
                parser.Read<CommaToken>();
            }

            // "]"
            parser.Read<AttributeCloseToken>();

            return ast;

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

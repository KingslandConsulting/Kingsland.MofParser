using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class QualifierValueAst : MofProductionAst
    {

        #region Constructors

        private QualifierValueAst()
        {
            this.Flavors = new List<string>();
        }

        #endregion

        #region Properties

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public PrimitiveTypeValueAst Initializer
        {
            get;
            private set;
        }

        public List<string> Flavors
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
        ///     qualifierValue = qualifierName [ qualifierValueInitializer / 
        ///                      qualiferValueArrayInitializer ] 
        ///                      
        ///     qualifierName                 = elementName
        ///     qualifierValueInitializer     = "(" literalValue ")" 
        ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
        ///
        /// </remarks>
        internal new static QualifierValueAst Parse(Parser parser)
        {

            var ast = new QualifierValueAst();

            ast.Name = parser.Read<IdentifierToken>();
            if(!StringValidator.IsElementName(ast.Name.Name))
            {
                throw new UnexpectedTokenException(parser.Peek());
            }

            if (parser.Peek<ParenthesesOpenToken>() != null)
            {
                parser.Read<ParenthesesOpenToken>();
                ast.Initializer = LiteralValueAst.Parse(parser);
                parser.Read<ParenthesesCloseToken>();
            }
            else if (parser.Peek<BlockOpenToken>() != null)
            {
                ast.Initializer = LiteralValueArrayAst.Parse(parser);
            }

            // A.8 Qualifier type declaration
            // Because DSP0004 in CIM v3 the qualifier flavor has been replaced with qualifier policy, the MOF v2 
            // qualifier declarations have to be converted to MOF v3 before parsing.
            if (parser.Peek<ColonToken>() != null)
            {
                parser.Read<ColonToken>();
                while (parser.Peek<IdentifierToken>() != null)
                {
                    ast.Flavors.Add(parser.Read<IdentifierToken>().Name);
                }
            }

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

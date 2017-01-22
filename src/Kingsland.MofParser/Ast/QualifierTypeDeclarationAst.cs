using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{

    public sealed class QualifierTypeDeclarationAst : MofProductionAst
    {

        #region Constructors

        private QualifierTypeDeclarationAst()
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
        /// A.8 Qualifier type declaration
        ///
        ///     qualifierTypeDeclaration = [qualifierList]
        ///                                QUALIFIER qualifierName ":" qualifierType
        ///                                qualifierScope[qualifierPolicy] ";"
        ///                               
        ///     qualifierName    = elementName
        ///     qualifierType    = primitiveQualifierType / enumQualiferType 
        ///                        primitiveQualifierType = primitiveType[array] 
        ///                        [ "=" primitiveTypeValue] ";" 
        ///     enumQualiferType = enumName[array] "=" enumTypeValue ";" 
        ///
        ///     qualifierScope   = SCOPE "(" ANY / scopeKindList ")"
        ///
        ///     qualifierPolicy  = POLICY "(" policyKind ")" 
        ///     policyKind       = DISABLEOVERRIDE / 
        ///                        ENABLEOVERRIDE / 
        ///                        RESTRICTED 
        ///                        
        ///     scopeKindList    = scopeKind*( "," scopeKind ) 
        ///     scopeKind        = STRUCTURE / CLASS / ASSOCIATION / 
        ///                        ENUMERATION / ENUMERATIONVALUE / 
        ///                        PROPERTY / REFPROPERTY / 
        ///                        METHOD / PARAMETER / 
        ///                        QUALIFIERTYPE 
        ///                        
        ///     SCOPE            = "scope" ; keyword: case insensitive 
        ///     ANY              = "any" ; keyword: case insensitive 
        ///     POLICY           = "policy" ; keyword: case insensitive 
        ///     ENABLEOVERRIDE   = "enableoverride" ; keyword: case insensitive 
        ///     DISABLEOVERRIDE  = "disableoverride" ; keyword: case insensitive 
        ///     RESTRICTED       = "restricted" ; keyword: case insensitive 
        ///     ENUMERATIONVALUE = "enumerationvalue" ; keyword: case insensitive 
        ///     PROPERTY         = "property" ; keyword: case insensitive 
        ///     REFPROPETY       = "reference" ; keyword: case insensitive 
        ///     METHOD           = "method" ; keyword: case insensitive 
        ///     PARAMETER        = "parameter" ; keyword: case insensitive 
        ///     QUALIFIERTYPE   = "qualifiertype" ; keyword: case insensitive
        ///     
        /// </remarks>
        internal new static QualifierTypeDeclarationAst Parse(Parser parser)
        {

            var state = parser.CurrentState;
            var ast = new QualifierTypeDeclarationAst();

            ast.Name = state.Read<IdentifierToken>();

            if (state.Peek<ParenthesesOpenToken>() != null)
            {
                state.Read<ParenthesesOpenToken>();
                ast.Initializer = LiteralValueAst.Parse(parser);
                state.Read<ParenthesesCloseToken>();
            }
            else if (state.Peek<BlockOpenToken>() != null)
            {
                ast.Initializer = LiteralValueArrayAst.Parse(parser);
            }

            if (state.Peek<ColonToken>() != null)
            {
                state.Read<ColonToken>();
                while (state.Peek<IdentifierToken>() != null)
                {
                    ast.Flavors.Add(state.Read<IdentifierToken>().Name);
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

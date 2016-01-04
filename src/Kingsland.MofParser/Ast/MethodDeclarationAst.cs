using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System.Collections.Generic;

namespace Kingsland.MofParser.Ast
{
    public sealed class MethodDeclarationAst : ClassFeatureAst
    {

        #region Constructors

        internal MethodDeclarationAst()
        {
            this.Parameters = new List<ParameterDeclarationAst>();
        }

        #endregion

        #region Properties

        public IdentifierToken ReturnType
        {
            get;
            internal set;
        }

        public bool ReturnTypeIsArray
        {
            get;
            internal set;
        }

        public List<ParameterDeclarationAst> Parameters
        {
            get;
            private set;
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

using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public sealed class PropertyDeclarationAst : StructureFeatureAst
    {

        #region Constructors

        internal PropertyDeclarationAst()
        {
        }

        #endregion

        #region Properties

        public IdentifierToken Type
        {
            get;
            internal set;
        }

        public bool IsArray
        {
            get;
            internal set;
        }

        public bool IsRef
        {
            get;
            internal set;
        }

        public PrimitiveTypeValueAst Initializer
        {
            get;
            internal set;
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

using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public string ReturnType
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

        #region AstNode Members

        public override string GetMofSource()
        {
            var source = new StringBuilder();
            if ((this.Qualifiers != null) && this.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0} ", this.Qualifiers.GetMofSource());
            }
            source.AppendFormat("{0} {1}", this.ReturnType, this.Name);
            if (this.Parameters.Count == 0)
            {
                source.Append("();");
            }
            else
            {
                source.AppendFormat("({0});", string.Join(", ", this.Parameters.Select(a => a.ToString())));
            }
            return source.ToString();
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

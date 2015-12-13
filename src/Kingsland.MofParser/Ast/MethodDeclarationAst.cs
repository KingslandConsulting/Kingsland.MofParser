using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Ast
{
    public sealed class MethodDeclarationAst : ClassFeatureAst
    {

        public class Argument
        {
            public QualifierListAst Qualifiers { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsRef { get; set; }
            public bool IsArray { get; set; }
            public AstNode DefaultValue { get; set; }

            public override string ToString()
            {
                var source = new StringBuilder();
                if (this.Qualifiers.Qualifiers.Count > 0)
                {
                    source.AppendFormat("{0} ", this.Qualifiers.ToString());
                }
                source.AppendFormat("{0} {1}", this.Type.ToString(), this.Name.ToString());
                if(this.IsArray)
                {
                    source.Append("[]");
                }
                if (this.DefaultValue != null)
                {
                    source.AppendFormat(" = {0}", this.DefaultValue.GetMofSource());
                }
                return source.ToString();
            }

        }

        #region Constructors

        public MethodDeclarationAst()
        {
            this.Arguments = new List<Argument>();
        }

        #endregion

        #region Properties

        public string ReturnType
        {
            get;
            set;
        }

        public bool ReturnTypeIsArray
        {
            get;
            set;
        }

        public List<Argument> Arguments
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
            if (this.Arguments.Count == 0)
            {
                source.Append("();");
            }
            else
            {
                source.AppendFormat("({0});", string.Join(", ", this.Arguments.Select(a => a.ToString())));
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

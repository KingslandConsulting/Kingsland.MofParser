using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.Ast
{
    public sealed class MethodAst : ClassFeatureAst
    {

        public class Argument
        {
            public QualifierListAst Qualifiers { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public bool IsRef { get; set; }
            public AstNode DefaultValue { get; set; }

            public override string ToString()
            {
                var result = new StringBuilder();
                if (this.Qualifiers.Qualifiers.Count > 0)
                {
                    result.AppendFormat("{0} ", this.Qualifiers.ToString());
                }
                result.AppendFormat("{0} {1}", this.Type.ToString(), this.Name.ToString());
                if (this.DefaultValue != null)
                {
                    result.AppendFormat(" = {0}", this.DefaultValue.ToString());
                }
                return result.ToString();
            }

        }

        public MethodAst()
        {
            this.Arguments = new List<Argument>();
        }

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

        public override string ToString()
        {
            var result = new StringBuilder();
            if (this.Qualifiers.Qualifiers.Count > 0)
            {
                result.AppendFormat("{0} ", this.Qualifiers.ToString());
            }
            result.AppendFormat("{0} {1}", this.ReturnType.ToString(), this.Name.ToString());
            if (this.Arguments.Count == 0)
            {
                result.Append("();");
            }
            else
            {
                result.AppendFormat("({0});", string.Join(", ", this.Arguments.Select(a => a.ToString())));
            }
            return result.ToString();
        }

    }

}

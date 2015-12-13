using Kingsland.MofParser.Parsing;
using System.Text;

namespace Kingsland.MofParser.Ast
{

    public sealed class PropertyDeclarationAst : ClassFeatureAst
    {

        #region Constructors

        internal PropertyDeclarationAst()
        {
        }

        #endregion

        #region Properties

        public string Type
        {
            get;
            set;
        }

        public bool IsArray
        {
            get;
            set;
        }

        public bool IsRef
        {
            get;
            set;
        }

        public PrimitiveTypeValueAst Initializer
        {
            get;
            set;
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
            source.AppendFormat("{0} ", this.Type);
            if(this.IsRef)
            {
                source.AppendFormat("{0} ", Keywords.REF);
            }
            source.Append(this.Name);
            if(this.IsArray)
            {
                source.Append("[]");
            }
            if (this.Initializer != null)
            {
                source.AppendFormat(" = {0}", this.Initializer.GetMofSource());
            }
            source.Append(";");
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

using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Kingsland.MofParser.CodeGen
{

    public sealed class MofGenerator
    {

        #region Dispatcher

        public static string ConvertToMof(AstNode node, MofQuirks quirks = MofQuirks.None)
        {
            if (node == null)
            {
                return null;
            }
            // A.1 Value definitions
            if (node is LiteralValueArrayAst) { return MofGenerator.ConvertToMof((LiteralValueArrayAst)node, quirks); }
            if (node is PropertyValueAst) { return MofGenerator.ConvertToMof((PropertyValueAst)node, quirks); }
            // A.2 MOF specification
            if (node is MofSpecificationAst) { return MofGenerator.ConvertToMof((MofSpecificationAst)node, quirks); }
            // A.3 Compiler directive
            if (node is CompilerDirectiveAst) { return MofGenerator.ConvertToMof((CompilerDirectiveAst)node, quirks); }
            // A.5 Class declaration
            if (node is ClassDeclarationAst) { return MofGenerator.ConvertToMof((ClassDeclarationAst)node, quirks); }
            // A.8 Qualifier type declaration
            if (node is QualifierValueAst) { return MofGenerator.ConvertToMof((QualifierValueAst)node, quirks); }
            // A.9 Qualifier list
            if (node is QualifierListAst) { return MofGenerator.ConvertToMof((QualifierListAst)node, quirks); }
            // A.10 Property declaration
            if (node is PropertyDeclarationAst) { return MofGenerator.ConvertToMof((PropertyDeclarationAst)node, quirks); }
            // A.11 Method declaration
            if (node is MethodDeclarationAst) { return MofGenerator.ConvertToMof((MethodDeclarationAst)node, quirks); }
            // A.12 Parameter declaration
            if (node is ParameterDeclarationAst) { return MofGenerator.ConvertToMof((ParameterDeclarationAst)node, quirks); }
            // A.14 Complex type value
            if (node is ComplexValueArrayAst) { return MofGenerator.ConvertToMof((ComplexValueArrayAst)node, quirks); }
            if (node is ComplexValueAst) { return MofGenerator.ConvertToMof((ComplexValueAst)node, quirks); }
            // A.17.1 Integer value
            if (node is IntegerValueAst) { return MofGenerator.ConvertToMof((IntegerValueAst)node, quirks); }
            // A.17.2 Real value
            if (node is RealValueAst) { return MofGenerator.ConvertToMof((RealValueAst)node, quirks); }
            // A.17.3 String values
            if (node is StringValueAst) { return MofGenerator.ConvertToMof((StringValueAst)node, quirks); }
            // A.17.6 Boolean value
            if (node is BooleanValueAst) { return MofGenerator.ConvertToMof((BooleanValueAst)node, quirks); }
            // A.17.7 Null value
            if (node is NullValueAst) { return MofGenerator.ConvertToMof((NullValueAst)node, quirks); }
            // A.19 Reference type value
            if (node is ReferenceTypeValueAst) { return MofGenerator.ConvertToMof((ReferenceTypeValueAst)node, quirks); }
            // unknown
            throw new InvalidOperationException(node.GetType().FullName);
        }

        #endregion

        #region A.1 Value definitions

        public static string ConvertToMof(LiteralValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            var values = node.Values.Select(v => MofGenerator.ConvertToMof(v, quirks)).ToArray();
            return string.Format("{{{0}}}", string.Join(", ", values));
        }

        public static string ConvertToMof(PropertyValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.ToString();
        }

        #endregion

        #region A.2 MOF specification

        public static string ConvertToMof(MofSpecificationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            for (var i = 0; i < node.Productions.Count; i++)
            {
                if (i > 0)
                {
                    source.AppendLine();
                }
                source.Append(MofGenerator.ConvertToMof(node.Productions[i], quirks));
            }
            return source.ToString();
        }

        #endregion

        #region A.3 Compiler directive

        public static string ConvertToMof(CompilerDirectiveAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("!!!!!{0}!!!!!", node.GetType().Name);
        }

        #endregion

        #region A.5 Class declaration

        public static string ConvertToMof(ClassDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendLine(MofGenerator.ConvertToMof(node.Qualifiers, quirks));
            }
            if (node.Superclass == null)
            {
                source.AppendFormat("class {0}\r\n", node.ClassName.Name);
            }
            else
            {
                source.AppendFormat("class {0} : {1}\r\n", node.ClassName.Name, node.Superclass.Name);
            }
            source.AppendLine("{");
            foreach (var feature in node.Features)
            {
                source.AppendFormat("\t{0}\r\n", MofGenerator.ConvertToMof(feature, quirks));
            }
            source.AppendLine("};");
            return source.ToString();
        }

        #endregion

        #region A.8 Qualifier type declaration

        public static string ConvertToMof(QualifierValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (!string.IsNullOrEmpty(node.Name.Name))
            {
                source.Append(node.Name.Name);
            }
            if (node.Initializer != null)
            {
                if (node.Initializer is LiteralValueAst)
                {
                    source.AppendFormat("({0})", MofGenerator.ConvertToMof(node.Initializer, quirks));
                }
                else if (node.Initializer is LiteralValueArrayAst)
                {
                    source.Append(MofGenerator.ConvertToMof(node.Initializer, quirks));
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }
            if (node.Flavors.Count > 0)
            {
                source.AppendFormat(": {0}", string.Join(" ", node.Flavors.ToArray()));
            }
            return source.ToString();
        }

        #endregion

        #region A.9 Qualifier list

        public static string ConvertToMof(QualifierListAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            var lastQualifierName = default(string);
            source.Append("[");
            for (var i = 0; i < node.Qualifiers.Count; i++)
            {
                var thisQualifier = node.Qualifiers[i];
                var thisQualifierName = node.Qualifiers[i].Name.GetNormalizedName();
                if (i > 0)
                {
                    source.Append(",");
                    var quirkEnabled = (quirks & MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations) == MofQuirks.OmitSpaceBetweenInOutQualifiersForParameterDeclarations;
                    if (!quirkEnabled || (lastQualifierName != "in") || (thisQualifierName != "out"))
                    {
                        source.Append(" ");
                    }
                }
                source.Append(MofGenerator.ConvertToMof(thisQualifier, quirks));
                lastQualifierName = thisQualifierName;
            }
            source.Append("]");
            return source.ToString();
        }

        #endregion

        #region A.10 Property declaration

        public static string ConvertToMof(PropertyDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0} ", MofGenerator.ConvertToMof(node.Qualifiers, quirks));
            }
            source.AppendFormat("{0} ", node.Type.Name);
            if (node.IsRef)
            {
                source.AppendFormat("{0} ", Keywords.REF);
            }
            source.Append(node.Name.Name);
            if (node.IsArray)
            {
                source.Append("[]");
            }
            if (node.Initializer != null)
            {
                source.AppendFormat(" = {0}", MofGenerator.ConvertToMof(node.Initializer, quirks));
            }
            source.Append(";");
            return source.ToString();
        }

        #endregion

        #region A.11 Method declaration

        public static string ConvertToMof(MethodDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0}", MofGenerator.ConvertToMof(node.Qualifiers, quirks));
            }
            var quirkEnabled = (quirks & MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations) == MofQuirks.PrefixSpaceBeforeQualifierlessMethodDeclarations;
            if (quirkEnabled || ((node.Qualifiers != null) && node.Qualifiers.Qualifiers.Count > 0))
            {
                source.AppendFormat(" ");
            }
            source.AppendFormat("{0} {1}", node.ReturnType.Name, node.Name.Name);
            if (node.Parameters.Count == 0)
            {
                source.Append("();");
            }
            else
            {
                var values = node.Parameters.Select(p => MofGenerator.ConvertToMof(p, quirks)).ToArray();
                source.AppendFormat("({0});", string.Join(", ", values));
            }
            return source.ToString();
        }

        #endregion

        #region A.12 Parameter declaration

        public static string ConvertToMof(ParameterDeclarationAst node, MofQuirks quirks = MofQuirks.None)
        {
            var source = new StringBuilder();
            if (node.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0} ", MofGenerator.ConvertToMof(node.Qualifiers, quirks));
            }
            source.AppendFormat("{0} ", node.Type.Name);
            if (node.IsRef)
            {
                source.AppendFormat("{0} ", Keywords.REF);
            }
            source.Append(node.Name.Name);
            if (node.IsArray)
            {
                source.Append("[]");
            }
            if (node.DefaultValue != null)
            {
                source.AppendFormat(" = {0}", MofGenerator.ConvertToMof(node.DefaultValue, quirks));
            }
            return source.ToString();
        }

        #endregion

        #region A.14 Complex type value

        public static string ConvertToMof(ComplexValueArrayAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("{{{0}}}", string.Join(", ", node.Values.Select(v => v.ToString()).ToArray()));
        }

        public static string ConvertToMof(ComplexValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("!!!!!{0}!!!!!", node.GetType().Name);
        }

        #endregion

        #region A.17.1 Integer value

        public static string ConvertToMof(IntegerValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Value.ToString();
        }

        #endregion

        #region A.17.2 Real value

        public static string ConvertToMof(RealValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Value.ToString();
        }

        #endregion

        #region A.17.3 String values

        public static string ConvertToMof(StringValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("\"{0}\"", MofGenerator.EscapeString(node.Value));
        }

        internal static string EscapeString(string value)
        {
            var escapeMap = new Dictionary<char, char>()
            {
                { '\\' , '\\' }, { '\"' , '\"' },  {'\b', 'b'}, {'\t', 't'},  {'\n', 'n'}, {'\f', 'f'}, {'\r', 'r'}
            };
            if (string.IsNullOrEmpty(value))
            {
                return value;
            }
            var escaped = new StringBuilder();
            foreach (var @char in value.ToCharArray())
            {
                if (escapeMap.ContainsKey(@char))
                {
                    escaped.AppendFormat("\\{0}", escapeMap[@char]);
                }
                else if ((@char >= 32) && (@char <= 126))
                {
                    escaped.Append(@char);
                }
                else
                {
                    throw new InvalidOperationException(new string(new char[] { @char }));
                }
            }
            return escaped.ToString();
        }

        #endregion

        #region A.17.6 Boolean value

        public static string ConvertToMof(BooleanValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Token.Extent.Text;
        }

        #endregion

        #region A.17.7 Null value

        public static string ConvertToMof(NullValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return node.Token.Extent.Text;
        }

        #endregion

        #region A.19 Reference type value

        public static string ConvertToMof(ReferenceValueAst node, MofQuirks quirks = MofQuirks.None)
        {
            return string.Format("${0}", node.Name);
        }

        #endregion

    }

}

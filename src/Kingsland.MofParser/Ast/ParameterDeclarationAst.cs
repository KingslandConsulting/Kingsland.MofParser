using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;
using System.Text;

namespace Kingsland.MofParser.Ast
{

    public sealed class ParameterDeclarationAst : AstNode
    {

        #region Constructors

        private ParameterDeclarationAst()
        {
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        public string Name
        {
            get;
            private set;
        }

        public IdentifierToken Type
        {
            get;
            private set;
        }

        public bool IsRef
        {
            get;
            set;
        }

        public bool IsArray
        {
            get;
            set;
        }

        public AstNode DefaultValue
        {
            get;
            set;
        }

        #endregion

        #region Parsing Methods


        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0a.pdf
        /// A.12 Parameter declaration
        ///
        ///     parameterDeclaration = [ qualifierList ] ( primitiveParamDeclaration /
        ///                            complexParamDeclaration /
        ///                            enumParamDeclaration /
        ///                            referenceParamDeclaration )
        ///
        ///     primitiveParamDeclaration = primitiveType parameterName [ array ]
        ///                                 [ "=" primitiveTypeValue ]
        ///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
        ///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///     enumParamDeclaration      = enumName parameterName [ array ]
        ///                                 [ "=" enumValue ]
        ///     referenceParamDeclaration = classReference parameterName [ array ]
        ///                                 [ "=" referenceTypeValue ]
        ///
        ///     parameterName = IDENTIFIER
        ///
        /// </remarks>
        internal static ParameterDeclarationAst Parse(ParserStream stream)
        {
            var parameter = new ParameterDeclarationAst();
            var qualifiers = default(QualifierListAst);
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                qualifiers = QualifierListAst.Parse(stream);
            }
            parameter.Qualifiers = qualifiers;
            parameter.Type = stream.Read<IdentifierToken>();
            if (stream.PeekIdentifier(Keywords.REF))
            {
                stream.ReadIdentifier(Keywords.REF);
                parameter.IsRef = true;
            }
            else
            {
                parameter.IsRef = false;
            }
            parameter.Name = stream.Read<IdentifierToken>().Name;
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                parameter.IsArray = true;
            }
            if (stream.Peek<EqualsOperatorToken>() != null)
            {
                stream.Read<EqualsOperatorToken>();
                parameter.DefaultValue = ClassFeatureAst.ReadDefaultValue(stream, parameter.Type);
            }
            return parameter;
        }

    #endregion

        #region AstNode Members

        public override string GetMofSource()
        {
            var source = new StringBuilder();
            if (this.Qualifiers.Qualifiers.Count > 0)
            {
                source.AppendFormat("{0} ", this.Qualifiers.ToString());
            }
            source.AppendFormat("{0} {1}", this.Type.Name, this.Name);
            if (this.IsArray)
            {
                source.Append("[]");
            }
            if (this.DefaultValue != null)
            {
                source.AppendFormat(" = {0}", this.DefaultValue.GetMofSource());
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


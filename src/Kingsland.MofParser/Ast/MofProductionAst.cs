using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class MofProductionAst : AstNode
    {

        #region Constructors

        internal MofProductionAst()
        {
        }

        #endregion

        #region Parsing Methods

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        /// See http://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.0.pdf
        /// Section A.2 - MOF specification
        ///
        ///     mofProduction = compilerDirective /
        ///                     structureDeclaration /
        ///                     classDeclaration /
        ///                     associationDeclaration /
        ///                     enumerationDeclaration /
        ///                     instanceDeclaration /
        ///                     qualifierDeclaration
        ///
        /// </remarks>
        internal static MofProductionAst Parse(Parser parser)
        {

            var peek = parser.Peek();

            // compilerDirective
            var pragma = peek as PragmaToken;
            if (pragma != null)
            {
                return CompilerDirectiveAst.Parse(parser);
            }

            // all other mofProduction structures can start with an optional qualifierList
            var qualifiers = default(QualifierListAst);
            if (peek is AttributeOpenToken)
            {
                qualifiers = QualifierListAst.Parse(parser);
            }

            var identifier = parser.Peek<IdentifierToken>();
            switch(identifier.GetNormalizedName())
            {

                case Keywords.STRUCTURE:
                    // structureDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.CLASS:
                    // classDeclaration
                    var @class = ClassDeclarationAst.Parse(parser, qualifiers);
                    return @class;

                case Keywords.ASSOCIATION:
                    // associationDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.ENUMERATION:
                    // enumerationDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.INSTANCE:
                case Keywords.VALUE:
                    // instanceDeclaration
                    var instance = ComplexTypeValueAst.Parse(parser, qualifiers);
                    return instance;

                case Keywords.QUALIFIER:
                    // qualifierDeclaration
                    throw new UnsupportedTokenException(identifier);

                default:
                    throw new UnexpectedTokenException(peek);

            }

            #endregion

        }

    }

}

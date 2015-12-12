using System;
using Kingsland.MofParser.Parsing;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast
{

    public abstract class MofProductionAst : AstNode
    {

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
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
        internal static MofProductionAst Parse(ParserStream stream)
        {

            var peek = stream.Peek();

            // compilerDirective
            var pragma = peek as PragmaToken;
            if (pragma != null)
            {
                return PragmaAst.Parse(stream);
            }

            // all other mofProduction structures can start with an optional qualifierList
            var qualifiers = default(QualifierListAst);
            if (peek is AttributeOpenToken)
            {
                qualifiers = QualifierListAst.Parse(stream);
            }

            var identifier = stream.Peek<IdentifierToken>();
            switch(identifier.GetNormalizedName())
            {

                case Keywords.STRUCTURE:
                    // structureDeclaration
                    throw new UnsupportedTokenException(identifier);

                case Keywords.CLASS:
                    // classDeclaration
                    var @class = ClassAst.Parse(stream, qualifiers);
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
                    var instance = ComplexTypeValueAst.Parse(stream, qualifiers);
                    return instance;

                case Keywords.QUALIFIER:
                    // qualifierDeclaration
                    throw new UnsupportedTokenException(identifier);

                default:
                    throw new UnexpectedTokenException(peek);

            }

        }

    }

}

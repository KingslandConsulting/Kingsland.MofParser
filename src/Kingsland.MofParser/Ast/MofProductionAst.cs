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
        ///     mofSpecification = *mofProduction
        ///     mofProduction    = compilerDirective /
        ///                        structureDeclaration /
        ///                        classDeclaration /
        ///                        associationDeclaration /
        ///                        enumerationDeclaration /
        ///                        instanceDeclaration /
        ///                        qualifierDeclaration
        ///
        /// </remarks>
        internal static MofProductionAst Parse(ParserStream stream)
        {

            var peek = stream.Peek();
            var identifier = peek as IdentifierToken;
            var pragma = peek as PragmaToken;
            var attribute = peek as AttributeOpenToken;

            if ((identifier != null) &&
                (identifier.Name == "instance" || identifier.Name == "value"))
            {
                return ComplexTypeValueAst.Parse(stream, null);
            }
            else if (identifier != null && identifier.Name == "class")
            {
                return ClassAst.Parse(stream);
            }
            else if (pragma != null)
            {
                return PragmaAst.Parse(stream);
            }
            else if (attribute != null)
            {
                var qualifiers = QualifierListAst.Parse(stream);

                peek = stream.Peek();
                identifier = peek as IdentifierToken;

                if (identifier != null &&
                    (identifier.Name == "instance" || identifier.Name == "value"))
                {
                    return ComplexTypeValueAst.Parse(stream, qualifiers);
                }
                else if (identifier != null && identifier.Name == "class")
                {
                    return ClassAst.Parse(stream);
                }
                else
                {
                    throw new InvalidOperationException(
                        string.Format("Invalid lexer token type '{0}'", peek.GetType().Name));
                }
            }
            else
            {
                throw new InvalidOperationException(
                    string.Format("Invalid lexer token '{0}'", peek));
            }
        }
    }
}

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
            var peek = stream.Peek<IdentifierToken>();
            switch (peek.Name)
            {
                case "instance":
                case "value":
                    return ComplexTypeValueAst.Parse(stream);
                default:
                    throw new InvalidOperationException(
                        string.Format("Invalid lexer token type '{0}'", peek.GetType().Name));
            }
        }
        
    }

}

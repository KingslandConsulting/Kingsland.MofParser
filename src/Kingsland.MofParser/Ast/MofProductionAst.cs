namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.2 MOF specification
    ///
    ///     mofProduction = compilerDirective /
    ///                     structureDeclaration /
    ///                     classDeclaration /
    ///                     associationDeclaration /
    ///                     enumerationDeclaration /
    ///                     instanceValueDeclaration /
    ///                     structureValueDeclaration /
    ///                     qualifierTypeDeclaration
    ///
    /// </remarks>
    public abstract class MofProductionAst : AstNode
    {

        #region Constructors

        internal MofProductionAst()
        {
        }

        #endregion

    }

}

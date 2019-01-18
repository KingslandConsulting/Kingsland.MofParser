namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.1 Structure declaration
    ///
    ///     structureFeature = structureDeclaration /   ; local structure
    ///                        enumerationDeclaration / ; local enumeration
    ///                        propertyDeclaration
    ///
    /// </remarks>
    public interface IStructureFeatureAst : IClassFeatureAst
    {

    }

}

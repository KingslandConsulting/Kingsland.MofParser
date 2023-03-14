using Kingsland.ParseFx.Parsing;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.5.2 Class declaration
///
///     classFeature     = structureFeature / methodDeclaration
///
///     structureFeature = structureDeclaration /   ; local structure
///                        enumerationDeclaration / ; local enumeration
///                        propertyDeclaration
///
/// </remarks>
public interface IClassFeatureAst : IAstNode
{
}

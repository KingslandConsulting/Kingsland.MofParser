using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Models.Types;

namespace Kingsland.MofParser.Models.Converter;

internal static partial class ModelConverter
{

    #region 7.2 MOF specification

    public static Module ConvertMofSpecificationAst(MofSpecificationAst node)
    {
        return new Module(
            //enumerations: node.Productions
            //    .OfType<EnumerationDeclarationAst>()
            //    .Select(ModelConverter.ConvertEnumerationDeclarationAst)
            //    .ToList(),
            instances: node.Productions
                .OfType<InstanceValueDeclarationAst>()
                .Select(ModelConverter.ConvertInstanceValueDeclarationAst)
                .ToList()
            );
    }

    #endregion

}

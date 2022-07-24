using Kingsland.MofParser.Ast;
using System.Text;

namespace Kingsland.MofParser.CodeGen;

public static class AstSerializer
{

    public static string Serialize(MofSpecificationAst node)
    {
        return AstSerializer.Serialize(
            node, new MofWriterSettings()
        );
    }

    public static string Serialize(MofSpecificationAst node, MofWriterSettings settings)
    {
        var stringBuilder = new StringBuilder();
        using var stringWriter = new StringWriter(stringBuilder);
        var astWriter = AstWriter.Create(stringWriter, settings);
        astWriter.WriteMofSpecificationAst(node);
        stringWriter.Flush();
        return stringBuilder.ToString();
    }

}

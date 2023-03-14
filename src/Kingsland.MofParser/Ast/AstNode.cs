using Kingsland.MofParser.CodeGen;

namespace Kingsland.MofParser.Ast;

public abstract record AstNode : ParseFx.Parsing.AstNode
{

    #region Constructors

    protected AstNode() : base()
    {
    }

    #endregion

    #region Object Overrides

    public sealed override string ToString()
    {
        var buffer = new StringWriter();
        var writer = new AstWriter(
            buffer
        );
        writer.WriteAstNode(this);
        writer.Flush();
        return buffer.ToString();

    }

    public string ToString(AstWriterOptions options)
    {
        var buffer = new StringWriter();
        var writer = new AstWriter(
            buffer, options
        );
        writer.WriteAstNode(this);
        writer.Flush();
        return buffer.ToString();

    }

    #endregion

}

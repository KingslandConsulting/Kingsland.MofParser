using Kingsland.MofParser.Ast;

namespace Kingsland.MofParser.Parsing
{
    internal class TryParseHelper
    {

        internal delegate bool TryParseDelegate<T>(Parser parser, ref T node, bool throwIfError);

        internal static bool TryParseCommit<T>(Parser parser, ref T node, TryParseDelegate<T> tryParse)
        {
            parser.Descend();
            var value = default(T);
            if (tryParse(parser, ref value, false))
            {
                parser.Commit();
                node = value;
                return true;
            }
            parser.Backtrack();
            return false;
        }

        internal bool TryParseExample(Parser parser, ref ComplexTypeValueAst node, bool throwIfError = false)
        {
            // complexValue
            if (TryParseHelper.TryParseCommit(parser, ref node, ComplexValueAst.TryParse))
            {
                return true;
            }
            return AstNode.HandleUnexpectedToken(parser.Peek(), throwIfError);
        }

    }

}

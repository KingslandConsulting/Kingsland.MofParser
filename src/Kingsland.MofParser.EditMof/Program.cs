using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Lexing;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Text;

namespace Kingsland.MofParser.EditMof;

static class Program
{

    static void Main()
    {

        Program.ModifyTokens();

    }

    private static void ModifyTokens()
    {

        // this example shows a way to modify a token stream to change the value of a property.
        // for example, changing this line:
        //
        // Name = "Web-Server";
        //
        // to
        //
        // Name = "Another-Web-Server";

        const string sourceText = @"
instance of MSFT_RoleResource as $MSFT_RoleResource1ref
{
    ResourceID = ""[WindowsFeature]IIS"";
    Ensure = ""Present"";
    SourceInfo = ""D:\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature"";
    Name = ""Web-Server"";
    ModuleName = ""PSDesiredStateConfiguration"";
    ModuleVersion = ""1.0"";
};";

        // turn the text into a stream of characters for lexing
        var reader = SourceReader.From(sourceText);

        // lex the characters into a sequence of tokens
        var tokens = Lexer.Lex(reader);

        // find the first identifier (keyword) token with the name "Name"
        var name = tokens
            .OfType<IdentifierToken>()
            .First(token => token.Name == "Name");

        // find the first string literal after the name token
        var oldValue = tokens
            .SkipWhile(token => !object.ReferenceEquals(token, name))
            .Skip(1)
            .First(token => token is StringLiteralToken);

        // build the token to replace into the token stream
        var newValue = new StringLiteralToken("Another-Web-Server");

        // replace the token
        tokens[tokens.IndexOf(oldValue)] = newValue;

        // generate the new source text
        var _ = TokenSerializer.ToSourceText(tokens);

        // instance of MSFT_RoleResource as $MSFT_RoleResource1ref
        // {
        //     ResourceID = "[WindowsFeature]IIS";
        //     Ensure = "Present";
        //     SourceInfo = "D:\\dsc\\MyServerConfig.ps1::6::9::WindowsFeature";
        //     Name = "Another-Web-Server";
        //     ModuleName = "PSDesiredStateConfiguration";
        //     ModuleVersion = "1.0";
        // };

    }

}

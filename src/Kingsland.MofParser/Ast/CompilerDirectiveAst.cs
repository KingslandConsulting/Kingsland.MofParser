using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;

namespace Kingsland.MofParser.Ast;

/// <summary>
/// </summary>
/// <remarks>
///
/// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
///
/// 7.3 Compiler directives
///
///     compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
///                         "(" pragmaParameter ")"
///
///     pragmaName         = directiveName
///
///     standardPragmaName = INCLUDE
///
///     pragmaParameter    = stringValue ; if the pragma is INCLUDE,
///                                      ; the parameter value
///                                      ; shall represent a relative
///                                      ; or full file path
///
///     PRAGMA             = "#pragma"  ; keyword: case insensitive
///
///     INCLUDE            = "include"  ; keyword: case insensitive
///
///     directiveName      = org-id "_" IDENTIFIER
///
public sealed record CompilerDirectiveAst : MofProductionAst
{

    #region Builder

    public sealed class Builder
    {

        public PragmaToken? PragmaKeyword
        {
            get;
            set;
        }

        public IdentifierToken? PragmaName
        {
            get;
            set;
        }

        public StringValueAst? PragmaParameter
        {
            get;
            set;
        }

        public CompilerDirectiveAst Build()
        {
            return new CompilerDirectiveAst(
                this.PragmaKeyword ?? throw new InvalidOperationException(
                    $"{nameof(this.PragmaKeyword)} property must be set before calling {nameof(Build)}."
                ),
                this.PragmaName ?? throw new InvalidOperationException(
                    $"{nameof(this.PragmaName)} property must be set before calling {nameof(Build)}."
                ),
                this.PragmaParameter ?? throw new InvalidOperationException(
                    $"{nameof(this.PragmaParameter)} property must be set before calling {nameof(Build)}."
                )
            );
        }

    }

    #endregion

    #region Constructors

    internal CompilerDirectiveAst(
        PragmaToken pragmaKeyword,
        IdentifierToken pragmaName,
        StringValueAst pragmaParameter
    )
    {
        this.PragmaKeyword = pragmaKeyword;
        this.PragmaName = pragmaName;
        this.PragmaParameter = pragmaParameter;
    }

    #endregion

    #region Properties

    public PragmaToken PragmaKeyword
    {
        get;
        private init;
    }

    public IdentifierToken PragmaName
    {
        get;
        private init;
    }

    public StringValueAst PragmaParameter
    {
        get;
        private init;
    }

    #endregion

    #region Object Overrides

    public override string ToString()
    {
        return AstMofGenerator.ConvertCompilerDirectiveAst(this);
    }

    #endregion

}

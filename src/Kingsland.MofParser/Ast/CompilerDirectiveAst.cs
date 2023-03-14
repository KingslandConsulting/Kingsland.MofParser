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
/// </remarks>
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
            return new(
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
        this.PragmaKeyword = pragmaKeyword ?? throw new ArgumentNullException(nameof(pragmaKeyword));
        this.PragmaName = pragmaName ?? throw new ArgumentNullException(nameof(pragmaName));
        this.PragmaParameter = pragmaParameter ?? throw new ArgumentNullException(nameof(pragmaParameter));
    }

    #endregion

    #region Properties

    public PragmaToken PragmaKeyword
    {
        get;
    }

    public IdentifierToken PragmaName
    {
        get;
    }

    public StringValueAst PragmaParameter
    {
        get;
    }

    #endregion

}

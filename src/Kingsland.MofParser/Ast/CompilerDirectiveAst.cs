using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.3 Compiler directives
    ///
    /// Compiler directives direct the processing of MOF files. Compiler directives do not create, modify, or
    /// annotate the language elements.
    ///
    /// Compiler directives shall conform to the format defined by ABNF rule compilerDirective (whitespace
    /// as defined in 5.2 is allowed between the elements of the rules in this ABNF section):
    ///
    ///     compilerDirective = PRAGMA ( pragmaName / standardPragmaName )
    ///                         "(" pragmaParameter ")"
    ///
    ///     pragmaName         = directiveName
    ///     standardPragmaName = INCLUDE
    ///     pragmaParameter    = stringValue ; if the pragma is INCLUDE,
    ///                                      ; the parameter value
    ///                                      ; shall represent a relative
    ///                                      ; or full file path
    ///
    ///     PRAGMA             = "#pragma"  ; keyword: case insensitive
    ///     INCLUDE            = "include"  ; keyword: case insensitive
    ///
    ///     directiveName      = org-id "_" IDENTIFIER
    ///

    public sealed class CompilerDirectiveAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public PragmaToken PragmaKeyword
            {
                get;
                set;
            }

            public IdentifierToken PragmaName
            {
                get;
                set;
            }

            public StringValueAst PragmaParameter
            {
                get;
                set;
            }

            public CompilerDirectiveAst Build()
            {
                return new CompilerDirectiveAst(
                    this.PragmaKeyword,
                    this.PragmaName,
                    this.PragmaParameter
                );
            }

        }

        #endregion

        #region Constructors

        private CompilerDirectiveAst(PragmaToken pragmaKeyword, IdentifierToken pragmaName, StringValueAst pragmaParameter)
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
            private set;
        }

        public IdentifierToken PragmaName
        {
            get;
            private set;
        }

        public StringValueAst PragmaParameter
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertCompilerDirectiveAst(this);
        }

        #endregion

    }

}

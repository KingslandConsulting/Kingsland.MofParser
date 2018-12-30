using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    ///
    /// </summary>
    /// <param name="stream"></param>
    /// <returns>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.4 Qualifiers
    ///
    /// </returns>
    public sealed class QualifierTypeDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Flavors = new List<string>();
            }

            public IdentifierToken Name
            {
                get;
                set;
            }

            public PrimitiveTypeValueAst Initializer
            {
                get;
                set;
            }

            public List<string> Flavors
            {
                get;
                set;
            }

            public QualifierTypeDeclarationAst Build()
            {
                return new QualifierTypeDeclarationAst(
                    this.Name,
                    this.Initializer,
                    new ReadOnlyCollection<string>(
                        this.Flavors ?? new List<string>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        private QualifierTypeDeclarationAst(
            IdentifierToken name,
            PrimitiveTypeValueAst initializer,
            ReadOnlyCollection<string> flavors
        )
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
            this.Flavors = flavors ?? throw new ArgumentNullException(nameof(flavors));
        }

        #endregion

        #region Properties

        public IdentifierToken Name
        {
            get;
            private set;
        }

        public PrimitiveTypeValueAst Initializer
        {
            get;
            private set;
        }

        public ReadOnlyCollection<string> Flavors
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertToMof(this);
        }

        #endregion

    }

}

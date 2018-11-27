using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    public sealed class QualifierDeclarationAst : MofProductionAst
    {

        #region Builder

        public sealed class Builder
        {

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

            public QualifierDeclarationAst Build()
            {
                return new QualifierDeclarationAst(
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

        private QualifierDeclarationAst(
            IdentifierToken name,
            PrimitiveTypeValueAst initializer,
            ReadOnlyCollection<string> flavors
        )
        {
            this.Name = name ?? throw new ArgumentNullException(nameof(name));
            this.Initializer = initializer ?? throw new ArgumentNullException(nameof(initializer));
            this.Flavors = Flavors ?? throw new ArgumentNullException(nameof(flavors));
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

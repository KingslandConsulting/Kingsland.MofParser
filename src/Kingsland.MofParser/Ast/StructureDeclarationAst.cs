using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.1 Structure declaration
    ///
    ///     structureDeclaration = [ qualifierList ] STRUCTURE structureName
    ///                            [ superStructure ]
    ///                            "{" *structureFeature "}" ";"
    ///
    ///     structureName        = elementName
    ///     superStructure       = ":" structureName
    ///     structureFeature     = structureDeclaration / ; local structure
    ///                            enumerationDeclaration / ; local enumeration
    ///                            propertyDeclaration
    ///
    ///     STRUCTURE            = "structure" ; keyword: case insensitive
    ///
    /// </remarks>
    public sealed class StructureDeclarationAst : MofProductionAst, IStructureFeatureAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.Features = new List<IStructureFeatureAst>();
            }

            public QualifierListAst Qualifiers
            {
                get;
                set;
            }

            public IdentifierToken StructureName
            {
                get;
                set;
            }

            public IdentifierToken SuperStructure
            {
                get;
                set;
            }

            public List<IStructureFeatureAst> Features
            {
                get;
                set;
            }

            public StructureDeclarationAst Build()
            {
                return new StructureDeclarationAst(
                    this.Qualifiers,
                    this.StructureName,
                    this.SuperStructure,
                    new ReadOnlyCollection<IStructureFeatureAst>(
                        this.Features ?? new List<IStructureFeatureAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public StructureDeclarationAst(QualifierListAst qualifiers, IdentifierToken structureName, IdentifierToken superStructure, ReadOnlyCollection<IStructureFeatureAst> features)
        {
            this.Qualifiers = qualifiers ?? new QualifierListAst.Builder().Build();
            this.StructureName = structureName ?? throw new ArgumentNullException(nameof(structureName));
            this.SuperStructure = superStructure;
            this.Features = features ?? new ReadOnlyCollection<IStructureFeatureAst>(new List<IStructureFeatureAst>());
        }

        #endregion

        #region Properties

        public QualifierListAst Qualifiers
        {
            get;
            private set;
        }

        public IdentifierToken StructureName
        {
            get;
            private set;
        }

        public IdentifierToken SuperStructure
        {
            get;
            private set;
        }

        public ReadOnlyCollection<IStructureFeatureAst> Features
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertStructureDeclarationAst(this);
        }

        #endregion

    }

}

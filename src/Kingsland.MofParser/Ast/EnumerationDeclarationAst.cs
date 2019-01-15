using Kingsland.MofParser.CodeGen;
using Kingsland.MofParser.Tokens;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Kingsland.MofParser.Ast
{

    /// <summary>
    /// </summary>
    /// <returns></returns>
    /// <remarks>
    ///
    /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
    ///
    /// 7.5.4 Enumeration declaration
    ///
    ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
    ///
    ///     enumTypeHeader         = [ qualifierList ] ENUMERATION
    ///     enumName               = elementName
    ///     enumTypeDeclaration    = (DT_INTEGER / integerEnumName ) integerEnumDeclaration /
    ///                              (DT_STRING / stringEnumName) stringEnumDeclaration
    ///
    ///     integerEnumName        = enumName
    ///     stringEnumName         = enumName
    ///
    ///     integerEnumDeclaration = "{" [ integerEnumElement
    ///                              *( "," integerEnumElement) ] "}"
    ///     stringEnumDeclaration  = "{" [ stringEnumElement
    ///                              *( "," stringEnumElement) ] "}"
    ///
    ///     integerEnumElement     = [ qualifierList ] enumLiteral "=" integerValue
    ///     stringEnumElement      = [ qualifierList ] enumLiteral [ "=" stringValue ]
    ///
    ///     enumLiteral            = IDENTIFIER
    ///
    ///     ENUMERATION            = "enumeration" ; keyword: case insensitive
    ///
    /// </remarks>
    public sealed class EnumerationDeclarationAst : MofProductionAst, IStructureFeatureAst
    {

        #region Builder

        public sealed class Builder
        {

            public Builder()
            {
                this.EnumElements = new List<EnumElementAst>();
            }

            public QualifierListAst QualifierList
            {
                get;
                set;
            }

            public IdentifierToken EnumName
            {
                get;
                set;
            }

            public IdentifierToken EnumType
            {
                get;
                set;
            }

            public List<EnumElementAst> EnumElements
            {
                get;
                set;
            }

            public EnumerationDeclarationAst Build()
            {
                return new EnumerationDeclarationAst(
                    this.QualifierList,
                    this.EnumName,
                    this.EnumType,
                    new ReadOnlyCollection<EnumElementAst>(
                        this.EnumElements ?? new List<EnumElementAst>()
                    )
                );
            }

        }

        #endregion

        #region Constructors

        public EnumerationDeclarationAst(QualifierListAst qualifierList, IdentifierToken enumName, IdentifierToken enumType, ReadOnlyCollection<EnumElementAst> enumElements)
        {
            this.QualifierList = qualifierList ?? new QualifierListAst.Builder().Build();
            this.EnumName = enumName ?? throw new ArgumentNullException(nameof(enumName));
            this.EnumType = enumType ?? throw new ArgumentNullException(nameof(enumType));
            this.EnumElements = enumElements ?? new ReadOnlyCollection<EnumElementAst>(
                new List<EnumElementAst>()
            );
        }

        #endregion

        #region Properties

        public QualifierListAst QualifierList
        {
            get;
            private set;
        }

        public IdentifierToken EnumName
        {
            get;
            private set;
        }

        public IdentifierToken EnumType
        {
            get;
            private set;
        }

        public ReadOnlyCollection<EnumElementAst> EnumElements
        {
            get;
            private set;
        }

        #endregion

        #region Object Overrides

        public override string ToString()
        {
            return MofGenerator.ConvertEnumerationDeclarationAst(this);
        }

        #endregion

    }

}

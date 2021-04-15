using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
using Kingsland.ParseFx.Parsing;
using Kingsland.ParseFx.Syntax;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Kingsland.MofParser.Parsing
{

    internal sealed class ParserEngine
    {

        #region 7.2 MOF specification

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.2 MOF specification
        ///
        ///     mofSpecification = *mofProduction
        ///
        /// </remarks>
        public static MofSpecificationAst ParseMofSpecificationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            var node = new MofSpecificationAst.Builder();
            while (!stream.Eof)
            {
                node.Productions.Add(
                    ParserEngine.ParseMofProductionAst(stream, quirks)
                );
            }
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.2 MOF specification
        ///
        ///     mofProduction          = compilerDirective /
        ///                              structureDeclaration /
        ///                              classDeclaration /
        ///                              associationDeclaration /
        ///                              enumerationDeclaration /
        ///                              instanceValueDeclaration /
        ///                              structureValueDeclaration /
        ///                              qualifierTypeDeclaration
        ///
        /// 7.3 Compiler directives
        ///
        ///     compilerDirective      = PRAGMA ( pragmaName / standardPragmaName )
        ///                              "(" pragmaParameter ")"
        ///
        /// 7.5.1 Structure declaration
        ///
        ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
        ///                              [ superStructure ]
        ///                              "{" *structureFeature "}" ";"
        ///
        /// 7.5.2 Class declaration
        ///
        ///     classDeclaration       = [ qualifierList ] CLASS className [ superClass ]
        ///                              "{" *classFeature "}" ";"
        ///
        /// 7.5.3 Association declaration
        ///
        ///     associationDeclaration = [ qualifierList ] ASSOCIATION associationName
        ///                              [ superAssociation ]
        ///                              "{" * classFeature "}" ";"
        ///
        /// 7.5.4 Enumeration declaration
        ///
        ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
        ///
        ///     enumTypeHeader         = [qualifierList] ENUMERATION
        ///
        /// 7.6.2 Complex type value
        ///
        ///     instanceValueDeclaration  = INSTANCE OF ( className / associationName )
        ///                                 [ alias ]
        ///                                 propertyValueList ";"
        ///
        ///     structureValueDeclaration = VALUE OF
        ///                                 ( className / associationName / structureName )
        ///                                 alias
        ///                                 propertyValueList ";"
        ///
        /// 7.4 Qualifiers
        ///
        ///     qualifierTypeDeclaration  = [ qualifierList ] QUALIFIER qualifierName ":"
        ///                                 qualifierType qualifierScope
        ///                                 [ qualifierPolicy ] ";"
        ///
        /// </remarks>
        public static MofProductionAst ParseMofProductionAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var peek = stream.Peek();

            // compilerDirective
            if (peek is PragmaToken)
            {
                return ParserEngine.ParseCompilerDirectiveAst(stream, quirks);
            }

            if (peek is IdentifierToken valueOrInstance)
            {
                switch (valueOrInstance.GetNormalizedName())
                {
                    case Constants.VALUE:
                        // structureValueDeclaration
                        return ParserEngine.ParseStructureValueDeclarationAst(stream, quirks);
                    case Constants.INSTANCE:
                        // instanceValueDeclaration
                        return ParserEngine.ParseInstanceValueDeclarationAst(stream, quirks);
                }
            }

            // all other mofProduction elements start with [ qualifieList ]
            var qualifierList = (peek is AttributeOpenToken)
                ? ParserEngine.ParseQualifierListAst(stream, quirks)
                : new QualifierListAst();

            var productionType = stream.Peek<IdentifierToken>();
            if (productionType == null)
            {
                throw new UnexpectedTokenException(stream.Peek());
            }
            return productionType.GetNormalizedName() switch
            {
                Constants.STRUCTURE =>
                    // structureDeclaration
                    ParserEngine.ParseStructureDeclarationAst(stream, qualifierList, quirks),
                Constants.CLASS =>
                    // classDeclaration
                    ParserEngine.ParseClassDeclarationAst(stream, qualifierList, quirks),
                Constants.ASSOCIATION =>
                    // associationDeclaration
                    ParserEngine.ParseAssociationDeclarationAst(stream, qualifierList, quirks),
                Constants.ENUMERATION =>
                    // enumerationDeclaration
                    ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList, quirks),
                Constants.QUALIFIER =>
                    // qualifierTypeDeclaration
                    ParserEngine.ParseQualifierTypeDeclarationAst(stream, qualifierList, quirks),
                _ =>
                    throw new UnexpectedTokenException(productionType)
            };
        }

        #endregion

        #region 7.3 Compiler directives

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
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
        ///     PRAGMA             = "#pragma"   ; keyword: case insensitive
        ///
        ///     INCLUDE            = "include"   ; keyword: case insensitive
        ///
        ///     directiveName      = org-id "_" IDENTIFIER
        ///
        /// </remarks>
        public static CompilerDirectiveAst ParseCompilerDirectiveAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new CompilerDirectiveAst.Builder();

            // PRAGMA
            node.PragmaKeyword = stream.Read<PragmaToken>();

            // ( pragmaName / standardPragmaName )
            node.PragmaName = stream.Read<IdentifierToken>();

            // "("
            var parenthesisOpen = stream.Read<ParenthesisOpenToken>();

            // pragmaParameter
            node.PragmaParameter = ParserEngine.ParseStringValueAst(stream, quirks);

            // ")"
            var parenthesisClose = stream.Read<ParenthesisCloseToken>();

            return node.Build();

        }

        #endregion

        #region 7.4 Qualifiers

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
        ///     qualifierTypeDeclaration = [ qualifierList ] QUALIFIER qualifierName ":"
        ///                                qualifierType qualifierScope
        ///                                [ qualifierPolicy ] ";"
        ///
        ///     qualifierName            = elementName
        ///
        ///     qualifierType            = primitiveQualifierType / enumQualiferType
        ///
        ///     primitiveQualifierType   = primitiveType [ array ]
        ///                                [ "=" primitiveTypeValue ] ";"
        ///
        ///     enumQualiferType         = enumName [ array ] "=" enumTypeValue ";"
        ///
        ///     qualifierScope           = SCOPE "(" ANY / scopeKindList ")"
        ///
        /// </returns>
        public static QualifierTypeDeclarationAst ParseQualifierTypeDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new QualifierTypeDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // QUALIFIER
            node.QualifierKeyword = stream.ReadIdentifierToken(Constants.QUALIFIER);

            // qualifierName
            node.QualifierName = stream.Read<IdentifierToken>();

            // ":"
            var colon = stream.Read<ColonToken>();

            throw new NotImplementedException();

            // qualifierType

            // qualifierScope

            // [qualifierPolicy]

            // ";"
            //stream.Read<StatementEndToken>();

            //return node.Build();

        }

        #endregion

        #region 7.4.1 QualifierList

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.4.1 QualifierList
        ///
        ///     qualifierList = "[" qualifierValue *( "," qualifierValue ) "]"
        ///
        /// </remarks>
        public static QualifierListAst ParseQualifierListAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new QualifierListAst.Builder();

            // "["
            var attributeOpen = stream.Read<AttributeOpenToken>();

            // qualifierValue
            node.QualifierValues.Add(
                ParserEngine.ParseQualifierValueAst(stream, quirks)
            );

            // *( "," qualifierValue )
            while (stream.TryRead<CommaToken>(out var comma))
            {
                node.QualifierValues.Add(
                    ParserEngine.ParseQualifierValueAst(stream, quirks)
                );
            }

            // "]"
            var attributeClose = stream.Read<AttributeCloseToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.4.1 QualifierList
        ///
        ///     qualifierValue = qualifierName [ qualifierValueInitializer /
        ///                      qualiferValueArrayInitializer ]
        ///
        /// 7.4 Qualifiers
        ///
        ///     qualifierName  = elementName
        ///
        /// </remarks>
        public static QualifierValueAst ParseQualifierValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new QualifierValueAst.Builder();

            // qualifierName
            node.QualifierName = stream.ReadIdentifierToken(
                t => StringValidator.IsElementName(t.Name)
            );

            // [ qualifierValueInitializer / qualiferValueArrayInitializer ]
            switch (stream.Peek())
            {
                case ParenthesisOpenToken paranthesesOpen:
                    // qualifierValueInitializer
                    node.Initializer = ParserEngine.ParseQualifierValueInitializer(stream, quirks);
                    break;
                case BlockOpenToken blockOpen:
                    // qualiferValueArrayInitializer
                    node.Initializer = ParserEngine.ParseQualifierValueArrayInitializer(stream, quirks);
                    break;
            }

            // see https://github.com/mikeclayton/MofParser/issues/49
            //
            // Pseudo-ABNF for MOF V2 qualifiers:
            //
            //     qualifierList_v2       = "[" qualifierValue_v2 *( "," qualifierValue_v2 ) "]"
            //     qualifierValue_v2      = qualifierName [ qualifierValueInitializer / qualiferValueArrayInitializer ] ":" qualifierFlavourList_v2
            //     qualifierFlavorList_v2 = qualifierFlavorName *( " " qualifierFlavorName )
            //
            var quirkEnabled = (quirks & ParserQuirks.AllowMofV2Qualifiers) == ParserQuirks.AllowMofV2Qualifiers;
            if (quirkEnabled)
            {
                if (stream.TryRead<ColonToken>(out var colon))
                {
                    node.Flavors.Add(stream.Read<IdentifierToken>());
                    while (stream.TryRead<IdentifierToken>(out var identifier))
                    {
                        node.Flavors.Add(identifier!);
                    }
                }
            }

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.4.1 QualifierList
        ///
        ///     qualifierValueInitializer = "(" literalValue ")"
        ///
        /// </remarks>
        public static QualifierValueInitializerAst ParseQualifierValueInitializer(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            var node = new QualifierValueInitializerAst.Builder();
            // "("
            var parenthesisOpen = stream.Read<ParenthesisOpenToken>();
            // literalValue
            node.Value = ParserEngine.ParseLiteralValueAst(stream, quirks);
            // ")"
            var parenthesisClose = stream.Read<ParenthesisCloseToken>();
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.4.1 QualifierList
        ///
        ///     qualiferValueArrayInitializer = "{" literalValue *( "," literalValue ) "}"
        ///
        /// </remarks>
        public static QualifierValueArrayInitializerAst ParseQualifierValueArrayInitializer(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new QualifierValueArrayInitializerAst.Builder();

            // "{"
            var blockOpen = stream.Read<BlockOpenToken>();

            // check if we allow empty qualifier arrays
            // see https://github.com/mikeclayton/MofParser/issues/51
            var quirkEnabled = (quirks & ParserQuirks.AllowEmptyQualifierValueArrays) == ParserQuirks.AllowEmptyQualifierValueArrays;
            if (quirkEnabled && stream.TryPeek<BlockCloseToken>())
            {
                // this is an empty array, and the quirk to allow empty arrays is enabled,
                // so skip trying to read the array values
            }
            else
            {
                // literalValue
                node.Values.Add(
                    ParserEngine.ParseLiteralValueAst(stream, quirks)
                );
                // *( "," literalValue )
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    node.Values.Add(
                        ParserEngine.ParseLiteralValueAst(stream, quirks)
                    );
                }
            }

            // "}"
            var blockClose = stream.Read<BlockCloseToken>();

            return node.Build();

        }

        #endregion

        #region 7.5.1 Structure declaration

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="qualifiers"></param>
        /// <returns></returns>
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
        ///
        ///     superStructure       = ":" structureName
        ///
        ///     structureFeature     = structureDeclaration /   ; local structure
        ///                            enumerationDeclaration / ; local enumeration
        ///                            propertyDeclaration
        ///
        ///     STRUCTURE            = "structure" ; keyword: case insensitive
        ///
        /// </remarks>
        public static StructureDeclarationAst ParseStructureDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new StructureDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // STRUCTURE
            var structureKeyword = stream.ReadIdentifierToken(Constants.STRUCTURE);

            // structureName
            node.StructureName = stream.ReadIdentifierToken(
                t => StringValidator.IsStructureName(t.Name)
            );

            // [ superStructure ]
            {
                // ":"
                if (stream.TryRead<ColonToken>(out var colon))
                {
                    // structureName
                    node.SuperStructure = stream.ReadIdentifierToken(
                        t => StringValidator.IsStructureName(t.Name)
                    );
                }
            }

            // "{"
            var blockOpen = stream.Read<BlockOpenToken>();

            // *structureFeature
            while (!stream.TryPeek<BlockCloseToken>())
            {
                node.StructureFeatures.Add(
                    ParserEngine.ParseStructureFeatureAst(stream, quirks)
                );
            }

            // "}"
            var blockClose = stream.Read<BlockCloseToken>();

            // ";"
            var statementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.1 Structure declaration
        ///
        ///     structureFeature       = structureDeclaration /   ; local structure
        ///                              enumerationDeclaration / ; local enumeration
        ///                              propertyDeclaration
        ///
        ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
        ///                              [ superStructure ]
        ///                              "{" *structureFeature "}" ";"
        ///
        /// 7.5.4 Enumeration declaration
        ///
        ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
        ///
        ///     enumTypeHeader         = [qualifierList] ENUMERATION
        ///
        /// 7.5.5 Property declaration
        ///
        ///     propertyDeclaration    = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                              complexPropertyDeclaration /
        ///                              enumPropertyDeclaration /
        ///                              referencePropertyDeclaration ) ";"
        ///
        ///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
        ///                                    [ "=" primitiveTypeValue ]
        ///
        ///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
        ///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///
        ///     enumPropertyDeclaration      = enumName propertyName [ array ]
        ///                                    [ "=" enumTypeValue ]
        ///
        ///     referencePropertyDeclaration = classReference propertyName [ array ]
        ///                                    [ "=" referenceTypeValue ]
        ///
        /// </remarks>
        public static IStructureFeatureAst ParseStructureFeatureAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            // all structureFeatures start with an optional "[ qualifierList ]"
            var qualifierList = (stream.TryPeek<AttributeOpenToken>())
                ? ParserEngine.ParseQualifierListAst(stream, quirks)
                : new QualifierListAst();

            // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
            // or propertyDeclaration

            // structureDeclaration   => STRUCTURE
            // enumerationDeclaration => ENUMERATION
            // propertyDeclaration    => primitiveType / structureOrClassName / enumName / classReference

            if (!stream.TryPeek<IdentifierToken>(out var identifier))
            {
                throw new UnexpectedTokenException(stream.Peek());
            }

            return identifier!.GetNormalizedName() switch
            {
                Constants.STRUCTURE =>
                    // structureDeclaration
                    ParserEngine.ParseStructureDeclarationAst(stream, qualifierList, quirks),
                Constants.ENUMERATION =>
                    // enumerationDeclaration
                    ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList, quirks),
                _ =>
                    // propertyDeclaration
                    ParserEngine.ParsePropertyDeclarationAst(stream, qualifierList, quirks)
            };
        }

        #endregion

        #region 7.5.2 Class declaration

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.2 Class declaration
        ///
        ///     classDeclaration = [ qualifierList ] CLASS className [ superClass ]
        ///                        "{" *classFeature "}" ";"
        ///
        ///     className        = elementName
        ///
        ///     superClass       = ":" className
        ///
        ///     classFeature     = structureFeature /
        ///                        methodDeclaration
        ///
        ///     CLASS            = "class" ; keyword: case insensitive
        ///
        /// </remarks>
        public static ClassDeclarationAst ParseClassDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new ClassDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // CLASS
            var classKeyword = stream.ReadIdentifierToken(Constants.CLASS);

            // className
            node.ClassName = stream.ReadIdentifierToken(
                t => StringValidator.IsClassName(t.Name)
            );

            // [ superClass ]
            {
                // ":"
                if (stream.TryRead<ColonToken>(out var colon))
                {
                    // className
                    node.SuperClass = stream.ReadIdentifierToken(
                        t => StringValidator.IsClassName(t.Name)
                    );
                }
            }

            // "{"
            var blockOpen = stream.Read<BlockOpenToken>();

            // *classFeature
            while (!stream.TryPeek<BlockCloseToken>())
            {
                node.ClassFeatures.Add(
                    ParserEngine.ParseClassFeatureAst(stream, quirks)
                );
            }

            // "}"
            var blockClose = stream.Read<BlockCloseToken>();

            // ";"
            var statementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.2 Class declaration
        ///
        ///     classFeature           = structureFeature / methodDeclaration
        ///
        /// 7.5.1 Structure declaration
        ///
        ///     structureDeclaration   = [ qualifierList ] STRUCTURE structureName
        ///                              [superStructure]
        ///                              "{" *structureFeature "}" ";"
        ///
        ///     structureFeature       = structureDeclaration /   ; local structure
        ///                              enumerationDeclaration / ; local enumeration
        ///                              propertyDeclaration
        ///
        /// 7.5.4 Enumeration declaration
        ///
        ///     enumerationDeclaration = enumTypeHeader enumName ":" enumTypeDeclaration ";"
        ///
        ///     enumTypeHeader         = [qualifierList] ENUMERATION
        ///
        /// </remarks>
        public static IClassFeatureAst ParseClassFeatureAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            // all classFeatures start with an optional "[ qualifierList ]"
            var qualifierList = stream.TryPeek<AttributeOpenToken>()
                ? ParserEngine.ParseQualifierListAst(stream, quirks)
                : new QualifierListAst();

            // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
            // propertyDeclaration or methodDeclaration

            if (!stream.TryPeek<IdentifierToken>(out var identifier))
            {
                throw new UnexpectedTokenException(stream.Peek());
            }

            return identifier!.GetNormalizedName() switch
            {
                Constants.STRUCTURE =>
                    // structureDeclaration
                    ParserEngine.ParseStructureDeclarationAst(stream, qualifierList, quirks),
                Constants.ENUMERATION =>
                    // enumerationDeclaration
                    ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList, quirks),
                _ =>
                    // propertyDeclaration or methodDeclaration
                    ParserEngine.ParseMemberDeclarationAst(stream, qualifierList, true, true, quirks)
            };
        }

        #endregion

        #region 7.5.3 Association declaration

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.3 Association declaration
        ///
        ///     associationDeclaration = [ qualifierList ] ASSOCIATION associationName
        ///                              [ superAssociation ]
        ///                              "{" * classFeature "}" ";"
        ///
        ///     associationName        = elementName
        ///
        ///     superAssociation       = ":" elementName
        ///
        ///     ASSOCIATION            = "association" ; keyword: case insensitive
        ///
        /// </remarks>
        public static AssociationDeclarationAst ParseAssociationDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new AssociationDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // ASSOCIATION
            stream.ReadIdentifierToken(Constants.ASSOCIATION);

            // associationName
            node.AssociationName = stream.ReadIdentifierToken(
                t => StringValidator.IsAssociationName(t.Name)
            );

            // [ superAssociation ]
            if (stream.TryPeek<ColonToken>())
            {
                // ":"
                var colon = stream.Read<ColonToken>();
                // associationName
                node.SuperAssociation = stream.ReadIdentifierToken(
                    t => StringValidator.IsAssociationName(t.Name)
                );
            }

            // "{"
            var blockOpen = stream.Read<BlockOpenToken>();

            // *classFeature
            while (!stream.TryPeek<BlockCloseToken>())
            {
                node.ClassFeatures.Add(
                    ParserEngine.ParseClassFeatureAst(stream, quirks)
                );
            }

            // "}"
            var blockClose = stream.Read<BlockCloseToken>();

            // ";"
            var statementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        #endregion

        #region 7.5.4 Enumeration declaration

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
        ///
        ///     enumName               = elementName
        ///
        ///     enumTypeDeclaration    = ( DT_INTEGER / integerEnumName ) integerEnumDeclaration /
        ///                              ( DT_STRING / stringEnumName ) stringEnumDeclaration
        ///
        ///     integerEnumName        = enumName
        ///     stringEnumName         = enumName
        ///
        ///     integerEnumDeclaration = "{" [ integerEnumElement *( "," integerEnumElement ) ] "}"
        ///     stringEnumDeclaration  = "{" [ stringEnumElement *( "," stringEnumElement ) ] "}"
        ///
        ///     ENUMERATION            = "enumeration" ; keyword: case insensitive
        ///
        /// </remarks>
        public static EnumerationDeclarationAst ParseEnumerationDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new EnumerationDeclarationAst.Builder();

            var isIntegerEnum = false;
            var isStringEnum = false;

            // [ qualifierList ]
            // note - this has already been read for us and gets passed in as a parameter
            node.QualifierList = qualifierList;

            // ENUMERATION
            var enumeration = stream.ReadIdentifierToken(Constants.ENUMERATION);

            // enumName
            node.EnumName = stream.ReadIdentifierToken(
                t => StringValidator.IsEnumName(t.Name)
            );

            // ":"
            var colon = stream.Read<ColonToken>();

            // ( DT_INTEGER / integerEnumName ) / ( DT_STRING / stringEnumName )
            var enumTypeDeclaration = stream.Peek<IdentifierToken>();
            if (enumTypeDeclaration == null)
            {
                throw new UnexpectedTokenException(stream.Peek());
            }
            var enumTypeDeclarationName = enumTypeDeclaration.GetNormalizedName();
            switch (enumTypeDeclarationName)
            {
                case Constants.DT_INTEGER:
                    isIntegerEnum = true;
                    break;
                case Constants.DT_STRING:
                    isStringEnum = true;
                    break;
                default:
                    // check if we allow deprecated integer sutypes
                    // see https://github.com/mikeclayton/MofParser/issues/52
                    var quirksEnabled = (quirks & ParserQuirks.AllowDeprecatedMof300IntegerTypesAsEnumerationDeclarationsBase) == ParserQuirks.AllowDeprecatedMof300IntegerTypesAsEnumerationDeclarationsBase;
                    if (quirksEnabled)
                    {
                        var found = false;
                        switch (enumTypeDeclarationName)
                        {
                            case Constants.DT_UINT8:
                            case Constants.DT_UINT16:
                            case Constants.DT_UINT32:
                            case Constants.DT_UINT64:
                            case Constants.DT_SINT8:
                            case Constants.DT_SINT16:
                            case Constants.DT_SINT32:
                            case Constants.DT_SINT64:
                                isIntegerEnum = true;
                                found = true;
                                break;
                        }
                        if (found)
                        {
                            break;
                        }
                    }
                    // this enumerationDeclaration is inheriting from a base enum.
                    // as a result, we don't know whether this is an integer or
                    // string enum until we inspect the type of the base enum
                    if (!StringValidator.IsEnumName(enumTypeDeclaration.Name))
                    {
                        throw new UnexpectedTokenException(enumTypeDeclaration);
                    }
                    break;
            }
            node.EnumType = stream.Read<IdentifierToken>();

            // "{"
            stream.Read<BlockOpenToken>();

            // [ integerEnumElement *( "," integerEnumElement ) ]
            // [ stringEnumElement *( "," stringEnumElement ) ]
            if (!stream.TryPeek<BlockCloseToken>())
            {
                // integerEnumElement / stringEnumElement
                node.EnumElements.Add(
                    ParserEngine.ParseEnumElementAst(stream, isIntegerEnum, isStringEnum, quirks)
                );
                // *( "," integerEnumElement ) / *( "," stringEnumElement )
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    node.EnumElements.Add(
                        ParserEngine.ParseEnumElementAst(stream, isIntegerEnum, isStringEnum, quirks)
                    );
                }
            }

            // "}"
            var blockClose = stream.Read<BlockCloseToken>();

            // ";"
            var statementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.4 Enumeration declaration
        ///
        ///     integerEnumElement = [ qualifierList ] enumLiteral "=" integerValue
        ///     stringEnumElement  = [ qualifierList ] enumLiteral [ "=" stringValue ]
        ///
        ///     enumLiteral        = IDENTIFIER
        ///
        /// </remarks>
        public static EnumElementAst ParseEnumElementAst(TokenStream stream, bool isIntegerEnum, bool isStringEnum, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new EnumElementAst.Builder();

            // [ qualifierList ]
            if (stream.TryPeek<AttributeOpenToken>())
            {
                node.QualifierList = ParserEngine.ParseQualifierListAst(stream, quirks);
            }

            // enumLiteral
            node.EnumElementName = stream.Read<IdentifierToken>();

            // "=" integerValue / [ "=" stringValue ]
            if (stream.TryRead<EqualsOperatorToken>(out var equals))
            {
                var enumValue = stream.Peek();
                switch (enumValue)
                {
                    case IntegerLiteralToken _:
                        // integerValue
                        if (isStringEnum)
                        {
                            throw new UnexpectedTokenException(enumValue);
                        }
                        node.EnumElementValue = ParserEngine.ParseIntegerValueAst(stream, quirks);
                        break;
                    case StringLiteralToken _:
                        // stringValue
                        if (isIntegerEnum)
                        {
                            throw new UnexpectedTokenException(enumValue);
                        }
                        node.EnumElementValue = ParserEngine.ParseStringValueAst(stream, quirks);
                        break;
                    default:
                        throw new UnexpectedTokenException(enumValue);
                }
            }
            else if (isIntegerEnum)
            {
                // "=" is mandatory for integer enums
                throw new UnexpectedTokenException(stream.Peek());
            }

            return node.Build();

        }

        #endregion

        #region 7.5.5 Property declaration

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.5 Property declaration
        ///
        ///     propertyDeclaration          = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                                    complexPropertyDeclaration /
        ///                                    enumPropertyDeclaration /
        ///                                    referencePropertyDeclaration) ";"
        ///
        /// </remarks>
        public static PropertyDeclarationAst ParsePropertyDeclarationAst(TokenStream stream, QualifierListAst qualifierList, ParserQuirks quirks = ParserQuirks.None)
        {
            return (PropertyDeclarationAst)ParserEngine.ParseMemberDeclarationAst(stream, qualifierList, true, false, quirks);
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.5 Property declaration
        ///
        ///     propertyDeclaration          = [ qualifierList ] ( primitivePropertyDeclaration /
        ///                                    complexPropertyDeclaration /
        ///                                    enumPropertyDeclaration /
        ///                                    referencePropertyDeclaration) ";"
        ///
        ///     primitivePropertyDeclaration = primitiveType propertyName [ array ]
        ///                                    [ "=" primitiveTypeValue ]
        ///
        ///     complexPropertyDeclaration   = structureOrClassName propertyName [ array ]
        ///                                    [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///
        ///     enumPropertyDeclaration      = enumName propertyName [ array ]
        ///                                    [ "=" enumTypeValue ]
        ///
        ///     referencePropertyDeclaration = classReference propertyName [ array ]
        ///                                    [ "=" referenceTypeValue ]
        ///
        ///     array                        = "[" "]"
        ///     propertyName                 = IDENTIFIER
        ///     structureOrClassName         = IDENTIFIER
        ///
        ///     classReference               = DT_REFERENCE
        ///     DT_REFERENCE                 = className REF
        ///     REF                          = "ref" ; keyword: case insensitive
        ///
        /// 7.5.6 Method declaration
        ///
        ///     methodDeclaration = [ qualifierList ]
        ///                         ( ( returnDataType [ array ] ) / VOID ) methodName
        ///                         "(" [ parameterList ] ")" ";"
        ///
        ///     returnDataType    = primitiveType /
        ///                         structureOrClassName /
        ///                         enumName /
        ///                         classReference
        ///
        ///     methodName        = IDENTIFIER
        ///     classReference    = DT_REFERENCE
        ///     DT_REFERENCE      = className REF
        ///     VOID              = "void" ; keyword: case insensitive
        ///     parameterList     = parameterDeclaration *( "," parameterDeclaration )
        ///
        public static IClassFeatureAst ParseMemberDeclarationAst(
            TokenStream stream, QualifierListAst qualifierList,
            bool allowPropertyDeclaration, bool allowMethodDeclaration,
            ParserQuirks quirks
        )
        {

            var isMethodDeclaration = false;
            var isPropertyDeclaration = false;

            // [ qualifierList ]
            // note - this has already been read for us and gets passed in as a parameter

            // read the return type of the propertyDeclaration or methodDeclaration
            //
            //     primitivePropertyDeclaration => primitiveType
            //     complexPropertyDeclaration   => structureOrClassName
            //     enumPropertyDeclaration      => enumName
            //     referencePropertyDeclaration => classReference
            //
            //     methodDeclaration            => returnDataType => primitiveType /
            //                                                       structureOrClassName /
            //                                                       enumName /
            //                                                       classReference
            //
            var memberReturnType = stream.Read<IdentifierToken>();
            var memberReturnTypeIsArray = false;

            // if we're reading a:
            //     + referencePropertyDeclaration or a
            //     + methodDeclaration that returns a classReference type
            // then the next token is the REF keyword in the classReference
            stream.TryReadIdentifierToken(Constants.REF, out var memberReturnTypeRef);

            // if we're reading a methodDeclaration then the next token
            // in the methodDeclaration after returnDataType could be [ array ]
            if (stream.TryPeek<AttributeOpenToken>())
            {
                // check we're expecting a methodDeclaration
                if (isPropertyDeclaration || !allowMethodDeclaration)
                {
                    throw new UnsupportedTokenException(stream.Peek());
                }
                // [ array ]
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                memberReturnTypeIsArray = true;
                // we know this is a methodDeclaration now
                isMethodDeclaration = true;
            }

            // propertyName / methodName
            var memberName = stream.Read<IdentifierToken>();

            // if we're reading a propertyDeclaration then the next token
            // after the propertyName could be [ array ]
            if (stream.TryPeek<AttributeOpenToken>())
            {
                // check we're expecting a propertyDeclaration
                if (isMethodDeclaration || !allowPropertyDeclaration)
                {
                    throw new UnsupportedTokenException(stream.Peek());
                }
                // [ array ]
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                memberReturnTypeIsArray = true;
                // we know this is a propertyDeclaration now
                isPropertyDeclaration = true;
            }

            // if we're reading a methodDeclaration, then the next tokens *must*
            // be "(" [ parameterList ] ")"
            var methodParameterDeclarations = new List<ParameterDeclarationAst>();
            if (isMethodDeclaration || stream.TryPeek<ParenthesisOpenToken>())
            {
                // check we're expecting a methodDeclaration
                if (isPropertyDeclaration || !allowMethodDeclaration)
                {
                    throw new UnsupportedTokenException(stream.Peek());
                }
                // "("
                var methodParenthesisOpen = stream.Read<ParenthesisOpenToken>();
                //  [ parameterDeclaration *( "," parameterDeclaration ) ]
                if (!stream.TryPeek<ParenthesisCloseToken>())
                {
                    // parameterDeclaration
                    methodParameterDeclarations.Add(
                        ParserEngine.ParseParameterDeclarationAst(stream, quirks)
                    );
                    // *( "," parameterDeclaration )
                    while (stream.TryRead<CommaToken>(out var comma))
                    {
                        methodParameterDeclarations.Add(
                            ParserEngine.ParseParameterDeclarationAst(stream, quirks)
                        );
                    }
                }
                // ")"
                var methodParenthesisClose = stream.Read<ParenthesisCloseToken>();
                // we know this is a methodDeclaration now
                isMethodDeclaration = true;
            }
            else
            {
                // check we're expecting a propertyDeclaration
                if (isMethodDeclaration || !allowPropertyDeclaration)
                {
                    throw new UnsupportedTokenException(stream.Peek());
                }
                // we know this is a propertyDeclaration now
                isPropertyDeclaration = true;
            }

            // if we're reading a propertyDeclaration, then there *could* be
            // be a property initializer:
            //
            //     primitivePropertyDeclaration => [ "=" primitiveTypeValue ]
            //     complexPropertyDeclaration   => [ "=" ( complexTypeValue / aliasIdentifier ) ]
            //     enumPropertyDeclaration      => [ "=" enumValue ]
            //     referencePropertyDeclaration => [ "=" referenceTypeValue ]
            //
            var propertyInitializer = default(PropertyValueAst);
            if (isPropertyDeclaration)
            {
                if (stream.TryPeek<EqualsOperatorToken>())
                {
                    // check we're expecting a propertyDeclaration
                    if (isMethodDeclaration || !allowPropertyDeclaration)
                    {
                        throw new UnsupportedTokenException(stream.Peek());
                    }
                    // "="
                    var equalsOperator = stream.Read<EqualsOperatorToken>();
                    propertyInitializer = ParserEngine.ParsePropertyValueAst(stream, quirks);
                }
            }

            // ";"
            stream.Read<StatementEndToken>();

            if (isPropertyDeclaration)
            {
                // check we're expecting a propertyDeclaration
                if (isMethodDeclaration || !allowPropertyDeclaration)
                {
                    throw new InvalidOperationException();
                }
                var node = new PropertyDeclarationAst.Builder
                {
                    QualifierList = qualifierList,
                    ReturnType = memberReturnType,
                    ReturnTypeRef = memberReturnTypeRef,
                    PropertyName = memberName,
                    ReturnTypeIsArray = memberReturnTypeIsArray,
                    Initializer = propertyInitializer
                };
                return node.Build();
            }
            else if (isMethodDeclaration)
            {
                // check we're expecting a methodDeclaration
                if (isPropertyDeclaration || !allowMethodDeclaration)
                {
                    throw new InvalidOperationException();
                }
                var node = new MethodDeclarationAst.Builder
                {
                    QualifierList = qualifierList,
                    ReturnType = memberReturnType,
                    ReturnTypeRef = memberReturnTypeRef,
                    ReturnTypeIsArray = memberReturnTypeIsArray,
                    MethodName = memberName,
                    Parameters = methodParameterDeclarations
                };
                return node.Build();
            }
            else
            {
                // we couldn't work out whether this was a propertyDeclaration or a methodDeclaration
                throw new InvalidOperationException();
            }

        }

        #endregion

        #region 7.5.7 Parameter declaration

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.7 Parameter declaration
        ///
        ///     parameterDeclaration      = [ qualifierList ] ( primitiveParamDeclaration /
        ///                                 complexParamDeclaration /
        ///                                 enumParamDeclaration /
        ///                                 referenceParamDeclaration )
        ///
        ///     primitiveParamDeclaration = primitiveType parameterName [ array ]
        ///                                 [ "=" primitiveTypeValue ]
        ///
        ///     complexParamDeclaration   = structureOrClassName parameterName [ array ]
        ///                                 [ "=" ( complexTypeValue / aliasIdentifier ) ]
        ///
        ///     enumParamDeclaration      = enumName parameterName [ array ]
        ///                                 [ "=" enumValue ]
        ///
        ///     referenceParamDeclaration = classReference parameterName [ array ]
        ///                                 [ "=" referenceTypeValue ]
        ///
        ///     parameterName             = IDENTIFIER
        ///
        /// 7.5.6 Method declaration
        ///
        ///     classReference            = DT_REFERENCE
        ///
        /// 7.5.10 Reference type declaration
        ///
        ///     DT_REFERENCE              = className REF
        ///
        /// </remarks>
        public static ParameterDeclarationAst ParseParameterDeclarationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            // [ qualifierList ]
            var qualifierList = stream.TryPeek<AttributeOpenToken>()
                ? ParserEngine.ParseQualifierListAst(stream, quirks)
                : new QualifierListAst();

            // read the type of the parameter
            //
            //     primitiveParamDeclaration => primitiveType
            //     complexParamDeclaration   => structureOrClassName
            //     enumParamDeclaration      => enumName
            //     referenceParamDeclaration => classReference
            //
            var parameterTypeName = stream.Read<IdentifierToken>();

            // if we're reading a referenceParamDeclaration then the next token
            // is the 'ref' keyword
            stream.TryReadIdentifierToken(Constants.REF, out var parameterRef);

            // parameterName
            var parameterName = stream.Read<IdentifierToken>();

            // [ array ]
            var parameterIsArray = false;
            if (stream.TryPeek<AttributeOpenToken>())
            {
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                parameterIsArray = true;
            }

            // read the default value if there is one
            //
            //     primitiveParamDeclaration => [ "=" primitiveTypeValue ]
            //     complexParamDeclaration   => [ "=" ( complexTypeValue / aliasIdentifier ) ]
            //     enumParamDeclaration      => [ "=" enumValue ]
            //     referenceParamDeclaration => [ "=" referenceTypeValue ]
            //
            var parameterDefaultValue = default(PropertyValueAst);
            if (stream.TryRead<EqualsOperatorToken>(out var equals))
            {
                parameterDefaultValue = ParserEngine.ParsePropertyValueAst(stream, quirks);
            }

            return new ParameterDeclarationAst.Builder {
                QualifierList = qualifierList,
                ParameterType = parameterTypeName,
                ParameterRef = parameterRef,
                ParameterName = parameterName,
                ParameterIsArray = parameterIsArray,
                DefaultValue = parameterDefaultValue
            }.Build();

        }

        #endregion

        #region 7.5.9 Complex type value

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexTypeValue = complexValue / complexValueArray
        ///
        /// </remarks>
        public static ComplexTypeValueAst ParseComplexTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            if (!stream.TryPeek<BlockOpenToken>())
            {
                // complexValue
                return ParserEngine.ParseComplexValueAst(stream, quirks);
            }
            else
            {
                // complexValueArray
                return ParserEngine.ParseComplexValueArrayAst(stream, quirks);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
        ///
        /// </remarks>
        public static ComplexValueArrayAst ParseComplexValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            // complexValueArray =
            var node = new ComplexValueArrayAst.Builder();

            // "{"
            stream.Read<BlockOpenToken>();
            if (!stream.TryPeek<BlockCloseToken>())
            {

                // complexValue
                node.Values.Add(
                    ParserEngine.ParseComplexValueAst(stream, quirks)
                );

                // *( "," complexValue)
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    node.Values.Add(
                        ParserEngine.ParseComplexValueAst(stream, quirks)
                    );
                }

            }

            // "}"
            stream.Read<BlockCloseToken>();

            // return the result
            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexValue = aliasIdentifier /
        ///                    ( VALUE OF
        ///                      ( structureName / className / associationName )
        ///                      propertyValueList )
        ///
        /// </remarks>
        public static ComplexValueAst ParseComplexValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new ComplexValueAst.Builder();

            if (stream.TryPeek<AliasIdentifierToken>())
            {

                // aliasIdentifier
                node.Alias = stream.Read<AliasIdentifierToken>();

            }
            else
            {

                // VALUE
                node.Value = stream.ReadIdentifierToken(Constants.VALUE);

                // OF
                node.Of = stream.ReadIdentifierToken(Constants.OF);

                // ( structureName / className / associationName )
                node.TypeName = stream.ReadIdentifierToken(
                    t => StringValidator.IsStructureName(t.Name) &&
                         StringValidator.IsClassName(t.Name) &&
                         StringValidator.IsAssociationName(t.Name)
                );

                // propertyValueList
                node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream, quirks);

            }

            // return the result
            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     propertyValueList = "{" *propertySlot "}"
        ///
        ///     propertySlot      = propertyName "=" propertyValue ";"
        ///
        ///     propertyName      = IDENTIFIER
        ///
        /// </remarks>
        internal static PropertyValueListAst ParsePropertyValueListAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new PropertyValueListAst.Builder();

            // "{"
            stream.Read<BlockOpenToken>();

            // *propertySlot
            {

                while (!stream.TryPeek<BlockCloseToken>())
                {

                    // propertyName
                    var propertyName = stream.ReadIdentifierToken(
                        t => StringValidator.IsIdentifier(t.Name)
                    );

                    // "="
                    stream.Read<EqualsOperatorToken>();

                    // propertyValue
                    var propertyValue = ParserEngine.ParsePropertyValueAst(stream, quirks);

                    // ";"
                    stream.Read<StatementEndToken>();

                    node.PropertyValues.Add(propertyName.Name, propertyValue);

                }

            }

            // "}"
            stream.Read<BlockCloseToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.5.9 Complex type value
        ///
        ///     propertyValue = primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///     literalValueArray  = "{" [ literalValue *( "," literalValue ) ] "}"
        ///
        /// 7.5.9 Complex type value
        ///
        ///     complexTypeValue  = complexValue / complexValueArray
        ///     complexValueArray = "{" [ complexValue *( "," complexValue) ] "}"
        ///
        /// 7.6.4 Reference type value
        ///
        ///     referenceTypeValue   = objectPathValue / objectPathValueArray
        ///     objectPathValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
        ///                            "}"
        /// 7.6.3 Enum type value
        ///
        ///     enumTypeValue  = enumValue / enumValueArray
        ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
        ///     enumValue      = [ enumName "." ] enumLiteral
        ///     enumLiteral    = IDENTIFIER
        ///
        /// </remarks>
        internal static PropertyValueAst ParsePropertyValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            bool IsPrimitiveValueToken(SyntaxToken token)
            {
                return (token is IntegerLiteralToken) ||
                       (token is RealLiteralToken) ||
                       //(token is DateTimeLiteralToken) ||
                       (token is StringLiteralToken) ||
                       (token is BooleanLiteralToken) ||
                       //(token is OctetStringLiteralToken) ||
                       (token is NullLiteralToken);
            }

            bool IsComplexValueToken(SyntaxToken token)
            {
                return (token is AliasIdentifierToken) ||
                       ((token is IdentifierToken identifier) && (identifier.GetNormalizedName() == Constants.VALUE));
            }

            bool IsReferenceValueToken(SyntaxToken token)
            {
                // TODO: not implemented
                return false;
            }

            // if we've got an aray we need to read the next item before we can determine the type
            var itemValue = stream.Peek();
            if (itemValue is BlockOpenToken)
            {
                stream.Read();
                itemValue = stream.Peek();
                stream.Backtrack();
                if (itemValue is BlockCloseToken)
                {
                    // this is an empty array, so just pick a default type for now.
                    // (we probably need a "public sealed class UnknownTypeValue : PropertyValueAst"
                    // if we ever start doing type analysis on the ast).
                    return ParserEngine.ParsePrimitiveTypeValueAst(stream, quirks);
                }
            }

            // primitiveTypeValue / complexTypeValue / referenceTypeValue / enumTypeValue
            if (IsPrimitiveValueToken(itemValue))
            {
                // primitiveTypeValue = literalValue / literalValueArray
                return ParserEngine.ParsePrimitiveTypeValueAst(stream, quirks);
            }
            else if (IsComplexValueToken(itemValue))
            {
                // complexTypeValue = complexValue / complexValueArray
                return ParserEngine.ParseComplexTypeValueAst(stream, quirks);
            }
            else if (IsReferenceValueToken(itemValue))
            {
                // referenceTypeValue = objectPathValue / objectPathValueArray
                return ParserEngine.ParseReferenceTypeValueAst(stream, quirks);
            }
            else
            {
                // enumTypeValue = enumValue / enumValueArray
                return ParserEngine.ParseEnumTypeValueAst(stream, quirks);
            }

        }

        #endregion

        #region 7.6.1 Primitive type value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     primitiveTypeValue = literalValue / literalValueArray
        ///
        /// </remarks>
        public static PrimitiveTypeValueAst ParsePrimitiveTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            if (!stream.TryPeek<BlockOpenToken>())
            {
                // literalValue
                return ParserEngine.ParseLiteralValueAst(stream, quirks);
            }
            else
            {
                // literalValueArray
                return ParserEngine.ParseLiteralValueArrayAst(stream, quirks);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     literalValueArray = "{" [ literalValue *( "," literalValue ) ] "}"
        ///
        /// </remarks>
        public static LiteralValueArrayAst ParseLiteralValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            var node = new LiteralValueArrayAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ literalValue *( "," literalValue ) ]
            if (!stream.TryPeek<BlockCloseToken>())
            {
                // literalValue
                node.Values.Add(
                    ParserEngine.ParseLiteralValueAst(stream, quirks)
                );
                // *( "," literalValue )
                while (stream.TryRead<CommaToken>(out var comma))
                {
                    node.Values.Add(
                        ParserEngine.ParseLiteralValueAst(stream, quirks)
                    );
                }
            }
            // "}"
            stream.Read<BlockCloseToken>();
            // return the result
            return node.Build();
        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1 Primitive type value
        ///
        ///     literalValue = integerValue /
        ///                    realValue /
        ///                    booleanValue /
        ///                    nullValue /
        ///                    stringValue
        ///                      ; NOTE stringValue covers octetStringValue and
        ///                      ; dateTimeValue
        ///
        /// </remarks>
        public static LiteralValueAst ParseLiteralValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            var peek = stream.Peek();
            return peek switch
            {
                IntegerLiteralToken _ =>
                    // integerValue
                    ParserEngine.ParseIntegerValueAst(stream, quirks),
                RealLiteralToken _ =>
                    // realValue
                    ParserEngine.ParseRealValueAst(stream, quirks),
                BooleanLiteralToken _ =>
                    // booleanValue
                    ParserEngine.ParseBooleanValueAst(stream, quirks),
                NullLiteralToken _ =>
                    // nullValue
                    ParserEngine.ParseNullValueAst(stream, quirks),
                StringLiteralToken _ =>
                    // stringValue
                    ParserEngine.ParseStringValueAst(stream, quirks),
                _ =>
                    throw new UnexpectedTokenException(peek)
            };
        }

        #endregion

        #region 7.6.1.1 Integer value

        /// <summary>
        ///
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.1 Integer value
        ///
        ///     integerValue = binaryValue / octalValue / hexValue / decimalValue
        ///
        /// </remarks>
        public static IntegerValueAst ParseIntegerValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            var node = new IntegerValueAst.Builder();
            node.IntegerLiteralToken = stream.Read<IntegerLiteralToken>();
            return node.Build();
        }

        #endregion

        #region 7.6.1.2 Real value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///
        ///     realValue            = [ "+" / "-" ] * decimalDigit "." 1*decimalDigit
        ///                            [ ("e" / "E") [ "+" / "-" ] 1*decimalDigit ]
        ///
        ///     decimalDigit         = "0" / positiveDecimalDigit
        ///
        ///     positiveDecimalDigit = "1"..."9"
        ///
        /// </remarks>
        public static RealValueAst ParseRealValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            var node = new RealValueAst.Builder();
            node.RealLiteralToken = stream.Read<RealLiteralToken>();
            return node.Build();
        }

        #endregion

        #region 7.6.1.3 String values

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.3 String values
        ///
        ///     singleStringValue = DOUBLEQUOTE *stringChar DOUBLEQUOTE
        ///
        ///     stringValue       = singleStringValue *( *WS singleStringValue )
        ///
        /// </remarks>
        public static StringValueAst ParseStringValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new StringValueAst.Builder();

            // singleStringValue
            node.StringLiteralValues.Add(
                stream.Read<StringLiteralToken>()
            );

            // *( *WS singleStringValue )
            while (stream.TryRead<StringLiteralToken>(out var stringLiteral))
            {
                node.StringLiteralValues.Add(stringLiteral!);
            }

            node.Value = string.Join(
                string.Empty,
                node.StringLiteralValues.Select(s => s.Value).ToList()
            );

            return node.Build();

        }

        #endregion

        #region 7.6.1.5 Boolean value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.1.5 Boolean value
        ///
        ///     booleanValue = TRUE / FALSE
        ///
        ///     FALSE        = "false" ; keyword: case insensitive
        ///     TRUE         = "true"  ; keyword: case insensitive
        ///
        /// </remarks>
        public static BooleanValueAst ParseBooleanValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            return new BooleanValueAst(
                stream.Read<BooleanLiteralToken>()
            );
        }

        #endregion

        #region 7.6.1.6 Null value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// 7.6.1.6 Null value
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///     nullValue = NULL
        ///
        ///     NULL      = "null" ; keyword: case insensitive
        ///                        ; second
        ///
        /// </remarks>
        public static NullValueAst ParseNullValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            return new NullValueAst.Builder
            {
                Token = stream.Read<NullLiteralToken>()
            }.Build();
        }

        #endregion

        #region 7.6.2 Complex type value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// 7.6.2 Complex type value
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///     instanceValueDeclaration = INSTANCE OF ( className / associationName )
        ///                                [ alias ]
        ///                                propertyValueList ";"
        ///
        ///     alias                    = AS aliasIdentifier
        ///
        /// </remarks>
        public static InstanceValueDeclarationAst ParseInstanceValueDeclarationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new InstanceValueDeclarationAst.Builder();

            // INSTANCE
            node.Instance = stream.ReadIdentifierToken(Constants.INSTANCE);

            // OF
            node.Of = stream.ReadIdentifierToken(Constants.OF);

            // ( className / associationName )
            node.TypeName = stream.ReadIdentifierToken(
                 t => StringValidator.IsClassName(t.Name) ||
                      StringValidator.IsAssociationName(t.Name)
            );

            // [ alias ]
            {
                // AS
                if (stream.TryReadIdentifierToken(Constants.AS, out var @as))
                {
                    node.As = @as;
                    // aliasIdentifier
                    node.Alias = stream.Read<AliasIdentifierToken>();
                }
            }

            // propertyValueList
            node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream, quirks);

            // ";"
            node.StatementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// 7.6.2 Complex type value
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        ///     structureValueDeclaration = VALUE OF
        ///                                 ( className / associationName / structureName )
        ///                                 alias
        ///                                 propertyValueList ";"
        ///
        ///     alias                     = AS aliasIdentifier
        ///
        /// </remarks>
        public static StructureValueDeclarationAst ParseStructureValueDeclarationAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new StructureValueDeclarationAst.Builder();

            // VALUE
            node.Value = stream.ReadIdentifierToken(Constants.VALUE);

            // OF
            node.Of = stream.ReadIdentifierToken(Constants.OF);

            // ( className / associationName / structureName )
            node.TypeName = stream.ReadIdentifierToken(
                 t => StringValidator.IsClassName(t.Name) ||
                      StringValidator.IsAssociationName(t.Name) ||
                      StringValidator.IsStructureName(t.Name)
            );

            // [alias]
            {
                // AS
                if (stream.TryReadIdentifierToken(Constants.AS, out var @as))
                {
                    node.As = @as;
                    // aliasIdentifier
                    node.Alias = stream.Read<AliasIdentifierToken>();
                }
            }

            // propertyValueList
            node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream, quirks);

            // ";"
            node.StatementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        #endregion

        #region 7.6.3 Enum type value

        /// <summary>
        /// </summary>
        /// <param name="stream"></param>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.3 Enum type value
        ///
        ///     enumTypeValue = enumValue / enumValueArray
        ///
        /// </remarks>
        public static EnumTypeValueAst ParseEnumTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            if (!stream.TryPeek<BlockOpenToken>())
            {
                // enumValue
                return ParserEngine.ParseEnumValueAst(stream, quirks);
            }
            else
            {
                // enumValueArray
                return ParserEngine.ParseEnumValueArrayAst(stream, quirks);
            }
        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.3 Enum type value
        ///
        ///     enumValue   = [ enumName "." ] enumLiteral
        ///
        ///     enumLiteral = IDENTIFIER
        ///
        /// 7.5.4 Enumeration declaration
        ///
        ///     enumName    = elementName
        ///
        /// 7.7.1 Names
        ///
        ///     elementName         = localName / schemaQualifiedName
        ///     localName           = IDENTIFIER
        ///     IDENTIFIER          = firstIdentifierChar* (nextIdentifierChar )
        ///     firstIdentifierChar = UPPERALPHA / LOWERALPHA / UNDERSCORE
        ///     nextIdentifierChar  = firstIdentifierChar / decimalDigit
        ///
        /// 7.7.2 Schema-qualified name
        ///
        ///     schemaQualifiedName = schemaName UNDERSCORE IDENTIFIER
        ///     schemaName          = firstSchemaChar *( nextSchemaChar )
        ///     firstSchemaChar     = UPPERALPHA / LOWERALPHA
        ///     nextSchemaChar      = firstSchemaChar / decimalDigit
        ///
        /// </remarks>
        public static EnumValueAst ParseEnumValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new EnumValueAst.Builder();

            // read the first token and try to determine whether we have a
            //
            //     [ enumName "." ] enumLiteral
            //
            // or just a plain old
            //
            //     enumLiteral

            // [ enumName "." ] / enumLiteral
            var enumIdentifier = stream.Read<IdentifierToken>();

            // look at the next token to see if it's a "."
            if (stream.TryRead<DotOperatorToken>(out var _))
            {
                // [ enumName "." ]
                node.EnumName = enumIdentifier;
                // enumLiteral
                node.EnumLiteral = stream.Read<IdentifierToken>();
            }
            else
            {
                // enumLiteral
                node.EnumLiteral = enumIdentifier;
            }

            return node.Build();

        }

        /// <summary>
        /// </summary>
        /// <returns></returns>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.3 Enum type value
        ///
        ///     enumValueArray = "{" [ enumName *( "," enumName ) ] "}"
        ///
        /// </remarks>
        public static EnumValueArrayAst ParseEnumValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {

            var node = new EnumValueArrayAst.Builder();

            // "{"
            var blockOpen = stream.Read<BlockOpenToken>();

            // see  https://github.com/mikeclayton/MofParser/issues/25
            var quirkEnabled = (quirks & ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames) == ParserQuirks.EnumValueArrayContainsEnumValuesNotEnumNames;
            if (quirkEnabled)
            {

                // [ enumValue *( "," enumValue ) ]
                if (!stream.TryPeek<BlockCloseToken>())
                {
                    // enumValue
                    node.Values.Add(
                        ParserEngine.ParseEnumValueAst(stream, quirks)
                    );
                    // *( "," enumValue )
                    while (stream.TryRead<CommaToken>(out var comma))
                    {
                        node.Values.Add(
                            ParserEngine.ParseEnumValueAst(stream, quirks)
                        );
                    }
                }

            }
            else
            {

                // [ enumName *( "," enumName ) ]
                if (!stream.TryPeek<BlockCloseToken>())
                {
                    // enumName
                    node.Values.Add(
                        new EnumValueAst.Builder
                        {
                            EnumLiteral = stream.Read<IdentifierToken>()
                        }.Build()
                    );
                    // *( "," enumName )
                    while (stream.TryRead<CommaToken>(out var comma))
                    {
                        node.Values.Add(
                            new EnumValueAst.Builder
                            {
                                EnumLiteral = stream.Read<IdentifierToken>()
                            }.Build()
                       );
                    }
                }

            }

            // "}"
            var blockClose = stream.Read<BlockCloseToken>();

            // return the result
            return node.Build();

        }

        #endregion

        #region 7.6.4 Reference type value

        /// <summary>
        /// </summary>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.4 Reference type value
        ///
        ///     referenceTypeValue = objectPathValue / objectPathValueArray
        ///
        /// </remarks>
        public static PrimitiveTypeValueAst ParseReferenceTypeValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            if (stream.TryPeek<BlockOpenToken>())
            {
                // objectPathValueArray
                return ParserEngine.ParseObjectPathValueArrayAst(stream, quirks);
            }
            else
            {
                // objectPathValue
                return ParserEngine.ParseObjectPathValueAst(stream, quirks);
            }
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.4 Reference type value
        ///
        ///     ; Note: objectPathValues are URLs and shall conform to RFC 3986 (Uniform
        ///     ; Resource Identifiers(URI): Generic Syntax) and to the following ABNF.
        ///
        ///     objectPathValue  = [namespacePath ":"] instanceId
        ///
        ///     namespacePath    = [serverPath] namespaceName
        ///
        ///     ; Note: The production rules for host and port are defined in IETF
        ///     ; RFC 3986 (Uniform Resource Identifiers (URI): Generic Syntax).
        ///
        ///     serverPath       = (host / LOCALHOST) [ ":" port] "/"
        ///     LOCALHOST        = "localhost" ; Case insensitive
        ///     instanceId       = className "." instanceKeyValue
        ///     instanceKeyValue = keyValue *( "," keyValue )
        ///     keyValue         = propertyName "=" literalValue
        ///
        /// </remarks>
        public static PrimitiveTypeValueAst ParseObjectPathValueAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// </summary>
        /// <remarks>
        ///
        /// See https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf
        ///
        /// 7.6.4 Reference type value
        ///
        ///     objectPathValueArray = "{" [ objectPathValue *( "," objectPathValue ) ]
        ///
        /// </remarks>
        public static PrimitiveTypeValueAst ParseObjectPathValueArrayAst(TokenStream stream, ParserQuirks quirks = ParserQuirks.None)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}
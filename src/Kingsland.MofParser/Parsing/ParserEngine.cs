using Kingsland.MofParser.Ast;
using Kingsland.MofParser.Tokens;
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
        public static MofSpecificationAst ParseMofSpecificationAst(ParserStream stream)
        {
            var node = new MofSpecificationAst.Builder();
            while (!stream.Eof)
            {
                var production = ParserEngine.ParseMofProductionAst(stream);
                node.Productions.Add(production);
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
        public static MofProductionAst ParseMofProductionAst(ParserStream stream)
        {

            var peek = stream.Peek();

            // compilerDirective
            if (peek is PragmaToken)
            {
                return ParserEngine.ParseCompilerDirectiveAst(stream);
            }

            if (peek is IdentifierToken identifier)
            {
                switch (identifier.GetNormalizedName())
                {
                    case Constants.INSTANCE:
                        // instanceValueDeclaration
                        return ParserEngine.ParseInstanceValueDeclarationAst(stream);
                    case Constants.VALUE:
                        // structureValueDeclaration
                        return ParserEngine.ParseStructureValueDeclarationAst(stream);
                }
            }

            // all other mofProduction elements start with [ qualifieList ]
            var qualifierList = default(QualifierListAst);
            if (peek is AttributeOpenToken)
            {
                qualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

            identifier = stream.Peek<IdentifierToken>();
            switch (identifier.GetNormalizedName())
            {
                case Constants.STRUCTURE:
                    // structureDeclaration
                    return ParserEngine.ParseStructureDeclarationAst(stream, qualifierList);
                case Constants.CLASS:
                    // classDeclaration
                    return ParserEngine.ParseClassDeclarationAst(stream, qualifierList);
                case Constants.ASSOCIATION:
                    // associationDeclaration
                    return ParserEngine.ParseAssociationDeclarationAst(stream, qualifierList);
                case Constants.ENUMERATION:
                    // enumerationDeclaration
                    return ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList);
                case Constants.QUALIFIER:
                    // qualifierTypeDeclaration
                    //return ParserEngine.ParseQualifierTypeDeclarationAst(stream, qualifierList);
                    throw new NotImplementedException($"MofProduction type '{identifier.Name}' not implemented.");
                default:
                    throw new UnexpectedTokenException(identifier);
            }

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
        public static CompilerDirectiveAst ParseCompilerDirectiveAst(ParserStream stream)
        {

            var node = new CompilerDirectiveAst.Builder();

            // PRAGMA
            node.PragmaKeyword = stream.Read<PragmaToken>();

            // ( pragmaName / standardPragmaName )
            node.PragmaName = stream.Read<IdentifierToken>();

            // "("
            stream.Read<ParenthesisOpenToken>();

            // pragmaParameter
            node.PragmaParameter = ParserEngine.ParseStringValueAst(stream);

            // ")"
            stream.Read<ParenthesisCloseToken>();

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
        public static QualifierTypeDeclarationAst ParseQualifierDeclarationAst(ParserStream stream)
        {

            var node = new QualifierTypeDeclarationAst.Builder();

            // [ qualifierList ]
            var peek = stream.Peek<BlockOpenToken>();
            if (peek != null)
            {
                node.QualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

            // QUALIFIER
            node.QualifierKeyword = stream.ReadIdentifierToken(Constants.QUALIFIER);

            // qualifierName
            node.QualifierName = stream.Read<IdentifierToken>();

            // ":"
            stream.Read<ColonToken>();

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
        public static QualifierListAst ParseQualifierListAst(ParserStream stream)
        {

            var node = new QualifierListAst.Builder();
            var qualifierValue = default(QualifierValueAst);

            // "["
            stream.Read<AttributeOpenToken>();

            // qualifierValue
            qualifierValue = ParserEngine.ParseQualifierValueAst(stream);
            node.QualifierValues.Add(qualifierValue);

            // *( "," qualifierValue )
            while (stream.Peek<CommaToken>() != null)
            {
                var commaToken = stream.Read<CommaToken>();
                qualifierValue = ParserEngine.ParseQualifierValueAst(stream);
                node.QualifierValues.Add(qualifierValue);
            }

            // "]"
            stream.Read<AttributeCloseToken>();

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
        public static QualifierValueAst ParseQualifierValueAst(ParserStream stream)
        {

            var node = new QualifierValueAst.Builder();

            // qualifierName
            var qualifierName = stream.Read<IdentifierToken>();
            if (!StringValidator.IsElementName(qualifierName.Name))
            {
                throw new UnexpectedTokenException(qualifierName);
            }
            node.QualifierName = qualifierName;

            // [ qualifierValueInitializer / qualiferValueArrayInitializer ]
            var peek = stream.Peek();
            switch (peek)
            {
                case ParenthesisOpenToken paranthesesOpen:
                    // qualifierValueInitializer
                    node.ValueInitializer = ParserEngine.ParseQualifierValueInitializer(stream);
                    break;
                case BlockOpenToken blockOpen:
                    // qualiferValueArrayInitializer
                    node.ValueArrayInitializer = ParserEngine.ParseQualifierValueArrayInitializer(stream);
                    break;
            }

            //
            // 7.4 Qualifiers
            //
            // NOTE A MOF v2 qualifier declaration has to be converted to MOF v3 qualifierTypeDeclaration because the
            // MOF v2 qualifier flavor has been replaced by the MOF v3 qualifierPolicy.
            //
            // In MOF V2, the ColonToken separates qualifier "values" and "flavors", e.g:
            //
            //     [Locale(1033): ToInstance, UUID("{BE46D060-7A7C-11d2-BC85-00104B2CF71C}"): ToInstance]
            //     class Win32_PrivilegesStatus : __ExtendedStatus
            //     {
            //         [read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesNotHeld[];
            //         [read: ToSubClass, MappingStrings{\"Win32API|AccessControl|Windows NT Privileges\"}: ToSubClass] string PrivilegesRequired[];
            //     }
            //
            // but this is no longer valid in MOF V3.
            //
            // We'll read them anyway for compatibility.
            //
            // Pseudo-ABNF for MOF V2 qualifiers:
            //
            //     qualifierList_v2       = "[" qualifierValue_v2 *( "," qualifierValue_v2 ) "]"
            //     qualifierValue_v2      = qualifierName [ qualifierValueInitializer / qualiferValueArrayInitializer ] ":" qualifierFlavourList_v2
            //     qualifierFlavorList_v2 = qualifierFlavorName *( " " qualifierFlavorName )
            //

            var allowV2QUalifierFlavors = true;

            if (allowV2QUalifierFlavors)
            {
                var flavors = new List<IdentifierToken>();
                peek = stream.Peek<ColonToken>();
                if (peek != null)
                {
                    stream.Read<ColonToken>();
                    flavors.Add(stream.Read<IdentifierToken>());
                    while (stream.Peek<IdentifierToken>() != null)
                    {
                        flavors.Add(stream.Read<IdentifierToken>());
                    }
                    node.Flavors = flavors;
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
        public static LiteralValueAst ParseQualifierValueInitializer(ParserStream stream)
        {
            // "("
            stream.Read<ParenthesisOpenToken>();
            // literalValue
            var valueInitializer = ParserEngine.ParseLiteralValueAst(stream);
            // ")"
            stream.Read<ParenthesisCloseToken>();
            return valueInitializer;
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
        public static LiteralValueArrayAst ParseQualifierValueArrayInitializer(ParserStream stream)
        {

            var valueArrayInitializer = new LiteralValueArrayAst.Builder();
            var literalValueAst = default(LiteralValueAst);

            // "{"
            stream.Read<BlockOpenToken>();

            // note - the MOF spec doesn't allow for empty qualifier value arrays like
            // this property from the "MSMCAEvent_InvalidError" class in WinXpProSp3WMI.mof:
            //
            //	[WmiDataId(3), ValueMap{}] uint32 Type;
            //
            // it's presumably allowed in earlier versions of the MOF spec, or maybe the
            // System.Management.ManagementBaseObject.GetFormat method returns invalid MOF
            // text for some classes, but we'll provide an option to allow or disallow empty
            // arrays here...

            var allowEmptyQualifierArrayInitializer = true;

            var isEmptyQualifierArrayInitializer = stream.Peek() is BlockCloseToken;
            if (!isEmptyQualifierArrayInitializer || !allowEmptyQualifierArrayInitializer)
            {

                // literalValue
                literalValueAst = ParserEngine.ParseLiteralValueAst(stream);
                valueArrayInitializer.Values.Add(literalValueAst);

                // *( "," literalValue )
                while (!stream.Eof && (stream.Peek<CommaToken>() != null))
                {
                    var commaToken = stream.Read<CommaToken>();
                    literalValueAst = ParserEngine.ParseLiteralValueAst(stream);
                    valueArrayInitializer.Values.Add(literalValueAst);
                }

            }

            // "}"
            stream.Read<BlockCloseToken>();

            return valueArrayInitializer.Build();

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
        public static StructureDeclarationAst ParseStructureDeclarationAst(ParserStream stream, QualifierListAst qualifierList)
        {

            var node = new StructureDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // STRUCTURE
            var structureKeyword = stream.ReadIdentifierToken(Constants.STRUCTURE);

            // structureName
            var structureName = stream.Read<IdentifierToken>();
            if (!StringValidator.IsStructureName(structureName.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid class name.");
            }
            node.StructureName = structureName;

            // [ superStructure ]
            if (stream.Peek<ColonToken>() != null)
            {

                // ":"
                stream.Read<ColonToken>();

                // structureName
                var superStructureName = stream.Read<IdentifierToken>();
                if (!StringValidator.IsStructureName(superStructureName.Name))
                {
                    throw new InvalidOperationException("Identifer is not a valid superclass name.");
                }
                node.SuperStructure = superStructureName;

            }

            // "{"
            stream.Read<BlockOpenToken>();

            // *structureFeature
            while (!stream.Eof)
            {
                if (stream.Peek() is BlockCloseToken)
                {
                    break;
                }
                var structureFeature = ParserEngine.ParseStructureFeatureAst(stream);
                node.StructureFeatures.Add(structureFeature);
            }

            // "}"
            stream.Read<BlockCloseToken>();

            // ";"
            stream.Read<StatementEndToken>();

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
        public static IStructureFeatureAst ParseStructureFeatureAst(ParserStream stream)
        {

            // all structureFeatures start with an optional "[ qualifierList ]"
            var qualifierList = default(QualifierListAst);
            var peek = stream.Peek() as AttributeOpenToken;
            if ((peek as AttributeOpenToken) != null)
            {
                qualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

            // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
            // or propertyDeclaration

            // structureDeclaration   => STRUCTURE
            // enumerationDeclaration => ENUMERATION
            // propertyDeclaration    => primitiveType / structureOrClassName / enumName / classReference
            var identifier = stream.Peek<IdentifierToken>();
            if (identifier == null)
            {
                throw new UnexpectedTokenException(peek);
            }

            var identifierName = identifier.GetNormalizedName();
            if (identifierName == Constants.STRUCTURE)
            {
                // structureDeclaration
                return ParserEngine.ParseStructureDeclarationAst(stream, qualifierList);
            }
            else if (identifierName == Constants.ENUMERATION)
            {
                // enumerationDeclaration
                return ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList);
            }
            else
            {
                // propertyDeclaration
                var memberDeclaration = ParserEngine.ParseMemberDeclaration(stream, qualifierList, true, false);
                if (memberDeclaration is PropertyDeclarationAst propertyDeclaration)
                {
                    return propertyDeclaration;
                }
                else
                {
                    throw new InvalidOperationException();
                }
            }

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
        public static ClassDeclarationAst ParseClassDeclarationAst(ParserStream stream, QualifierListAst qualifierList)
        {

            var node = new ClassDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // CLASS
            var classKeyword = stream.ReadIdentifierToken(Constants.CLASS);

            // className
            var className = stream.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(className.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid class name.");
            }
            node.ClassName = className;

            // [ superClass ]
            if (stream.Peek<ColonToken>() != null)
            {

                // ":"
                stream.Read<ColonToken>();

                // className
                var superClassName = stream.Read<IdentifierToken>();
                if (!StringValidator.IsClassName(superClassName.Name))
                {
                    throw new InvalidOperationException("Identifer is not a valid superclass name.");
                }
                node.SuperClass = superClassName;

            }

            // "{"
            stream.Read<BlockOpenToken>();

            // *classFeature
            while (!stream.Eof)
            {
                if (stream.Peek() is BlockCloseToken)
                {
                    break;
                }
                var classFeature = ParserEngine.ParseClassFeatureAst(stream);
                node.Features.Add(classFeature);
            }

            // "}"
            stream.Read<BlockCloseToken>();

            // ";"
            stream.Read<StatementEndToken>();

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
        public static IClassFeatureAst ParseClassFeatureAst(ParserStream stream)
        {

            // all classFeatures start with an optional "[ qualifierList ]"
            var qualifierList = default(QualifierListAst);
            var peek = stream.Peek() as AttributeOpenToken;
            if ((peek as AttributeOpenToken) != null)
            {
                qualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

            // we now need to work out if it's a structureDeclaration, enumerationDeclaration,
            // propertyDeclaration or methodDeclaration

            var identifier = stream.Peek<IdentifierToken>();
            if (identifier == null)
            {
                throw new UnexpectedTokenException(peek);
            }

            var identifierName = identifier.GetNormalizedName();
            if (identifierName == Constants.STRUCTURE)
            {
                // structureDeclaration
                return ParserEngine.ParseStructureDeclarationAst(stream, qualifierList);
            }
            else if (identifierName == Constants.ENUMERATION)
            {
                // enumerationDeclaration
                return ParserEngine.ParseEnumerationDeclarationAst(stream, qualifierList);
            }
            else
            {
                // propertyDeclaration or methodDeclaration
                return ParserEngine.ParseMemberDeclaration(stream, qualifierList, true, true);
            }

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
        public static IClassFeatureAst ParseMemberDeclaration(
            ParserStream stream, QualifierListAst qualifierList,
            bool allowPropertyDeclaration, bool allowMethodDeclaration
        )
        {

            var peek = default(Token);

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

            // if we're reading a:
            //     + referencePropertyDeclaration or a
            //     + methodDeclaration that returns a classReference type
            // then the next token is the REF keyword in the classReference
            var memberReturnTypeRef = default(IdentifierToken);
            if (stream.PeekIdentifierToken(Constants.REF) != null)
            {
                memberReturnTypeRef = stream.ReadIdentifierToken(Constants.REF);
            }

            // if we're reading a methodDeclaration then the next token
            // in the methodDeclaration after returnDataType could be [ array ]
            var methodReturnTypeIsArray = false;
            var methodAttributeOpen = stream.Peek<AttributeOpenToken>();
            if (methodAttributeOpen != null)
            {
                // check we're expecting a methodDeclaration
                if (isPropertyDeclaration || !allowMethodDeclaration)
                {
                    throw new UnsupportedTokenException(peek);
                }
                // [ array ]
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                methodReturnTypeIsArray = true;
                // we know this is a methodDeclaration now
                isMethodDeclaration = true;
            }

            // propertyName / methodName
            var memberName = stream.Read<IdentifierToken>();

            // if we're reading a propertyDeclaration then the next token
            // after the propertyName could be [ array ]
            var propertyReturnTypeIsArray = false;
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                // check we're expecting a propertyDeclaration
                if (isMethodDeclaration || !allowPropertyDeclaration)
                {
                    throw new UnsupportedTokenException(peek);
                }
                // [ array ]
                stream.Read<AttributeOpenToken>();
                stream.Read<AttributeCloseToken>();
                propertyReturnTypeIsArray = true;
                // we know this is a propertyDeclaration now
                isPropertyDeclaration = true;
            }

            // if we're reading a methodDeclaration, then the next tokens *must*
            // be "(" [ parameterList ] ")"
            var methodParameterDeclarations = new List<ParameterDeclarationAst>();
            var methodParenthesisOpenToken = stream.Peek<ParenthesisOpenToken>();
            if (isMethodDeclaration  || (methodParenthesisOpenToken != null))
            {
                // check we're expecting a methodDeclaration
                if (isPropertyDeclaration || !allowMethodDeclaration)
                {
                    throw new UnsupportedTokenException(peek);
                }
                // "("
                stream.Read<ParenthesisOpenToken>();
                //  [ parameterDeclaration *( "," parameterDeclaration ) ]
                if (stream.Peek<ParenthesisCloseToken>() == null)
                {
                    var methodParameterDeclaration = default(ParameterDeclarationAst);
                    // parameterDeclaration
                    methodParameterDeclaration = ParserEngine.ParseParameterDeclarationAst(stream);
                    methodParameterDeclarations.Add(methodParameterDeclaration);
                    // *( "," parameterDeclaration )
                    while (stream.Peek<CommaToken>() != null)
                    {
                        var commaToken = stream.Read<CommaToken>();
                        methodParameterDeclaration = ParserEngine.ParseParameterDeclarationAst(stream);
                        methodParameterDeclarations.Add(methodParameterDeclaration);
                    }
                }
                // ")"
                stream.Read<ParenthesisCloseToken>();
                // we know this is a methodDeclaration now
                isMethodDeclaration = true;
            }
            else
            {
                // check we're expecting a propertyDeclaration
                if (isMethodDeclaration || !allowPropertyDeclaration)
                {
                    throw new UnsupportedTokenException(peek);
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
                if (stream.Peek<EqualsOperatorToken>() != null)
                {
                    // check we're expecting a propertyDeclaration
                    if (isMethodDeclaration || !allowPropertyDeclaration)
                    {
                        throw new UnsupportedTokenException(peek);
                    }
                    // "="
                    stream.Read<EqualsOperatorToken>();
                    propertyInitializer = ParserEngine.ParsePropertyValueAst(stream);
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
                    ReturnTypeIsArray = propertyReturnTypeIsArray,
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
                    ReturnTypeIsArray = methodReturnTypeIsArray,
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
        public static AssociationDeclarationAst ParseAssociationDeclarationAst(ParserStream stream, QualifierListAst qualifierList)
        {

            var node = new AssociationDeclarationAst.Builder();

            // [ qualifierList ]
            node.QualifierList = qualifierList;

            // ASSOCIATION
            stream.ReadIdentifierToken(Constants.ASSOCIATION);

            // associationName
            var associationName = stream.Read<IdentifierToken>();
            if (!StringValidator.IsAssociationName(associationName.Name))
            {
                throw new InvalidOperationException("Identifer is not a valid asociation name.");
            }
            node.AssociationName = associationName;

            // [ superAssociation ]
            if (stream.Peek<ColonToken>() != null)
            {
                // ":"
                stream.Read<ColonToken>();
                // associationName
                var superAssociationName = stream.Read<IdentifierToken>();
                if (!StringValidator.IsAssociationName(superAssociationName.Name))
                {
                    throw new InvalidOperationException("Identifer is not a valid superassociation name.");
                }
                node.SuperAssociation = superAssociationName;
            }

            // "{"
            stream.Read<BlockOpenToken>();

            // *classFeature
            while (!stream.Eof)
            {
                if (stream.Peek() is BlockCloseToken)
                {
                    break;
                }
                var classFeature = ParserEngine.ParseClassFeatureAst(stream);
                node.Features.Add(classFeature);
            }

            // "}"
            stream.Read<BlockCloseToken>();

            // ";"
            stream.Read<StatementEndToken>();

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
        public static EnumerationDeclarationAst ParseEnumerationDeclarationAst(ParserStream stream, QualifierListAst qualifierList)
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
            var enumName = stream.Peek<IdentifierToken>();
            if ((enumName == null) || !StringValidator.IsEnumName(enumName.Name))
            {
                throw new UnexpectedTokenException(enumName);
            }
            node.EnumName = stream.Read<IdentifierToken>();

            // ":"
            stream.Read<ColonToken>();

            // ( DT_INTEGER / integerEnumName ) / ( DT_STRING / stringEnumName )
            var enumTypeDeclaration = stream.Peek<IdentifierToken>();
            if ((enumTypeDeclaration == null) || !StringValidator.IsEnumName(enumTypeDeclaration.Name))
            {
                throw new UnexpectedTokenException(enumName);
            }
            switch (enumTypeDeclaration.GetNormalizedName())
            {
                case Constants.DT_INTEGER:
                    isIntegerEnum = true;
                    break;
                case Constants.DT_STRING:
                    isStringEnum = true;
                    break;
                default:
                    // this enumerationDeclaration is inheriting from a base enum.
                    // as a result, we don't know whether this is an integer or
                    // string enum until we inspect the type of the base enum
                    break;
            }
            node.EnumType = stream.Read<IdentifierToken>();

            // "{"
            stream.Read<BlockOpenToken>();

            // [ integerEnumElement *( "," integerEnumElement ) ]
            // [ stringEnumElement *( "," stringEnumElement ) ]
            if (stream.Peek<BlockCloseToken>() == null)
            {
                // integerEnumElement / stringEnumElement
                node.EnumElements.Add(ParserEngine.ParseEnumElementAst(stream, isIntegerEnum, isStringEnum));
                // *( "," integerEnumElement ) / *( "," stringEnumElement )
                while (stream.Peek<CommaToken>() != null)
                {
                    stream.Read<CommaToken>();
                    node.EnumElements.Add(ParserEngine.ParseEnumElementAst(stream, isIntegerEnum, isStringEnum));
                }
            }

            // "}"
            stream.Read<BlockCloseToken>();

            // ";"
            stream.Read<StatementEndToken>();

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
        public static EnumElementAst ParseEnumElementAst(ParserStream stream, bool isIntegerEnum, bool isStringEnum)
        {

            var node = new EnumElementAst.Builder();

            // [ qualifierList ]
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                node.QualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

            // enumLiteral
            node.EnumElementName = stream.Read<IdentifierToken>();

            // "=" integerValue / [ "=" stringValue ]
            var peek = stream.Peek();
            if (isIntegerEnum || (peek is EqualsOperatorToken))
            {
                var equalsOperator = stream.Read<EqualsOperatorToken>();
                var enumValue = stream.Peek();
                switch (enumValue)
                {
                    case IntegerLiteralToken integerValue:
                        // integerValue
                        node.EnumElementValue = ParserEngine.ParseIntegerValueAst(stream);
                        break;
                    case StringLiteralToken stringValue:
                        // stringValue
                        node.EnumElementValue = ParserEngine.ParseStringValueAst(stream);
                        break;
                    default:
                        throw new UnsupportedTokenException(enumValue);
                }
            }
            return node.Build();
        }

        #endregion

        #region 7.5.7 Parameter declaration

        /// <summary>
        ///
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
        public static ParameterDeclarationAst ParseParameterDeclarationAst(ParserStream stream)
        {

            // [ qualifierList ]
            var qualifierList = default(QualifierListAst);
            if (stream.Peek<AttributeOpenToken>() != null)
            {
                qualifierList = ParserEngine.ParseQualifierListAst(stream);
            }

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
            var parameterRef = default(IdentifierToken);
            if (stream.PeekIdentifierToken(Constants.REF) != null)
            {
                parameterRef = stream.ReadIdentifierToken(Constants.REF);
            }

            // parameterName
            var parameterName = stream.Read<IdentifierToken>();

            // [ array ]
            var parameterIsArray = false;
            if (stream.Peek<AttributeOpenToken>() != null)
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
            if (stream.Peek<EqualsOperatorToken>() != null)
            {
                stream.Read<EqualsOperatorToken>();
                parameterDefaultValue = ParserEngine.ParsePropertyValueAst(stream);
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
        public static ComplexTypeValueAst ParseComplexTypeValueAst(ParserStream stream)
        {
            if (stream.Peek() is BlockOpenToken)
            {
                // complexValueArray
                return ParserEngine.ParseComplexValueArrayAst(stream);
            }
            else
            {
                // complexValue
                return ParserEngine.ParseComplexValueAst(stream);
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
        public static ComplexValueArrayAst ParseComplexValueArrayAst(ParserStream stream)
        {
            // complexValueArray =
            var node = new ComplexValueArrayAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            if (stream.Peek<BlockCloseToken>() == null)
            {
                // complexValue
                node.Values.Add(ParserEngine.ParseComplexValueAst(stream));
                // *( "," complexValue)
                while (stream.Peek<CommaToken>() != null)
                {
                    stream.Read<CommaToken>();
                    node.Values.Add(ParserEngine.ParseComplexValueAst(stream));
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
        public static ComplexValueAst ParseComplexValueAst(ParserStream stream)
        {

            var node = new ComplexValueAst.Builder();

            if (stream.Peek<AliasIdentifierToken>() != null)
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
                if (stream.Peek<IdentifierToken>() != null)
                {
                    var typeName = stream.Read<IdentifierToken>();
                    if (!StringValidator.IsStructureName(typeName.Name) &&
                        !StringValidator.IsClassName(typeName.Name) &&
                        !StringValidator.IsAssociationName(typeName.Name))
                    {
                        throw new InvalidOperationException("Identifer is not a structureName, className or associationName");
                    }
                    node.TypeName = typeName;
                }

                // propertyValueList
                node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream);

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
        internal static PropertyValueListAst ParsePropertyValueListAst(ParserStream stream)
        {
            var node = new PropertyValueListAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            // *propertySlot
            while (!stream.Eof && (stream.Peek<BlockCloseToken>() == null))
            {
                // propertyName
                var propertyName = stream.Read<IdentifierToken>().Name;
                if (!StringValidator.IsIdentifier(propertyName))
                {
                    throw new InvalidOperationException("Value is not a valid property name.");
                }
                // "="
                stream.Read<EqualsOperatorToken>();
                // propertyValue
                var propertyValue = ParserEngine.ParsePropertyValueAst(stream);
                // ";"
                stream.Read<StatementEndToken>();
                node.PropertyValues.Add(propertyName, propertyValue);
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
        internal static PropertyValueAst ParsePropertyValueAst(ParserStream stream)
        {
            bool IsLiteralValueToken(Token token)
            {
                return (token is IntegerLiteralToken) ||
                       (token is RealLiteralToken) ||
                       //(token is DateTimeLiteralToken) ||
                       (token is StringLiteralToken) ||
                       (token is BooleanLiteralToken) ||
                       //(token is OctetStringLiteralToken) ||
                       (token is NullLiteralToken);
            }
            bool IsComplexValueToken(Token token)
            {
                return (token is AliasIdentifierToken) ||
                       ((token is IdentifierToken identifier) && (identifier.GetNormalizedName() == Constants.VALUE));
            }
            var node = default(PropertyValueAst);
            var propertyValue = stream.Peek();
            // we'll check whether we've got a single value or an array first,
            // and process the value(s) based on that
            if (!(propertyValue is BlockOpenToken))
            {
                // literalValue / complexValue / objectPathValue / enumValue
                if (IsLiteralValueToken(propertyValue))
                {
                    // literalValue
                    node = ParserEngine.ParseLiteralValueAst(stream);
                }
                else if (IsComplexValueToken(propertyValue))
                {
                    // complexValue
                    node = ParserEngine.ParseComplexTypeValueAst(stream);
                }
                else if (false)
                {
                    // objectPathValue
                    throw new UnexpectedTokenException(propertyValue);
                }
                else
                {
                    // enumValue
                    node = ParserEngine.ParseEnumValueAst(stream);
                }
            }
            else
            {
                // we need to read the subsequent token to work out whether this is a
                // literalValueArray / complexValueArray / objectPathValueArray / enumValueArray
                stream.Read();
                propertyValue = stream.Peek();
                stream.Backtrack();
                if (propertyValue is BlockCloseToken)
                {
                    // it's an empty array so we can't tell what type of items it represents,
                    // so we'll just use complexValueArray as an arbitrary type
                    node = ParserEngine.ParseLiteralValueArrayAst(stream);
                }
                else if (IsLiteralValueToken(propertyValue))
                {
                    // literalValueArray
                    node = ParserEngine.ParseLiteralValueArrayAst(stream);
                }
                else if (IsComplexValueToken(propertyValue))
                {
                    // complexValueArray
                    node = ParserEngine.ParseComplexValueArrayAst(stream);
                }
                else if (false)
                {
                    // objectPathValueArray
                    throw new UnexpectedTokenException(propertyValue);
                }
                else
                {
                    // enumValueArray
                    node = ParserEngine.ParseEnumValueArrayAst(stream);
                }
            }
            // return the result
            return node;
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
        public static PrimitiveTypeValueAst ParsePrimitiveTypeValueAst(ParserStream stream)
        {
            var peek = stream.Peek();
            if (peek is BlockOpenToken)
            {
                // literalValueArray
                return ParserEngine.ParseLiteralValueArrayAst(stream);
            }
            else
            {
                // literalValue
                return ParserEngine.ParseLiteralValueAst(stream);
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
        public static LiteralValueArrayAst ParseLiteralValueArrayAst(ParserStream stream)
        {
            var node = new LiteralValueArrayAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            // [ literalValue *( "," literalValue ) ]
            if (stream.Peek<BlockCloseToken>() == null)
            {
                // literalValue
                node.Values.Add(ParserEngine.ParseLiteralValueAst(stream));
                // *( "," literalValue )
                while (stream.Peek<CommaToken>() != null)
                {
                    stream.Read<CommaToken>();
                    node.Values.Add(ParserEngine.ParseLiteralValueAst(stream));
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
        public static LiteralValueAst ParseLiteralValueAst(ParserStream stream)
        {
            var peek = stream.Peek();
            if (peek is IntegerLiteralToken)
            {
                // integerValue
                return ParserEngine.ParseIntegerValueAst(stream);
            }
            else if (peek is RealLiteralToken)
            {
                // realValue
                return ParserEngine.ParseRealValueAst(stream);
            }
            else if (peek is BooleanLiteralToken)
            {
                // booleanValue
                return ParserEngine.ParseBooleanValueAst(stream);
            }
            else if (peek is NullLiteralToken)
            {
                // nullValue
                return ParserEngine.ParseNullValueAst(stream);
            }
            else if (peek is StringLiteralToken)
            {
                // stringValue
                return ParserEngine.ParseStringValueAst(stream);
            }
            else
            {
                throw new UnexpectedTokenException(peek);
            }
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
        public static IntegerValueAst ParseIntegerValueAst(ParserStream stream)
        {
            var integerLiteral = stream.Read<IntegerLiteralToken>();
            return new IntegerValueAst.Builder()
            {
               Kind = integerLiteral.Kind,
               Value = integerLiteral.Value
            }.Build();
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
        public static RealValueAst ParseRealValueAst(ParserStream stream)
        {
            return new RealValueAst.Builder
            {
                Value = stream.Read<RealLiteralToken>().Value
            }.Build();
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
        public static StringValueAst ParseStringValueAst(ParserStream stream)
        {

            var node = new StringValueAst.Builder();
            var singleStringValue = default(StringLiteralToken);

            // singleStringValue *( *WS singleStringValue )

            // singleStringValue
            singleStringValue = stream.Read<StringLiteralToken>();
            node.StringLiteralValues.Add(singleStringValue);

            // *( *WS singleStringValue )
            while (!stream.Eof && (stream.Peek<StringLiteralToken>() != null))
            {
                singleStringValue = stream.Read<StringLiteralToken>();
                node.StringLiteralValues.Add(singleStringValue);
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
        public static BooleanValueAst ParseBooleanValueAst(ParserStream stream)
        {
            var token = stream.Read<BooleanLiteralToken>();
            return new BooleanValueAst(token);
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
        public static NullValueAst ParseNullValueAst(ParserStream stream)
        {
            var token = stream.Read<NullLiteralToken>();
            return new NullValueAst.Builder
            {
                Token = token
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
        public static InstanceValueDeclarationAst ParseInstanceValueDeclarationAst(ParserStream stream)
        {

            var node = new InstanceValueDeclarationAst.Builder();

            // INSTANCE
            node.Instance = stream.ReadIdentifierToken(Constants.INSTANCE);

            // OF
            node.Of = stream.ReadIdentifierToken(Constants.OF);

            // ( className / associationName )
            var nameToken = stream.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(nameToken.Name) &&
                !StringValidator.IsAssociationName(nameToken.Name))
            {
                throw new InvalidOperationException("Identifer is not a className or associationName");
            }
            node.TypeName = nameToken;

            // [ alias ]
            if (stream.PeekIdentifierToken(Constants.AS) != null)
            {
                // AS
                node.As = stream.ReadIdentifierToken(Constants.AS);
                // aliasIdentifier
                var aliasIdentifierToken = stream.Read<AliasIdentifierToken>();
                node.Alias = aliasIdentifierToken;
            }

            // propertyValueList
            node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream);

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
        public static StructureValueDeclarationAst ParseStructureValueDeclarationAst(ParserStream stream)
        {

            var node = new StructureValueDeclarationAst.Builder();

            // VALUE
            node.Value = stream.ReadIdentifierToken(Constants.VALUE);

            // OF
            node.Of = stream.ReadIdentifierToken(Constants.OF);

            // ( className / associationName / structureName )
            var nameToken = stream.Read<IdentifierToken>();
            if (!StringValidator.IsClassName(nameToken.Name) &&
                !StringValidator.IsAssociationName(nameToken.Name) &&
                !StringValidator.IsStructureName(nameToken.Name))
            {
                throw new InvalidOperationException("Identifer is not a className, associationName or structureName");
            }
            node.TypeName = nameToken;

            // [alias]
            if (stream.PeekIdentifierToken(Constants.AS) != null)
            {
                // AS
                node.As = stream.ReadIdentifierToken(Constants.AS);
                // aliasIdentifier
                var aliasIdentifierToken = stream.Read<AliasIdentifierToken>();
                node.Alias = aliasIdentifierToken;
            }

            // propertyValueList
            node.PropertyValues = ParserEngine.ParsePropertyValueListAst(stream);

            // ";"
            node.StatementEnd = stream.Read<StatementEndToken>();

            return node.Build();

        }

        #endregion

        #region 7.6.3 Enum type value

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
        /// </remarks>
        public static EnumValueAst ParseEnumValueAst(ParserStream stream)
        {

            var node = new EnumValueAst.Builder();

            // read the first token and try to determine whether we have
            // a leading [ enumName "." ]
            var enumIdentifier = stream.Peek<IdentifierToken>();
            if (StringValidator.IsIdentifier(enumIdentifier.Name))
            {
                // this might, or might not have a leading [ enumName "." ] as the first token
                // *could* be an enumName or an enumLiteral, so read past it and look for the "."
                stream.Read<IdentifierToken>();
                var peek = stream.Peek();
                if (peek is DotOperatorToken)
                {
                    // this has a leading [ enumName "." ]
                    if (!StringValidator.IsEnumName(enumIdentifier.Name))
                    {
                        throw new UnexpectedTokenException(peek);
                    }
                    node.EnumName = enumIdentifier;
                    stream.Read<DotOperatorToken>();
                    node.EnumLiteral = stream.Read<IdentifierToken>();
                }
                else
                {
                    // no leading [ enumName "." ]
                    node.EnumLiteral = enumIdentifier;
                }
            }
            else if (StringValidator.IsEnumName(enumIdentifier.Name))
            {
                // this has a leading [ enumName "." ]
                node.EnumName = enumIdentifier;
                stream.Read<DotOperatorToken>();
                node.EnumLiteral = stream.Read<IdentifierToken>();
            }
            else
            {
                // no leading [ enumName "." ]
                node.EnumLiteral = stream.Read<IdentifierToken>();
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
        public static EnumValueArrayAst ParseEnumValueArrayAst(ParserStream stream)
        {
            var node = new EnumValueArrayAst.Builder();
            // "{"
            stream.Read<BlockOpenToken>();
            // note - we're using enumValue, not enumName
            // [ enumValue *( "," enumValue ) ]
            if (stream.Peek<BlockCloseToken>() == null)
            {
                // enumValue
                node.Values.Add(ParserEngine.ParseEnumValueAst(stream));
                // *( "," enumValue )
                while (stream.Peek<CommaToken>() != null)
                {
                    stream.Read<CommaToken>();
                    node.Values.Add(ParserEngine.ParseEnumValueAst(stream));
                }
            }
            // "}"
            stream.Read<BlockCloseToken>();
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
        public static PrimitiveTypeValueAst ParseReferenceTypeValueAst(ParserStream stream)
        {
            var peek = stream.Peek();
            if (peek is BlockOpenToken)
            {
                // objectPathValueArray
                return ParserEngine.ParseObjectPathValueArrayAst(stream);
            }
            else
            {
                // objectPathValue
                return ParserEngine.ParseObjectPathValueAst(stream);
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
        public static PrimitiveTypeValueAst ParseObjectPathValueAst(ParserStream stream)
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
        public static PrimitiveTypeValueAst ParseObjectPathValueArrayAst(ParserStream stream)
        {
            throw new NotImplementedException();
        }

        #endregion

    }

}

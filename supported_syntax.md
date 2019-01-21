Kingsland.MofParser Feature Support
===================================

The following table shows which features of the [MOF 3.0.1 specification](https://www.dmtf.org/sites/default/files/standards/documents/DSP0221_3.0.1.pdf) are currently supported.

If you find a syntax rule which isn't supported, or doesn't process properly, please create an issue with an example of the MOF text that fails and I'll look at implementing it.

```text
5 MOF file content

    5.1 Encoding

    5.2 Whitespace .................................................. Yes

    5.3 Line termination ............................................ Yes

    5.4 Comments
        single-line comments ........................................ Yes
        multiline comments........................................... Yes

6 MOF and OCL

7 MOF language elements

    7.1 MOF grammar description

    7.2 MOF specification
        mofSpecification ............................................ Yes
        mofProduction ............................................... Yes

    7.3 Compiler directives
        compilerDirective ........................................... Yes

    7.4 Qualifiers
        qualifierTypeDeclration ..................................... No

        7.4.1 QualifierList
            qualifierList ........................................... Yes
            qualifierValue .......................................... Yes
            qualifierValueInitializer ............................... Yes
            qualifierValueArrayInitializer .......................... Yes

    7.5 Types

        7.5.1 Structure declaration
            structureDeclaration .................................... Yes
            structureFeature ........................................ Yes

        7.5.2 Class declaration
            classDeclaration ........................................ Yes
            classFeature ............................................ Yes

        7.5.3 Association declaration
            associationDeclaration .................................. Yes

        7.5.4 Enumeration declaration
            enumerationDeclaration .................................. Yes
            enumTypeDeclaration ..................................... Yes
            integerEnumDeclaration .................................. Yes
            stringEnumDeclaration ................................... Yes

        7.5.5 Property declaration
            propertyDeclaration ..................................... Yes
            primitivePropertyDeclaration ............................ Yes
            complexPropertyDeclaration .............................. Yes
            enumPropertyDeclaration ................................. Yes
            referencePropertyDeclaration ............................ Yes

        7.5.6 Method declaration
            methodDeclaration ....................................... Yes

        7.5.7 Parameter declaration
            parameterDeclaration .................................... Yes
            primitiveParamDeclaration ............................... Yes
            complexParamDeclaration ................................. Yes
            enumParamDeclaration .................................... Yes
            referenceParamDeclaration ............................... Yes

        7.5.8 Primitive type declarations
            primitiveType ........................................... Yes

        7.5.9 Complex type value
            complexTypeValue ........................................ Yes
            complexValueArray ....................................... Yes
            complexValue ............................................ Yes
            propertyValue ........................................... Yes

        7.5.10 Reference type declaration
            DT_REFERENCE ............................................ Yes

    7.6 Value definitions

        7.6.1 Primitive type value
            primitiveTypeValue ...................................... Yes
            literalValueArray ....................................... Yes
            literalValue ............................................ Yes

            7.6.1.1 Integer value
                integerValue ........................................ Yes
                binaryValue ......................................... Yes
                octalValue .......................................... Yes
                hexValue ............................................ Yes
                decimalValue ........................................ Yes

            7.6.1.2 Real value
                realValue ........................................... Y (no support for exponent)

            7.6.1.3 String values
                singleStringValue ................................... Yes
                stringValue ......................................... Yes
                escapedUCSchar ...................................... No

            7.6.1.4 OctetString value
                octetStringValue .................................... NA (parsed as a stringValue)

            7.6.1.5 Boolean value
                booleanValue ........................................ Yes

            7.6.1.6 Null value
                nullValue ........................................... Yes

            7.6.1.7 File path
                filePath ............................................ No

        7.6.2 Complex type value
            instanceValueDeclaration ................................ Yes
            structureValueDeclaration ............................... Yes

        7.6.3 Enum type value
            enumTypeValue ........................................... Yes
            enumValueArray .......................................... Yes
            enumValue ............................................... Yes

        7.6.4 Reference type value
            referenceTypeValue ...................................... No
            objectPathValueArray .................................... No
            objectPathValue ......................................... No

    7.7 Names and identifiers

        7.7.1 Names
            IDENTIFIER .............................................. Yes
            elementName ............................................. Yes
            localName ............................................... Yes

        7.7.2 Schema-qualified name
            schemaQualifiedName ..................................... Yes

        7.7.3 Alias identifier
            aliasIdentifier ......................................... Yes

        7.7.4 Namespace name
            namespaceName ........................................... No
```
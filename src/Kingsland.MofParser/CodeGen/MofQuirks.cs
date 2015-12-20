namespace Kingsland.MofParser.CodeGen
{

    public enum MofQuirks
    {

        None = 0,

        /// <summary>
        /// Omits a space between "in" and "out" qualifiers on parameter declarations. This reproduces the
        /// behaviour of the System.Management.ManagementBaseObject.GetText method.
        /// </summary>
        /// <remarks>
        /// Some tests will fail as follows without this quirk enabled:
        ///
        /// 1) Test Failure : Kingsland.MofParser.UnitTests.LexerTests+LexMethodSampleFiles.root.wmi.BFn.mof
        ///      Expected string length 345 but was 346. Strings differ at index 300.
        ///   Expected: "...void DoBFn([in,out, Description("Fn buf")] BDat Data);\r\n};\r\n"
        ///   But was:  "...void DoBFn([in, out, Description("Fn buf")] BDat Data);\r\n};\r\n"
        ///   -----------------------------^
        /// </remarks>
        OmitSpaceBetweenInOutQualifiersForParameterDeclarations = 1,

        /// <summary>
        /// Inserts an extra space before method declarations that have no qualifiers. This reproduces the
        /// behaviour of the System.Management.ManagementBaseObject.GetText method, where the space acts as
        /// a separator between the qualifier text and the method declaration's return type, but it still
        /// includes the space character if there are no qualifiers on the method declaration.
        /// </summary>
        /// <remarks>
        /// Some tests will fail as follows without this quirk enabled:
        ///
        /// 1) Test Failure : Kingsland.MofParser.UnitTests.LexerTests+LexMethodSampleFiles.root.cimv2.CIM_Action.mof
        ///      Expected string length 1194 but was 1193. Strings differ at index 1171.
        ///   Expected: "...ad: ToSubClass] string Description;\r\n\t uint32 Invoke();\r\n};\r\n"
        ///   But was:  "...ad: ToSubClass] string Description;\r\n\tuint32 Invoke();\r\n};\r\n"
        ///   -------------------------------------------------------^
        /// </remarks>
        PrefixSpaceBeforeQualifierlessMethodDeclarations = 2

    }

}

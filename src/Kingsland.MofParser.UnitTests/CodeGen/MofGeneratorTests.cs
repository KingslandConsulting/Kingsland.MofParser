using System;
using Kingsland.MofParser.Ast;
using Kingsland.MofParser.CodeGen;
using NUnit.Framework;

namespace Kingsland.MofParser.UnitTests.CodeGen
{
    public class MofGeneratorTests
    {
        [Test]
        public void ConvertingAPropertyNodeShouldNotCauseStackOverflow()
        {
            PropertyValueAst node = (PropertyValueAst) Activator.CreateInstance(typeof(ReferenceValueArrayAst), true);

            MofGenerator.ConvertToMof(node);
        }
    }
}
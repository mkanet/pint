using Pint;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PintTests
{
    public class FunctionInfoTests
    {
        [Fact]
        public void FunctionInfo_NoMandatoryParams_Empty()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param() }");             
            FunctionInfo info = new FunctionInfo(def);

            Assert.Empty(info.MandatoryParameters);
        }       
    }
}

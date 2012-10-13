using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Pint;
using System.Management.Automation.Language;

namespace PintTests
{
    public class FunctionDefinitionVisitorTests
    {        
        [Fact]
        public void GetFunctionDefinitions_ZeroFunctions_Empty()
        {
            ScriptBlockAst ast = Utilities.GetAst("1+1");

            FunctionDefinitionVisitor visitor = new FunctionDefinitionVisitor();
            ast.Visit(visitor);

            Assert.Empty(visitor.FunctionTable);
        }

        [Fact]
        public void GetFunctionDefinitions_OneFunction_FindsIt()
        {
            ScriptBlockAst ast = Utilities.GetAst("1+1; function foo() {}");

            FunctionDefinitionVisitor visitor = new FunctionDefinitionVisitor();
            ast.Visit(visitor);

            List<FunctionInfo> fooFuncs = visitor.FunctionTable["foo"];
            Assert.Equal(1, fooFuncs.Count);
        }

        [Fact]
        public void GetFunctionDefinitions_TwoMatching_FindsBoth()
        {
            ScriptBlockAst ast = Utilities.GetAst(
                @"1+1; 
                function foo() {}; 
                function foo() { param([Parameter(Mandatory=$true)]$x) }");

            FunctionDefinitionVisitor visitor = new FunctionDefinitionVisitor();
            ast.Visit(visitor);

            List<FunctionInfo> fooFuncs = visitor.FunctionTable["foo"];
            Assert.Equal(2, fooFuncs.Count);
        }

        [Fact]
        public void GetFunctionDefinitions_TwoDifferent_FindsBoth()
        {
            ScriptBlockAst ast = Utilities.GetAst(
                @"1+1; 
                function foo() {}; 
                function bar() { param([Parameter(Mandatory=$true)]$x) }");

            FunctionDefinitionVisitor visitor = new FunctionDefinitionVisitor();
            ast.Visit(visitor);

            List<FunctionInfo> fooFuncs = visitor.FunctionTable["foo"];
            Assert.Equal(1, fooFuncs.Count);
            List<FunctionInfo> barFunds = visitor.FunctionTable["bar"];
        }
    }
}

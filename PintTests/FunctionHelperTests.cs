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
    public class FunctionHelperTests
    {

        public FunctionHelperTests()
        {
            
        }

        private static ScriptBlockAst GetAst(string func)
        {
            Token[] tokens;
            ParseError[] errors;
            ScriptBlockAst ast = Parser.ParseInput(func, out tokens, out errors);
            Assert.Empty(errors);
            return ast;
        }
        
        [Fact]
        public void IsMandatory_OptionalParam_ReturnsFalse()
        {
            ScriptBlockAst  func = GetAst("function foo() { param($x) }");
            FunctionDefinitionAst def = (FunctionDefinitionAst)(func.EndBlock.Statements[0]);
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.False(param.IsMandatory());
        }

        [Fact]
        public void IsMandatory_OptionalParamWithType_ReturnsFalse()
        {
            ScriptBlockAst func = GetAst("function foo() { param([string]$x) }");
            FunctionDefinitionAst def = (FunctionDefinitionAst)(func.EndBlock.Statements[0]);
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.False(param.IsMandatory());
        }

        [Fact]
        public void IsMandatory_MandatoryParam_ReturnsTrue()
        {
            ScriptBlockAst func = GetAst("function foo() { param([Parameter(Mandatory=$true)]$x) }");
            FunctionDefinitionAst def = (FunctionDefinitionAst)(func.EndBlock.Statements[0]);
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.True(param.IsMandatory());     
        }

        [Fact]
        public void IsMandatory_MandatoryParam_ReturnsFalse()
        {
            ScriptBlockAst func = GetAst("function foo() { param([Parameter(Mandatory=$false)]$x) }");
            FunctionDefinitionAst def = (FunctionDefinitionAst)(func.EndBlock.Statements[0]);
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.False(param.IsMandatory());
        }
    }
}

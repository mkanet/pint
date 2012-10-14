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
        [Fact]
        public void IsMandatory_OptionalParam_ReturnsFalse()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param($x) }");
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.False(param.IsMandatory());
        }

        [Fact]
        public void IsMandatory_OptionalParamWithType_ReturnsFalse()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param([string]$x) }");            
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.False(param.IsMandatory());
        }

        [Fact]
        public void IsMandatory_MandatoryParam_ReturnsTrue()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param([Parameter(Mandatory=$true)]$x) }");
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.True(param.IsMandatory());     
        }

        [Fact]
        public void IsMandatory_MandatoryParam_ReturnsFalse()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param([Parameter(Mandatory=$false)]$x) }");
            ParameterAst param = (ParameterAst)(def.Body.ParamBlock.Parameters[0]);

            Assert.False(param.IsMandatory());
        }

        [Fact]
        public void GetMandatoryParams_NoParamBlock_ReturnsEmpty()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() {  }");
            Assert.Empty(def.MandatoryParameters());
        }

        [Fact]
        public void GetMandatoryParams_NoParams_ReturnsEmpty()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param() }");
            Assert.Empty(def.MandatoryParameters());
        }

        [Fact]
        public void GetMandatoryParams_OneOptionalParam_ReturnsEmpty()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param([Parameter(Mandatory=$false)]$x) }");
            Assert.Empty(def.MandatoryParameters());
        }

        [Fact]
        public void GetMandatoryParams_OneMandatoryParam_ReturnsName()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst("function foo() { param([Parameter(Mandatory=$true)]$x) }");
            var foundParams = def.MandatoryParameters().ToList();
            Assert.Equal(new[] { "x" }.ToList(), foundParams);
        }

        [Fact]
        public void GetMandatoryParams_TypeConstraintAttribute_Ignored()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst(
                "function foo() { param([ValidateNotNull][Parameter(Mandatory=$true)]$x) }");
            var foundParams = def.MandatoryParameters().ToList();
            Assert.Equal(new[] { "x" }.ToList(), foundParams);
        }        

        [Fact]
        public void GetMandatoryParams_TwoMandatoryOneOptionalParam_ReturnsNames()
        {
            FunctionDefinitionAst def = Utilities.GetSingleFunctionAst(
                @"function foo() { 
                    param(
                        [Parameter(Mandatory=$true)]$x,
                        [Parameter]$y,
                        [Parameter(Mandatory=$true)]$z
                    )
                }");
            
            var foundParams = def.MandatoryParameters().ToList();
            Assert.Equal(new[] { "x", "z" }.ToList(), foundParams);
        }        
    }
}

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
    public class MandatoryParametersPassedAnalyzerTests
    {

        [Fact]
        public void Analyze_Empty_EmptyResults()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast empty = Utilities.GetAst("");
            AnalysisResults results = a.Analyze(empty);

            Assert.Empty(results.Warnings);
        }

        [Fact]
        public void Analyze_MissingParam_ProducesWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(
                @"function foo() { param([Parameter(Mandatory=$true)]$x) }
                foo
                ");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(1, results.Warnings.Count);            
        }

        [Fact]
        public void Analyze_PassedParam_NoWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(
                @"function foo() { param([Parameter(Mandatory=$true)]$x) }
                foo -x
                ");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(0, results.Warnings.Count);            
        }

        [Fact]
        public void Analyze_PassedMoreParams_NoWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(
                @"function foo() { param([Parameter(Mandatory=$true)]$x) }
                foo -x -y -z
                ");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(0, results.Warnings.Count);
        }

        [Fact]
        public void Analyze_PassedParamDifferentCase_NoWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(
                @"function foo() { param([Parameter(Mandatory=$true)]$x) }
                foo -X
                ");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(0, results.Warnings.Count);
        }

        // If there are multiple funcs with same name, we don't try and work out which is called, we just give up
        [Fact]
        public void Analyze_AmbiguousFunction_NoWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(
                @"function foo() { param([Parameter(Mandatory=$true)]$x) }
                function foo() { param([Parameter(Mandatory=$true)]$y) }
                foo
                ");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(0, results.Warnings.Count);
        }

        [Fact]
        public void Analyze_CalledUnknownFunction_NoWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(@"foo");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(0, results.Warnings.Count);
        }

        // we don't try and work out what params are satisfied via splatting
        [Fact]
        public void Analyze_CalledWithSplat_NoWarning()
        {
            MandatoryParametersPassedAnalyzer a = new MandatoryParametersPassedAnalyzer();

            Ast ast = Utilities.GetAst(
               @"function foo() { param([Parameter(Mandatory=$true)]$x) }
                $p = @{}                 
                foo @p
                ");

            AnalysisResults results = a.Analyze(ast);

            Assert.Equal(0, results.Warnings.Count);
        }
    }
}

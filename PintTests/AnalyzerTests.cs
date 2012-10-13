using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Pint;

namespace PintTests
{
    public class AnalyzerTests
    {
        Analyzer a;

        public AnalyzerTests() {
            a = new Analyzer();
        }

        [Fact]
        public void NewAnalyzer_Success()
        {
            Assert.NotNull(a);
        }

        [Fact]
        public void Analyzer_LoadString_Success()
        {
            a.Load("");
        }

        [Fact]
        public void Analyzer_InvalidString_Throws()
        {
            AnalyzerParseException ex = Assert.Throws<AnalyzerParseException>( () => a.Load("<<>>"));
            Assert.NotNull(ex.Errors);
        }

        [Fact]
        public void Analyzer_FunctionCallMissingMandatory_Detected()
        {
            a.Load(@"
                function foo() { param([Parameter(Mandatory=$true)][string]$A) $A }
                foo
            ");

            //Assert.Equal(1, a.Warnings.ToList().Count);
        }
    }
}

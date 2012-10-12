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
            Assert.Throws<AnalyzerParseException>( () => a.Load("<<>>"));
        }
    }
}

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
        public void Analyzer_InvalidString_DetectsError()
        {
            a.Load("<<>>");
            Assert.NotEmpty(a.Errors);
        }

        [Fact]
        public void Analyzer_FormatError_FormatsCorrectly()
        {
            a.Load("+");
            string message = a.FormatError(a.Errors.First());
            Assert.Equal("(1,2,1,2): error MissingExpressionAfterOperator: Missing expression after unary operator '+'.", message);
        }

        [Fact]
        public void Analyzer_FormatError_IncludesFilename()
        {
            using (TempFile t = new TempFile("+"))
            {
                a.LoadFile(t.FileName);
                string message = a.FormatError(a.Errors.First());
                Assert.True(message.StartsWith(t.FileName));
            }
        }
    }
}

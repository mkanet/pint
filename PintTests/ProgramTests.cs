using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Pint;
using System.Management.Automation;
using System.Management.Automation.Language;

namespace PintTests
{
    public class ProgramTests
    {
        MockProgram p = new MockProgram();

        [Fact]
        public void Run_NoFiles_ReturnsBadArgument()
        {
            int result = p.Run(new string[] { });
            Assert.Equal(Program.BadArguments, result);
        }

        [Fact]
        public void Check_EmptyContent_Success()
        {
            p.Check("");
        }

        [Fact]
        public void Check_BadContent_RecordsErrors()
        {
            p.Check("+");
            Assert.NotEmpty(p.Analyzer.Errors);
        }

        [Fact]
        public void CheckFile_NoFile_RecordsError()
        {
            p.CheckFile("nonexistent.txt");
            ParseError err = p.Analyzer.Errors.First();
            Assert.Equal("FatalParserError", err.ErrorId);
            Assert.Equal("nonexistent.txt", err.Extent.File);
        }

        [Fact]
        public void CheckFile_GoodFile_Success()
        {
            using (TempFile t = new TempFile("1+1"))
            {
                p.CheckFile(t.FileName);
            }
        }

        [Fact]
        public void LogErrors_WritesFormattedErrors()
        {
            using (TempFile t = new TempFile("\n$$$"))
            {
                p.Run(new string[] { t.FileName} );                
                Assert.Equal(p.Analyzer.Errors.Select(e => p.Analyzer.FormatError(e)).ToList(), p.LoggedErrors);
            }
        }
    }
}

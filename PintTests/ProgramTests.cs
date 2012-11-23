using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Pint;

namespace PintTests
{
    public class ProgramTests
    {
        Program p = new MockProgram();

        [Fact]
        public void Run_NoFiles_ReturnsBadArgument()
        {
            int result = p.Run(new string[] { });
            Assert.Equal(Program.BadArguments, result);
        }

        [Fact]
        public void Check_EmptyFile_Success()
        {
            p.Check("");
        }
    }
}

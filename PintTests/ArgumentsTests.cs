using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Pint;

namespace PintTests
{
    public class ArgumentsTests
    {
        [Fact]
        public void ProcessArguments_NoFiles_ThrowsArgumentException()
        {
            Assert.Throws<ArgumentException>(() => new Arguments(new string[] { }));
        }

        [Fact]
        public void ProcessArguments_OneFile_RecordsFile()
        {
            string[] files = new string[] { "hello.ps1" };
            var a = new Arguments(files);
            Assert.Equal(files, a.FilesToProcess.ToArray());
        }

        [Fact]
        public void ProcessArguments_TwoFiles_RecordsFile()
        {
            string[] files = new string[] { "hello.ps1", "hello2.ps1" };
            var a = new Arguments(files);
            Assert.Equal(files, a.FilesToProcess.ToArray());
        }

        [Fact]
        public void ProcessArguments_NullEmpty_Skipped()
        {
            string[] files = new string[] { "hello.ps1", "", null };
            var a = new Arguments(files);
            Assert.Equal(new string[] { "hello.ps1" }, a.FilesToProcess.ToArray());
        }

        [Fact]
        public void ProcessArguments_Flags_Skipped()
        {
            string[] files = new string[] { "hello.ps1", "/foo", "hello2.ps1", "-bar" };
            var a = new Arguments(files);
            Assert.Equal(new string[] { "hello.ps1", "hello2.ps1" }, a.FilesToProcess.ToArray());
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Automation.Language;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace PintTests
{
    public static class Utilities
    {
        public static ScriptBlockAst GetAst(string func)
        {
            Token[] tokens;
            ParseError[] errors;
            ScriptBlockAst ast = Parser.ParseInput(func, out tokens, out errors);
            Assert.Empty(errors);
            return ast;
        }
    }
}
